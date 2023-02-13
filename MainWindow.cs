using CustomControls.RJControls;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NLog.Targets;
using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.Misc.Properties;
using PKHeX.Drawing.PokeSprite;
using RaidCrawler.Structures;
using RaidCrawler.Subforms;
using SysBot.Base;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using static RaidCrawler.Structures.Offsets;
using static SysBot.Base.SwitchButton;
using static System.Buffers.Binary.BinaryPrimitives;

namespace RaidCrawler
{
    public partial class MainWindow : Form
    {
        private readonly Config Config = new();

        private readonly static SwitchConnectionConfig ConnectionConfig = new() { Protocol = SwitchProtocol.WiFi, IP = "192.168.0.0", Port = 6000 };
        private readonly static SwitchSocketAsync SwitchConnection = new(ConnectionConfig);
        //private readonly static SwitchUSBAsync SwitchConnection = new(ConnectionConfig.Port);
        private readonly static OffsetUtil OffsetUtil = new(SwitchConnection);

        private readonly List<Raid> Raids = new();
        private readonly List<ITeraRaid?> Encounters = new();
        private List<uint> prev = new();
        private List<RaidFilter> RaidFilters = new();
        private static readonly Image map = Image.FromStream(new MemoryStream(Utils.GetBinaryResource("paldea.png")));
        private static Dictionary<string, float[]>? den_locations;

        // rewards
        private readonly List<List<(int, int, int)>?> RewardsList = new();

        private int index = 0;
        private ulong offset;
        private bool IsReading = false;
        private bool HideSeed = false;
        private bool ShowExtraMoves = false;

        private Color DefaultColor;
        private FormWindowState _WindowState;
        private readonly Stopwatch stopwatch = new();
        private readonly TeraRaidView teraRaidView;

        private bool stopRequested = false;
        private bool screenState = false;

        private CancellationTokenSource AdvanceDateCancellationTokenSource;
        public MainWindow()
        {
            string build = string.Empty;
#if DEBUG
            var date = File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly()!.Location);
            build = $" (dev-{date:yyyyMMdd})";
#endif
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version!;

            var raid_data = new[]
            {
                "raid_enemy_01_array.bin",
                "raid_enemy_02_array.bin",
                "raid_enemy_03_array.bin",
                "raid_enemy_04_array.bin",
                "raid_enemy_05_array.bin",
                "raid_enemy_06_array.bin",
            };

