using System.Data;

namespace RaidCrawler.Structures
{
    public class Config
    {
        // General
        public string SwitchIP { get; set; } = "192.168.0.0";
        public string SwitchUSBPort { get; set; } = "1";
        public string SwitchProtocol { get; set; } = "wifi";
        public string Game { get; set; } = "Scarlet";
        public int Progress { get; set; } = 0;
        public int EventProgress { get; set; } = 0;
        public Point Location { get; set; } = new(0, 0);

        // Match
        public bool FocusWindow { get; set; } = true;
        public bool PlaySound { get; set; } = true;
        public bool EnableAlertWindow { get; set; } = true;
        public string AlertWindowMessage { get; set; } = "Match found! Hold Shift and click one of the arrow keys to jump to the matching result.";
        public bool EnableNotification { get; set; } = false;
        public string DiscordWebhook { get; set; } = string.Empty;
        public string DiscordMessageContent { get; set; } = string.Empty;

        // Date Advance
        public bool UseTouch { get; set; } = true;
        public bool UseOvershoot { get; set; } = true;

        public decimal BaseDelay { get; set; } = 0;
        public decimal OpenHome { get; set; } = 1800;
        public decimal NavigateToSettings { get; set; } = 100;
        public decimal OpenSettings { get; set; } = 1000;
        public decimal Hold { get; set; } = 1700;
        public decimal SystemDDownPresses { get; set; } = 38;
        public decimal Submenu { get; set; } = 2200;
        public decimal DateChange { get; set; } = 500;
        public decimal DaysToSkip { get; set; } = 0;
        public decimal ReturnHome { get; set; } = 2500;
        public decimal ReturnGame { get; set; } = 4000;
        public decimal SystemOvershoot { get; set; } = 950;

        // Save
        public bool SaveAfterFind { get; set; } = false;
        public bool BackupAfterSave { get; set; } = false;
        public bool ContinueAfterBackup { get; set; } = false;
        public decimal SaveTiming { get; set; } = 0;
        public decimal JKSVTiming { get; set; } = 0;

        // Webhook
        public bool GifImage{ get; set; } = true;
        public bool CopyEmoji { get; set; } = false;
        public bool EnableEmoji { get; set; } = true;
        public bool VerboseIVs { get; set; } = false;
        public int IVsStyle { get; set; } = 0;
        public string IVsSpacer { get; set; } = " ";
        public bool ToggleDen { get; set; } = true;
        public Dictionary<string, string> Emoji { get; set; } = new Dictionary<string, string>
        {
            { "Bug", "<:tBug:1060235283976699995>" }, { "Dark", "<:tDark:1060235285394366564>" }, { "Dragon", "<:tDragon:1060235286879141917>"}, { "Electric", "<:tElectric:1060235288691093566>"},
            { "Fairy", "<:tFairy:1060235282127003730>"}, { "Fighting", "<:tFighting:1060235325705822309>"}, { "Fire", "<:tFire:1060235326834102382>"}, { "Flying", "<:tFlying:1060235328717336646>"},
            { "Ghost", "<:tGhost:1060235329665241129>"}, { "Grass", "<:tGrass:1060235303828332655>"}, { "Ground", "<:tGround:1060235355867058308>"}, { "Ice", "<:tIce:1060235356710109246>"},
            { "Normal", "<:tNormal:1060235360334008331>"}, { "Poison", "<:tPoison:1060235353732161569>"}, { "Psychic", "<:tPsychic:1060235385235570811>"}, { "Rock", "<:tRock:1060235386279972906>"},
            { "Steel", "<:tSteel:1060235358358491147>"}, { "Water", "<:tWater:1060235383411056640>"}, { "Male", "<:male:1060738367274352730>"}, { "Female", "<:female:1060738368541048965>"},
            { "Shiny", "<:shiny:1065558448995049493>"}, { "Square Shiny", "<:square:1065831026057814097>"}, { "Event Star", "<:raidStarB:1060475726572294144>"}, { "7 Star", "<:raidStarM:1060475723405606994>"},
            { "Star", "<:raidStarY:1060475725498560512>"}, { "Health 0", "<:m1Health0:1063983356309688430>"}, { "Health 31", "<:m1Health31:1063983357773500508>"}, { "Attack 0", "<:m2Attack0:1063983327385751683>"},
            { "Attack 31", "<:m2Attack31:1063983329097039992>"}, { "Defense 0", "<:m3Defence0:1063983331294838814>"}, { "Defense 31", "<:m3Defence31:1063983333056458822>"}, { "SpAttack 0", "<:m4SpecialAttack0:1063983360294273084>"},
            { "SpAttack 31", "<:m4SpecialAttack31:1063983361619660861>"}, { "SpDefense 0", "<:m5SpecialDefence0:1063983385762082867>"}, { "SpDefense 31", "<:m5SpecialDefence31:1063983387137822761>"}, { "Speed 0", "<:m6Speed0:1063983390052847659>" },
            { "Speed 31", "<:m6Speed31:1063983441672163469>" }, { "Sweet Herba", "<:herbaSweet:1058436152924844052>"}, { "Sour Herba", "<:herbaSour:1058436114752475228>"}, { "Salty Herba", "<:herbaSalty:1058436153931464764>"},
            { "Bitter Herba", "<:herbaBitter:1058436112034562088>"}, { "Spicy Herba", "<:herbaSpicy:1058436113276096614>"}, { "Bottle Cap", "<:bottlecap:1058436109761265765>"},
            { "Ability Capsule", "<:abilitycapsule:1059122237019537478>"}, { "Ability Patch", "<:abilitypatch:1059123255283302450>"}
        };

        // Experimental
        public bool StreamerView { get; set; } = false;
        public bool MapBackground { get; set; } = false;
        public string InstanceName { get; set; } = string.Empty;

        // Theme
        public int Theme { get; set; } = 0;
        public Color windowColor { get; set; } = Color.FromArgb(255, 240, 240, 240);
        public Color textColor { get; set; } = Color.Orange;
        public Color disabledText { get; set; } = Color.RebeccaPurple;
        public Color textBorder { get; set; } = Color.White;
        public Color buttonDisabledBackgroundColor { get; set; } = Color.FromArgb(255, 67, 69, 74);
        public Color buttonBackgroundColor { get; set; } = Color.FromArgb(255, 47, 49, 54);
        public Color buttonBorderColor { get; set; } = Color.FromArgb(255, 139, 154, 162);
        public Color boxColor { get; set; } = Color.FromArgb(255, 255, 80, 255);
    }
}
