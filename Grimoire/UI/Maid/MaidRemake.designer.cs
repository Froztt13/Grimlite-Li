namespace Grimoire.UI.Maid
{
	partial class MaidRemake
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaidRemake));
			this.trgtUsrnmLabel = new DarkUI.Controls.DarkLabel();
			this.cbEnablePlugin = new DarkUI.Controls.DarkCheckBox();
			this.tbSkillList = new DarkUI.Controls.DarkTextBox();
			this.label4 = new DarkUI.Controls.DarkLabel();
			this.gbOptions = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel5 = new DarkUI.Controls.DarkLabel();
			this.cmbUltraBoss = new DarkUI.Controls.DarkComboBox();
			this.lblWhitelistMap = new System.Windows.Forms.LinkLabel();
			this.cbWhitelistMap = new DarkUI.Controls.DarkCheckBox();
			this.lblLockedMapSetting = new System.Windows.Forms.LinkLabel();
			this.cbHandleLockedMap = new DarkUI.Controls.DarkCheckBox();
			this.cmbPreset = new DarkUI.Controls.DarkComboBox();
			this.label2 = new DarkUI.Controls.DarkLabel();
			this.cbStopIf = new DarkUI.Controls.DarkCheckBox();
			this.label1 = new DarkUI.Controls.DarkLabel();
			this.numRelogDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.btnLoad = new DarkUI.Controls.DarkButton();
			this.cbUnfollow = new DarkUI.Controls.DarkCheckBox();
			this.cbStopAttack = new DarkUI.Controls.DarkCheckBox();
			this.cbEnableGlobalHotkey = new DarkUI.Controls.DarkCheckBox();
			this.gbAdvancedOptions = new DarkUI.Controls.DarkGroupBox();
			this.cbAntiCounter = new DarkUI.Controls.DarkCheckBox();
			this.cbCopyWalk = new DarkUI.Controls.DarkCheckBox();
			this.numHealthPercent = new DarkUI.Controls.DarkNumericUpDown();
			this.lbUseHeal2 = new DarkUI.Controls.DarkLabel();
			this.tbHealSkill = new DarkUI.Controls.DarkTextBox();
			this.cbUseHeal = new DarkUI.Controls.DarkCheckBox();
			this.tbAttPriority = new DarkUI.Controls.DarkTextBox();
			this.cbAttackPriority = new DarkUI.Controls.DarkCheckBox();
			this.tbBuffSkill = new DarkUI.Controls.DarkTextBox();
			this.cbBuffIfStop = new DarkUI.Controls.DarkCheckBox();
			this.lbStopAttackBg = new System.Windows.Forms.Label();
			this.cbPartyCmd = new DarkUI.Controls.DarkCheckBox();
			this.timerStopAttack = new System.Windows.Forms.Timer(this.components);
			this.cmbGotoUsername = new System.Windows.Forms.ComboBox();
			this.cbWaitSkill = new DarkUI.Controls.DarkCheckBox();
			this.numSkillDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			this.gbActivation = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.gbConfig = new DarkUI.Controls.DarkGroupBox();
			this.tbSpecialMsg = new DarkUI.Controls.DarkTextBox();
			this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.cbSpecialAnims = new DarkUI.Controls.DarkCheckBox();
			this.numSkillAct = new DarkUI.Controls.DarkNumericUpDown();
			this.btnMe = new DarkUI.Controls.DarkButton();
			this.gbOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).BeginInit();
			this.gbAdvancedOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numHealthPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillDelay)).BeginInit();
			this.gbActivation.SuspendLayout();
			this.gbConfig.SuspendLayout();
			this.darkGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkillAct)).BeginInit();
			this.SuspendLayout();
			// 
			// trgtUsrnmLabel
			// 
			this.trgtUsrnmLabel.AutoSize = true;
			this.trgtUsrnmLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.trgtUsrnmLabel.Location = new System.Drawing.Point(24, 18);
			this.trgtUsrnmLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.trgtUsrnmLabel.Name = "trgtUsrnmLabel";
			this.trgtUsrnmLabel.Size = new System.Drawing.Size(127, 20);
			this.trgtUsrnmLabel.TabIndex = 4;
			this.trgtUsrnmLabel.Text = "Goto Username:";
			// 
			// cbEnablePlugin
			// 
			this.cbEnablePlugin.AutoSize = true;
			this.cbEnablePlugin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbEnablePlugin.Location = new System.Drawing.Point(22, 32);
			this.cbEnablePlugin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbEnablePlugin.Name = "cbEnablePlugin";
			this.cbEnablePlugin.Size = new System.Drawing.Size(92, 24);
			this.cbEnablePlugin.TabIndex = 7;
			this.cbEnablePlugin.Text = "Enable";
			this.cbEnablePlugin.CheckedChanged += new System.EventHandler(this.cbEnablePlugin_CheckedChanged);
			// 
			// tbSkillList
			// 
			this.tbSkillList.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbSkillList.Location = new System.Drawing.Point(28, 103);
			this.tbSkillList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbSkillList.Multiline = true;
			this.tbSkillList.Name = "tbSkillList";
			this.tbSkillList.Size = new System.Drawing.Size(138, 33);
			this.tbSkillList.TabIndex = 12;
			this.tbSkillList.Text = "1,2,3,4";
			this.tbSkillList.WordWrap = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label4.Location = new System.Drawing.Point(24, 80);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 20);
			this.label4.TabIndex = 13;
			this.label4.Text = "Skill List:";
			// 
			// gbOptions
			// 
			this.gbOptions.Controls.Add(this.darkLabel5);
			this.gbOptions.Controls.Add(this.cmbUltraBoss);
			this.gbOptions.Controls.Add(this.lblWhitelistMap);
			this.gbOptions.Controls.Add(this.cbWhitelistMap);
			this.gbOptions.Controls.Add(this.lblLockedMapSetting);
			this.gbOptions.Controls.Add(this.cbHandleLockedMap);
			this.gbOptions.Controls.Add(this.cmbPreset);
			this.gbOptions.Controls.Add(this.label2);
			this.gbOptions.Controls.Add(this.cbStopIf);
			this.gbOptions.Controls.Add(this.label1);
			this.gbOptions.Controls.Add(this.numRelogDelay);
			this.gbOptions.Location = new System.Drawing.Point(272, 18);
			this.gbOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbOptions.Name = "gbOptions";
			this.gbOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbOptions.Size = new System.Drawing.Size(237, 262);
			this.gbOptions.TabIndex = 15;
			this.gbOptions.TabStop = false;
			this.gbOptions.Text = "Options";
			// 
			// darkLabel5
			// 
			this.darkLabel5.AutoSize = true;
			this.darkLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel5.Location = new System.Drawing.Point(12, 197);
			this.darkLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel5.Name = "darkLabel5";
			this.darkLabel5.Size = new System.Drawing.Size(147, 20);
			this.darkLabel5.TabIndex = 30;
			this.darkLabel5.Text = "Ultra Boss w/ Extra:";
			// 
			// cmbUltraBoss
			// 
			this.cmbUltraBoss.FormattingEnabled = true;
			this.cmbUltraBoss.ItemHeight = 27;
			this.cmbUltraBoss.Items.AddRange(new object[] {
            "None",
            "Asc.Solstice P1",
            "Asc.Solstice P2",
            "Asc.Midnight P1",
            "Asc.Midnight P2",
            "Ast.Empyrean P1",
            "Ast.Empyrean P2",
            "Gramiel L1",
            "Gramiel L2",
            "Gramiel R1",
            "Gramiel R2"});
			this.cmbUltraBoss.Location = new System.Drawing.Point(18, 220);
			this.cmbUltraBoss.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbUltraBoss.Name = "cmbUltraBoss";
			this.cmbUltraBoss.Size = new System.Drawing.Size(208, 33);
			this.cmbUltraBoss.TabIndex = 29;
			this.cmbUltraBoss.SelectedIndexChanged += new System.EventHandler(this.cmbUltraBoss_SelectedIndexChanged);
			// 
			// lblWhitelistMap
			// 
			this.lblWhitelistMap.AutoSize = true;
			this.lblWhitelistMap.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblWhitelistMap.Location = new System.Drawing.Point(44, 95);
			this.lblWhitelistMap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblWhitelistMap.Name = "lblWhitelistMap";
			this.lblWhitelistMap.Size = new System.Drawing.Size(104, 20);
			this.lblWhitelistMap.TabIndex = 28;
			this.lblWhitelistMap.TabStop = true;
			this.lblWhitelistMap.Text = "Whitelist Map";
			this.lblWhitelistMap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblWhitelistMap_LinkClicked);
			// 
			// cbWhitelistMap
			// 
			this.cbWhitelistMap.AutoSize = true;
			this.cbWhitelistMap.Location = new System.Drawing.Point(18, 95);
			this.cbWhitelistMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbWhitelistMap.Name = "cbWhitelistMap";
			this.cbWhitelistMap.Size = new System.Drawing.Size(22, 21);
			this.cbWhitelistMap.TabIndex = 27;
			// 
			// lblLockedMapSetting
			// 
			this.lblLockedMapSetting.AutoSize = true;
			this.lblLockedMapSetting.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblLockedMapSetting.Location = new System.Drawing.Point(44, 65);
			this.lblLockedMapSetting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLockedMapSetting.Name = "lblLockedMapSetting";
			this.lblLockedMapSetting.Size = new System.Drawing.Size(162, 20);
			this.lblLockedMapSetting.TabIndex = 23;
			this.lblLockedMapSetting.TabStop = true;
			this.lblLockedMapSetting.Text = "Locked Zone Handler";
			this.lblLockedMapSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLockedMapSetting_LinkClicked);
			// 
			// cbHandleLockedMap
			// 
			this.cbHandleLockedMap.AutoSize = true;
			this.cbHandleLockedMap.Location = new System.Drawing.Point(18, 65);
			this.cbHandleLockedMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbHandleLockedMap.Name = "cbHandleLockedMap";
			this.cbHandleLockedMap.Size = new System.Drawing.Size(22, 21);
			this.cbHandleLockedMap.TabIndex = 22;
			// 
			// cmbPreset
			// 
			this.cmbPreset.FormattingEnabled = true;
			this.cmbPreset.ItemHeight = 27;
			this.cmbPreset.Items.AddRange(new object[] {
            "LR",
            "LC",
            "LOO",
            "SC",
            "AP",
            "AI",
            "AM",
            "CCMD",
            "SSOT",
            "NCM",
            "TK",
            "CSH",
            "CSH v2"});
			this.cmbPreset.Location = new System.Drawing.Point(123, 157);
			this.cmbPreset.Name = "cmbPreset";
			this.cmbPreset.Size = new System.Drawing.Size(100, 33);
			this.cmbPreset.TabIndex = 21;
			this.cmbPreset.SelectedIndexChanged += new System.EventHandler(this.cmbPreset_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label2.Location = new System.Drawing.Point(122, 131);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 20);
			this.label2.TabIndex = 20;
			this.label2.Text = "Skill Preset:";
			// 
			// cbStopIf
			// 
			this.cbStopIf.AutoSize = true;
			this.cbStopIf.Location = new System.Drawing.Point(18, 31);
			this.cbStopIf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbStopIf.Name = "cbStopIf";
			this.cbStopIf.Size = new System.Drawing.Size(210, 24);
			this.cbStopIf.TabIndex = 19;
			this.cbStopIf.Text = "Stop goto after 5 failures";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label1.Location = new System.Drawing.Point(14, 131);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 20);
			this.label1.TabIndex = 16;
			this.label1.Text = "Relog Delay:";
			// 
			// numRelogDelay
			// 
			this.numRelogDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numRelogDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numRelogDelay.Location = new System.Drawing.Point(18, 159);
			this.numRelogDelay.LoopValues = false;
			this.numRelogDelay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numRelogDelay.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
			this.numRelogDelay.Name = "numRelogDelay";
			this.numRelogDelay.Size = new System.Drawing.Size(93, 28);
			this.numRelogDelay.TabIndex = 15;
			this.numRelogDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			// 
			// btnSave
			// 
			this.btnSave.Checked = false;
			this.btnSave.Location = new System.Drawing.Point(128, 29);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(93, 35);
			this.btnSave.TabIndex = 25;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Checked = false;
			this.btnLoad.Location = new System.Drawing.Point(16, 29);
			this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(93, 35);
			this.btnLoad.TabIndex = 24;
			this.btnLoad.Text = "Load";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// cbUnfollow
			// 
			this.cbUnfollow.AutoSize = true;
			this.cbUnfollow.Location = new System.Drawing.Point(184, 25);
			this.cbUnfollow.Name = "cbUnfollow";
			this.cbUnfollow.Size = new System.Drawing.Size(122, 24);
			this.cbUnfollow.TabIndex = 16;
			this.cbUnfollow.Text = "Unfollow (R)";
			this.cbUnfollow.CheckedChanged += new System.EventHandler(this.cbLockCell_CheckedChanged);
			// 
			// cbStopAttack
			// 
			this.cbStopAttack.AutoSize = true;
			this.cbStopAttack.Location = new System.Drawing.Point(332, 26);
			this.cbStopAttack.Name = "cbStopAttack";
			this.cbStopAttack.Size = new System.Drawing.Size(138, 24);
			this.cbStopAttack.TabIndex = 17;
			this.cbStopAttack.Text = "StopAttack (T)";
			this.cbStopAttack.CheckedChanged += new System.EventHandler(this.cbStopAttack_CheckedChanged);
			// 
			// cbEnableGlobalHotkey
			// 
			this.cbEnableGlobalHotkey.AutoSize = true;
			this.cbEnableGlobalHotkey.Location = new System.Drawing.Point(10, 25);
			this.cbEnableGlobalHotkey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbEnableGlobalHotkey.Name = "cbEnableGlobalHotkey";
			this.cbEnableGlobalHotkey.Size = new System.Drawing.Size(135, 24);
			this.cbEnableGlobalHotkey.TabIndex = 18;
			this.cbEnableGlobalHotkey.Text = "Global Hotkey";
			this.cbEnableGlobalHotkey.CheckedChanged += new System.EventHandler(this.cbEnableGlobalHotkey_CheckedChanged);
			// 
			// gbAdvancedOptions
			// 
			this.gbAdvancedOptions.Controls.Add(this.cbAntiCounter);
			this.gbAdvancedOptions.Controls.Add(this.cbCopyWalk);
			this.gbAdvancedOptions.Controls.Add(this.numHealthPercent);
			this.gbAdvancedOptions.Controls.Add(this.lbUseHeal2);
			this.gbAdvancedOptions.Controls.Add(this.tbHealSkill);
			this.gbAdvancedOptions.Controls.Add(this.cbUseHeal);
			this.gbAdvancedOptions.Controls.Add(this.tbAttPriority);
			this.gbAdvancedOptions.Controls.Add(this.cbAttackPriority);
			this.gbAdvancedOptions.Controls.Add(this.tbBuffSkill);
			this.gbAdvancedOptions.Controls.Add(this.cbBuffIfStop);
			this.gbAdvancedOptions.Controls.Add(this.cbEnableGlobalHotkey);
			this.gbAdvancedOptions.Controls.Add(this.cbStopAttack);
			this.gbAdvancedOptions.Controls.Add(this.cbUnfollow);
			this.gbAdvancedOptions.Controls.Add(this.lbStopAttackBg);
			this.gbAdvancedOptions.Location = new System.Drawing.Point(18, 374);
			this.gbAdvancedOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbAdvancedOptions.Name = "gbAdvancedOptions";
			this.gbAdvancedOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbAdvancedOptions.Size = new System.Drawing.Size(490, 172);
			this.gbAdvancedOptions.TabIndex = 19;
			this.gbAdvancedOptions.TabStop = false;
			this.gbAdvancedOptions.Text = "Advanced Options";
			// 
			// cbAntiCounter
			// 
			this.cbAntiCounter.AutoSize = true;
			this.cbAntiCounter.Checked = true;
			this.cbAntiCounter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAntiCounter.Location = new System.Drawing.Point(10, 137);
			this.cbAntiCounter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbAntiCounter.Name = "cbAntiCounter";
			this.cbAntiCounter.Size = new System.Drawing.Size(124, 24);
			this.cbAntiCounter.TabIndex = 29;
			this.cbAntiCounter.Text = "Anti Counter";
			this.cbAntiCounter.CheckedChanged += new System.EventHandler(this.cbAntiCounter_CheckedChanged);
			// 
			// cbCopyWalk
			// 
			this.cbCopyWalk.AutoSize = true;
			this.cbCopyWalk.Location = new System.Drawing.Point(357, 100);
			this.cbCopyWalk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbCopyWalk.Name = "cbCopyWalk";
			this.cbCopyWalk.Size = new System.Drawing.Size(110, 24);
			this.cbCopyWalk.TabIndex = 28;
			this.cbCopyWalk.Text = "Copy Walk";
			// 
			// numHealthPercent
			// 
			this.numHealthPercent.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numHealthPercent.Location = new System.Drawing.Point(183, 62);
			this.numHealthPercent.LoopValues = false;
			this.numHealthPercent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numHealthPercent.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.numHealthPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numHealthPercent.Name = "numHealthPercent";
			this.numHealthPercent.Size = new System.Drawing.Size(48, 26);
			this.numHealthPercent.TabIndex = 27;
			this.numHealthPercent.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// lbUseHeal2
			// 
			this.lbUseHeal2.AutoSize = true;
			this.lbUseHeal2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lbUseHeal2.Location = new System.Drawing.Point(110, 66);
			this.lbUseHeal2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbUseHeal2.Name = "lbUseHeal2";
			this.lbUseHeal2.Size = new System.Drawing.Size(74, 20);
			this.lbUseHeal2.TabIndex = 26;
			this.lbUseHeal2.Text = "if health<";
			// 
			// tbHealSkill
			// 
			this.tbHealSkill.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbHealSkill.Location = new System.Drawing.Point(70, 62);
			this.tbHealSkill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbHealSkill.Multiline = true;
			this.tbHealSkill.Name = "tbHealSkill";
			this.tbHealSkill.Size = new System.Drawing.Size(36, 30);
			this.tbHealSkill.TabIndex = 25;
			this.tbHealSkill.Text = "1,2";
			this.tbHealSkill.WordWrap = false;
			// 
			// cbUseHeal
			// 
			this.cbUseHeal.AutoSize = true;
			this.cbUseHeal.Location = new System.Drawing.Point(10, 65);
			this.cbUseHeal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbUseHeal.Name = "cbUseHeal";
			this.cbUseHeal.Size = new System.Drawing.Size(64, 24);
			this.cbUseHeal.TabIndex = 24;
			this.cbUseHeal.Text = "Use";
			this.cbUseHeal.CheckedChanged += new System.EventHandler(this.cbUseHeal_CheckedChanged);
			// 
			// tbAttPriority
			// 
			this.tbAttPriority.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbAttPriority.Location = new System.Drawing.Point(147, 98);
			this.tbAttPriority.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbAttPriority.Multiline = true;
			this.tbAttPriority.Name = "tbAttPriority";
			this.tbAttPriority.Size = new System.Drawing.Size(190, 33);
			this.tbAttPriority.TabIndex = 23;
			this.tbAttPriority.Text = "Defense Drone,Attack Drone";
			this.tbAttPriority.WordWrap = false;
			// 
			// cbAttackPriority
			// 
			this.cbAttackPriority.AutoSize = true;
			this.cbAttackPriority.Location = new System.Drawing.Point(10, 102);
			this.cbAttackPriority.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbAttackPriority.Name = "cbAttackPriority";
			this.cbAttackPriority.Size = new System.Drawing.Size(132, 24);
			this.cbAttackPriority.TabIndex = 22;
			this.cbAttackPriority.Text = "AttackPriority:";
			this.cbAttackPriority.CheckedChanged += new System.EventHandler(this.cbAttackPriority_CheckedChanged);
			// 
			// tbBuffSkill
			// 
			this.tbBuffSkill.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbBuffSkill.Location = new System.Drawing.Point(414, 62);
			this.tbBuffSkill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbBuffSkill.Multiline = true;
			this.tbBuffSkill.Name = "tbBuffSkill";
			this.tbBuffSkill.Size = new System.Drawing.Size(59, 33);
			this.tbBuffSkill.TabIndex = 21;
			this.tbBuffSkill.Text = "2,3";
			this.tbBuffSkill.WordWrap = false;
			// 
			// cbBuffIfStop
			// 
			this.cbBuffIfStop.AutoSize = true;
			this.cbBuffIfStop.Location = new System.Drawing.Point(254, 65);
			this.cbBuffIfStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbBuffIfStop.Name = "cbBuffIfStop";
			this.cbBuffIfStop.Size = new System.Drawing.Size(167, 24);
			this.cbBuffIfStop.TabIndex = 20;
			this.cbBuffIfStop.Text = "Buff If StopAttack:";
			this.cbBuffIfStop.CheckedChanged += new System.EventHandler(this.cbBuffIfStop_CheckedChanged);
			// 
			// lbStopAttackBg
			// 
			this.lbStopAttackBg.AutoSize = true;
			this.lbStopAttackBg.BackColor = System.Drawing.Color.Transparent;
			this.lbStopAttackBg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStopAttackBg.Location = new System.Drawing.Point(290, 25);
			this.lbStopAttackBg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbStopAttackBg.Name = "lbStopAttackBg";
			this.lbStopAttackBg.Size = new System.Drawing.Size(167, 33);
			this.lbStopAttackBg.TabIndex = 21;
			this.lbStopAttackBg.Text = "                   ";
			// 
			// cbPartyCmd
			// 
			this.cbPartyCmd.AutoSize = true;
			this.cbPartyCmd.Checked = true;
			this.cbPartyCmd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbPartyCmd.Location = new System.Drawing.Point(124, 32);
			this.cbPartyCmd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbPartyCmd.Name = "cbPartyCmd";
			this.cbPartyCmd.Size = new System.Drawing.Size(104, 24);
			this.cbPartyCmd.TabIndex = 29;
			this.cbPartyCmd.Text = "PartyCmd";
			this.cbPartyCmd.CheckedChanged += new System.EventHandler(this.cbPartyCmd_CheckedChanged);
			// 
			// timerStopAttack
			// 
			this.timerStopAttack.Tick += new System.EventHandler(this.timerStopAttack_Tick);
			// 
			// cmbGotoUsername
			// 
			this.cmbGotoUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.cmbGotoUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmbGotoUsername.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbGotoUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cmbGotoUsername.FormattingEnabled = true;
			this.cmbGotoUsername.ItemHeight = 20;
			this.cmbGotoUsername.Location = new System.Drawing.Point(28, 43);
			this.cmbGotoUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbGotoUsername.Name = "cmbGotoUsername";
			this.cmbGotoUsername.Size = new System.Drawing.Size(181, 28);
			this.cmbGotoUsername.TabIndex = 20;
			this.cmbGotoUsername.Text = "username";
			this.cmbGotoUsername.Click += new System.EventHandler(this.cmbGotoUsername_Clicked);
			// 
			// cbWaitSkill
			// 
			this.cbWaitSkill.AutoSize = true;
			this.cbWaitSkill.Location = new System.Drawing.Point(174, 106);
			this.cbWaitSkill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbWaitSkill.Name = "cbWaitSkill";
			this.cbWaitSkill.Size = new System.Drawing.Size(67, 24);
			this.cbWaitSkill.TabIndex = 21;
			this.cbWaitSkill.Text = "Wait";
			// 
			// numSkillDelay
			// 
			this.numSkillDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSkillDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSkillDelay.Location = new System.Drawing.Point(114, 142);
			this.numSkillDelay.LoopValues = false;
			this.numSkillDelay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numSkillDelay.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
			this.numSkillDelay.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numSkillDelay.Name = "numSkillDelay";
			this.numSkillDelay.Size = new System.Drawing.Size(90, 28);
			this.numSkillDelay.TabIndex = 22;
			this.numSkillDelay.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(24, 148);
			this.darkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(85, 20);
			this.darkLabel1.TabIndex = 23;
			this.darkLabel1.Text = "Skill Delay:";
			// 
			// darkLabel2
			// 
			this.darkLabel2.AutoSize = true;
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(208, 148);
			this.darkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(30, 20);
			this.darkLabel2.TabIndex = 30;
			this.darkLabel2.Text = "ms";
			// 
			// gbActivation
			// 
			this.gbActivation.Controls.Add(this.cbPartyCmd);
			this.gbActivation.Controls.Add(this.cbEnablePlugin);
			this.gbActivation.Location = new System.Drawing.Point(18, 295);
			this.gbActivation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbActivation.Name = "gbActivation";
			this.gbActivation.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbActivation.Size = new System.Drawing.Size(237, 69);
			this.gbActivation.TabIndex = 31;
			this.gbActivation.TabStop = false;
			this.gbActivation.Text = "Activation";
			// 
			// darkLabel3
			// 
			this.darkLabel3.AutoSize = true;
			this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel3.Location = new System.Drawing.Point(12, 32);
			this.darkLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel3.Name = "darkLabel3";
			this.darkLabel3.Size = new System.Drawing.Size(47, 20);
			this.darkLabel3.TabIndex = 29;
			this.darkLabel3.Text = "msg: ";
			// 
			// gbConfig
			// 
			this.gbConfig.Controls.Add(this.btnLoad);
			this.gbConfig.Controls.Add(this.btnSave);
			this.gbConfig.Location = new System.Drawing.Point(272, 289);
			this.gbConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbConfig.Name = "gbConfig";
			this.gbConfig.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gbConfig.Size = new System.Drawing.Size(237, 75);
			this.gbConfig.TabIndex = 32;
			this.gbConfig.TabStop = false;
			this.gbConfig.Text = "Config : Default";
			// 
			// tbSpecialMsg
			// 
			this.tbSpecialMsg.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbSpecialMsg.Location = new System.Drawing.Point(60, 26);
			this.tbSpecialMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbSpecialMsg.Multiline = true;
			this.tbSpecialMsg.Name = "tbSpecialMsg";
			this.tbSpecialMsg.Size = new System.Drawing.Size(167, 33);
			this.tbSpecialMsg.TabIndex = 33;
			this.tbSpecialMsg.Text = "converge,truth,resist,bleee";
			this.tbSpecialMsg.WordWrap = false;
			// 
			// darkGroupBox1
			// 
			this.darkGroupBox1.Controls.Add(this.cbSpecialAnims);
			this.darkGroupBox1.Controls.Add(this.numSkillAct);
			this.darkGroupBox1.Controls.Add(this.darkLabel3);
			this.darkGroupBox1.Controls.Add(this.tbSpecialMsg);
			this.darkGroupBox1.Location = new System.Drawing.Point(18, 185);
			this.darkGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Name = "darkGroupBox1";
			this.darkGroupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Size = new System.Drawing.Size(237, 98);
			this.darkGroupBox1.TabIndex = 32;
			this.darkGroupBox1.TabStop = false;
			this.darkGroupBox1.Text = "Special Anims";
			// 
			// cbSpecialAnims
			// 
			this.cbSpecialAnims.AutoSize = true;
			this.cbSpecialAnims.Checked = true;
			this.cbSpecialAnims.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbSpecialAnims.Location = new System.Drawing.Point(14, 63);
			this.cbSpecialAnims.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbSpecialAnims.Name = "cbSpecialAnims";
			this.cbSpecialAnims.Size = new System.Drawing.Size(111, 24);
			this.cbSpecialAnims.TabIndex = 30;
			this.cbSpecialAnims.Text = "skill action:";
			this.cbSpecialAnims.CheckedChanged += new System.EventHandler(this.cbSpecialAnims_CheckedChanged);
			// 
			// numSkillAct
			// 
			this.numSkillAct.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSkillAct.Location = new System.Drawing.Point(158, 62);
			this.numSkillAct.LoopValues = false;
			this.numSkillAct.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numSkillAct.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numSkillAct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSkillAct.Name = "numSkillAct";
			this.numSkillAct.Size = new System.Drawing.Size(69, 26);
			this.numSkillAct.TabIndex = 29;
			this.numSkillAct.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// btnMe
			// 
			this.btnMe.Checked = false;
			this.btnMe.Location = new System.Drawing.Point(218, 43);
			this.btnMe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnMe.Name = "btnMe";
			this.btnMe.Size = new System.Drawing.Size(45, 33);
			this.btnMe.TabIndex = 26;
			this.btnMe.Text = "me";
			this.btnMe.Click += new System.EventHandler(this.btnMe_Click);
			// 
			// MaidRemake
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 562);
			this.Controls.Add(this.btnMe);
			this.Controls.Add(this.darkGroupBox1);
			this.Controls.Add(this.gbConfig);
			this.Controls.Add(this.gbActivation);
			this.Controls.Add(this.darkLabel2);
			this.Controls.Add(this.darkLabel1);
			this.Controls.Add(this.numSkillDelay);
			this.Controls.Add(this.cbWaitSkill);
			this.Controls.Add(this.cmbGotoUsername);
			this.Controls.Add(this.gbAdvancedOptions);
			this.Controls.Add(this.gbOptions);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tbSkillList);
			this.Controls.Add(this.trgtUsrnmLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MaidRemake";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Maid Remake";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.gbOptions.ResumeLayout(false);
			this.gbOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).EndInit();
			this.gbAdvancedOptions.ResumeLayout(false);
			this.gbAdvancedOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numHealthPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillDelay)).EndInit();
			this.gbActivation.ResumeLayout(false);
			this.gbActivation.PerformLayout();
			this.gbConfig.ResumeLayout(false);
			this.darkGroupBox1.ResumeLayout(false);
			this.darkGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkillAct)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer timerStopAttack;
		internal System.Windows.Forms.ComboBox cmbGotoUsername;
		private DarkUI.Controls.DarkLabel trgtUsrnmLabel;
		public DarkUI.Controls.DarkCheckBox cbEnablePlugin;
		internal DarkUI.Controls.DarkTextBox tbSkillList;
		private DarkUI.Controls.DarkLabel label4;
		private DarkUI.Controls.DarkGroupBox gbOptions;
		private DarkUI.Controls.DarkCheckBox cbStopIf;
		private DarkUI.Controls.DarkNumericUpDown numRelogDelay;
		private DarkUI.Controls.DarkCheckBox cbUnfollow;
		public DarkUI.Controls.DarkCheckBox cbStopAttack;
		private DarkUI.Controls.DarkCheckBox cbEnableGlobalHotkey;
		private DarkUI.Controls.DarkGroupBox gbAdvancedOptions;
		internal DarkUI.Controls.DarkTextBox tbBuffSkill;
		internal DarkUI.Controls.DarkCheckBox cbBuffIfStop;
		private DarkUI.Controls.DarkTextBox tbAttPriority;
		private DarkUI.Controls.DarkCheckBox cbAttackPriority;
		internal DarkUI.Controls.DarkCheckBox cbUseHeal;
		internal DarkUI.Controls.DarkTextBox tbHealSkill;
		internal DarkUI.Controls.DarkNumericUpDown numHealthPercent;
		private DarkUI.Controls.DarkLabel lbUseHeal2;
		private DarkUI.Controls.DarkLabel label1;
		private DarkUI.Controls.DarkComboBox cmbPreset;
		private DarkUI.Controls.DarkLabel label2;
		private System.Windows.Forms.Label lbStopAttackBg;
		internal DarkUI.Controls.DarkCheckBox cbWaitSkill;
		internal DarkUI.Controls.DarkCheckBox cbHandleLockedMap;
		private System.Windows.Forms.LinkLabel lblLockedMapSetting;
		private DarkUI.Controls.DarkNumericUpDown numSkillDelay;
		private DarkUI.Controls.DarkLabel darkLabel1;
		public DarkUI.Controls.DarkCheckBox cbCopyWalk;
		private DarkUI.Controls.DarkButton btnSave;
		private DarkUI.Controls.DarkButton btnLoad;
		private DarkUI.Controls.DarkCheckBox cbPartyCmd;
		private DarkUI.Controls.DarkLabel darkLabel2;
		private DarkUI.Controls.DarkGroupBox gbActivation;
		private System.Windows.Forms.LinkLabel lblWhitelistMap;
		public DarkUI.Controls.DarkCheckBox cbWhitelistMap;
		private DarkUI.Controls.DarkLabel darkLabel3;
		private DarkUI.Controls.DarkGroupBox gbConfig;
		internal DarkUI.Controls.DarkTextBox tbSpecialMsg;
		private DarkUI.Controls.DarkGroupBox darkGroupBox1;
		private DarkUI.Controls.DarkButton btnMe;
		internal DarkUI.Controls.DarkNumericUpDown numSkillAct;
		private DarkUI.Controls.DarkLabel darkLabel5;
		private DarkUI.Controls.DarkComboBox cmbUltraBoss;
		public DarkUI.Controls.DarkCheckBox cbAntiCounter;
		private DarkUI.Controls.DarkCheckBox cbSpecialAnims;
	}
}