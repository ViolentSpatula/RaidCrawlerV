namespace RaidCrawler
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CheckEnableFilters = new System.Windows.Forms.CheckBox();
            this.ConnectionStatusText = new System.Windows.Forms.Label();
            this.InputSwitchIP = new System.Windows.Forms.TextBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.LabelSwitchIP = new System.Windows.Forms.Label();
            this.LabelLoadedRaids = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ComboBox();
            this.LabelStoryProgress = new System.Windows.Forms.Label();
            this.LabelGame = new System.Windows.Forms.Label();
            this.Game = new System.Windows.Forms.ComboBox();
            this.LabelEventProgress = new System.Windows.Forms.Label();
            this.EventProgress = new System.Windows.Forms.ComboBox();
            this.Sprite = new System.Windows.Forms.PictureBox();
            this.GemIcon = new System.Windows.Forms.PictureBox();
            this.LabelSandwichBonus = new System.Windows.Forms.Label();
            this.RaidBoost = new System.Windows.Forms.ComboBox();
            this.ComboIndex = new System.Windows.Forms.ComboBox();
            this.SearchTimer = new System.Windows.Forms.Timer(this.components);
            this.SearchTime = new System.Windows.Forms.Label();
            this.DaySkipSuccessRate = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ButtonPrevious = new CustomControls.RJControls.RJButton();
            this.ButtonNext = new CustomControls.RJControls.RJButton();
            this.myPanel1 = new RaidCrawler.Structures.MyPanel();
            this.CopyAnnounce = new CustomControls.RJControls.RJButton();
            this.Rewards = new CustomControls.RJControls.RJButton();
            this.shinyBox = new System.Windows.Forms.PictureBox();
            this.LabelIsEvent = new RaidCrawler.Structures.RoundLabel();
            this.LabelUNK_2 = new RaidCrawler.Structures.RoundLabel();
            this.LabelMoves = new RaidCrawler.Structures.RoundLabel();
            this.LabelAbility = new RaidCrawler.Structures.RoundLabel();
            this.LabelIVs = new RaidCrawler.Structures.RoundLabel();
            this.SizeLabel = new RaidCrawler.Structures.RoundLabel();
            this.LabelNature = new RaidCrawler.Structures.RoundLabel();
            this.LabelGender = new RaidCrawler.Structures.RoundLabel();
            this.LabelSpecies = new RaidCrawler.Structures.RoundLabel();
            this.LabelTeraType = new RaidCrawler.Structures.RoundLabel();
            this.LabelDifficulty = new RaidCrawler.Structures.RoundLabel();
            this.LabelPID = new RaidCrawler.Structures.RoundLabel();
            this.LabelEC = new RaidCrawler.Structures.RoundLabel();
            this.LabelSeed = new RaidCrawler.Structures.RoundLabel();
            this.Area = new RaidCrawler.Structures.TransTextBox();
            this.Move4 = new RaidCrawler.Structures.TransTextBox();
            this.Move3 = new RaidCrawler.Structures.TransTextBox();
            this.Move2 = new RaidCrawler.Structures.TransTextBox();
            this.Move1 = new RaidCrawler.Structures.TransTextBox();
            this.SizeBox = new RaidCrawler.Structures.TransTextBox();
            this.Ability = new RaidCrawler.Structures.TransTextBox();
            this.IVs = new RaidCrawler.Structures.TransTextBox();
            this.Nature = new RaidCrawler.Structures.TransTextBox();
            this.Gender = new RaidCrawler.Structures.TransTextBox();
            this.Species = new RaidCrawler.Structures.TransTextBox();
            this.TeraType = new RaidCrawler.Structures.TransTextBox();
            this.Difficulty = new RaidCrawler.Structures.TransTextBox();
            this.PID = new RaidCrawler.Structures.TransTextBox();
            this.EC = new RaidCrawler.Structures.TransTextBox();
            this.Seed = new RaidCrawler.Structures.TransTextBox();
            this.myPanel2 = new RaidCrawler.Structures.MyPanel();
            this.ShinyCount = new System.Windows.Forms.Label();
            this.myPanel3 = new RaidCrawler.Structures.MyPanel();
            this.ConfigSettings = new CustomControls.RJControls.RJButton();
            this.SendScreenshot = new CustomControls.RJControls.RJButton();
            this.ButtonSceenState = new CustomControls.RJControls.RJButton();
            this.ButtonStopCrawl = new CustomControls.RJControls.RJButton();
            this.ButtonAdvanceDate = new CustomControls.RJControls.RJButton();
            this.StopFilter = new CustomControls.RJControls.RJButton();
            this.ButtonDownloadEvents = new CustomControls.RJControls.RJButton();
            this.ButtonViewRAM = new CustomControls.RJControls.RJButton();
            this.ButtonReadRaids = new CustomControls.RJControls.RJButton();
            this.ButtonDisconnect = new CustomControls.RJControls.RJButton();
            this.ButtonConnect = new CustomControls.RJControls.RJButton();
            this.myPanel4 = new RaidCrawler.Structures.MyPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Cheat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Sprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GemIcon)).BeginInit();
            this.panel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shinyBox)).BeginInit();
            this.myPanel2.SuspendLayout();
            this.myPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckEnableFilters
            // 
            this.CheckEnableFilters.AutoSize = true;
            this.CheckEnableFilters.Checked = true;
            this.CheckEnableFilters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckEnableFilters.Location = new System.Drawing.Point(110, 167);
            this.CheckEnableFilters.Name = "CheckEnableFilters";
            this.CheckEnableFilters.Size = new System.Drawing.Size(95, 19);
            this.CheckEnableFilters.TabIndex = 119;
            this.CheckEnableFilters.Text = "Enable Filters";
            this.toolTip.SetToolTip(this.CheckEnableFilters, "Enable Filters enables or disables all filters\r\nentirely.\r\n\r\nEnabled - Advance Da" +
        "te will continue until\r\na match occurs from a filter.\r\n\r\nDisabled - Advance Date" +
        " will only advance\r\none (1) day.");
            this.CheckEnableFilters.UseVisualStyleBackColor = true;
            // 
            // ConnectionStatusText
            // 
            this.ConnectionStatusText.AutoSize = true;
            this.ConnectionStatusText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConnectionStatusText.Location = new System.Drawing.Point(74, 34);
            this.ConnectionStatusText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ConnectionStatusText.Name = "ConnectionStatusText";
            this.ConnectionStatusText.Size = new System.Drawing.Size(83, 13);
            this.ConnectionStatusText.TabIndex = 9;
            this.ConnectionStatusText.Text = "Not connected";
            this.ConnectionStatusText.TextChanged += new System.EventHandler(this.ConnectionStatusText_TextChanged);
            // 
            // InputSwitchIP
            // 
            this.InputSwitchIP.Location = new System.Drawing.Point(74, 7);
            this.InputSwitchIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.InputSwitchIP.Name = "InputSwitchIP";
            this.InputSwitchIP.Size = new System.Drawing.Size(134, 23);
            this.InputSwitchIP.TabIndex = 8;
            this.InputSwitchIP.Text = "www.www.www.www";
            this.InputSwitchIP.TextChanged += new System.EventHandler(this.InputSwitchIP_Changed);
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(24, 32);
            this.LabelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(42, 15);
            this.LabelStatus.TabIndex = 7;
            this.LabelStatus.Text = "Status:";
            // 
            // LabelSwitchIP
            // 
            this.LabelSwitchIP.AutoSize = true;
            this.LabelSwitchIP.Location = new System.Drawing.Point(8, 10);
            this.LabelSwitchIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSwitchIP.Name = "LabelSwitchIP";
            this.LabelSwitchIP.Size = new System.Drawing.Size(58, 15);
            this.LabelSwitchIP.TabIndex = 6;
            this.LabelSwitchIP.Text = "Switch IP:";
            // 
            // LabelLoadedRaids
            // 
            this.LabelLoadedRaids.AutoSize = true;
            this.LabelLoadedRaids.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelLoadedRaids.Location = new System.Drawing.Point(3, 3);
            this.LabelLoadedRaids.Name = "LabelLoadedRaids";
            this.LabelLoadedRaids.Size = new System.Drawing.Size(107, 15);
            this.LabelLoadedRaids.TabIndex = 12;
            this.LabelLoadedRaids.Text = "Met Filters/Shiny: -";
            this.LabelLoadedRaids.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Progress
            // 
            this.Progress.FormattingEnabled = true;
            this.Progress.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.Progress.Location = new System.Drawing.Point(158, 256);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(48, 23);
            this.Progress.TabIndex = 87;
            this.Progress.Text = "w";
            this.Progress.SelectedIndexChanged += new System.EventHandler(this.Progress_SelectedIndexChanged);
            // 
            // LabelStoryProgress
            // 
            this.LabelStoryProgress.AutoSize = true;
            this.LabelStoryProgress.Location = new System.Drawing.Point(8, 259);
            this.LabelStoryProgress.Name = "LabelStoryProgress";
            this.LabelStoryProgress.Size = new System.Drawing.Size(115, 15);
            this.LabelStoryProgress.TabIndex = 88;
            this.LabelStoryProgress.Text = "Story Progress Level:";
            // 
            // LabelGame
            // 
            this.LabelGame.AutoSize = true;
            this.LabelGame.Location = new System.Drawing.Point(8, 232);
            this.LabelGame.Name = "LabelGame";
            this.LabelGame.Size = new System.Drawing.Size(41, 15);
            this.LabelGame.TabIndex = 100;
            this.LabelGame.Text = "Game:";
            // 
            // Game
            // 
            this.Game.FormattingEnabled = true;
            this.Game.Items.AddRange(new object[] {
            "Scarlet",
            "Violet"});
            this.Game.Location = new System.Drawing.Point(110, 229);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(96, 23);
            this.Game.TabIndex = 99;
            this.Game.Text = "w";
            this.Game.SelectedIndexChanged += new System.EventHandler(this.Game_SelectedIndexChanged);
            // 
            // LabelEventProgress
            // 
            this.LabelEventProgress.AutoSize = true;
            this.LabelEventProgress.Location = new System.Drawing.Point(8, 286);
            this.LabelEventProgress.Name = "LabelEventProgress";
            this.LabelEventProgress.Size = new System.Drawing.Size(117, 15);
            this.LabelEventProgress.TabIndex = 102;
            this.LabelEventProgress.Text = "Event Progress Level:";
            // 
            // EventProgress
            // 
            this.EventProgress.FormattingEnabled = true;
            this.EventProgress.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.EventProgress.Location = new System.Drawing.Point(158, 283);
            this.EventProgress.Name = "EventProgress";
            this.EventProgress.Size = new System.Drawing.Size(48, 23);
            this.EventProgress.TabIndex = 101;
            this.EventProgress.Text = "w";
            this.EventProgress.SelectedIndexChanged += new System.EventHandler(this.EventProgress_SelectedIndexChanged);
            // 
            // Sprite
            // 
            this.Sprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Sprite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sprite.Location = new System.Drawing.Point(279, 3);
            this.Sprite.Name = "Sprite";
            this.Sprite.Size = new System.Drawing.Size(68, 56);
            this.Sprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Sprite.TabIndex = 108;
            this.Sprite.TabStop = false;
            // 
            // GemIcon
            // 
            this.GemIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.GemIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GemIcon.Location = new System.Drawing.Point(233, 11);
            this.GemIcon.Name = "GemIcon";
            this.GemIcon.Size = new System.Drawing.Size(40, 40);
            this.GemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GemIcon.TabIndex = 111;
            this.GemIcon.TabStop = false;
            // 
            // LabelSandwichBonus
            // 
            this.LabelSandwichBonus.AutoSize = true;
            this.LabelSandwichBonus.Location = new System.Drawing.Point(8, 313);
            this.LabelSandwichBonus.Name = "LabelSandwichBonus";
            this.LabelSandwichBonus.Size = new System.Drawing.Size(120, 15);
            this.LabelSandwichBonus.TabIndex = 118;
            this.LabelSandwichBonus.Text = "Raid Sandwich Boost:";
            // 
            // RaidBoost
            // 
            this.RaidBoost.FormattingEnabled = true;
            this.RaidBoost.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.RaidBoost.Location = new System.Drawing.Point(158, 310);
            this.RaidBoost.Name = "RaidBoost";
            this.RaidBoost.Size = new System.Drawing.Size(48, 23);
            this.RaidBoost.TabIndex = 117;
            this.RaidBoost.Text = "w";
            this.RaidBoost.SelectedIndexChanged += new System.EventHandler(this.RaidBoost_SelectedIndexChanged);
            // 
            // ComboIndex
            // 
            this.ComboIndex.BackColor = System.Drawing.SystemColors.Window;
            this.ComboIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboIndex.FormattingEnabled = true;
            this.ComboIndex.Location = new System.Drawing.Point(38, 5);
            this.ComboIndex.Name = "ComboIndex";
            this.ComboIndex.Size = new System.Drawing.Size(175, 23);
            this.ComboIndex.TabIndex = 120;
            this.ComboIndex.SelectedIndexChanged += new System.EventHandler(this.ComboIndex_SelectedIndexChanged);
            // 
            // SearchTimer
            // 
            this.SearchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
            // 
            // SearchTime
            // 
            this.SearchTime.AutoSize = true;
            this.SearchTime.Location = new System.Drawing.Point(3, 20);
            this.SearchTime.Name = "SearchTime";
            this.SearchTime.Size = new System.Drawing.Size(82, 15);
            this.SearchTime.TabIndex = 122;
            this.SearchTime.Text = "Search Time: -";
            this.SearchTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DaySkipSuccessRate
            // 
            this.DaySkipSuccessRate.AutoSize = true;
            this.DaySkipSuccessRate.Location = new System.Drawing.Point(3, 37);
            this.DaySkipSuccessRate.Name = "DaySkipSuccessRate";
            this.DaySkipSuccessRate.Size = new System.Drawing.Size(205, 15);
            this.DaySkipSuccessRate.TabIndex = 123;
            this.DaySkipSuccessRate.Text = "Skip Rate: -     Total Miss: -      Streak: -";
            this.DaySkipSuccessRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.ButtonPrevious);
            this.panel2.Controls.Add(this.ButtonNext);
            this.panel2.Controls.Add(this.ComboIndex);
            this.panel2.Location = new System.Drawing.Point(274, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 33);
            this.panel2.TabIndex = 127;
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.ButtonPrevious.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonPrevious.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonPrevious.BorderRadius = 5;
            this.ButtonPrevious.BorderSize = 2;
            this.ButtonPrevious.Enabled = false;
            this.ButtonPrevious.FlatAppearance.BorderSize = 0;
            this.ButtonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrevious.ForeColor = System.Drawing.Color.Orange;
            this.ButtonPrevious.Location = new System.Drawing.Point(3, 3);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(33, 27);
            this.ButtonPrevious.TabIndex = 135;
            this.ButtonPrevious.Text = "<<";
            this.ButtonPrevious.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonPrevious.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonPrevious.UseVisualStyleBackColor = false;
            this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
            // 
            // ButtonNext
            // 
            this.ButtonNext.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonNext.BackColor = System.Drawing.Color.Transparent;
            this.ButtonNext.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonNext.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonNext.BorderRadius = 5;
            this.ButtonNext.BorderSize = 2;
            this.ButtonNext.Enabled = false;
            this.ButtonNext.FlatAppearance.BorderSize = 0;
            this.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonNext.ForeColor = System.Drawing.Color.Orange;
            this.ButtonNext.Location = new System.Drawing.Point(215, 3);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(33, 27);
            this.ButtonNext.TabIndex = 167;
            this.ButtonNext.Text = ">>";
            this.ButtonNext.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonNext.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonNext.UseVisualStyleBackColor = false;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // myPanel1
            // 
            this.myPanel1.back = System.Drawing.Color.Empty;
            this.myPanel1.BackColor = System.Drawing.Color.Transparent;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.Controls.Add(this.CopyAnnounce);
            this.myPanel1.Controls.Add(this.Rewards);
            this.myPanel1.Controls.Add(this.shinyBox);
            this.myPanel1.Controls.Add(this.LabelIsEvent);
            this.myPanel1.Controls.Add(this.LabelUNK_2);
            this.myPanel1.Controls.Add(this.LabelMoves);
            this.myPanel1.Controls.Add(this.LabelAbility);
            this.myPanel1.Controls.Add(this.LabelIVs);
            this.myPanel1.Controls.Add(this.SizeLabel);
            this.myPanel1.Controls.Add(this.LabelNature);
            this.myPanel1.Controls.Add(this.LabelGender);
            this.myPanel1.Controls.Add(this.LabelSpecies);
            this.myPanel1.Controls.Add(this.LabelTeraType);
            this.myPanel1.Controls.Add(this.LabelDifficulty);
            this.myPanel1.Controls.Add(this.LabelPID);
            this.myPanel1.Controls.Add(this.LabelEC);
            this.myPanel1.Controls.Add(this.LabelSeed);
            this.myPanel1.Controls.Add(this.Area);
            this.myPanel1.Controls.Add(this.Move4);
            this.myPanel1.Controls.Add(this.Move3);
            this.myPanel1.Controls.Add(this.Move2);
            this.myPanel1.Controls.Add(this.Move1);
            this.myPanel1.Controls.Add(this.SizeBox);
            this.myPanel1.Controls.Add(this.Ability);
            this.myPanel1.Controls.Add(this.IVs);
            this.myPanel1.Controls.Add(this.Nature);
            this.myPanel1.Controls.Add(this.Gender);
            this.myPanel1.Controls.Add(this.Species);
            this.myPanel1.Controls.Add(this.TeraType);
            this.myPanel1.Controls.Add(this.Difficulty);
            this.myPanel1.Controls.Add(this.PID);
            this.myPanel1.Controls.Add(this.EC);
            this.myPanel1.Controls.Add(this.Seed);
            this.myPanel1.Controls.Add(this.Sprite);
            this.myPanel1.Controls.Add(this.GemIcon);
            this.myPanel1.Location = new System.Drawing.Point(225, 40);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(350, 327);
            this.myPanel1.TabIndex = 130;
            // 
            // CopyAnnounce
            // 
            this.CopyAnnounce.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.CopyAnnounce.BackColor = System.Drawing.Color.Transparent;
            this.CopyAnnounce.BackgroundColor = System.Drawing.Color.Transparent;
            this.CopyAnnounce.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.CopyAnnounce.BorderRadius = 5;
            this.CopyAnnounce.BorderSize = 2;
            this.CopyAnnounce.Enabled = false;
            this.CopyAnnounce.FlatAppearance.BorderSize = 0;
            this.CopyAnnounce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyAnnounce.ForeColor = System.Drawing.Color.Orange;
            this.CopyAnnounce.Location = new System.Drawing.Point(271, 301);
            this.CopyAnnounce.Name = "CopyAnnounce";
            this.CopyAnnounce.Size = new System.Drawing.Size(76, 23);
            this.CopyAnnounce.TabIndex = 169;
            this.CopyAnnounce.Text = "Copy";
            this.CopyAnnounce.TextDisabledColor = System.Drawing.Color.Red;
            this.CopyAnnounce.TextEnabledColor = System.Drawing.Color.Purple;
            this.CopyAnnounce.UseVisualStyleBackColor = false;
            this.CopyAnnounce.Click += new System.EventHandler(this.CopyAnnounce_Click);
            // 
            // Rewards
            // 
            this.Rewards.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.Rewards.BackColor = System.Drawing.Color.Transparent;
            this.Rewards.BackgroundColor = System.Drawing.Color.Transparent;
            this.Rewards.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.Rewards.BorderRadius = 5;
            this.Rewards.BorderSize = 2;
            this.Rewards.Enabled = false;
            this.Rewards.FlatAppearance.BorderSize = 0;
            this.Rewards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Rewards.ForeColor = System.Drawing.Color.Orange;
            this.Rewards.Location = new System.Drawing.Point(77, 301);
            this.Rewards.Name = "Rewards";
            this.Rewards.Size = new System.Drawing.Size(76, 23);
            this.Rewards.TabIndex = 168;
            this.Rewards.Text = "Rewards";
            this.Rewards.TextDisabledColor = System.Drawing.Color.Red;
            this.Rewards.TextEnabledColor = System.Drawing.Color.Purple;
            this.Rewards.UseVisualStyleBackColor = false;
            this.Rewards.Click += new System.EventHandler(this.Rewards_Click);
            // 
            // shinyBox
            // 
            this.shinyBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.shinyBox.Location = new System.Drawing.Point(195, 15);
            this.shinyBox.Name = "shinyBox";
            this.shinyBox.Size = new System.Drawing.Size(32, 32);
            this.shinyBox.TabIndex = 166;
            this.shinyBox.TabStop = false;
            // 
            // LabelIsEvent
            // 
            this.LabelIsEvent.AutoSize = true;
            this.LabelIsEvent.back = System.Drawing.Color.Empty;
            this.LabelIsEvent.BackColor = System.Drawing.Color.Transparent;
            this.LabelIsEvent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LabelIsEvent.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LabelIsEvent.Location = new System.Drawing.Point(294, 65);
            this.LabelIsEvent.Name = "LabelIsEvent";
            this.LabelIsEvent.Size = new System.Drawing.Size(39, 15);
            this.LabelIsEvent.TabIndex = 165;
            this.LabelIsEvent.Text = "Event";
            this.LabelIsEvent.Visible = false;
            // 
            // LabelUNK_2
            // 
            this.LabelUNK_2.AutoSize = true;
            this.LabelUNK_2.back = System.Drawing.Color.Empty;
            this.LabelUNK_2.BackColor = System.Drawing.Color.Transparent;
            this.LabelUNK_2.Location = new System.Drawing.Point(36, 278);
            this.LabelUNK_2.Name = "LabelUNK_2";
            this.LabelUNK_2.Size = new System.Drawing.Size(34, 15);
            this.LabelUNK_2.TabIndex = 164;
            this.LabelUNK_2.Text = "Area:";
            // 
            // LabelMoves
            // 
            this.LabelMoves.AutoSize = true;
            this.LabelMoves.back = System.Drawing.Color.Empty;
            this.LabelMoves.BackColor = System.Drawing.Color.Transparent;
            this.LabelMoves.Location = new System.Drawing.Point(25, 236);
            this.LabelMoves.Name = "LabelMoves";
            this.LabelMoves.Size = new System.Drawing.Size(45, 15);
            this.LabelMoves.TabIndex = 163;
            this.LabelMoves.Text = "Moves:";
            // 
            // LabelAbility
            // 
            this.LabelAbility.AutoSize = true;
            this.LabelAbility.back = System.Drawing.Color.Empty;
            this.LabelAbility.BackColor = System.Drawing.Color.Transparent;
            this.LabelAbility.Location = new System.Drawing.Point(26, 197);
            this.LabelAbility.Name = "LabelAbility";
            this.LabelAbility.Size = new System.Drawing.Size(44, 15);
            this.LabelAbility.TabIndex = 162;
            this.LabelAbility.Text = "Ability:";
            // 
            // LabelIVs
            // 
            this.LabelIVs.AutoSize = true;
            this.LabelIVs.back = System.Drawing.Color.Empty;
            this.LabelIVs.BackColor = System.Drawing.Color.Transparent;
            this.LabelIVs.Location = new System.Drawing.Point(45, 170);
            this.LabelIVs.Name = "LabelIVs";
            this.LabelIVs.Size = new System.Drawing.Size(25, 15);
            this.LabelIVs.TabIndex = 161;
            this.LabelIVs.Text = "IVs:";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.back = System.Drawing.Color.Empty;
            this.SizeLabel.BackColor = System.Drawing.Color.Transparent;
            this.SizeLabel.Location = new System.Drawing.Point(243, 197);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(30, 15);
            this.SizeLabel.TabIndex = 160;
            this.SizeLabel.Text = "Size:";
            // 
            // LabelNature
            // 
            this.LabelNature.AutoSize = true;
            this.LabelNature.back = System.Drawing.Color.Empty;
            this.LabelNature.BackColor = System.Drawing.Color.Transparent;
            this.LabelNature.Location = new System.Drawing.Point(195, 143);
            this.LabelNature.Name = "LabelNature";
            this.LabelNature.Size = new System.Drawing.Size(46, 15);
            this.LabelNature.TabIndex = 159;
            this.LabelNature.Text = "Nature:";
            // 
            // LabelGender
            // 
            this.LabelGender.AutoSize = true;
            this.LabelGender.back = System.Drawing.Color.Empty;
            this.LabelGender.BackColor = System.Drawing.Color.Transparent;
            this.LabelGender.Location = new System.Drawing.Point(22, 143);
            this.LabelGender.Name = "LabelGender";
            this.LabelGender.Size = new System.Drawing.Size(48, 15);
            this.LabelGender.TabIndex = 158;
            this.LabelGender.Text = "Gender:";
            // 
            // LabelSpecies
            // 
            this.LabelSpecies.AutoSize = true;
            this.LabelSpecies.back = System.Drawing.Color.Empty;
            this.LabelSpecies.BackColor = System.Drawing.Color.Transparent;
            this.LabelSpecies.Location = new System.Drawing.Point(21, 116);
            this.LabelSpecies.Name = "LabelSpecies";
            this.LabelSpecies.Size = new System.Drawing.Size(49, 15);
            this.LabelSpecies.TabIndex = 157;
            this.LabelSpecies.Text = "Species:";
            // 
            // LabelTeraType
            // 
            this.LabelTeraType.AutoSize = true;
            this.LabelTeraType.back = System.Drawing.Color.Empty;
            this.LabelTeraType.BackColor = System.Drawing.Color.Transparent;
            this.LabelTeraType.Location = new System.Drawing.Point(183, 89);
            this.LabelTeraType.Name = "LabelTeraType";
            this.LabelTeraType.Size = new System.Drawing.Size(58, 15);
            this.LabelTeraType.TabIndex = 156;
            this.LabelTeraType.Text = "Tera Type:";
            // 
            // LabelDifficulty
            // 
            this.LabelDifficulty.AutoSize = true;
            this.LabelDifficulty.back = System.Drawing.Color.Empty;
            this.LabelDifficulty.BackColor = System.Drawing.Color.Transparent;
            this.LabelDifficulty.Location = new System.Drawing.Point(12, 89);
            this.LabelDifficulty.Name = "LabelDifficulty";
            this.LabelDifficulty.Size = new System.Drawing.Size(58, 15);
            this.LabelDifficulty.TabIndex = 155;
            this.LabelDifficulty.Text = "Difficulty:";
            // 
            // LabelPID
            // 
            this.LabelPID.AutoSize = true;
            this.LabelPID.back = System.Drawing.Color.Empty;
            this.LabelPID.BackColor = System.Drawing.Color.Transparent;
            this.LabelPID.Location = new System.Drawing.Point(42, 62);
            this.LabelPID.Name = "LabelPID";
            this.LabelPID.Size = new System.Drawing.Size(28, 15);
            this.LabelPID.TabIndex = 154;
            this.LabelPID.Text = "PID:";
            // 
            // LabelEC
            // 
            this.LabelEC.AutoSize = true;
            this.LabelEC.back = System.Drawing.Color.Empty;
            this.LabelEC.BackColor = System.Drawing.Color.Transparent;
            this.LabelEC.Location = new System.Drawing.Point(46, 35);
            this.LabelEC.Name = "LabelEC";
            this.LabelEC.Size = new System.Drawing.Size(24, 15);
            this.LabelEC.TabIndex = 153;
            this.LabelEC.Text = "EC:";
            // 
            // LabelSeed
            // 
            this.LabelSeed.AutoSize = true;
            this.LabelSeed.back = System.Drawing.Color.Empty;
            this.LabelSeed.BackColor = System.Drawing.Color.Transparent;
            this.LabelSeed.Location = new System.Drawing.Point(35, 8);
            this.LabelSeed.Name = "LabelSeed";
            this.LabelSeed.Size = new System.Drawing.Size(35, 15);
            this.LabelSeed.TabIndex = 152;
            this.LabelSeed.Text = "Seed:";
            // 
            // Area
            // 
            this.Area.BackColor = System.Drawing.Color.Transparent;
            this.Area.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Area.Location = new System.Drawing.Point(77, 274);
            this.Area.Name = "Area";
            this.Area.Size = new System.Drawing.Size(270, 23);
            this.Area.TabIndex = 151;
            this.Area.Click += new System.EventHandler(this.DisplayMap);
            // 
            // Move4
            // 
            this.Move4.BackColor = System.Drawing.Color.Transparent;
            this.Move4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move4.Location = new System.Drawing.Point(214, 247);
            this.Move4.Name = "Move4";
            this.Move4.Size = new System.Drawing.Size(133, 23);
            this.Move4.TabIndex = 150;
            this.Move4.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move3
            // 
            this.Move3.BackColor = System.Drawing.Color.Transparent;
            this.Move3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move3.Location = new System.Drawing.Point(77, 247);
            this.Move3.Name = "Move3";
            this.Move3.Size = new System.Drawing.Size(133, 23);
            this.Move3.TabIndex = 149;
            this.Move3.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move2
            // 
            this.Move2.BackColor = System.Drawing.Color.Transparent;
            this.Move2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move2.Location = new System.Drawing.Point(214, 220);
            this.Move2.Name = "Move2";
            this.Move2.Size = new System.Drawing.Size(133, 23);
            this.Move2.TabIndex = 148;
            this.Move2.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // Move1
            // 
            this.Move1.BackColor = System.Drawing.Color.Transparent;
            this.Move1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Move1.Location = new System.Drawing.Point(77, 220);
            this.Move1.Name = "Move1";
            this.Move1.Size = new System.Drawing.Size(133, 23);
            this.Move1.TabIndex = 147;
            this.Move1.Click += new System.EventHandler(this.Move_Clicked);
            // 
            // SizeBox
            // 
            this.SizeBox.BackColor = System.Drawing.Color.Transparent;
            this.SizeBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SizeBox.Location = new System.Drawing.Point(279, 193);
            this.SizeBox.Name = "SizeBox";
            this.SizeBox.Size = new System.Drawing.Size(68, 23);
            this.SizeBox.TabIndex = 146;
            // 
            // Ability
            // 
            this.Ability.BackColor = System.Drawing.Color.Transparent;
            this.Ability.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ability.Location = new System.Drawing.Point(77, 193);
            this.Ability.Name = "Ability";
            this.Ability.Size = new System.Drawing.Size(133, 23);
            this.Ability.TabIndex = 145;
            // 
            // IVs
            // 
            this.IVs.BackColor = System.Drawing.Color.Transparent;
            this.IVs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IVs.Location = new System.Drawing.Point(77, 166);
            this.IVs.Name = "IVs";
            this.IVs.Size = new System.Drawing.Size(270, 23);
            this.IVs.TabIndex = 144;
            // 
            // Nature
            // 
            this.Nature.BackColor = System.Drawing.Color.Transparent;
            this.Nature.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Nature.Location = new System.Drawing.Point(247, 139);
            this.Nature.Name = "Nature";
            this.Nature.Size = new System.Drawing.Size(100, 23);
            this.Nature.TabIndex = 143;
            // 
            // Gender
            // 
            this.Gender.BackColor = System.Drawing.Color.Transparent;
            this.Gender.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Gender.Location = new System.Drawing.Point(77, 139);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(100, 23);
            this.Gender.TabIndex = 142;
            // 
            // Species
            // 
            this.Species.BackColor = System.Drawing.Color.Transparent;
            this.Species.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Species.Location = new System.Drawing.Point(77, 112);
            this.Species.Name = "Species";
            this.Species.Size = new System.Drawing.Size(270, 23);
            this.Species.TabIndex = 141;
            // 
            // TeraType
            // 
            this.TeraType.BackColor = System.Drawing.Color.Transparent;
            this.TeraType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TeraType.Location = new System.Drawing.Point(247, 85);
            this.TeraType.Name = "TeraType";
            this.TeraType.Size = new System.Drawing.Size(100, 23);
            this.TeraType.TabIndex = 140;
            // 
            // Difficulty
            // 
            this.Difficulty.BackColor = System.Drawing.Color.Transparent;
            this.Difficulty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Difficulty.Location = new System.Drawing.Point(77, 85);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.Size = new System.Drawing.Size(100, 23);
            this.Difficulty.TabIndex = 139;
            // 
            // PID
            // 
            this.PID.BackColor = System.Drawing.Color.Transparent;
            this.PID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PID.Location = new System.Drawing.Point(77, 58);
            this.PID.Name = "PID";
            this.PID.Size = new System.Drawing.Size(100, 23);
            this.PID.TabIndex = 138;
            this.PID.Click += new System.EventHandler(this.PID_Clicked);
            // 
            // EC
            // 
            this.EC.BackColor = System.Drawing.Color.Transparent;
            this.EC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EC.Location = new System.Drawing.Point(77, 31);
            this.EC.Name = "EC";
            this.EC.Size = new System.Drawing.Size(100, 23);
            this.EC.TabIndex = 137;
            this.EC.Click += new System.EventHandler(this.EC_Clicked);
            // 
            // Seed
            // 
            this.Seed.BackColor = System.Drawing.Color.Transparent;
            this.Seed.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Seed.Location = new System.Drawing.Point(77, 4);
            this.Seed.Name = "Seed";
            this.Seed.Size = new System.Drawing.Size(100, 23);
            this.Seed.TabIndex = 136;
            this.Seed.Click += new System.EventHandler(this.Seed_Clicked);
            // 
            // myPanel2
            // 
            this.myPanel2.back = System.Drawing.Color.Empty;
            this.myPanel2.BackColor = System.Drawing.Color.Transparent;
            this.myPanel2.Controls.Add(this.ShinyCount);
            this.myPanel2.Controls.Add(this.LabelLoadedRaids);
            this.myPanel2.Controls.Add(this.DaySkipSuccessRate);
            this.myPanel2.Controls.Add(this.SearchTime);
            this.myPanel2.Location = new System.Drawing.Point(225, 373);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(350, 55);
            this.myPanel2.TabIndex = 131;
            // 
            // ShinyCount
            // 
            this.ShinyCount.AutoSize = true;
            this.ShinyCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ShinyCount.Location = new System.Drawing.Point(205, 3);
            this.ShinyCount.Name = "ShinyCount";
            this.ShinyCount.Size = new System.Drawing.Size(120, 15);
            this.ShinyCount.TabIndex = 124;
            this.ShinyCount.Text = "Total Shinies Found: -";
            this.ShinyCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // myPanel3
            // 
            this.myPanel3.back = System.Drawing.Color.Empty;
            this.myPanel3.BackColor = System.Drawing.Color.Transparent;
            this.myPanel3.Controls.Add(this.ConfigSettings);
            this.myPanel3.Controls.Add(this.SendScreenshot);
            this.myPanel3.Controls.Add(this.ButtonSceenState);
            this.myPanel3.Controls.Add(this.ButtonStopCrawl);
            this.myPanel3.Controls.Add(this.ButtonAdvanceDate);
            this.myPanel3.Controls.Add(this.StopFilter);
            this.myPanel3.Controls.Add(this.ButtonDownloadEvents);
            this.myPanel3.Controls.Add(this.ButtonViewRAM);
            this.myPanel3.Controls.Add(this.ButtonReadRaids);
            this.myPanel3.Controls.Add(this.ButtonDisconnect);
            this.myPanel3.Controls.Add(this.ButtonConnect);
            this.myPanel3.Controls.Add(this.LabelSwitchIP);
            this.myPanel3.Controls.Add(this.LabelStatus);
            this.myPanel3.Controls.Add(this.Game);
            this.myPanel3.Controls.Add(this.CheckEnableFilters);
            this.myPanel3.Controls.Add(this.LabelStoryProgress);
            this.myPanel3.Controls.Add(this.InputSwitchIP);
            this.myPanel3.Controls.Add(this.LabelGame);
            this.myPanel3.Controls.Add(this.LabelSandwichBonus);
            this.myPanel3.Controls.Add(this.Progress);
            this.myPanel3.Controls.Add(this.ConnectionStatusText);
            this.myPanel3.Controls.Add(this.EventProgress);
            this.myPanel3.Controls.Add(this.RaidBoost);
            this.myPanel3.Controls.Add(this.LabelEventProgress);
            this.myPanel3.Location = new System.Drawing.Point(7, 8);
            this.myPanel3.Name = "myPanel3";
            this.myPanel3.Size = new System.Drawing.Size(212, 420);
            this.myPanel3.TabIndex = 132;
            // 
            // ConfigSettings
            // 
            this.ConfigSettings.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ConfigSettings.BackColor = System.Drawing.Color.Transparent;
            this.ConfigSettings.BackgroundColor = System.Drawing.Color.Transparent;
            this.ConfigSettings.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ConfigSettings.BorderRadius = 5;
            this.ConfigSettings.BorderSize = 2;
            this.ConfigSettings.FlatAppearance.BorderSize = 0;
            this.ConfigSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConfigSettings.ForeColor = System.Drawing.Color.Orange;
            this.ConfigSettings.Location = new System.Drawing.Point(7, 384);
            this.ConfigSettings.Name = "ConfigSettings";
            this.ConfigSettings.Size = new System.Drawing.Size(199, 33);
            this.ConfigSettings.TabIndex = 134;
            this.ConfigSettings.Text = "Open Settings";
            this.ConfigSettings.TextDisabledColor = System.Drawing.Color.Red;
            this.ConfigSettings.TextEnabledColor = System.Drawing.Color.Purple;
            this.ConfigSettings.UseVisualStyleBackColor = false;
            this.ConfigSettings.Click += new System.EventHandler(this.ConfigSettings_Click);
            // 
            // SendScreenshot
            // 
            this.SendScreenshot.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.SendScreenshot.BackColor = System.Drawing.Color.Transparent;
            this.SendScreenshot.BackgroundColor = System.Drawing.Color.Transparent;
            this.SendScreenshot.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.SendScreenshot.BorderRadius = 5;
            this.SendScreenshot.BorderSize = 2;
            this.SendScreenshot.Enabled = false;
            this.SendScreenshot.FlatAppearance.BorderSize = 0;
            this.SendScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendScreenshot.ForeColor = System.Drawing.Color.Orange;
            this.SendScreenshot.Location = new System.Drawing.Point(7, 360);
            this.SendScreenshot.Name = "SendScreenshot";
            this.SendScreenshot.Size = new System.Drawing.Size(199, 23);
            this.SendScreenshot.TabIndex = 133;
            this.SendScreenshot.Text = "Screenshot Switch";
            this.SendScreenshot.TextDisabledColor = System.Drawing.Color.Red;
            this.SendScreenshot.TextEnabledColor = System.Drawing.Color.Purple;
            this.SendScreenshot.UseVisualStyleBackColor = false;
            this.SendScreenshot.Click += new System.EventHandler(this.SendScreenshot_Click);
            // 
            // ButtonSceenState
            // 
            this.ButtonSceenState.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonSceenState.BackColor = System.Drawing.Color.Transparent;
            this.ButtonSceenState.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonSceenState.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonSceenState.BorderRadius = 5;
            this.ButtonSceenState.BorderSize = 2;
            this.ButtonSceenState.Enabled = false;
            this.ButtonSceenState.FlatAppearance.BorderSize = 0;
            this.ButtonSceenState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSceenState.ForeColor = System.Drawing.Color.Orange;
            this.ButtonSceenState.Location = new System.Drawing.Point(7, 336);
            this.ButtonSceenState.Name = "ButtonSceenState";
            this.ButtonSceenState.Size = new System.Drawing.Size(199, 23);
            this.ButtonSceenState.TabIndex = 132;
            this.ButtonSceenState.Text = "Screen Off";
            this.ButtonSceenState.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonSceenState.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonSceenState.UseVisualStyleBackColor = false;
            this.ButtonSceenState.Click += new System.EventHandler(this.ButtonSceenState_Click);
            // 
            // ButtonStopCrawl
            // 
            this.ButtonStopCrawl.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonStopCrawl.BackColor = System.Drawing.Color.Transparent;
            this.ButtonStopCrawl.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonStopCrawl.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonStopCrawl.BorderRadius = 5;
            this.ButtonStopCrawl.BorderSize = 2;
            this.ButtonStopCrawl.Enabled = false;
            this.ButtonStopCrawl.FlatAppearance.BorderSize = 0;
            this.ButtonStopCrawl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonStopCrawl.ForeColor = System.Drawing.Color.Orange;
            this.ButtonStopCrawl.Location = new System.Drawing.Point(108, 189);
            this.ButtonStopCrawl.Name = "ButtonStopCrawl";
            this.ButtonStopCrawl.Size = new System.Drawing.Size(98, 33);
            this.ButtonStopCrawl.TabIndex = 131;
            this.ButtonStopCrawl.Text = "Stop";
            this.ButtonStopCrawl.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonStopCrawl.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonStopCrawl.UseVisualStyleBackColor = false;
            this.ButtonStopCrawl.Click += new System.EventHandler(this.ButtonStopCrawl_Click);
            // 
            // ButtonAdvanceDate
            // 
            this.ButtonAdvanceDate.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonAdvanceDate.BackColor = System.Drawing.Color.Transparent;
            this.ButtonAdvanceDate.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonAdvanceDate.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonAdvanceDate.BorderRadius = 5;
            this.ButtonAdvanceDate.BorderSize = 2;
            this.ButtonAdvanceDate.Enabled = false;
            this.ButtonAdvanceDate.FlatAppearance.BorderSize = 0;
            this.ButtonAdvanceDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAdvanceDate.ForeColor = System.Drawing.Color.Orange;
            this.ButtonAdvanceDate.Location = new System.Drawing.Point(7, 189);
            this.ButtonAdvanceDate.Name = "ButtonAdvanceDate";
            this.ButtonAdvanceDate.Size = new System.Drawing.Size(98, 33);
            this.ButtonAdvanceDate.TabIndex = 130;
            this.ButtonAdvanceDate.Text = "Advance Date";
            this.ButtonAdvanceDate.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonAdvanceDate.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonAdvanceDate.UseVisualStyleBackColor = false;
            this.ButtonAdvanceDate.Click += new System.EventHandler(this.ButtonAdvanceDate_Click);
            // 
            // StopFilter
            // 
            this.StopFilter.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.StopFilter.BackColor = System.Drawing.Color.Transparent;
            this.StopFilter.BackgroundColor = System.Drawing.Color.Transparent;
            this.StopFilter.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.StopFilter.BorderRadius = 5;
            this.StopFilter.BorderSize = 2;
            this.StopFilter.FlatAppearance.BorderSize = 0;
            this.StopFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopFilter.ForeColor = System.Drawing.Color.Orange;
            this.StopFilter.Location = new System.Drawing.Point(7, 160);
            this.StopFilter.Name = "StopFilter";
            this.StopFilter.Size = new System.Drawing.Size(98, 23);
            this.StopFilter.TabIndex = 129;
            this.StopFilter.Text = "Edit Filters";
            this.StopFilter.TextDisabledColor = System.Drawing.Color.Red;
            this.StopFilter.TextEnabledColor = System.Drawing.Color.Purple;
            this.StopFilter.UseVisualStyleBackColor = false;
            this.StopFilter.Click += new System.EventHandler(this.StopFilter_Click);
            // 
            // ButtonDownloadEvents
            // 
            this.ButtonDownloadEvents.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonDownloadEvents.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDownloadEvents.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonDownloadEvents.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonDownloadEvents.BorderRadius = 5;
            this.ButtonDownloadEvents.BorderSize = 2;
            this.ButtonDownloadEvents.Enabled = false;
            this.ButtonDownloadEvents.FlatAppearance.BorderSize = 0;
            this.ButtonDownloadEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDownloadEvents.ForeColor = System.Drawing.Color.Orange;
            this.ButtonDownloadEvents.Location = new System.Drawing.Point(108, 106);
            this.ButtonDownloadEvents.Name = "ButtonDownloadEvents";
            this.ButtonDownloadEvents.Size = new System.Drawing.Size(98, 23);
            this.ButtonDownloadEvents.TabIndex = 128;
            this.ButtonDownloadEvents.Text = "Pull Events";
            this.ButtonDownloadEvents.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonDownloadEvents.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonDownloadEvents.UseVisualStyleBackColor = false;
            this.ButtonDownloadEvents.Click += new System.EventHandler(this.DownloadEvents_Click);
            // 
            // ButtonViewRAM
            // 
            this.ButtonViewRAM.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonViewRAM.BackColor = System.Drawing.Color.Transparent;
            this.ButtonViewRAM.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonViewRAM.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonViewRAM.BorderRadius = 5;
            this.ButtonViewRAM.BorderSize = 2;
            this.ButtonViewRAM.Enabled = false;
            this.ButtonViewRAM.FlatAppearance.BorderSize = 0;
            this.ButtonViewRAM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonViewRAM.ForeColor = System.Drawing.Color.Orange;
            this.ButtonViewRAM.Location = new System.Drawing.Point(108, 83);
            this.ButtonViewRAM.Name = "ButtonViewRAM";
            this.ButtonViewRAM.Size = new System.Drawing.Size(98, 23);
            this.ButtonViewRAM.TabIndex = 127;
            this.ButtonViewRAM.Text = "Dump Raid";
            this.ButtonViewRAM.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonViewRAM.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonViewRAM.UseVisualStyleBackColor = false;
            this.ButtonViewRAM.Click += new System.EventHandler(this.ViewRAM_Click);
            // 
            // ButtonReadRaids
            // 
            this.ButtonReadRaids.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonReadRaids.BackColor = System.Drawing.Color.Transparent;
            this.ButtonReadRaids.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonReadRaids.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonReadRaids.BorderRadius = 5;
            this.ButtonReadRaids.BorderSize = 2;
            this.ButtonReadRaids.Enabled = false;
            this.ButtonReadRaids.FlatAppearance.BorderSize = 0;
            this.ButtonReadRaids.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonReadRaids.ForeColor = System.Drawing.Color.Orange;
            this.ButtonReadRaids.Location = new System.Drawing.Point(7, 89);
            this.ButtonReadRaids.Name = "ButtonReadRaids";
            this.ButtonReadRaids.Size = new System.Drawing.Size(98, 33);
            this.ButtonReadRaids.TabIndex = 126;
            this.ButtonReadRaids.Text = "Read Raids";
            this.ButtonReadRaids.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonReadRaids.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonReadRaids.UseVisualStyleBackColor = false;
            this.ButtonReadRaids.Click += new System.EventHandler(this.ButtonReadRaids_Click);
            // 
            // ButtonDisconnect
            // 
            this.ButtonDisconnect.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDisconnect.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonDisconnect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonDisconnect.BorderRadius = 5;
            this.ButtonDisconnect.BorderSize = 2;
            this.ButtonDisconnect.Enabled = false;
            this.ButtonDisconnect.FlatAppearance.BorderSize = 0;
            this.ButtonDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDisconnect.ForeColor = System.Drawing.Color.Orange;
            this.ButtonDisconnect.Location = new System.Drawing.Point(108, 50);
            this.ButtonDisconnect.Name = "ButtonDisconnect";
            this.ButtonDisconnect.Size = new System.Drawing.Size(98, 33);
            this.ButtonDisconnect.TabIndex = 125;
            this.ButtonDisconnect.Text = "Disconnect";
            this.ButtonDisconnect.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonDisconnect.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonDisconnect.UseVisualStyleBackColor = false;
            this.ButtonDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // ButtonConnect
            // 
            this.ButtonConnect.back = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.ButtonConnect.BackColor = System.Drawing.Color.Transparent;
            this.ButtonConnect.BackgroundColor = System.Drawing.Color.Transparent;
            this.ButtonConnect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ButtonConnect.BorderRadius = 5;
            this.ButtonConnect.BorderSize = 2;
            this.ButtonConnect.FlatAppearance.BorderSize = 0;
            this.ButtonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonConnect.ForeColor = System.Drawing.Color.Orange;
            this.ButtonConnect.Location = new System.Drawing.Point(7, 50);
            this.ButtonConnect.Name = "ButtonConnect";
            this.ButtonConnect.Size = new System.Drawing.Size(98, 33);
            this.ButtonConnect.TabIndex = 124;
            this.ButtonConnect.Text = "Connect";
            this.ButtonConnect.TextDisabledColor = System.Drawing.Color.Red;
            this.ButtonConnect.TextEnabledColor = System.Drawing.Color.Purple;
            this.ButtonConnect.UseVisualStyleBackColor = false;
            this.ButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // myPanel4
            // 
            this.myPanel4.back = System.Drawing.Color.Empty;
            this.myPanel4.Location = new System.Drawing.Point(381, 188);
            this.myPanel4.Name = "myPanel4";
            this.myPanel4.Size = new System.Drawing.Size(200, 100);
            this.myPanel4.TabIndex = 128;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(225, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(43, 33);
            this.panel1.TabIndex = 133;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.Cheat);
            this.panel3.Location = new System.Drawing.Point(532, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(43, 33);
            this.panel3.TabIndex = 134;
            // 
            // Cheat
            // 
            this.Cheat.AutoSize = true;
            this.Cheat.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Cheat.ForeColor = System.Drawing.SystemColors.Control;
            this.Cheat.Location = new System.Drawing.Point(-2, 2);
            this.Cheat.Name = "Cheat";
            this.Cheat.Size = new System.Drawing.Size(42, 30);
            this.Cheat.TabIndex = 0;
            this.Cheat.Text = "👾";
            this.Cheat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Cheat.Click += new System.EventHandler(this.Cheat_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(582, 435);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.myPanel3);
            this.Controls.Add(this.myPanel2);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Sprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GemIcon)).EndInit();
            this.panel2.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shinyBox)).EndInit();
            this.myPanel2.ResumeLayout(false);
            this.myPanel2.PerformLayout();
            this.myPanel3.ResumeLayout(false);
            this.myPanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolTip toolTip;
        private Label ConnectionStatusText;
        private TextBox InputSwitchIP;
        private Label LabelStatus;
        private Label LabelSwitchIP;
        private Label LabelLoadedRaids;
        private ComboBox Progress;
        private Label LabelStoryProgress;
        private Label LabelGame;
        private ComboBox Game;
        private Label LabelEventProgress;
        private ComboBox EventProgress;
        private PictureBox Sprite;
        private PictureBox GemIcon;
        private Label LabelSandwichBonus;
        private ComboBox RaidBoost;
        private CheckBox CheckEnableFilters;
        private ComboBox ComboIndex;
        private System.Windows.Forms.Timer SearchTimer;
        private Label SearchTime;
        private int DaySkipTries = 0;
        private int DaySkipSuccess = 0;
        private int DaySkipStreak = 0;
        private int DaySkipShiny = 0;
        private Label DaySkipSuccessRate;
        private Panel panel2;
        private Structures.MyPanel myPanel1;
        private Structures.MyPanel myPanel2;
        private Structures.MyPanel myPanel3;
        private Structures.MyPanel myPanel4;
        private Panel panel1;
        private Panel panel3;
        private Label ShinyCount;
        private Structures.TransTextBox Seed;
        private Structures.TransTextBox Difficulty;
        private Structures.TransTextBox PID;
        private Structures.TransTextBox EC;
        private Structures.TransTextBox TeraType;
        private Structures.TransTextBox Area;
        private Structures.TransTextBox Move4;
        private Structures.TransTextBox Move3;
        private Structures.TransTextBox Move2;
        private Structures.TransTextBox Move1;
        private Structures.TransTextBox SizeBox;
        private Structures.TransTextBox Ability;
        private Structures.TransTextBox IVs;
        private Structures.TransTextBox Nature;
        private Structures.TransTextBox Gender;
        private Structures.TransTextBox Species;
        private Structures.RoundLabel LabelSeed;
        private Structures.RoundLabel LabelUNK_2;
        private Structures.RoundLabel LabelMoves;
        private Structures.RoundLabel LabelAbility;
        private Structures.RoundLabel LabelIVs;
        private Structures.RoundLabel SizeLabel;
        private Structures.RoundLabel LabelNature;
        private Structures.RoundLabel LabelGender;
        private Structures.RoundLabel LabelSpecies;
        private Structures.RoundLabel LabelTeraType;
        private Structures.RoundLabel LabelDifficulty;
        private Structures.RoundLabel LabelPID;
        private Structures.RoundLabel LabelEC;
        private Structures.RoundLabel LabelIsEvent;
        private Label Cheat;
        private PictureBox shinyBox;
        private CustomControls.RJControls.RJButton ButtonConnect;
        private CustomControls.RJControls.RJButton ButtonPrevious;
        private CustomControls.RJControls.RJButton ButtonNext;
        private CustomControls.RJControls.RJButton CopyAnnounce;
        private CustomControls.RJControls.RJButton Rewards;
        private CustomControls.RJControls.RJButton ConfigSettings;
        private CustomControls.RJControls.RJButton SendScreenshot;
        private CustomControls.RJControls.RJButton ButtonSceenState;
        private CustomControls.RJControls.RJButton ButtonStopCrawl;
        private CustomControls.RJControls.RJButton ButtonAdvanceDate;
        private CustomControls.RJControls.RJButton StopFilter;
        private CustomControls.RJControls.RJButton ButtonDownloadEvents;
        private CustomControls.RJControls.RJButton ButtonViewRAM;
        private CustomControls.RJControls.RJButton ButtonReadRaids;
        private CustomControls.RJControls.RJButton ButtonDisconnect;
    }
}