            Raid.GemTeraRaids = TeraEncounter.GetAllEncounters(raid_data);
            var filterpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "filters.json");
            if (File.Exists(filterpath))
                RaidFilters = JsonConvert.DeserializeObject<List<RaidFilter>>(File.ReadAllText(filterpath)) ?? new List<RaidFilter>();
            den_locations = JsonConvert.DeserializeObject<Dictionary<string, float[]>>(Utils.GetStringResource("den_locations.json") ?? "{}");

            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (File.Exists(configpath))
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configpath)) ?? new Config();
            }
            else
            {
                Config = new Config();
                SaveConfig();
            }
            Text = "RaidCrawlerV v" + v.Major + "." + v.Minor + "." + v.Build + build + " " + Config.InstanceName;


            // load rewards
            Raid.BaseFixedRewards = JsonConvert.DeserializeObject<List<RaidFixedRewards>>(Utils.GetStringResource("raid_fixed_reward_item_array.json") ?? "[]");
            Raid.BaseLotteryRewards = JsonConvert.DeserializeObject<List<RaidLotteryRewards>>(Utils.GetStringResource("raid_lottery_reward_item_array.json") ?? "[]");

            Raid.Game = Config.Game;
            SpriteBuilder.ShowTeraThicknessStripe = 0x4;
            SpriteBuilder.ShowTeraOpacityStripe = 0xAF;
            SpriteBuilder.ShowTeraOpacityBackground = 0xFF;
            SpriteUtil.ChangeMode(SpriteBuilderMode.SpritesArtwork5668);

            teraRaidView = new TeraRaidView(Config);

            InitializeComponent();
        }

        private void SaveConfig()
        {
            var configpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string output = JsonConvert.SerializeObject(Config);
            using StreamWriter sw = new(configpath);
            sw.Write(output);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Location = Config.Location;
            if (Location.X == 0 && Location.Y == 0)
                CenterToScreen();
            InputSwitchIP.Text = Config.SwitchIP;
            //InputSwitchIP.Text = Config.SwitchIP.Port
            DefaultColor = IVs.BackColor;
            Progress.SelectedIndex = Config.Progress;
            EventProgress.SelectedIndex = Config.EventProgress;
            Game.SelectedIndex = Game.FindString(Config.Game);
            RaidBoost.SelectedIndex = 0;

            if (Config.StreamerView)
            {
                teraRaidView.Map.Image = map;
                teraRaidView.Show();
            }

            if (Config.BackupAfterSave) Cheat.ForeColor = Color.BlueViolet;
            if (Config.ContinueAfterBackup) Cheat.ForeColor = Color.OrangeRed;
            if (!Config.BackupAfterSave & !Config.ContinueAfterBackup) Cheat.ForeColor = Config.windowColor;

            SetTheme(Config.Theme);
            LoadTheme();
        }

        private void InputSwitchIP_Changed(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text != "192.168.0.0")
            {
                Config.SwitchIP = textBox.Text;
                ConnectionConfig.IP = textBox.Text;
            }
            SaveConfig();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (AdvanceDateCancellationTokenSource != null)
                AdvanceDateCancellationTokenSource.Cancel(true);
            Disconnect();
            ConnectionStatusText.Text = "Disconnected";
            stopwatch.Stop();
            ButtonConnect.Enabled = true;
            ButtonDisconnect.Enabled = false;
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            ButtonStopCrawl.Enabled = false;
            ButtonViewRAM.Enabled = false;
            ButtonDownloadEvents.Enabled = false;
            ButtonSceenState.Enabled = false;
            SendScreenshot.Enabled = false;
        }

        private async void Connect()
        {
            if (!SwitchConnection.Connected)
            {
                try
                {
                    ConnectionStatusText.Text = "Connecting";
                    SwitchConnection.Connect();
                    ConnectionStatusText.Text = "Connected!";
                    IsReading = true;
                    ConnectionStatusText.Text = "Detecting game version";
                    string id = await GetGameID(CancellationToken.None);
                    if (id is ScarletID)
                    {
                        Game.SelectedIndex = Game.FindString("Scarlet");
                    }
                    else if (id is VioletID)
                    {
                        Game.SelectedIndex = Game.FindString("Violet");
                    }
                    else
                    {
                        MessageBox.Show("Unable to detect game!");
                        Disconnect();
                        IsReading = false;
                    }

                    ConnectionStatusText.Text = "Reading story progress";
                    Progress.SelectedIndex = await GetStoryProgress(CancellationToken.None);
                    EventProgress.SelectedIndex = Math.Min(Progress.SelectedIndex, 3);

                    ConnectionStatusText.Text = "Reading event";
                    await ReadEventRaids();

                    ConnectionStatusText.Text = "Reading raids";
                    await ReadRaids(CancellationToken.None);

                    IsReading = false;
                    stopRequested = false;
                    ButtonAdvanceDate.Enabled = true;
                    ButtonReadRaids.Enabled = true;
                    ButtonConnect.Enabled = false;
                    ButtonDisconnect.Enabled = true;
                    ButtonViewRAM.Enabled = true;
                    ButtonDownloadEvents.Enabled = true;
                    ButtonSceenState.Enabled = true;
                    SendScreenshot.Enabled = true;
                }
                catch (SocketException err)
                {
                    Disconnect();
                    // a bit hacky but it works
                    if (err.Message.Contains("failed to respond") || err.Message.Contains("actively refused"))
                    {
                        ConnectionStatusText.Text = "Unable to connect";
                        MessageBox.Show(err.Message);
                    }
                }
            }
        }

        private static async void Disconnect()
        {
            if (SwitchConnection.Connected)
            {
                await SwitchConnection.SendAsync(SwitchCommand.DetachController(true), CancellationToken.None).ConfigureAwait(false);
                SwitchConnection.Disconnect();
            }
        }

        private static async Task<int> GetStoryProgress(CancellationToken token)
        {
            int progress = 0;
            for (int i = DifficultyFlags.Length - 1; i >= 0 && progress == 0; i--)
            {
                // See https://github.com/Lincoln-LM/sv-live-map/pull/43
                var block = await ReadSaveBlock(DifficultyFlags[i].Item1, 1, DifficultyFlags[i].Item2, token);
                if (block[0] == 2) return i + 1;
            }
            return progress;
        }

        public static async Task<byte[]> ReadSaveBlock(int offset, int size, uint key, CancellationToken token)
        {
            (offset, key) = await SearchSaveBlock(offset, key, token);
            var block_ofs = await OffsetUtil.GetPointerAddress($"[{SaveBlockPointer}+{offset + 8:X}]", token);
            var block = await SwitchConnection.ReadBytesAbsoluteAsync(block_ofs, size, token);
            return DecryptBlock(key, block);
        }

        public static async Task<byte[]> ReadSaveBlockObject(int offset, uint key, CancellationToken token)
        {
            (offset, key) = await SearchSaveBlock(offset, key, token);
            var header_ofs = await OffsetUtil.GetPointerAddress($"[{SaveBlockPointer}+{offset + 8:X}]", token);
            var header = await SwitchConnection.ReadBytesAbsoluteAsync(header_ofs, 5, token);
            header = DecryptBlock(key, header);
            var size = ReadUInt32LittleEndian(header.AsSpan()[1..]);
            var obj = await SwitchConnection.ReadBytesAbsoluteAsync(header_ofs, 5 + (int)size, token);
            return DecryptBlock(key, obj)[5..];
        }

        public static async Task<byte[]> ReadBlockDefault((int, uint) offsets, string? cache = null, bool force = false)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "cache");
            Directory.CreateDirectory(folder);
            if (force == false && cache != null && File.Exists(Path.Combine(folder, cache)))
                return File.ReadAllBytes(Path.Combine(folder, cache));
            var bin = await ReadSaveBlockObject(offsets.Item1, offsets.Item2, CancellationToken.None);
            if (cache != null)
                File.WriteAllBytes(Path.Combine(folder, cache), bin);
            return bin;
        }

        public static async Task ReadEventRaids(bool force = false)
        {
            var prio_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array");
            if (File.Exists(prio_file))
            {
                (_, var version) = FlatbufferDumper.DumpDeliveryPriorities(File.ReadAllBytes(prio_file));
                var blk = await ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array.tmp", true);
                (_, var v2) = FlatbufferDumper.DumpDeliveryPriorities(blk);
                if (version != v2)
                    force = true;
                var tmp_file = Path.Combine(Directory.GetCurrentDirectory(), "cache", "raid_priority_array.tmp");
                if (File.Exists(tmp_file))
                    File.Delete(tmp_file);
                if (v2 == 0) // raid reset
                    return;
            }
            var delivery_raid_prio = await ReadBlockDefault(BCATRaidPriorityLocation, "raid_priority_array", force);
            (Raid.DeliveryRaidPriority, var priority) = FlatbufferDumper.DumpDeliveryPriorities(delivery_raid_prio);
            if (priority == 0)
                return;

            var delivery_raid_fbs = await ReadBlockDefault(BCATRaidBinaryLocation, "raid_enemy_array", force);
            var delivery_fixed_rewards = await ReadBlockDefault(BCATRaidFixedRewardLocation, "fixed_reward_item_array", force);
            var delivery_lottery_rewards = await ReadBlockDefault(BCATRaidLotteryRewardLocation, "lottery_reward_item_array", force);

            Raid.DistTeraRaids = TeraDistribution.GetAllEncounters(delivery_raid_fbs);
            Raid.DeliveryRaidFixedRewards = FlatbufferDumper.DumpFixedRewards(delivery_fixed_rewards);
            Raid.DeliveryRaidLotteryRewards = FlatbufferDumper.DumpLotteryRewards(delivery_lottery_rewards);
        }

        public static async Task<(int, uint)> SearchSaveBlock(int base_offset, uint? key, CancellationToken token)
        {
            var key_addr = await OffsetUtil.GetPointerAddress($"{SaveBlockPointer}+{base_offset:X}", token);
            var read_key = ReadUInt32LittleEndian(await SwitchConnection.ReadBytesAbsoluteAsync(key_addr, 4, token));
            if (key == null)
                return (base_offset, read_key);
            if (read_key == key)
                return (base_offset, read_key);
            var direction = key > read_key ? 1 : -1;
            for (int offset = base_offset; offset < base_offset + 0x1000 && offset > base_offset - 0x1000; offset += direction * 0x20)
            {
                key_addr = await OffsetUtil.GetPointerAddress($"{SaveBlockPointer}+{offset:X}", token);
                read_key = ReadUInt32LittleEndian(await SwitchConnection.ReadBytesAbsoluteAsync(key_addr, 4, token));
                if (read_key == key)
                    return (offset, read_key);
            }
            throw new ArgumentOutOfRangeException("Save block not found in range +- 0x1000");
        }

        private static byte[] DecryptBlock(uint key, byte[] block)
        {
            var rng = new SCXorShift32(key);
            for (int i = 0; i < block.Length; i++)
                block[i] = (byte)(block[i] ^ rng.Next());
            return block;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void DisplayRaid(int index)
        {
            if (Raids.Count > index)
            {
                Raid raid = Raids[index];
                var encounter = Encounters[index];

                Seed.Text = !HideSeed ? $"{raid.Seed:X8}" : "Hidden";
                EC.Text = !HideSeed ? $"{raid.EC:X8}" : "Hidden";
                PID.Text = GetPIDString(raid, encounter);
                Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";
                LabelIsEvent.Visible = raid.IsEvent;

                var teratype = Raid.GetTeraType(encounter, raid);
                TeraType.Text = Raid.strings.types[teratype];
                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);
                Difficulty.Text = string.Concat(Enumerable.Repeat("★", StarCount));

                if (encounter != null)
                {
                    var map = GenerateMap(raid, teratype);
                    myPanel1.BackgroundImage = (Config.MapBackground ? map : null);

                    var param = Raid.GetParam(encounter);
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };
                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite(SpriteBuilderTweak.None);
                    img = ApplyTeraColor((byte)teratype, img, SpriteBackgroundType.BottomStripe);
                    Species.Text = $"{Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"-{encounter.Form}" : "")}";
                    Sprite.Image = img;
                    GemIcon.Image = PKHeX.Drawing.Misc.TypeSpriteUtil.GetTypeSpriteGem((byte)teratype);
                    Gender.Text = $"{(Gender)blank.Gender}";
                    var nature = blank.Nature;
                    Nature.Text = $"{Raid.strings.Natures[nature]}";
                    Ability.Text = $"{Raid.strings.Ability[blank.Ability]}";
                    SizeBox.Text = $"{PokeSizeDetailedUtil.GetSizeRating(blank.Scale)} ({blank.Scale})";
                    var extra_moves = new ushort[] { 0, 0, 0, 0 };
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        if (i < extra_moves.Length) extra_moves[i] = encounter.ExtraMoves[i];
                    Move1.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[0]] : Raid.strings.Move[encounter.Move1];
                    Move2.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[1]] : Raid.strings.Move[encounter.Move2];
                    Move3.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[2]] : Raid.strings.Move[encounter.Move3];
                    Move4.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[3]] : Raid.strings.Move[encounter.Move4];
                    IVs.Text = IVsString(ToSpeedLast(blank.IVs));
                    toolTip.SetToolTip(IVs, IVsString(ToSpeedLast(blank.IVs), true));

                    shinyBox.Image = Raid.CheckIsShiny(raid, encounter) ? (ShinyExtensions.IsSquareShinyExist(blank) ? Properties.Resources.square : Properties.Resources.shiny) : null;
                    shinyBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    Species.Text = string.Empty;
                    Move1.Text = string.Empty;
                    Move2.Text = string.Empty;
                    Move3.Text = string.Empty;
                    Move4.Text = string.Empty;
                    IVs.Text = string.Empty;
                    Gender.Text = string.Empty;
                    Nature.Text = string.Empty;
                    Ability.Text = string.Empty;
                    SizeBox.Text = string.Empty;
                }

                Color panel1Box = Color.FromArgb(125, Config.windowColor.R, Config.windowColor.G, Config.windowColor.B);
                PID.BackColor = Raid.CheckIsShiny(raid, encounter) ? Color.FromArgb(125, 255, 215, 0) : panel1Box;
                IVs.BackColor = IVs.Text is "31/31/31/31/31/31" ? Color.FromArgb(125, 154, 205, 50) : panel1Box;
                EC.BackColor = (raid.EC % 100 == 0 && (encounter!.Species == 945 || encounter.Species == 206) ? Color.FromArgb(125, 0, 215, 255) : panel1Box);
            }
            else
            {
                MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        private void DisplayPrettyRaid(int index)
        {
            if (Raids.Count > index)
            {
                Raid raid = Raids[index];
                var encounter = Encounters[index];

                teraRaidView.Area.Text = $"{Areas.Area[raid.Area - 1]} - Den {raid.Den}";

                var teratype = Raid.GetTeraType(encounter, raid);
                teraRaidView.TeraType.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("gem_text_" + teratype)!;

                int StarCount = encounter is TeraDistribution ? encounter.Stars : Raid.GetStarCount(raid.Difficulty, Progress.SelectedIndex, raid.IsBlack);
                teraRaidView.Difficulty.Text = string.Concat(Enumerable.Repeat("⭐", StarCount));

                if (encounter != null)
                {
                    var param = Raid.GetParam(encounter);
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };
                    Encounter9RNG.GenerateData(blank, param, EncounterCriteria.Unrestricted, raid.Seed);
                    var img = blank.Sprite(SpriteBuilderTweak.None);

                    teraRaidView.picBoxPokemon.Image = img;

                    teraRaidView.Species.Text = Raid.strings.Species[encounter.Species];
                    //teraRaidView.Form.Text = encounter.Form != 0 ? $"{encounter.Form}" : " ";

                    teraRaidView.Gender.Text = $"{(Gender)blank.Gender}";

                    var nature = blank.Nature;
                    teraRaidView.Nature.Text = $"{Raid.strings.Natures[nature]}";
                    teraRaidView.Ability.Text = $"{Raid.strings.Ability[blank.Ability]}";

                    teraRaidView.Moveset1.Text = Raid.strings.Move[encounter.Move1];
                    teraRaidView.Moveset2.Text = Raid.strings.Move[encounter.Move2];
                    teraRaidView.Moveset3.Text = Raid.strings.Move[encounter.Move3];
                    teraRaidView.Moveset4.Text = Raid.strings.Move[encounter.Move4];

                    var extra_moves = new ushort[] { 0, 0, 0, 0 };
                    for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                        if (i < extra_moves.Length) extra_moves[i] = encounter.ExtraMoves[i];
                    teraRaidView.Moveset5.Text = (encounter.ExtraMoves.Length > 0) ? Raid.strings.Move[extra_moves[0]] : "---";
                    teraRaidView.Moveset6.Text = (encounter.ExtraMoves.Length > 1) ? Raid.strings.Move[extra_moves[1]] : "---";
                    teraRaidView.Moveset7.Text = (encounter.ExtraMoves.Length > 2) ? Raid.strings.Move[extra_moves[2]] : "---";
                    teraRaidView.Moveset8.Text = (encounter.ExtraMoves.Length > 3) ? Raid.strings.Move[extra_moves[3]] : "---";


                    var ivs = ToSpeedLast(blank.IVs);

                    // HP
                    teraRaidView.HP.Text = $"{ivs[0]:D2}";
                    teraRaidView.HP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.HP.Text is "31")
                    {
                        teraRaidView.HP.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.HP.Text is "00")
                    {
                        teraRaidView.HP.BackColor = Color.DarkRed;
                    }

                    // ATK
                    teraRaidView.ATK.Text = $"{ivs[1]:D2}";
                    teraRaidView.ATK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.ATK.Text is "31")
                    {
                        teraRaidView.ATK.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.ATK.Text is "00")
                    {
                        teraRaidView.ATK.BackColor = Color.DarkRed;
                    }

                    // DEF
                    teraRaidView.DEF.Text = $"{ivs[2]:D2}";
                    teraRaidView.DEF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.DEF.Text is "31")
                    {
                        teraRaidView.DEF.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.DEF.Text is "00")
                    {
                        teraRaidView.DEF.BackColor = Color.DarkRed;
                    }

                    // SPA
                    teraRaidView.SPA.Text = $"{ivs[3]:D2}";
                    teraRaidView.SPA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPA.Text is "31")
                    {
                        teraRaidView.SPA.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPA.Text is "00")
                    {
                        teraRaidView.SPA.BackColor = Color.DarkRed;
                    }

                    // SPD
                    teraRaidView.SPD.Text = $"{ivs[4]:D2}";
                    teraRaidView.SPD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPD.Text is "31")
                    {
                        teraRaidView.SPD.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPD.Text is "00")
                    {
                        teraRaidView.SPD.BackColor = Color.DarkRed;
                    }

                    // SPEED
                    teraRaidView.SPEED.Text = $"{ivs[5]:D2}";
                    teraRaidView.SPEED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(25)))));
                    if (teraRaidView.SPEED.Text is "31")
                    {
                        teraRaidView.SPEED.BackColor = Color.ForestGreen;
                    }
                    else if (teraRaidView.SPEED.Text is "00")
                    {
                        teraRaidView.SPEED.BackColor = Color.DarkRed;
                    }


                    var map = GenerateMap(raid, teratype);
                    if (map == null)
                    {
                        MessageBox.Show("Error generating map.");
                    }
                    teraRaidView.Map.Image = map;

                    // Rewards
                    var rewards = RewardsList[index];

                    teraRaidView.textAbilityPatch.Text = "0";
                    teraRaidView.textAbilityPatch.ForeColor = Color.DimGray;
                    teraRaidView.labelAbilityPatch.ForeColor = Color.DimGray;

                    teraRaidView.textAbilityCapsule.Text = "0";
                    teraRaidView.textAbilityCapsule.ForeColor = Color.DimGray;
                    teraRaidView.labelAbilityCapsule.ForeColor = Color.DimGray;

                    teraRaidView.textBottleCap.Text = "0";
                    teraRaidView.textBottleCap.ForeColor = Color.DimGray;
                    teraRaidView.labelBottleCap.ForeColor = Color.DimGray;

                    teraRaidView.textSweetHerba.Text = "0";
                    teraRaidView.textSweetHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSweetHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSaltyHerba.Text = "0";
                    teraRaidView.textSaltyHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSaltyHerba.ForeColor = Color.DimGray;

                    teraRaidView.textBitterHerba.Text = "0";
                    teraRaidView.textBitterHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelBitterHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSourHerba.Text = "0";
                    teraRaidView.textSourHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSourHerba.ForeColor = Color.DimGray;

                    teraRaidView.textSpicyHerba.Text = "0";
                    teraRaidView.textSpicyHerba.ForeColor = Color.DimGray;
                    teraRaidView.labelSpicyHerba.ForeColor = Color.DimGray;

                    for (int i = 0; i < rewards!.Count; i++)
                    {
                        if (rewards[i].Item1 == 645)
                        {
                            teraRaidView.textAbilityCapsule.Text = (int.Parse(teraRaidView.textAbilityCapsule.Text) + 1).ToString();
                            teraRaidView.textAbilityCapsule.ForeColor = Color.White;
                            teraRaidView.labelAbilityCapsule.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 795)
                        {
                            teraRaidView.textBottleCap.Text = (int.Parse(teraRaidView.textBottleCap.Text) + 1).ToString();
                            teraRaidView.textBottleCap.ForeColor = Color.White;
                            teraRaidView.labelBottleCap.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1606)
                        {
                            teraRaidView.textAbilityPatch.Text = (int.Parse(teraRaidView.textAbilityPatch.Text) + 1).ToString();
                            teraRaidView.textAbilityPatch.ForeColor = Color.White;
                            teraRaidView.labelAbilityPatch.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1904)
                        {
                            teraRaidView.textSweetHerba.Text = (int.Parse(teraRaidView.textSweetHerba.Text) + 1).ToString();
                            teraRaidView.textSweetHerba.ForeColor = Color.White;
                            teraRaidView.labelSweetHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1905)
                        {
                            teraRaidView.textSaltyHerba.Text = (int.Parse(teraRaidView.textSaltyHerba.Text) + 1).ToString();
                            teraRaidView.textSaltyHerba.ForeColor = Color.White;
                            teraRaidView.labelSaltyHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1906)
                        {
                            teraRaidView.textSourHerba.Text = (int.Parse(teraRaidView.textSourHerba.Text) + 1).ToString();
                            teraRaidView.textSourHerba.ForeColor = Color.White;
                            teraRaidView.labelSourHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1907)
                        {
                            teraRaidView.textBitterHerba.Text = (int.Parse(teraRaidView.textBitterHerba.Text) + 1).ToString();
                            teraRaidView.textBitterHerba.ForeColor = Color.White;
                            teraRaidView.labelBitterHerba.ForeColor = Color.WhiteSmoke;
                        }
                        if (rewards[i].Item1 == 1908)
                        {
                            teraRaidView.textSpicyHerba.Text = (int.Parse(teraRaidView.textSpicyHerba.Text) + 1).ToString();
                            teraRaidView.textSpicyHerba.ForeColor = Color.White;
                            teraRaidView.labelSpicyHerba.ForeColor = Color.WhiteSmoke;
                        }
                    }
                    teraRaidView.Shiny.Visible = Raid.CheckIsShiny(raid, encounter);
                    teraRaidView.picShinyAlert.Enabled = Raid.CheckIsShiny(raid, encounter);
                }
                else
                {
                    // TODO Clear all the fields.
                }
            }
            else
            {
                MessageBox.Show($"Unable to display raid at index {index}. Ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");
            }
        }

        private string GetPIDString(Raid raid, ITeraRaid? enc)
        {
            var shiny_mark = " (☆)";
            if (HideSeed)
                return "Hidden";
            var pid = $"{raid.PID:X8}";
            return Raid.CheckIsShiny(raid, enc) ? pid + shiny_mark : pid;
        }

        private static string IVsString(int[] ivs, bool verbose = false)
        {
            string s = string.Empty;
            var stats = new[] { "HP", "Atk", "Def", "SpA", "SpD", "Spe" };
            for (int i = 0; i < ivs.Length; i++)
            {
                s += $"{ivs[i]:D2}{(verbose ? " " + stats[i] : string.Empty)}";
                if (i < 5)
                    s += "/";
            }
            return s;
        }

        private static int[] ToSpeedLast(int[] ivs)
        {
            var res = new int[6];
            res[0] = ivs[0];
            res[1] = ivs[1];
            res[2] = ivs[2];
            res[3] = ivs[4];
            res[4] = ivs[5];
            res[5] = ivs[3];
            return res;
        }

        private static Image ApplyTeraColor(byte elementalType, Image img, SpriteBackgroundType type)
        {
            var color = TypeColor.GetTypeSpriteColor(elementalType);
            var thk = SpriteBuilder.ShowTeraThicknessStripe;
            var op = SpriteBuilder.ShowTeraOpacityStripe;
            var bg = SpriteBuilder.ShowTeraOpacityBackground;
            return ApplyColor(img, type, color, thk, op, bg);
        }

        private static Image ApplyColor(Image img, SpriteBackgroundType type, Color color, int thick, byte opacStripe, byte opacBack)
        {
            if (type == SpriteBackgroundType.BottomStripe)
            {
                int stripeHeight = thick; // from bottom
                if ((uint)stripeHeight > img.Height) // clamp negative & too-high values back to height.
                    stripeHeight = img.Height;

                return ImageUtil.BlendTransparentTo(img, color, opacStripe, img.Width * 4 * (img.Height - stripeHeight));
            }
            if (type == SpriteBackgroundType.TopStripe)
            {
                int stripeHeight = thick; // from top
                if ((uint)stripeHeight > img.Height) // clamp negative & too-high values back to height.
                    stripeHeight = img.Height;

                return ImageUtil.BlendTransparentTo(img, color, opacStripe, 0, (img.Width * 4 * stripeHeight) - 4);
            }
            if (type == SpriteBackgroundType.FullBackground) // full background
            {
                return ImageUtil.BlendTransparentTo(img, color, opacBack);
            }
            return img;
        }

        private static Image? GenerateMap(Raid raid, int teratype)
        {
            var original = PKHeX.Drawing.Misc.TypeSpriteUtil.GetTypeSpriteGem((byte)teratype);
            if (original == null)
                return null;
            var gem = (Image)new Bitmap(original, new Size(30, 30));
            var gem2 = (Image)new Bitmap(original, new Size(30, 30));
            SpriteUtil.GetSpriteGlow(gem, 0xFF, 0xFF, 0xFF, out var glow, true);
            SpriteUtil.GetSpriteGlow(gem, 0xCC, 0xCC, 0xCC, out var glow2, true);
            gem = ImageUtil.LayerImage(gem, ImageUtil.GetBitmap(glow, gem.Width, gem.Height, gem.PixelFormat), 0, 0);
            gem2 = ImageUtil.LayerImage(gem, ImageUtil.GetBitmap(glow2, gem.Width, gem.Height, gem.PixelFormat), 0, 0);
            if (den_locations == null || den_locations.Count == 0)
                return null;
            try
            {
                var x = (den_locations[$"{raid.Area}-{raid.Den}"][0] - 100.072021484) * 512 / 5000;
                var y = (den_locations[$"{raid.Area}-{raid.Den}"][2] + 5350.240018) * 512 / 5000;
                var mapimg = ImageUtil.LayerImage(map, gem, (int)x, (int)y);
                if (den_locations.ContainsKey($"{raid.Area}-{raid.Den}_"))
                {
                    x = (den_locations[$"{raid.Area}-{raid.Den}_"][0] - 100.072021484) * 512 / 5000;
                    y = (den_locations[$"{raid.Area}-{raid.Den}_"][2] + 5350.240018) * 512 / 5000;
                    mapimg = ImageUtil.LayerImage(mapimg, gem2, (int)x, (int)y);
                }
                return mapimg;
            }
            catch { return null; }
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if (Raids.Count > 0)
            {
                index = (index + Raids.Count - 1) % Raids.Count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        var chk = (index + Raids.Count - i) % Raids.Count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Encounters[chk], Raids[chk], RaidBoost.SelectedIndex)))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                ComboIndex.SelectedIndex = index;
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (Raids.Count > 0)
            {
                index = (index + Raids.Count + 1) % Raids.Count; // Wrap around
                if (ModifierKeys == Keys.Shift)
                {
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        var chk = (index + Raids.Count + i) % Raids.Count;
                        if (StopAdvances || RaidFilters.Any(z => z.FilterSatisfied(Encounters[chk], Raids[chk], RaidBoost.SelectedIndex)))
                        {
                            index = chk;
                            break;
                        }
                    }
                }
                ComboIndex.SelectedIndex = index;
            }
        }

        private static new async Task Click(SwitchButton button, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Click(button, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }


        private static async Task Touch(int x, int y, int hold, int delay, CancellationToken token)
        {
            var command = Encoding.ASCII.GetBytes($"touchHold {x} {y} {hold}\r\n");
            await SwitchConnection.SendAsync(command, token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private static async Task PressAndHold(SwitchButton b, int hold, int delay, CancellationToken token)
        {
            await SwitchConnection.SendAsync(SwitchCommand.Hold(b, true), token).ConfigureAwait(false);
            await Task.Delay(hold, token).ConfigureAwait(false);
            await SwitchConnection.SendAsync(SwitchCommand.Release(b, true), token).ConfigureAwait(false);
            await Task.Delay(delay, token).ConfigureAwait(false);
        }

        private static async Task<string> GetGameID(CancellationToken token) => await SwitchConnection.GetTitleID(token).ConfigureAwait(false);
        private bool StopAdvances => RaidFilters.Count == 0 || RaidFilters.All(x => x.Enabled == false);

        private async Task AdvanceDate(CancellationToken token)
        {
            if (Config.StreamerView)
            {
                teraRaidView.startProgress();
            }

            ConnectionStatusText.Text = "Changing date";
            int BaseDelay = (int)Config.BaseDelay;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case
                                                                                 // HOME Menu
            await Click(HOME, (int)Config.OpenHome + BaseDelay, token).ConfigureAwait(false);
            // Navigate to Settings
            if (Config.UseTouch)
            {
                await Touch(840, 540, 0_050, BaseDelay, token).ConfigureAwait(false);
            }
            else
            {
                await Click(DDOWN, (int)Config.NavigateToSettings + 0_100 + BaseDelay, token).ConfigureAwait(false);
                for (int i = 0; i < 5; i++) await Click(DRIGHT, (int)Config.NavigateToSettings + BaseDelay, token).ConfigureAwait(false);
            }
            await Click(A, (int)Config.OpenSettings + BaseDelay, token).ConfigureAwait(false);
            // Scroll to bottom
            await PressAndHold(DDOWN, (int)Config.Hold, BaseDelay, token).ConfigureAwait(false);

            // Navigate to "Date and Time"
            await Click(DRIGHT, 0_200 + BaseDelay, token).ConfigureAwait(false);
            // Hold down to overshoot Date/Time by one. DUP to recover.
            if (Config.UseOvershoot)
            {
                await PressAndHold(DDOWN, (int)Config.SystemOvershoot, 0, token).ConfigureAwait(false);
                await Click(DUP, 0_500, token).ConfigureAwait(false);
            }
            else
                // I tried using holds here but could not get the timing consistent
                // Even if this is slightly slower, it is at least consistent
                // And not missing any cycles means it's faster overall
                for (int i = 0; i < Config.SystemDDownPresses; i++) await Click(DDOWN, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, (int)Config.Submenu + BaseDelay, token).ConfigureAwait(false);

            // Navigate to Change Date/Time
            if (Config.UseTouch)
            {
                await Touch(840, 400, 0_050, 0_300 + BaseDelay, token).ConfigureAwait(false);
            }
            else
            {
                for (int i = 0; i < 2; i++) await Click(DDOWN, 0_200 + BaseDelay, token).ConfigureAwait(false);
                await Click(A, (int)Config.DateChange + BaseDelay, token).ConfigureAwait(false);
            }

            // Change the date
            for (int i = 0; i < Config.DaysToSkip; i++) await Click(DUP, 0_200 + BaseDelay, token).ConfigureAwait(false); // Not actually necessary, so we default to 0 as per #29


            for (int i = 0; i < 6; i++) await Click(DRIGHT, 0_050 + BaseDelay, token).ConfigureAwait(false);
            await Click(A, 0_500 + BaseDelay, token).ConfigureAwait(false);

            // Return to game
            await Click(HOME, (int)Config.ReturnHome + BaseDelay, token).ConfigureAwait(false);
            await Click(HOME, (int)Config.ReturnGame + BaseDelay, token).ConfigureAwait(false);
        }

        private async Task SaveGame(CancellationToken token)
        {
            int BaseDelay = (int)Config.BaseDelay;
            int SaveTiming = (int)Config.SaveTiming;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case

            //Make sure menus are closed
            await Click(B, 0_1000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
            await Click(B, 0_1000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
            //Open menu and save
            await Click(X, 0_1000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
            await Click(R, 0_1000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
            await Click(A, 0_5000 + BaseDelay + SaveTiming, token).ConfigureAwait(false);
        }

        private async Task JKSV(CancellationToken token, ITeraRaid encounter, Raid raid)
        {
            int BaseDelay = (int)Config.BaseDelay;
            int JKSVTiming = (int)Config.JKSVTiming;
            await Click(LSTICK, 0_050 + BaseDelay, token).ConfigureAwait(false); // Sometimes it seems like the first command doesn't go through so send this just in case

            //Go to home screen and close game
            await Click(HOME, 0_2000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(X, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_3000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //navigate to JKSV
            await Click(DDOWN, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(DRIGHT, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(DRIGHT, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(DRIGHT, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //Open JKSV, game, create new save
            await Click(A, 0_5000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //Navigate before date
            for (int i = 0; i < 23; i++) await Click(L, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //Keyboard stuff
            var blank = new PK9
            {
                Species = encounter.Species,
                Form = encounter.Form
            };
            var s = $"{Raid.strings.Species[encounter.Species]}";
            var s2 = $"{Raid.strings.types[Raid.GetTeraType(encounter, raid)]}";
            var strokes = s.ToUpper().ToCharArray();
            var strokes2 = s2.ToUpper().ToCharArray();
            var number = $"NumPad";
            List<HidKeyboardKey> keystopress = new();
            foreach (var str in strokes)
            {
                foreach (HidKeyboardKey keypress in (HidKeyboardKey[])Enum.GetValues(typeof(HidKeyboardKey)))
                {
                    if (str.ToString().Equals(keypress.ToString()) || (number + str.ToString()).Equals(keypress.ToString()))
                        keystopress.Add(keypress);
                }
            }
            List<HidKeyboardKey> keystopress2 = new();
            foreach (var str in strokes2)
            {
                foreach (HidKeyboardKey keypress in (HidKeyboardKey[])Enum.GetValues(typeof(HidKeyboardKey)))
                {
                    if (str.ToString().Equals(keypress.ToString()) || (number + str.ToString()).Equals(keypress.ToString()))
                        keystopress2.Add(keypress);
                }
            }
            //Type word1, space, word2, space
            await SwitchConnection.TypeMultiKeys(keystopress, token).ConfigureAwait(false);
            await Task.Delay(500);
            await Click(Y, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await SwitchConnection.TypeMultiKeys(keystopress2, token).ConfigureAwait(false);
            await Task.Delay(500);
            await Click(Y, 0_100 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //Save
            await Click(PLUS, 0_3000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(PLUS, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            //Open the game
            await Click(HOME, 0_2000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_1000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_19000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(A, 0_17000 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
            await Click(X, 0_050 + BaseDelay + JKSVTiming, token).ConfigureAwait(false);
        }


        private void ButtonStopCrawl_Click(object sender, EventArgs e)
        {
            stopRequested = true;
            this.ButtonStopCrawl.Enabled = false;
        }

        private async void ButtonAdvanceDate_Click(object sender, EventArgs e)
        {
        Cheater:
            if (SwitchConnection.Connected)
            {
                SearchTimer.Start();
                stopwatch.Reset();
                stopwatch.Start();
                DaySkipTries = 0;
                DaySkipSuccess = 0;
                DaySkipStreak = 0;
                DaySkipShiny = 0;

                ButtonReadRaids.Enabled = false;
                ButtonAdvanceDate.Enabled = false;
                ButtonStopCrawl.Enabled = true;
                _WindowState = WindowState;
                var prompt = false;
                do
                {
                    // average run takes between 15-18secs on a v1 switch, undocked
                    // if a single run of the loop takes more than a minute, stop it, something is wrong
                    AdvanceDateCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                    prev = Raids.Select(z => z.Seed).ToList();
                    try
                    {
                        await AdvanceDate(AdvanceDateCancellationTokenSource.Token);
                        await ReadRaids(AdvanceDateCancellationTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // Stop button was pressed or timed out
                    }
                } while (CheckAdvanceDate(AdvanceDateCancellationTokenSource.Token, out prompt));
                if (prompt)
                {
                    stopwatch.Stop();
                    var timeSpan = stopwatch.Elapsed;
                    var timeEmpty = new TimeSpan(0, 0, 0, 0);
                    string time = string.Empty;
                    if (((int)timeSpan.TotalDays) != timeEmpty.TotalDays) { time = timeSpan.ToString(@"d\ %h\:mm\:ss"); }
                    else if (((int)timeSpan.TotalHours) != timeEmpty.TotalHours) { time = timeSpan.ToString(@"%h\:mm\:ss"); }
                    else if (((int)timeSpan.TotalMinutes) != timeEmpty.TotalMinutes) { time = timeSpan.ToString(@"%m\:ss"); }
                    else { time = timeSpan.ToString(@"%s"); }
                    var encounter = Encounters[0];
                    var raid = Raids[0];
                    if (Config.PlaySound) System.Media.SystemSounds.Asterisk.Play();
                    if (Config.FocusWindow)
                    {
                        WindowState = _WindowState;
                        Activate();
                    }
                    for (int i = 0; i < Raids.Count; i++)
                    {
                        var satisfied_filters = new List<RaidFilter>();
                        foreach (var filter in RaidFilters)
                        {
                            if (filter == null)
                                continue;
                            if (filter.FilterSatisfied(Encounters[i], Raids[i], RaidBoost.SelectedIndex))
                            {
                                satisfied_filters.Add(filter);
                                ComboIndex.SelectedIndex = i;
                            }
                        }
                        if (satisfied_filters.Count > 0)
                        {
                            NotificationHandler.SendNotifications(Config, Encounters[i], Raids[i], satisfied_filters, time, RewardsList[i]);
                            encounter = Encounters[i];
                            raid = Raids[i];
                        }
                    }
                    try
                    {
                        if (Config.SaveAfterFind) await SaveGame(AdvanceDateCancellationTokenSource.Token);
                        if (Config.BackupAfterSave)
                        {
                            await JKSV(AdvanceDateCancellationTokenSource.Token, encounter!, raid);
                            await Task.Delay(1500);
                        }
                        if (Config.ContinueAfterBackup)
                        {
                            //This is bad, change it later. 
                            goto Cheater;
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Stop button was pressed or timed out
                    }

                    if (Config.EnableAlertWindow) MessageBox.Show(Config.AlertWindowMessage + "\n\nTime Spent: " + time, "Result found!", MessageBoxButtons.OK);
                }

                ButtonStopCrawl.Enabled = false;
                ButtonReadRaids.Enabled = true;
                ButtonAdvanceDate.Enabled = true;
                SearchTimer.Stop();
            }
        }

        private bool CheckAdvanceDate(CancellationToken cancellationToken, out bool prompt)
        {
            prompt = false;
            if (cancellationToken.IsCancellationRequested || !CheckEnableFilters.Checked)
                return false;
            if (prev.Count != Raids.Count)
                return true;
            var sameraids = true;
            for (int i = 0; i < prev.Count; i++)
            {
                if (Raids[i].Seed != prev[i])
                    sameraids = false;
            }

            DaySkipTries++;
            if (!sameraids)
            {
                DaySkipSuccess++;
                DaySkipStreak++;
            }
            else
            {
                DaySkipStreak = 0;
            }

            DaySkipSuccessRate.Text = $"Skip Rate: {DaySkipSuccess}/{DaySkipTries}     Total Miss: {DaySkipTries - DaySkipSuccess}     Streak: {DaySkipStreak}";

            DaySkipShiny += Enumerable.Range(0, Raids.Count).Where(i => Raid.CheckIsShiny(Raids[i], Encounters[i])).Count();
            ShinyCount.Text = $"Total Shinies Found: {DaySkipShiny}";


            if (sameraids)
                return true;
            if (RaidFilters.Any(z => z.FilterSatisfied(Encounters, Raids, RaidBoost.SelectedIndex)))
                prompt = true;
            if (stopRequested)
            {
                stopRequested = false;
                return false;
            }
            if (StopAdvances || prompt == true)
                return false;
            return true;
        }

        private async void ButtonReadRaids_Click(object sender, EventArgs e)
        {
            ButtonReadRaids.Enabled = false;
            ButtonAdvanceDate.Enabled = false;
            if (IsReading)
            {
                MessageBox.Show("Please wait for the current RAM read to finish.");
            }
            else
            {
                IsReading = true;
                await ReadRaids(CancellationToken.None);
                IsReading = false;
            }
            ButtonReadRaids.Enabled = true;
            ButtonAdvanceDate.Enabled = true;

        }

        private async Task ReadRaids(CancellationToken token)
        {
            ConnectionStatusText.Text = "Parsing pointer";
            offset = await OffsetUtil.GetPointerAddress(RaidBlockPointer, CancellationToken.None);

            Raids.Clear();
            Encounters.Clear();
            RewardsList.Clear();
            index = 0;

            ConnectionStatusText.Text = "Reading raid block";
            var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset + RaidBlock.HEADER_SIZE, (int)(RaidBlock.SIZE - RaidBlock.HEADER_SIZE), token);
            Raid raid;
            var count = Data.Length / Raid.SIZE;
            HashSet<int> possible_groups = new HashSet<int>();
            if (Raid.DistTeraRaids != null)
            {
                foreach (TeraDistribution e in Raid.DistTeraRaids)
                {
                    if (TeraDistribution.AvailableInGame(e.Entity, Config.Game))
                        possible_groups.Add(e.DeliveryGroupID);
                }
            }
            var eventct = 0;
            for (int i = 0; i < count; i++)
            {
                raid = new Raid(Data.Skip(i * Raid.SIZE).Take(Raid.SIZE).ToArray());
                var progress = raid.IsEvent ? EventProgress.SelectedIndex : Progress.SelectedIndex;
                var raid_delivery_group_id = raid.IsEvent ? TeraDistribution.GetDeliveryGroupID(eventct, Raid.DeliveryRaidPriority, possible_groups) : -1;
                var encounter = raid.Encounter(progress, raid_delivery_group_id);
                if (raid.IsValid)
                {
                    Raids.Add(raid);
                    Encounters.Add(encounter);
                    RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, raid.TeraType, RaidBoost.SelectedIndex));
                }
                if (raid.IsEvent)
                    eventct++;
            }

            ConnectionStatusText.Text = "Completed";
            LabelLoadedRaids.Text = $"Met Filters/Shiny: {Enumerable.Range(0, Raids.Count).Count(i => RaidFilters.Any(z => z.FilterSatisfied(Encounters[i], Raids[i], RaidBoost.SelectedIndex)))}/{Enumerable.Range(0, Raids.Count).Where(i => Raid.CheckIsShiny(Raids[i], Encounters[i])).Count()}";
            //LabelLoadedRaids.Text = $"Raids: {Raids.Count} | Shiny: {Enumerable.Range(0, Raids.Count).Where(i => Raid.CheckIsShiny(Raids[i], Encounters[i])).Count()}";
            if (Raids.Count > 0)
            {
                ButtonPrevious.Enabled = true;
                ButtonNext.Enabled = true;
                CopyAnnounce.Enabled = true;
                Rewards.Enabled = true;

                if (Encounters.Count > 0)
                {
                    ComboIndex.DataSource = GetComboList();
                    ///ComboIndex.DataSource = Enumerable.Range(0, Raids.Count).Select(z => $"{z + 1:D2} / {Raids.Count:D2} {Raid.strings.Species[Encounters[z].Species]}{(Encounters[z].Form != 0 ? $"-{Encounters[z].Form}" : "")}").ToArray();
                    //ComboIndex.DataSource = Enumerable.Range(0, Raids.Count ).Select(z => $"{(Raid.CheckIsShiny(Raids[z], Encounters[z]) ? "☆" : "")}{z + 1:D2} {Raid.strings.Species[Encounters[z].Species]} {(Encounters[z].Form != 0 ? $"-{Encounters[z].Form}" : "")}{(Raid.CheckIsShiny(Raids[z], Encounters[z]) ? "☆" : "")}").ToArray();
                    ComboIndex.SelectedIndex = index < Raids.Count ? index : 0;
                }
            }
            else
            {
                ButtonPrevious.Enabled = false;
                ButtonNext.Enabled = false;
                CopyAnnounce.Enabled = false;
                Rewards.Enabled = false;

                if (Raids.Count > RaidBlock.MAX_COUNT || Raids.Count == 0) MessageBox.Show("Bad read, ensure there are no cheats running or anything else that might shift RAM (Edizon, overlays, etc.), then reboot your console and try again.");

            }
        }

        private List<string> GetComboList()
        {
            var combo = new List<String>();
            //List<Raid> Raids, List<ITeraRaid?> Encounters
            for (var i = 0; Raids.Count > i; i++)
            {
                string s = string.Empty;
                string form = string.Empty;
                string name = string.Empty;
                Raid raid = Raids[i];
                var encounter = Encounters[i];

                if (encounter is not null)
                {
                    var blank = new PK9
                    {
                        Species = encounter.Species,
                        Form = encounter.Form
                    };
                    form = ShowdownParsing.GetStringFromForm(encounter.Form, GameInfo.GetStrings(GameLanguage.DefaultLanguage), blank.Species, blank.Context);
                    name = $" {Raid.strings.Species[encounter.Species]}{(encounter.Form != 0 ? $"{(form == "F" ? "" : $" ({form})")}" : "")}";
                }

                var shiny = $"{(Raid.CheckIsShiny(raid, encounter) ? "☆" : "")}";
                s = $"{shiny}{i + 1:D2}{name}{shiny}";

                combo.Add(s);


            }
            return combo;
        }

        private void Progress_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.Progress = Progress.SelectedIndex;
            SaveConfig();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private void EventProgress_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.EventProgress = EventProgress.SelectedIndex;
            SaveConfig();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private async void ViewRAM_Click(object sender, EventArgs e)
        {
            if (SwitchConnection.Connected && ModifierKeys == Keys.Shift)
            {
                if (IsReading)
                {
                    MessageBox.Show("Please wait for the current RAM read to finish.");
                }
                else
                {
                    IsReading = true;
                    var Data = await SwitchConnection.ReadBytesAbsoluteAsync(offset, (int)RaidBlock.SIZE, CancellationToken.None);
                    RaidBlockViewer BlockViewerWindow = new(Data, offset);
                    BlockViewerWindow.ShowDialog();
                    IsReading = false;
                }
            }
            else if (Raids[index] != null)
            {
                RaidBlockViewer BlockViewerWindow = new(Raids[index].Data, offset);
                BlockViewerWindow.ShowDialog();
            }
        }

        private void Game_SelectedIndexChanged(object sender, EventArgs e)
        {
            Raid.Game = Game.Text;
            Config.Game = Game.Text;
            SaveConfig();
            if (Raids.Count > 0) DisplayRaid(index);
        }

        private void StopFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterSettings(ref RaidFilters);
            form.ShowDialog();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Location = Location;
            SaveConfig();
            Disconnect();
        }

        private async void DownloadEvents_Click(object sender, EventArgs e)
        {
            if (!SwitchConnection.Connected)
                return;
            await ReadEventRaids(true);
        }

        private void Seed_Clicked(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;
            if (Raids.Count <= index)
                return;
            var raid = Raids[index];
            Seed.Text = HideSeed ? $"{raid.Seed:X8}" : "Hidden";
            EC.Text = HideSeed ? $"{raid.EC:X8}" : "Hidden";
            PID.Text = (HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
            HideSeed = !HideSeed;
            ActiveControl = null;
        }

        private void EC_Clicked(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;
            if (Raids.Count <= index)
                return;
            var raid = Raids[index];
            Seed.Text = HideSeed ? $"{raid.Seed:X8}" : "Hidden";
            EC.Text = HideSeed ? $"{raid.EC:X8}" : "Hidden";
            PID.Text = (HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
            HideSeed = !HideSeed;
            ActiveControl = null;
        }

        private void PID_Clicked(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;
            if (Raids.Count <= index)
                return;
            var raid = Raids[index];
            Seed.Text = HideSeed ? $"{raid.Seed:X8}" : "Hidden";
            EC.Text = HideSeed ? $"{raid.EC:X8}" : "Hidden";
            PID.Text = (HideSeed ? $"{raid.PID:X8}" : "Hidden") + $"{(raid.IsShiny ? " (☆)" : string.Empty)}";
            HideSeed = !HideSeed;
            ActiveControl = null;
        }

        private void ConfigSettings_Click(object sender, EventArgs e)
        {
            var form = new ConfigWindow(Config);
            form.ShowDialog();
        }

        private void DisplayMap(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }
            var raid = Raids[index];
            var encounter = Encounters[index];
            var teratype = Raid.GetTeraType(encounter, raid);
            var map = GenerateMap(raid, teratype);
            if (map == null)
            {
                MessageBox.Show("Error generating map.");
                return;
            }
            var form = new MapView(map);
            form.ShowDialog();
        }

        private void Rewards_Click(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }

            var rewards = RewardsList[index];

            if (rewards == null)
            {
                MessageBox.Show("Error while displaying rewards.");
                return;
            }

            var form = new RewardsView(rewards);
            form.ShowDialog();
        }

        private void RaidBoost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RewardsList.Clear();
            for (int i = 0; i < Raids.Count; i++)
            {
                var raid = Raids[i];
                var encounter = Encounters[i];
                RewardsList.Add(Structures.Rewards.GetRewards(encounter, raid.Seed, raid.TeraType, RaidBoost.SelectedIndex));
            }
        }

        Point Default = new(25, 236);
        Point ShowExtra = new(34, 236);
        private void Move_Clicked(object sender, EventArgs e)
        {
            if (Raids.Count == 0)
            {
                MessageBox.Show("Raids not loaded.");
                return;
            }
            var encounter = Encounters[index];
            if (encounter == null)
                return;
            ShowExtraMoves = !ShowExtraMoves;
            LabelMoves.Text = ShowExtraMoves ? "Extra:" : "Moves:";
            LabelMoves.Location = ShowExtraMoves ? ShowExtra : Default;
            var extra_moves = new ushort[] { 0, 0, 0, 0 };
            for (int i = 0; i < encounter.ExtraMoves.Length; i++)
                if (i < extra_moves.Length) extra_moves[i] = encounter.ExtraMoves[i];
            Move1.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[0]] : Raid.strings.Move[encounter.Move1];
            Move2.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[1]] : Raid.strings.Move[encounter.Move2];
            Move3.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[2]] : Raid.strings.Move[encounter.Move3];
            Move4.Text = ShowExtraMoves ? Raid.strings.Move[extra_moves[3]] : Raid.strings.Move[encounter.Move4];
        }

        private void ComboIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            index = ComboIndex.SelectedIndex;
            DisplayRaid(index);

            if (Config.StreamerView)
            {
                DisplayPrettyRaid(index);
            }
        }

        private void SendScreenshot_Click(object sender, EventArgs e)
        {
            if (!SwitchConnection.Connected)
            {
                MessageBox.Show("Cannot send a screenshot, Switch not connected.");
                return;
            }

            NotificationHandler.SendScreenshot(Config, SwitchConnection);
        }

        private void ConnectionStatusText_TextChanged(object sender, EventArgs e)
        {
            if (Config.StreamerView)
            {
                teraRaidView.debug.Text = ConnectionStatusText.Text;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            var timeSpan = stopwatch.Elapsed;
            var timeEmpty = new TimeSpan(0, 0, 0, 0);
            string time = string.Empty;
            if (((int)timeSpan.TotalDays) != timeEmpty.TotalDays) { time = timeSpan.ToString(@"d\ %h\:mm\:ss"); }
            else if (((int)timeSpan.TotalHours) != timeEmpty.TotalHours) { time = timeSpan.ToString(@"%h\:mm\:ss"); }
            else if (((int)timeSpan.TotalMinutes) != timeEmpty.TotalMinutes) { time = timeSpan.ToString(@"%m\:ss"); }
            else { time = timeSpan.ToString(@"%s"); }

            SearchTime.Text = "Search Time: " + time;
            if (Config.StreamerView)
            {
                teraRaidView.textSearchTime.Text = time;
            }
        }

        public void TestWebhook()
        {
            var filter = new RaidFilter { Name = "Test Webhook" };
            var satisfied_filters = new List<RaidFilter> { filter };

            int i = ComboIndex.SelectedIndex;
            if (i > -1 && Encounters[i] != null && Raids[i] != null)
            {
                var timeSpan = stopwatch.Elapsed;
                string time = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                NotificationHandler.SendNotifications(Config, Encounters[i], Raids[i], satisfied_filters, time, RewardsList[i]);
            }
            else
            {
                MessageBox.Show("Please connect to your device and ensure a raid has been found.");
            }
        }

        private void CopyAnnounce_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
                Clipboard.SetText(NotificationHandler.GenerateRaidBot(true, Config, Encounters[index], Raids[index], RewardsList[index]));
            else
                Clipboard.SetText(NotificationHandler.GenerateRaidBot(false, Config, Encounters[index], Raids[index], RewardsList[index]));
        }

        private async void ButtonSceenState_Click(object sender, EventArgs e)
        {
            ButtonSceenState.Text = $"{(screenState ? "Screen Off" : "Screen On")}";
            await SwitchConnection.SendAsync(SwitchCommand.SetScreen(screenState ? ScreenState.On : ScreenState.Off), CancellationToken.None).ConfigureAwait(false);
            screenState = !screenState;
        }

        private void Cheat_Click(object sender, EventArgs e)
        {
            if (ModifierKeys != Keys.Shift)
                return;
            if (!Config.BackupAfterSave & !Config.ContinueAfterBackup) { Config.BackupAfterSave = true; }
            else if (Config.BackupAfterSave & !Config.ContinueAfterBackup) { Config.ContinueAfterBackup = true; }
            else if (Config.BackupAfterSave & Config.ContinueAfterBackup)
            {
                Config.BackupAfterSave = false;
                Config.ContinueAfterBackup = false;
            }

            if (Config.BackupAfterSave)
            {
                Cheat.ForeColor = Color.BlueViolet;
            }
            else
            {
                Cheat.ForeColor = Config.windowColor;
            }

            if (Config.ContinueAfterBackup) Cheat.ForeColor = Color.OrangeRed;
        }

        public void SetTheme(int index)
        {
            //Theme definitions
            //Default
            Config.Theme = index;
            if (index == 0)
            {
                Config.windowColor = Color.FromArgb(255, 240, 240, 240);
                Config.textColor = Color.Black;
                Config.disabledText = Color.LightSlateGray;
                Config.textBorder = Color.FromArgb(255, 240, 240, 240);
                Config.buttonBackgroundColor = Color.FromArgb(255, 253, 253, 253);
                Config.buttonDisabledBackgroundColor = Color.FromArgb(255, 249, 249, 249);
                Config.buttonBorderColor = Color.FromArgb(255, 186, 186, 186);
                //Config.buttonDisabledBorderColor = Color.FromArgb(255, 233, 233, 233);
                Config.boxColor = Color.White;
            }
            //Dark
            else if (index == 1)
            {
                Config.windowColor = Color.FromArgb(255, 54, 57, 63);
                Config.textColor = Color.White;
                Config.disabledText = Color.LightSlateGray;
                Config.textBorder = Color.FromArgb(255, 54, 57, 63);
                Config.buttonBackgroundColor = Color.FromArgb(255, 47, 49, 54);
                Config.buttonDisabledBackgroundColor = Color.FromArgb(255, 67, 69, 74);
                Config.buttonBorderColor = Color.FromArgb(255, 139, 154, 162);
                Config.boxColor = Color.FromArgb(255, 255, 80, 255);
            }
            SaveConfig();
        }
    

        public void LoadTheme()
        { 
            //Load it
            this.BackColor = Config.windowColor;
            this.ForeColor = Config.textColor;
            Color panel1Button = Color.FromArgb(125, Config.buttonBackgroundColor.R, Config.buttonBackgroundColor.G, Config.buttonBackgroundColor.B);
            Color panel1Box = Color.FromArgb(125, Config.windowColor.R, Config.windowColor.G, Config.windowColor.B);
            /*foreach (Panel p in this.Controls)
            {
               
            }*/
            foreach (Control p in myPanel1.Controls)
            {
                if (p is RJButton)
                {
                    RJButton rj = (RJButton)p;
                    rj.BackgroundColor = rj.Enabled ? panel1Button : Config.buttonDisabledBackgroundColor;
                    rj.BorderColor = rj.Enabled ? Config.buttonBorderColor : Config.buttonBorderColor;
                    rj.TextEnabledColor = Config.textColor;
                    rj.TextDisabledColor = Config.disabledText;
                    //p.BackColor = panel1Button;
                    //p.ForeColor = Config.textColor;
                }
                if (p is TextBox)
                {
                    p.BackColor = panel1Box;
                    p.ForeColor = Config.textColor;
                }
                if (p is RoundLabel)
                {
                    RoundLabel rl = (RoundLabel)p;
                    rl.back = Config.windowColor;
                    p.ForeColor = Config.textColor;
                }
            }
            foreach (Control p in myPanel2.Controls)
            {
                
            }
            foreach (Control p in myPanel3.Controls)
            {
                if (p is RJButton)
                {
                    RJButton rj = (RJButton)p;
                    rj.BackgroundColor = rj.Enabled ? Config.buttonBackgroundColor : Config.buttonDisabledBackgroundColor;
                    rj.BorderColor = rj.Enabled ? Config.buttonBorderColor : Config.buttonBorderColor;
                    rj.TextEnabledColor = Config.textColor;
                    rj.TextDisabledColor = Config.disabledText;
                    //rj.TextColor = rj.Enabled ? Config.textColor : Config.disabledText;

                    //p.BackColor = Config.buttonBackgroundColor;
                    //p.ForeColor = Config.textColor;
                }
                if (p is TextBox)
                {
                    p.BackColor = Config.boxColor;
                    p.ForeColor = Config.textColor;
                }
                if (p is ComboBox)
                {
                    p.BackColor = Config.boxColor;
                    p.ForeColor = Config.textColor;
                }
            }
            foreach (Control p in panel2.Controls)
            {
                if (p is RJButton)
                {
                    RJButton rj = (RJButton)p;
                    rj.BackgroundColor = rj.Enabled ? Config.buttonBackgroundColor : Config.buttonDisabledBackgroundColor;
                    rj.BorderColor = rj.Enabled ? Config.buttonBorderColor : Config.buttonBorderColor;
                    rj.TextEnabledColor = Config.textColor;
                    rj.TextDisabledColor = Config.disabledText;
                    //rj.TextColor = rj.Enabled ? Config.textColor : Config.disabledText;
                    //p.BackColor = Config.buttonBackgroundColor;
                    //p.ForeColor = Config.textColor;
                }
                if (p is ComboBox)
                {
                    p.BackColor = Config.boxColor;
                    p.ForeColor = Config.textColor;
                }
            }
            Cheat.ForeColor = Config.windowColor;
            LabelIsEvent.ForeColor = Color.FromArgb(255, 100, 120, 255);
            //ButtonConnect.back = Color.Blue;
        }
    }
}
