using System.ComponentModel;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Grimoire.UI
{
	public partial class BotManager
	{

		#region Definitions
		private IContainer components;
		private Panel[] _panels;
		public static LogForm Log;
		private string _customName;
		private string _customGuild;
		private TabPage tabCombat;
		private DarkButton btnUseSkillSet;
		private DarkButton btnAddSkillSet;
		private DarkTextBox txtSkillSet;
		private DarkCheckBox chkSafeMp;
		private DarkLabel label17;
		private DarkButton btnRest;
		private DarkButton btnRestF;
		private DarkCheckBox chkSkillCD;
		private DarkLabel label12;
		private DarkLabel label11;
		private DarkLabel label10;
		private DarkButton btnKill;
		private DarkLabel label13;
		private DarkCheckBox chkExistQuest;
		private DarkNumericUpDown numRestMP;
		private DarkCheckBox chkMP;
		private DarkNumericUpDown numRest;
		private DarkCheckBox chkHP;
		private DarkNumericUpDown numSkillD;
		private DarkLabel label2;
		private DarkNumericUpDown numSafe;
		private DarkButton btnAddSafe;
		private DarkButton btnAddSkill;
		private DarkNumericUpDown numSkill;
		private DarkCheckBox chkExitRest;
		private DarkCheckBox chkAllSkillsCD;
		private DarkTextBox txtKillFQ;
		private DarkTextBox txtKillFItem;
		private DarkTextBox txtKillFMon;
		private DarkRadioButton rbTemp;
		private DarkRadioButton rbItems;
		private DarkButton btnKillF;
		private DarkTextBox txtMonster;
		private TabPage tabMap;
		private DarkButton btnWalkCur;
		private DarkButton btnWalk;
		private DarkNumericUpDown numWalkY;
		private DarkNumericUpDown numWalkX;
		private DarkButton btnCellSwap;
		private DarkButton btnJump;
		private DarkButton btnCurrCell;
		private DarkTextBox txtPad;
		private DarkTextBox txtCell;
		private DarkButton btnJoin;
		private DarkTextBox txtJoinPad;
		private DarkTextBox txtJoinCell;
		private DarkTextBox txtJoin;
		private TabPage tabQuest;
		private TabPage tabHunt;
		private DarkButton btnQuestAccept;
		private DarkButton btnQuestComplete;
		private DarkButton btnQuestAdd;
		private DarkNumericUpDown numQuestItem;
		private DarkCheckBox chkQuestItem;
		private DarkNumericUpDown numQuestID;
		private DarkLabel label4;
		private TabPage tabMisc;
		private DarkCheckBox chkRestartDeath;
		private DarkCheckBox chkMerge;
		private DarkButton button2;
		private DarkButton btnLogout;
		private DarkTextBox txtDescription;
		private DarkButton btnSave;
		private DarkButton btnDelay;
		private DarkNumericUpDown numDelay;
		private DarkLabel label3;
		private DarkNumericUpDown numBotDelay;
		private DarkButton btnBotDelay;
		private DarkTextBox txtPlayer;
		private DarkButton btnGoto;
		public DarkButton btnLoad;
		private DarkButton btnLoadCmd;
		private DarkCheckBox chkSkip;
		private DarkButton btnStatementAdd;
		private DarkTextBox txtStatement2;
		private DarkTextBox txtStatement1;
		private DarkComboBox cbStatement;
		private DarkComboBox cbCategories;
		private DarkTextBox txtPacket;
		private DarkButton btnPacket;
		private TabPage tabOptions;
		public DarkCheckBox chkEnableSettings;
		public DarkCheckBox chkDisableAnims;
		private DarkTextBox txtSoundItem;
		private DarkButton btnSoundAdd;
		private DarkButton btnSoundDelete;
		private DarkButton btnSoundTest;
		private ListBox lstSoundItems;
		private DarkLabel label9;
		public DarkNumericUpDown numWalkSpeed;
		public DarkCheckBox chkSkipCutscenes;
		public DarkCheckBox chkHidePlayers;
		public DarkCheckBox chkLag;
		public DarkCheckBox chkMagnet;
		public DarkCheckBox chkProvoke;
		public DarkCheckBox chkInfiniteRange;
		private DarkGroupBox grpLogin;
		private DarkComboBox cbServers;
		private DarkCheckBox chkRelogRetry;
		private DarkCheckBox chkRelog;
		private DarkNumericUpDown numRelogDelay;
		private DarkLabel label7;
		private DarkTextBox txtUsername;
		private DarkTextBox txtGuild;
		private DarkButton btnchangeName;
		private DarkButton btnchangeGuild;
		private TabPage tabBots;
		private DarkLabel lblBoosts;
		private DarkLabel lblDrops;
		private DarkLabel lblQuests;
		private DarkLabel lblSkills;
		private DarkLabel lblCommands;
		private DarkLabel lblItems;
		private DarkTextBox txtSavedAuthor;
		private DarkLabel lblBots;
		private TreeView treeBots;
		private DarkTextBox txtSavedAdd;
		private DarkButton btnSavedAdd;
		private DarkTextBox txtSaved;
		private DarkButton btnProvokeOn;
		private DarkButton btnProvokeOff;
		private ListBox lstLogText;
		private DarkButton btnLogDebug;
		private DarkButton btnLog;
		private DarkTextBox txtLog;
		private DarkLabel label5;
		private DarkNumericUpDown numOptionsTimer;
		private DarkLabel label6;
		private DarkNumericUpDown numEnsureTries;
		private DarkButton btnWalkRdm;
		private DarkButton btnBlank;
		private DarkCheckBox chkAFK;
		private SplitContainer splitContainer1;
		private DarkComboBox cbLists;
		private DarkCheckBox chkAll;
		private DarkButton btnClear;
		private DarkButton btnDown;
		private DarkButton btnRemove;
		private DarkButton btnUp;
		private Panel panel1;
		private SplitContainer splitContainer2;
		private DarkButton btnSetSpawn;
		private DarkButton btnBeep;
		private DarkNumericUpDown numBeepTimes;
		private Panel panel3;
		private Panel panel2;
		private DarkButton btnSkillCmd;
		private TabPage tabItem;
		private DarkCheckBox checkBox1;
		private DarkCheckBox chkBuffup;
		private TabPage tabOptions2;
		private DarkButton btnSetUndecided;
		private DarkButton btnSetChaos;
		private DarkButton btnSetEvil;
		private DarkButton btnSetGood;
		private DarkGroupBox grpAlignment;
		private DarkGroupBox grpAccessLevel;
		private DarkButton btnSetMem;
		private DarkButton btnSetModerator;
		private DarkButton btnSetNonMem;
		private DarkCheckBox chkToggleMute;
		private ContextMenuStrip BotManagerMenuStrip;
		private ToolStripMenuItem changeFontsToolStripMenuItem;
		private DarkButton btnGoDownIndex;
		private DarkButton btnGoUpIndex;
		private DarkButton btnGotoIndex;
		private DarkNumericUpDown numIndexCmd;
		private ToolStripMenuItem multilineToggleToolStripMenuItem;
		private ToolStripMenuItem toggleTabpagesToolStripMenuItem;
		private ToolStripMenuItem commandColorsToolStripMenuItem;
		private DarkButton btnChangeNameCmd;
		private DarkButton btnChangeGuildCmd;
		private DarkButton btnClientPacket;
		private DarkNumericUpDown numSetInt;
		private DarkTextBox txtSetInt;
		private DarkButton btnSetInt;
		private DarkButton btnDecreaseInt;
		private DarkButton btnIncreaseInt;
		private DarkButton btnSearchCmd;
		private DarkTextBox txtSearchCmd;
		private TabPage tabMisc2;
		private DarkGroupBox groupBox1;
		private DarkButton btnAddInfoMsg;
		private DarkButton btnAddWarnMsg;
		private DarkTextBox inputMsgClient;
		private Panel panel4;
		private DarkGroupBox darkGroupBox1;
		private DarkButton btnReturnCmd;
		private DarkGroupBox darkGroupBox4;
		private DarkCheckBox chkPickupAll;
		public DarkCheckBox chkPickup;
		private DarkCheckBox chkReject;
		public DarkCheckBox chkPickupAcTag;
		private DarkCheckBox chkBankOnStop;
		private DarkGroupBox darkGroupBox3;
		private DarkButton btnLoadShop;
		private DarkTextBox tbShopItemName;
		private DarkButton btnBuy;
		private DarkButton btnBuyFast;
		private DarkGroupBox darkGroupBox2;
		private DarkButton btnWhitelistToggle;
		private DarkButton btnWhitelistOn;
		private DarkButton btnWhitelistOff;
		private DarkButton btnBoost;
		private DarkComboBox cbBoosts;
		private DarkNumericUpDown numMapItem;
		private DarkButton btnMapItem;
		private DarkButton btnSwap;
		private DarkTextBox txtSwapInv;
		private DarkTextBox txtSwapBank;
		private DarkButton btnWhitelist;
		private DarkButton btnBoth;
		private DarkTextBox txtWhitelist;
		private DarkButton btnItem;
		private DarkButton btnUnbanklst;
		private DarkTextBox txtItem;
		private DarkComboBox cbItemCmds;
		private DarkGroupBox darkGroupBox5;
		private DarkGroupBox darkGroupBox6;
		private TabPage tabInfo;
		private Panel panel5;
		public RichTextBox rtbInfo;
		private DarkGroupBox darkGroupBox7;
		private DarkGroupBox darkGroupBox8;
		private DarkGroupBox darkGroupBox9;
		private BackgroundWorker backgroundWorker1;
		private SplitContainer splitContainer3;
		private DarkGroupBox darkGroupBox12;
		private DarkGroupBox darkGroupBox13;
		private RichTextBox richTextBox2;
		private DarkPanel darkPanel1;
		private DarkGroupBox darkGroupBox14;
		private DarkButton btnAttack;
		private SplitContainer splitContainer5;
		public CheckBox chkEnable;
		private DarkComboBox cbSafeType;
		private DarkCheckBox chkAddToWhiteList;
		private DarkNumericUpDown numSpamTimes;
		private DarkButton btnSetLevelCmd;
		private DarkButton btnSetLevel;
		private DarkTextBox tbLevel;
		public DarkCheckBox chkWalkSpeed;
		public DarkCheckBox chkReloginCompleteQuest;
		private DarkButton btnAddCmdHunt;
		private DarkCheckBox chkIsTempF;
		private DarkCheckBox chkAddToWhitelistF;
		private DarkTextBox tbItemQtyF;
		private DarkTextBox tbItemNameF;
		private DarkTextBox tbMonNameF;
		private DarkCheckBox cbBlankFirstF;
		private DarkButton btnGetMapF;
		private DarkTextBox tbPadF;
		private DarkTextBox tbCellF;
		private DarkTextBox tbMapF;
		private DarkLabel darkLabel2;
		private DarkButton btnAllSkill;
		private DarkCheckBox chkQRequirements;
		private DarkCheckBox chkQRewards;
		private DarkButton btnQAddToWhitelist;
		private DarkNumericUpDown numQQuestId;
		private DarkLabel darkLabel4;
		private DarkNumericUpDown numSetFPS;
		private DarkButton btnSetFPS;
		public DarkCheckBox chkGender;
		private DarkButton btnBSStop;
		private Label label8;
		private DarkTextBox tbBSLabel;
		private DarkNumericUpDown numBSDelay;
		private DarkTextBox tbBSPacket;
		private DarkButton btnBSAdd;
		private DarkLabel darkLabel6;
		private DarkNumericUpDown numSaveProgress;
		private DarkCheckBox chkSaveProgress;
		#endregion

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lstCommands = new System.Windows.Forms.ListBox();
			this.lstBoosts = new System.Windows.Forms.ListBox();
			this.lstDrops = new System.Windows.Forms.ListBox();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.lstQuests = new System.Windows.Forms.ListBox();
			this.lstSkills = new System.Windows.Forms.ListBox();
			this.mainTabControl = new FlatTabControl.FlatTabControl();
			this.tabCombat = new System.Windows.Forms.TabPage();
			this.darkGroupBox20 = new DarkUI.Controls.DarkGroupBox();
			this.txtSkillSet = new DarkUI.Controls.DarkTextBox();
			this.btnAddSkillSet = new DarkUI.Controls.DarkButton();
			this.btnUseSkillSet = new DarkUI.Controls.DarkButton();
			this.darkGroupBox19 = new DarkUI.Controls.DarkGroupBox();
			this.btnRest = new DarkUI.Controls.DarkButton();
			this.chkHP = new DarkUI.Controls.DarkCheckBox();
			this.numRest = new DarkUI.Controls.DarkNumericUpDown();
			this.chkMP = new DarkUI.Controls.DarkCheckBox();
			this.btnRestF = new DarkUI.Controls.DarkButton();
			this.numRestMP = new DarkUI.Controls.DarkNumericUpDown();
			this.label10 = new DarkUI.Controls.DarkLabel();
			this.label12 = new DarkUI.Controls.DarkLabel();
			this.label11 = new DarkUI.Controls.DarkLabel();
			this.darkGroupBox17 = new DarkUI.Controls.DarkGroupBox();
			this.rbItems = new DarkUI.Controls.DarkRadioButton();
			this.rbTemp = new DarkUI.Controls.DarkRadioButton();
			this.txtKillFMon = new DarkUI.Controls.DarkTextBox();
			this.txtKillFItem = new DarkUI.Controls.DarkTextBox();
			this.txtKillFQ = new DarkUI.Controls.DarkTextBox();
			this.chkAddToWhiteList = new DarkUI.Controls.DarkCheckBox();
			this.btnKillF = new DarkUI.Controls.DarkButton();
			this.darkGroupBox18 = new DarkUI.Controls.DarkGroupBox();
			this.btnLeaveCombat = new DarkUI.Controls.DarkButton();
			this.btnStopAttack = new DarkUI.Controls.DarkButton();
			this.numSkillCmd = new DarkUI.Controls.DarkNumericUpDown();
			this.cbSkillCmdWait = new DarkUI.Controls.DarkCheckBox();
			this.txtMonsterSkillCmd = new DarkUI.Controls.DarkTextBox();
			this.btnSkillCmd = new DarkUI.Controls.DarkButton();
			this.chkForceSkill = new DarkUI.Controls.DarkCheckBox();
			this.chkSkillCD = new DarkUI.Controls.DarkCheckBox();
			this.btnAttack = new DarkUI.Controls.DarkButton();
			this.btnKill = new DarkUI.Controls.DarkButton();
			this.chkExistQuest = new DarkUI.Controls.DarkCheckBox();
			this.chkExitRest = new DarkUI.Controls.DarkCheckBox();
			this.chkAllSkillsCD = new DarkUI.Controls.DarkCheckBox();
			this.txtMonster = new DarkUI.Controls.DarkTextBox();
			this.boxSkillSet = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel7 = new DarkUI.Controls.DarkLabel();
			this.numSkill = new DarkUI.Controls.DarkNumericUpDown();
			this.btnAddSkill = new DarkUI.Controls.DarkButton();
			this.btnAddSafe = new DarkUI.Controls.DarkButton();
			this.btnAllSkill = new DarkUI.Controls.DarkButton();
			this.numSafe = new DarkUI.Controls.DarkNumericUpDown();
			this.label2 = new DarkUI.Controls.DarkLabel();
			this.label17 = new DarkUI.Controls.DarkLabel();
			this.chkSafeMp = new DarkUI.Controls.DarkCheckBox();
			this.cbSafeType = new DarkUI.Controls.DarkComboBox();
			this.label13 = new DarkUI.Controls.DarkLabel();
			this.numSkillD = new DarkUI.Controls.DarkNumericUpDown();
			this.tabMap = new System.Windows.Forms.TabPage();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.cbJoinMapSwf = new DarkUI.Controls.DarkCheckBox();
			this.tbJoinMapSwf = new DarkUI.Controls.DarkTextBox();
			this.darkLabel10 = new DarkUI.Controls.DarkLabel();
			this.numJoinTry = new DarkUI.Controls.DarkNumericUpDown();
			this.btnSetSpawn = new DarkUI.Controls.DarkButton();
			this.btnWalkRdm = new DarkUI.Controls.DarkButton();
			this.btnWalkCur = new DarkUI.Controls.DarkButton();
			this.btnWalk = new DarkUI.Controls.DarkButton();
			this.numWalkY = new DarkUI.Controls.DarkNumericUpDown();
			this.numWalkX = new DarkUI.Controls.DarkNumericUpDown();
			this.button2 = new DarkUI.Controls.DarkButton();
			this.btnCellSwap = new DarkUI.Controls.DarkButton();
			this.btnJump = new DarkUI.Controls.DarkButton();
			this.btnCurrCell = new DarkUI.Controls.DarkButton();
			this.txtPad = new DarkUI.Controls.DarkTextBox();
			this.txtCell = new DarkUI.Controls.DarkTextBox();
			this.btnJoin = new DarkUI.Controls.DarkButton();
			this.txtJoinPad = new DarkUI.Controls.DarkTextBox();
			this.txtJoinCell = new DarkUI.Controls.DarkTextBox();
			this.txtJoin = new DarkUI.Controls.DarkTextBox();
			this.tabItem = new System.Windows.Forms.TabPage();
			this.darkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
			this.btnMapItemExe = new DarkUI.Controls.DarkButton();
			this.btnMapItem = new DarkUI.Controls.DarkButton();
			this.numMapItem = new DarkUI.Controls.DarkNumericUpDown();
			this.darkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
			this.chkInventOnStart = new DarkUI.Controls.DarkCheckBox();
			this.chkPickupAll = new DarkUI.Controls.DarkCheckBox();
			this.chkPickup = new DarkUI.Controls.DarkCheckBox();
			this.chkReject = new DarkUI.Controls.DarkCheckBox();
			this.chkPickupAcTag = new DarkUI.Controls.DarkCheckBox();
			this.chkBankOnStop = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
			this.radBuyByName = new DarkUI.Controls.DarkRadioButton();
			this.radBuyByID = new DarkUI.Controls.DarkRadioButton();
			this.tbShopId = new DarkUI.Controls.DarkTextBox();
			this.tbShopItemId = new DarkUI.Controls.DarkTextBox();
			this.tbItemId = new DarkUI.Controls.DarkTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numBuyQty = new DarkUI.Controls.DarkNumericUpDown();
			this.btnLoadShop = new DarkUI.Controls.DarkButton();
			this.tbShopItemName = new DarkUI.Controls.DarkTextBox();
			this.btnBuy = new DarkUI.Controls.DarkButton();
			this.btnBuyFast = new DarkUI.Controls.DarkButton();
			this.darkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
			this.btnWhitelistToggle = new DarkUI.Controls.DarkButton();
			this.btnWhitelistOn = new DarkUI.Controls.DarkButton();
			this.btnWhitelistOff = new DarkUI.Controls.DarkButton();
			this.btnBoost = new DarkUI.Controls.DarkButton();
			this.cbBoosts = new DarkUI.Controls.DarkComboBox();
			this.btnSwap = new DarkUI.Controls.DarkButton();
			this.txtSwapInv = new DarkUI.Controls.DarkTextBox();
			this.txtSwapBank = new DarkUI.Controls.DarkTextBox();
			this.btnWhitelist = new DarkUI.Controls.DarkButton();
			this.btnBoth = new DarkUI.Controls.DarkButton();
			this.txtWhitelist = new DarkUI.Controls.DarkTextBox();
			this.btnItem = new DarkUI.Controls.DarkButton();
			this.btnUnbanklst = new DarkUI.Controls.DarkButton();
			this.txtItem = new DarkUI.Controls.DarkTextBox();
			this.cbItemCmds = new DarkUI.Controls.DarkComboBox();
			this.tabQuest = new System.Windows.Forms.TabPage();
			this.darkGroupBox26 = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.numQQuestId = new DarkUI.Controls.DarkNumericUpDown();
			this.btnQAddToWhitelist = new DarkUI.Controls.DarkButton();
			this.chkQRequirements = new DarkUI.Controls.DarkCheckBox();
			this.chkQRewards = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox25 = new DarkUI.Controls.DarkGroupBox();
			this.chkReloginCompleteQuest = new DarkUI.Controls.DarkCheckBox();
			this.darkLabel13 = new DarkUI.Controls.DarkLabel();
			this.darkLabel8 = new DarkUI.Controls.DarkLabel();
			this.numQuestListID = new DarkUI.Controls.DarkNumericUpDown();
			this.numQuestDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnAddQuestList = new DarkUI.Controls.DarkButton();
			this.chkQuestListItem = new DarkUI.Controls.DarkCheckBox();
			this.darkLabel5 = new DarkUI.Controls.DarkLabel();
			this.numQuestListItem = new DarkUI.Controls.DarkNumericUpDown();
			this.btnRemoveQuestList = new DarkUI.Controls.DarkButton();
			this.btnQuestAdd = new DarkUI.Controls.DarkButton();
			this.darkGroupBox24 = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel12 = new DarkUI.Controls.DarkLabel();
			this.label4 = new DarkUI.Controls.DarkLabel();
			this.numQuestID = new DarkUI.Controls.DarkNumericUpDown();
			this.chkQuestItem = new DarkUI.Controls.DarkCheckBox();
			this.numQuestItem = new DarkUI.Controls.DarkNumericUpDown();
			this.btnQuestComplete = new DarkUI.Controls.DarkButton();
			this.btnQuestAccept = new DarkUI.Controls.DarkButton();
			this.numEnsureTries = new DarkUI.Controls.DarkNumericUpDown();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.darkGroupBox15 = new DarkUI.Controls.DarkGroupBox();
			this.btnFollowCmd = new DarkUI.Controls.DarkButton();
			this.tbFollowPlayer = new DarkUI.Controls.DarkTextBox();
			this.darkGroupBox11 = new DarkUI.Controls.DarkGroupBox();
			this.lbLabels = new DarkUI.Controls.DarkListBox(this.components);
			this.darkGroupBox10 = new DarkUI.Controls.DarkGroupBox();
			this.btnStop = new DarkUI.Controls.DarkButton();
			this.btnRestart = new DarkUI.Controls.DarkButton();
			this.btnAddLabel = new DarkUI.Controls.DarkButton();
			this.btnGotoLabel = new DarkUI.Controls.DarkButton();
			this.txtLabel = new DarkUI.Controls.DarkTextBox();
			this.numSpamTimes = new DarkUI.Controls.DarkNumericUpDown();
			this.darkGroupBox14 = new DarkUI.Controls.DarkGroupBox();
			this.txtPlayer = new DarkUI.Controls.DarkTextBox();
			this.btnGoto = new DarkUI.Controls.DarkButton();
			this.darkGroupBox12 = new DarkUI.Controls.DarkGroupBox();
			this.txtSetInt = new DarkUI.Controls.DarkTextBox();
			this.btnSetInt = new DarkUI.Controls.DarkButton();
			this.numSetInt = new DarkUI.Controls.DarkNumericUpDown();
			this.btnIncreaseInt = new DarkUI.Controls.DarkButton();
			this.btnDecreaseInt = new DarkUI.Controls.DarkButton();
			this.btnReturnCmd = new DarkUI.Controls.DarkButton();
			this.darkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
			this.numIndexCmd = new DarkUI.Controls.DarkNumericUpDown();
			this.btnGotoIndex = new DarkUI.Controls.DarkButton();
			this.btnGoUpIndex = new DarkUI.Controls.DarkButton();
			this.btnGoDownIndex = new DarkUI.Controls.DarkButton();
			this.darkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
			this.btnProvokeOff = new DarkUI.Controls.DarkButton();
			this.btnProvokeOn = new DarkUI.Controls.DarkButton();
			this.btnBlank = new DarkUI.Controls.DarkButton();
			this.btnLogout = new DarkUI.Controls.DarkButton();
			this.btnBeep = new DarkUI.Controls.DarkButton();
			this.numBeepTimes = new DarkUI.Controls.DarkNumericUpDown();
			this.btnDelay = new DarkUI.Controls.DarkButton();
			this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnStatementAdd = new DarkUI.Controls.DarkButton();
			this.txtStatement2 = new DarkUI.Controls.DarkTextBox();
			this.txtStatement1 = new DarkUI.Controls.DarkTextBox();
			this.cbStatement = new DarkUI.Controls.DarkComboBox();
			this.cbCategories = new DarkUI.Controls.DarkComboBox();
			this.txtPacket = new DarkUI.Controls.DarkTextBox();
			this.btnClientPacket = new DarkUI.Controls.DarkButton();
			this.btnPacket = new DarkUI.Controls.DarkButton();
			this.btnLoadCmd = new DarkUI.Controls.DarkButton();
			this.tabMisc2 = new System.Windows.Forms.TabPage();
			this.darkGroupBox21 = new DarkUI.Controls.DarkGroupBox();
			this.chkSpecial = new DarkUI.Controls.DarkCheckBox();
			this.cmbSpecials = new DarkUI.Controls.DarkComboBox();
			this.darkGroupBox13 = new DarkUI.Controls.DarkGroupBox();
			this.chkRestartAFK = new DarkUI.Controls.DarkCheckBox();
			this.label3 = new DarkUI.Controls.DarkLabel();
			this.chkSkip = new DarkUI.Controls.DarkCheckBox();
			this.btnBotDelay = new DarkUI.Controls.DarkButton();
			this.numBotDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.chkRestartDeath = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox9 = new DarkUI.Controls.DarkGroupBox();
			this.txtAuthor = new DarkUI.Controls.DarkTextBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.btnLoad = new DarkUI.Controls.DarkButton();
			this.txtDescription = new DarkUI.Controls.DarkTextBox();
			this.chkMerge = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.btnBSStop = new DarkUI.Controls.DarkButton();
			this.label8 = new System.Windows.Forms.Label();
			this.tbBSLabel = new DarkUI.Controls.DarkTextBox();
			this.numBSDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.tbBSPacket = new DarkUI.Controls.DarkTextBox();
			this.btnBSAdd = new DarkUI.Controls.DarkButton();
			this.tabOptions = new System.Windows.Forms.TabPage();
			this.chkAntiCounter = new DarkUI.Controls.DarkCheckBox();
			this.chkFollowOnly = new DarkUI.Controls.DarkCheckBox();
			this.tbFollowPlayer2 = new DarkUI.Controls.DarkTextBox();
			this.chkWalkSpeed = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.btnLog = new DarkUI.Controls.DarkButton();
			this.btnLogDebug = new DarkUI.Controls.DarkButton();
			this.lstLogText = new System.Windows.Forms.ListBox();
			this.txtLog = new DarkUI.Controls.DarkTextBox();
			this.label5 = new DarkUI.Controls.DarkLabel();
			this.label6 = new DarkUI.Controls.DarkLabel();
			this.numOptionsTimer = new DarkUI.Controls.DarkNumericUpDown();
			this.chkEnableSettings = new DarkUI.Controls.DarkCheckBox();
			this.chkDisableAnims = new DarkUI.Controls.DarkCheckBox();
			this.txtSoundItem = new DarkUI.Controls.DarkTextBox();
			this.btnSoundAdd = new DarkUI.Controls.DarkButton();
			this.btnSoundDelete = new DarkUI.Controls.DarkButton();
			this.btnSoundTest = new DarkUI.Controls.DarkButton();
			this.lstSoundItems = new System.Windows.Forms.ListBox();
			this.label9 = new DarkUI.Controls.DarkLabel();
			this.numWalkSpeed = new DarkUI.Controls.DarkNumericUpDown();
			this.chkSkipCutscenes = new DarkUI.Controls.DarkCheckBox();
			this.chkHidePlayers = new DarkUI.Controls.DarkCheckBox();
			this.chkLag = new DarkUI.Controls.DarkCheckBox();
			this.chkMagnet = new DarkUI.Controls.DarkCheckBox();
			this.chkProvoke = new DarkUI.Controls.DarkCheckBox();
			this.chkInfiniteRange = new DarkUI.Controls.DarkCheckBox();
			this.grpLogin = new DarkUI.Controls.DarkGroupBox();
			this.chkAFK = new DarkUI.Controls.DarkCheckBox();
			this.cbServers = new DarkUI.Controls.DarkComboBox();
			this.chkRelogRetry = new DarkUI.Controls.DarkCheckBox();
			this.chkRelog = new DarkUI.Controls.DarkCheckBox();
			this.numRelogDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.label7 = new DarkUI.Controls.DarkLabel();
			this.tabOptions2 = new System.Windows.Forms.TabPage();
			this.darkGroupBox27 = new DarkUI.Controls.DarkGroupBox();
			this.tbLoadMap = new DarkUI.Controls.DarkTextBox();
			this.btnLoadMap = new DarkUI.Controls.DarkButton();
			this.darkLabel11 = new DarkUI.Controls.DarkLabel();
			this.btnSetFPSCmd = new DarkUI.Controls.DarkButton();
			this.btnSetSpawn2 = new DarkUI.Controls.DarkButton();
			this.darkGroupBox23 = new DarkUI.Controls.DarkGroupBox();
			this.txtSearchCmd = new DarkUI.Controls.DarkTextBox();
			this.btnSearchCmd = new DarkUI.Controls.DarkButton();
			this.btnReloadMap = new DarkUI.Controls.DarkButton();
			this.btnHideLoading = new DarkUI.Controls.DarkButton();
			this.darkGroupBox22 = new DarkUI.Controls.DarkGroupBox();
			this.chkSaveProgress = new DarkUI.Controls.DarkCheckBox();
			this.numSaveProgress = new DarkUI.Controls.DarkNumericUpDown();
			this.darkLabel6 = new DarkUI.Controls.DarkLabel();
			this.colorfulCommands = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox16 = new DarkUI.Controls.DarkGroupBox();
			this.chkAMStopBot = new DarkUI.Controls.DarkCheckBox();
			this.chkAMLogout = new DarkUI.Controls.DarkCheckBox();
			this.chkAntiMod = new DarkUI.Controls.DarkCheckBox();
			this.chkGender = new DarkUI.Controls.DarkCheckBox();
			this.numSetFPS = new DarkUI.Controls.DarkNumericUpDown();
			this.btnSetFPS = new DarkUI.Controls.DarkButton();
			this.btnSetLevelCmd = new DarkUI.Controls.DarkButton();
			this.btnSetLevel = new DarkUI.Controls.DarkButton();
			this.tbLevel = new DarkUI.Controls.DarkTextBox();
			this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.btnAddInfoMsg = new DarkUI.Controls.DarkButton();
			this.btnAddWarnMsg = new DarkUI.Controls.DarkButton();
			this.inputMsgClient = new DarkUI.Controls.DarkTextBox();
			this.grpAccessLevel = new DarkUI.Controls.DarkGroupBox();
			this.chkToggleMute = new DarkUI.Controls.DarkCheckBox();
			this.btnSetMem = new DarkUI.Controls.DarkButton();
			this.btnSetModerator = new DarkUI.Controls.DarkButton();
			this.btnSetNonMem = new DarkUI.Controls.DarkButton();
			this.grpAlignment = new DarkUI.Controls.DarkGroupBox();
			this.btnSetChaos = new DarkUI.Controls.DarkButton();
			this.btnSetUndecided = new DarkUI.Controls.DarkButton();
			this.btnSetGood = new DarkUI.Controls.DarkButton();
			this.btnSetEvil = new DarkUI.Controls.DarkButton();
			this.txtUsername = new DarkUI.Controls.DarkTextBox();
			this.btnChangeNameCmd = new DarkUI.Controls.DarkButton();
			this.btnchangeName = new DarkUI.Controls.DarkButton();
			this.btnChangeGuildCmd = new DarkUI.Controls.DarkButton();
			this.btnchangeGuild = new DarkUI.Controls.DarkButton();
			this.txtGuild = new DarkUI.Controls.DarkTextBox();
			this.tabHunt = new System.Windows.Forms.TabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnAddCmdHunt = new DarkUI.Controls.DarkButton();
			this.chkIsTempF = new DarkUI.Controls.DarkCheckBox();
			this.chkAddToWhitelistF = new DarkUI.Controls.DarkCheckBox();
			this.tbItemQtyF = new DarkUI.Controls.DarkTextBox();
			this.tbItemNameF = new DarkUI.Controls.DarkTextBox();
			this.tbMonNameF = new DarkUI.Controls.DarkTextBox();
			this.cbBlankFirstF = new DarkUI.Controls.DarkCheckBox();
			this.btnGetMapF = new DarkUI.Controls.DarkButton();
			this.tbPadF = new DarkUI.Controls.DarkTextBox();
			this.tbCellF = new DarkUI.Controls.DarkTextBox();
			this.tbMapF = new DarkUI.Controls.DarkTextBox();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			this.tabBots = new System.Windows.Forms.TabPage();
			this.btnSetBotsDir = new DarkUI.Controls.DarkButton();
			this.txtSavedDesc = new DarkUI.Controls.DarkTextBox();
			this.darkPanel1 = new DarkUI.Controls.DarkPanel();
			this.treeBots = new System.Windows.Forms.TreeView();
			this.lblBoosts = new DarkUI.Controls.DarkLabel();
			this.lblDrops = new DarkUI.Controls.DarkLabel();
			this.lblQuests = new DarkUI.Controls.DarkLabel();
			this.lblSkills = new DarkUI.Controls.DarkLabel();
			this.lblCommands = new DarkUI.Controls.DarkLabel();
			this.lblItems = new DarkUI.Controls.DarkLabel();
			this.txtSavedAuthor = new DarkUI.Controls.DarkTextBox();
			this.lblBots = new DarkUI.Controls.DarkLabel();
			this.txtSavedAdd = new DarkUI.Controls.DarkTextBox();
			this.btnSavedAdd = new DarkUI.Controls.DarkButton();
			this.txtSaved = new DarkUI.Controls.DarkTextBox();
			this.tabInfo = new System.Windows.Forms.TabPage();
			this.panel5 = new System.Windows.Forms.Panel();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cbLists = new DarkUI.Controls.DarkComboBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnDown = new DarkUI.Controls.DarkButton();
			this.chkAll = new DarkUI.Controls.DarkCheckBox();
			this.btnClear = new DarkUI.Controls.DarkButton();
			this.chkEnable = new System.Windows.Forms.CheckBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnRemove = new DarkUI.Controls.DarkButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnUp = new DarkUI.Controls.DarkButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.checkBox1 = new DarkUI.Controls.DarkCheckBox();
			this.chkBuffup = new DarkUI.Controls.DarkCheckBox();
			this.BotManagerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.multilineToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleTabpagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commandColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.mainTabControl.SuspendLayout();
			this.tabCombat.SuspendLayout();
			this.darkGroupBox20.SuspendLayout();
			this.darkGroupBox19.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRestMP)).BeginInit();
			this.darkGroupBox17.SuspendLayout();
			this.darkGroupBox18.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkillCmd)).BeginInit();
			this.boxSkillSet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkill)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSafe)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillD)).BeginInit();
			this.tabMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numJoinTry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkX)).BeginInit();
			this.tabItem.SuspendLayout();
			this.darkGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMapItem)).BeginInit();
			this.darkGroupBox4.SuspendLayout();
			this.darkGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBuyQty)).BeginInit();
			this.darkGroupBox2.SuspendLayout();
			this.tabQuest.SuspendLayout();
			this.darkGroupBox26.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQQuestId)).BeginInit();
			this.darkGroupBox25.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQuestListID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestListItem)).BeginInit();
			this.darkGroupBox24.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQuestID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).BeginInit();
			this.tabMisc.SuspendLayout();
			this.darkGroupBox15.SuspendLayout();
			this.darkGroupBox11.SuspendLayout();
			this.darkGroupBox10.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).BeginInit();
			this.darkGroupBox14.SuspendLayout();
			this.darkGroupBox12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetInt)).BeginInit();
			this.darkGroupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).BeginInit();
			this.darkGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
			this.tabMisc2.SuspendLayout();
			this.darkGroupBox21.SuspendLayout();
			this.darkGroupBox13.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).BeginInit();
			this.darkGroupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.darkGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBSDelay)).BeginInit();
			this.tabOptions.SuspendLayout();
			this.darkGroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).BeginInit();
			this.grpLogin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).BeginInit();
			this.tabOptions2.SuspendLayout();
			this.darkGroupBox27.SuspendLayout();
			this.darkGroupBox23.SuspendLayout();
			this.darkGroupBox22.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSaveProgress)).BeginInit();
			this.darkGroupBox16.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.grpAccessLevel.SuspendLayout();
			this.grpAlignment.SuspendLayout();
			this.tabHunt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabBots.SuspendLayout();
			this.darkPanel1.SuspendLayout();
			this.tabInfo.SuspendLayout();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.BotManagerMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstCommands
			// 
			this.lstCommands.AllowDrop = true;
			this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstCommands.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lstCommands.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstCommands.ForeColor = System.Drawing.Color.Gainsboro;
			this.lstCommands.FormattingEnabled = true;
			this.lstCommands.IntegralHeight = false;
			this.lstCommands.ItemHeight = 32;
			this.lstCommands.Location = new System.Drawing.Point(0, 0);
			this.lstCommands.Name = "lstCommands";
			this.lstCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstCommands.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstCommands.Size = new System.Drawing.Size(381, 399);
			this.lstCommands.TabIndex = 1;
			this.lstCommands.Click += new System.EventHandler(this.lstCommands_Click);
			this.lstCommands.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCommands_DrawItem);
			this.lstCommands.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragDrop);
			this.lstCommands.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragEnter);
			this.lstCommands.DoubleClick += new System.EventHandler(this.lstCommands_DoubleClick);
			this.lstCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			this.lstCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCommands_KeyPress);
			// 
			// lstBoosts
			// 
			this.lstBoosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstBoosts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstBoosts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstBoosts.FormattingEnabled = true;
			this.lstBoosts.HorizontalScrollbar = true;
			this.lstBoosts.ItemHeight = 20;
			this.lstBoosts.Location = new System.Drawing.Point(0, 0);
			this.lstBoosts.Name = "lstBoosts";
			this.lstBoosts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstBoosts.Size = new System.Drawing.Size(381, 382);
			this.lstBoosts.TabIndex = 25;
			this.lstBoosts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstDrops
			// 
			this.lstDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstDrops.FormattingEnabled = true;
			this.lstDrops.HorizontalScrollbar = true;
			this.lstDrops.ItemHeight = 20;
			this.lstDrops.Location = new System.Drawing.Point(0, 0);
			this.lstDrops.Name = "lstDrops";
			this.lstDrops.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstDrops.Size = new System.Drawing.Size(381, 382);
			this.lstDrops.TabIndex = 26;
			this.lstDrops.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstItems.FormattingEnabled = true;
			this.lstItems.HorizontalScrollbar = true;
			this.lstItems.ItemHeight = 20;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstItems.Size = new System.Drawing.Size(381, 382);
			this.lstItems.TabIndex = 145;
			this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstQuests
			// 
			this.lstQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstQuests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstQuests.FormattingEnabled = true;
			this.lstQuests.HorizontalScrollbar = true;
			this.lstQuests.ItemHeight = 20;
			this.lstQuests.Location = new System.Drawing.Point(0, 0);
			this.lstQuests.Name = "lstQuests";
			this.lstQuests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstQuests.Size = new System.Drawing.Size(381, 382);
			this.lstQuests.TabIndex = 27;
			this.lstQuests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstSkills
			// 
			this.lstSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSkills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.lstSkills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstSkills.FormattingEnabled = true;
			this.lstSkills.HorizontalScrollbar = true;
			this.lstSkills.ItemHeight = 20;
			this.lstSkills.Location = new System.Drawing.Point(0, 0);
			this.lstSkills.Name = "lstSkills";
			this.lstSkills.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstSkills.Size = new System.Drawing.Size(381, 382);
			this.lstSkills.TabIndex = 28;
			this.lstSkills.DoubleClick += new System.EventHandler(this.lstSkills_DoubleClick);
			this.lstSkills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.tabCombat);
			this.mainTabControl.Controls.Add(this.tabMap);
			this.mainTabControl.Controls.Add(this.tabItem);
			this.mainTabControl.Controls.Add(this.tabQuest);
			this.mainTabControl.Controls.Add(this.tabMisc);
			this.mainTabControl.Controls.Add(this.tabMisc2);
			this.mainTabControl.Controls.Add(this.tabOptions);
			this.mainTabControl.Controls.Add(this.tabOptions2);
			this.mainTabControl.Controls.Add(this.tabHunt);
			this.mainTabControl.Controls.Add(this.tabBots);
			this.mainTabControl.Controls.Add(this.tabInfo);
			this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabControl.Location = new System.Drawing.Point(0, 0);
			this.mainTabControl.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.Padding = new System.Drawing.Point(8, 5);
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(826, 499);
			this.mainTabControl.TabIndex = 146;
			this.mainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
			// 
			// tabCombat
			// 
			this.tabCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabCombat.Controls.Add(this.darkGroupBox20);
			this.tabCombat.Controls.Add(this.darkGroupBox19);
			this.tabCombat.Controls.Add(this.darkGroupBox17);
			this.tabCombat.Controls.Add(this.darkGroupBox18);
			this.tabCombat.Controls.Add(this.chkSkillCD);
			this.tabCombat.Controls.Add(this.btnAttack);
			this.tabCombat.Controls.Add(this.btnKill);
			this.tabCombat.Controls.Add(this.chkExistQuest);
			this.tabCombat.Controls.Add(this.chkExitRest);
			this.tabCombat.Controls.Add(this.chkAllSkillsCD);
			this.tabCombat.Controls.Add(this.txtMonster);
			this.tabCombat.Controls.Add(this.boxSkillSet);
			this.tabCombat.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabCombat.Location = new System.Drawing.Point(4, 33);
			this.tabCombat.Margin = new System.Windows.Forms.Padding(0);
			this.tabCombat.Name = "tabCombat";
			this.tabCombat.Padding = new System.Windows.Forms.Padding(3);
			this.tabCombat.Size = new System.Drawing.Size(818, 462);
			this.tabCombat.TabIndex = 0;
			this.tabCombat.Text = "Combat";
			// 
			// darkGroupBox20
			// 
			this.darkGroupBox20.Controls.Add(this.txtSkillSet);
			this.darkGroupBox20.Controls.Add(this.btnAddSkillSet);
			this.darkGroupBox20.Controls.Add(this.btnUseSkillSet);
			this.darkGroupBox20.Location = new System.Drawing.Point(583, 5);
			this.darkGroupBox20.Name = "darkGroupBox20";
			this.darkGroupBox20.Size = new System.Drawing.Size(206, 146);
			this.darkGroupBox20.TabIndex = 77;
			this.darkGroupBox20.TabStop = false;
			this.darkGroupBox20.Text = "Specific Skill Set";
			// 
			// txtSkillSet
			// 
			this.txtSkillSet.Location = new System.Drawing.Point(13, 26);
			this.txtSkillSet.Multiline = true;
			this.txtSkillSet.Name = "txtSkillSet";
			this.txtSkillSet.Size = new System.Drawing.Size(177, 32);
			this.txtSkillSet.TabIndex = 63;
			this.txtSkillSet.Text = "Skill Set Name";
			this.txtSkillSet.WordWrap = false;
			this.txtSkillSet.Click += new System.EventHandler(this.TextboxEnter);
			this.txtSkillSet.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtSkillSet.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnAddSkillSet
			// 
			this.btnAddSkillSet.Checked = false;
			this.btnAddSkillSet.Location = new System.Drawing.Point(13, 61);
			this.btnAddSkillSet.Name = "btnAddSkillSet";
			this.btnAddSkillSet.Size = new System.Drawing.Size(177, 33);
			this.btnAddSkillSet.TabIndex = 64;
			this.btnAddSkillSet.Text = "Add Skill Set";
			this.btnAddSkillSet.Click += new System.EventHandler(this.btnAddSkillSet_Click);
			// 
			// btnUseSkillSet
			// 
			this.btnUseSkillSet.Checked = false;
			this.btnUseSkillSet.Location = new System.Drawing.Point(13, 96);
			this.btnUseSkillSet.Name = "btnUseSkillSet";
			this.btnUseSkillSet.Size = new System.Drawing.Size(177, 33);
			this.btnUseSkillSet.TabIndex = 65;
			this.btnUseSkillSet.Text = "Use Skill Set";
			this.btnUseSkillSet.Click += new System.EventHandler(this.btnUseSkillSet_Click);
			// 
			// darkGroupBox19
			// 
			this.darkGroupBox19.Controls.Add(this.btnRest);
			this.darkGroupBox19.Controls.Add(this.chkHP);
			this.darkGroupBox19.Controls.Add(this.numRest);
			this.darkGroupBox19.Controls.Add(this.chkMP);
			this.darkGroupBox19.Controls.Add(this.btnRestF);
			this.darkGroupBox19.Controls.Add(this.numRestMP);
			this.darkGroupBox19.Controls.Add(this.label10);
			this.darkGroupBox19.Controls.Add(this.label12);
			this.darkGroupBox19.Controls.Add(this.label11);
			this.darkGroupBox19.Location = new System.Drawing.Point(583, 161);
			this.darkGroupBox19.Name = "darkGroupBox19";
			this.darkGroupBox19.Size = new System.Drawing.Size(206, 160);
			this.darkGroupBox19.TabIndex = 77;
			this.darkGroupBox19.TabStop = false;
			this.darkGroupBox19.Text = "Rest";
			// 
			// btnRest
			// 
			this.btnRest.Checked = false;
			this.btnRest.Location = new System.Drawing.Point(13, 25);
			this.btnRest.Name = "btnRest";
			this.btnRest.Size = new System.Drawing.Size(69, 33);
			this.btnRest.TabIndex = 43;
			this.btnRest.Text = "Rest";
			this.btnRest.Click += new System.EventHandler(this.btnRest_Click);
			// 
			// chkHP
			// 
			this.chkHP.AutoSize = true;
			this.chkHP.Location = new System.Drawing.Point(16, 90);
			this.chkHP.Name = "chkHP";
			this.chkHP.Size = new System.Drawing.Size(79, 24);
			this.chkHP.TabIndex = 47;
			this.chkHP.Text = "HP <=";
			// 
			// numRest
			// 
			this.numRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numRest.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numRest.Location = new System.Drawing.Point(100, 89);
			this.numRest.LoopValues = false;
			this.numRest.Name = "numRest";
			this.numRest.Size = new System.Drawing.Size(61, 28);
			this.numRest.TabIndex = 48;
			this.numRest.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// chkMP
			// 
			this.chkMP.AutoSize = true;
			this.chkMP.Location = new System.Drawing.Point(16, 125);
			this.chkMP.Name = "chkMP";
			this.chkMP.Size = new System.Drawing.Size(80, 24);
			this.chkMP.TabIndex = 49;
			this.chkMP.Text = "MP <=";
			// 
			// btnRestF
			// 
			this.btnRestF.Checked = false;
			this.btnRestF.Location = new System.Drawing.Point(88, 25);
			this.btnRestF.Name = "btnRestF";
			this.btnRestF.Size = new System.Drawing.Size(107, 33);
			this.btnRestF.TabIndex = 44;
			this.btnRestF.Text = "Rest fully";
			this.btnRestF.Click += new System.EventHandler(this.btnRestF_Click);
			// 
			// numRestMP
			// 
			this.numRestMP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numRestMP.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numRestMP.Location = new System.Drawing.Point(100, 123);
			this.numRestMP.LoopValues = false;
			this.numRestMP.Name = "numRestMP";
			this.numRestMP.Size = new System.Drawing.Size(61, 28);
			this.numRestMP.TabIndex = 50;
			this.numRestMP.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label10.Location = new System.Drawing.Point(167, 94);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(23, 20);
			this.label10.TabIndex = 55;
			this.label10.Text = "%";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label12.Location = new System.Drawing.Point(12, 67);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(55, 20);
			this.label12.TabIndex = 57;
			this.label12.Text = "Rest if";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label11.Location = new System.Drawing.Point(168, 126);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(23, 20);
			this.label11.TabIndex = 56;
			this.label11.Text = "%";
			// 
			// darkGroupBox17
			// 
			this.darkGroupBox17.Controls.Add(this.rbItems);
			this.darkGroupBox17.Controls.Add(this.rbTemp);
			this.darkGroupBox17.Controls.Add(this.txtKillFMon);
			this.darkGroupBox17.Controls.Add(this.txtKillFItem);
			this.darkGroupBox17.Controls.Add(this.txtKillFQ);
			this.darkGroupBox17.Controls.Add(this.chkAddToWhiteList);
			this.darkGroupBox17.Controls.Add(this.btnKillF);
			this.darkGroupBox17.Location = new System.Drawing.Point(6, 89);
			this.darkGroupBox17.Name = "darkGroupBox17";
			this.darkGroupBox17.Size = new System.Drawing.Size(243, 245);
			this.darkGroupBox17.TabIndex = 77;
			this.darkGroupBox17.TabStop = false;
			this.darkGroupBox17.Text = "Kill for items";
			// 
			// rbItems
			// 
			this.rbItems.AutoSize = true;
			this.rbItems.Checked = true;
			this.rbItems.ForeColor = System.Drawing.Color.Gainsboro;
			this.rbItems.Location = new System.Drawing.Point(12, 26);
			this.rbItems.Name = "rbItems";
			this.rbItems.Size = new System.Drawing.Size(74, 24);
			this.rbItems.TabIndex = 30;
			this.rbItems.TabStop = true;
			this.rbItems.Text = "Items";
			// 
			// rbTemp
			// 
			this.rbTemp.AutoSize = true;
			this.rbTemp.ForeColor = System.Drawing.Color.Gainsboro;
			this.rbTemp.Location = new System.Drawing.Point(109, 26);
			this.rbTemp.Name = "rbTemp";
			this.rbTemp.Size = new System.Drawing.Size(116, 24);
			this.rbTemp.TabIndex = 31;
			this.rbTemp.Text = "Temp items";
			// 
			// txtKillFMon
			// 
			this.txtKillFMon.Location = new System.Drawing.Point(12, 93);
			this.txtKillFMon.Multiline = true;
			this.txtKillFMon.Name = "txtKillFMon";
			this.txtKillFMon.Size = new System.Drawing.Size(213, 32);
			this.txtKillFMon.TabIndex = 32;
			this.txtKillFMon.Text = "Monster (* = random)";
			this.txtKillFMon.WordWrap = false;
			this.txtKillFMon.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtKillFMon.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtKillFItem
			// 
			this.txtKillFItem.Location = new System.Drawing.Point(12, 129);
			this.txtKillFItem.Multiline = true;
			this.txtKillFItem.Name = "txtKillFItem";
			this.txtKillFItem.Size = new System.Drawing.Size(213, 32);
			this.txtKillFItem.TabIndex = 33;
			this.txtKillFItem.Text = "Item name";
			this.txtKillFItem.WordWrap = false;
			this.txtKillFItem.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtKillFItem.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtKillFQ
			// 
			this.txtKillFQ.Location = new System.Drawing.Point(12, 165);
			this.txtKillFQ.Multiline = true;
			this.txtKillFQ.Name = "txtKillFQ";
			this.txtKillFQ.Size = new System.Drawing.Size(213, 32);
			this.txtKillFQ.TabIndex = 34;
			this.txtKillFQ.Text = "Quantity (* = any)";
			this.txtKillFQ.WordWrap = false;
			this.txtKillFQ.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtKillFQ.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// chkAddToWhiteList
			// 
			this.chkAddToWhiteList.AutoSize = true;
			this.chkAddToWhiteList.Checked = true;
			this.chkAddToWhiteList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAddToWhiteList.Location = new System.Drawing.Point(12, 59);
			this.chkAddToWhiteList.Name = "chkAddToWhiteList";
			this.chkAddToWhiteList.Size = new System.Drawing.Size(144, 24);
			this.chkAddToWhiteList.TabIndex = 68;
			this.chkAddToWhiteList.Text = "add to Whitelist";
			// 
			// btnKillF
			// 
			this.btnKillF.Checked = false;
			this.btnKillF.Location = new System.Drawing.Point(12, 203);
			this.btnKillF.Name = "btnKillF";
			this.btnKillF.Size = new System.Drawing.Size(213, 33);
			this.btnKillF.TabIndex = 29;
			this.btnKillF.Text = "Kill for [KF]";
			this.btnKillF.Click += new System.EventHandler(this.btnKillF_Click);
			// 
			// darkGroupBox18
			// 
			this.darkGroupBox18.Controls.Add(this.btnLeaveCombat);
			this.darkGroupBox18.Controls.Add(this.btnStopAttack);
			this.darkGroupBox18.Controls.Add(this.numSkillCmd);
			this.darkGroupBox18.Controls.Add(this.cbSkillCmdWait);
			this.darkGroupBox18.Controls.Add(this.txtMonsterSkillCmd);
			this.darkGroupBox18.Controls.Add(this.btnSkillCmd);
			this.darkGroupBox18.Controls.Add(this.chkForceSkill);
			this.darkGroupBox18.Location = new System.Drawing.Point(278, 195);
			this.darkGroupBox18.Name = "darkGroupBox18";
			this.darkGroupBox18.Size = new System.Drawing.Size(270, 177);
			this.darkGroupBox18.TabIndex = 76;
			this.darkGroupBox18.TabStop = false;
			this.darkGroupBox18.Text = "Skill Command";
			// 
			// btnLeaveCombat
			// 
			this.btnLeaveCombat.Checked = false;
			this.btnLeaveCombat.Location = new System.Drawing.Point(9, 136);
			this.btnLeaveCombat.Name = "btnLeaveCombat";
			this.btnLeaveCombat.Size = new System.Drawing.Size(136, 33);
			this.btnLeaveCombat.TabIndex = 76;
			this.btnLeaveCombat.Text = "Leave combat";
			this.btnLeaveCombat.Click += new System.EventHandler(this.btnLeaveCombat_Click);
			// 
			// btnStopAttack
			// 
			this.btnStopAttack.Checked = false;
			this.btnStopAttack.Location = new System.Drawing.Point(8, 98);
			this.btnStopAttack.Name = "btnStopAttack";
			this.btnStopAttack.Size = new System.Drawing.Size(137, 33);
			this.btnStopAttack.TabIndex = 74;
			this.btnStopAttack.Text = "Stop attack";
			this.btnStopAttack.Click += new System.EventHandler(this.btnStopAttack_Click);
			// 
			// numSkillCmd
			// 
			this.numSkillCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSkillCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSkillCmd.Location = new System.Drawing.Point(8, 62);
			this.numSkillCmd.LoopValues = false;
			this.numSkillCmd.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numSkillCmd.Name = "numSkillCmd";
			this.numSkillCmd.Size = new System.Drawing.Size(67, 28);
			this.numSkillCmd.TabIndex = 73;
			this.numSkillCmd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cbSkillCmdWait
			// 
			this.cbSkillCmdWait.AutoSize = true;
			this.cbSkillCmdWait.Location = new System.Drawing.Point(185, 27);
			this.cbSkillCmdWait.Name = "cbSkillCmdWait";
			this.cbSkillCmdWait.Size = new System.Drawing.Size(67, 24);
			this.cbSkillCmdWait.TabIndex = 75;
			this.cbSkillCmdWait.Text = "Wait";
			// 
			// txtMonsterSkillCmd
			// 
			this.txtMonsterSkillCmd.Location = new System.Drawing.Point(8, 24);
			this.txtMonsterSkillCmd.Multiline = true;
			this.txtMonsterSkillCmd.Name = "txtMonsterSkillCmd";
			this.txtMonsterSkillCmd.Size = new System.Drawing.Size(171, 32);
			this.txtMonsterSkillCmd.TabIndex = 74;
			this.txtMonsterSkillCmd.Text = "Monster (* = random)";
			this.txtMonsterSkillCmd.WordWrap = false;
			this.txtMonsterSkillCmd.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtMonsterSkillCmd.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnSkillCmd
			// 
			this.btnSkillCmd.Checked = false;
			this.btnSkillCmd.Location = new System.Drawing.Point(82, 60);
			this.btnSkillCmd.Name = "btnSkillCmd";
			this.btnSkillCmd.Size = new System.Drawing.Size(63, 33);
			this.btnSkillCmd.TabIndex = 38;
			this.btnSkillCmd.Text = "Add";
			this.btnSkillCmd.Click += new System.EventHandler(this.btnAddSkillCmd_Click);
			// 
			// chkForceSkill
			// 
			this.chkForceSkill.AutoSize = true;
			this.chkForceSkill.Location = new System.Drawing.Point(186, 63);
			this.chkForceSkill.Name = "chkForceSkill";
			this.chkForceSkill.Size = new System.Drawing.Size(76, 24);
			this.chkForceSkill.TabIndex = 73;
			this.chkForceSkill.Text = "Force";
			this.chkForceSkill.MouseHover += new System.EventHandler(this.chkUseSkillTargeted_MouseHover);
			// 
			// chkSkillCD
			// 
			this.chkSkillCD.AutoSize = true;
			this.chkSkillCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkSkillCD.Location = new System.Drawing.Point(10, 388);
			this.chkSkillCD.Name = "chkSkillCD";
			this.chkSkillCD.Size = new System.Drawing.Size(208, 24);
			this.chkSkillCD.TabIndex = 60;
			this.chkSkillCD.Text = "Wait for skill to cooldown";
			// 
			// btnAttack
			// 
			this.btnAttack.Checked = false;
			this.btnAttack.Location = new System.Drawing.Point(123, 48);
			this.btnAttack.Name = "btnAttack";
			this.btnAttack.Size = new System.Drawing.Size(102, 33);
			this.btnAttack.TabIndex = 54;
			this.btnAttack.Text = "Attack";
			this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
			// 
			// btnKill
			// 
			this.btnKill.Checked = false;
			this.btnKill.Location = new System.Drawing.Point(10, 48);
			this.btnKill.Name = "btnKill";
			this.btnKill.Size = new System.Drawing.Size(107, 33);
			this.btnKill.TabIndex = 54;
			this.btnKill.Text = "Kill";
			this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
			// 
			// chkExistQuest
			// 
			this.chkExistQuest.AutoSize = true;
			this.chkExistQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkExistQuest.Location = new System.Drawing.Point(270, 424);
			this.chkExistQuest.Name = "chkExistQuest";
			this.chkExistQuest.Size = new System.Drawing.Size(293, 24);
			this.chkExistQuest.TabIndex = 51;
			this.chkExistQuest.Text = "Exit combat before completing quest";
			// 
			// chkExitRest
			// 
			this.chkExitRest.AutoSize = true;
			this.chkExitRest.Checked = true;
			this.chkExitRest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExitRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkExitRest.Location = new System.Drawing.Point(270, 388);
			this.chkExitRest.Name = "chkExitRest";
			this.chkExitRest.Size = new System.Drawing.Size(220, 24);
			this.chkExitRest.TabIndex = 36;
			this.chkExitRest.Text = "Exit combat before resting";
			// 
			// chkAllSkillsCD
			// 
			this.chkAllSkillsCD.AutoSize = true;
			this.chkAllSkillsCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkAllSkillsCD.Location = new System.Drawing.Point(10, 424);
			this.chkAllSkillsCD.Name = "chkAllSkillsCD";
			this.chkAllSkillsCD.Size = new System.Drawing.Size(239, 24);
			this.chkAllSkillsCD.TabIndex = 35;
			this.chkAllSkillsCD.Text = "Wait for all skills to cool down";
			// 
			// txtMonster
			// 
			this.txtMonster.Location = new System.Drawing.Point(10, 12);
			this.txtMonster.Multiline = true;
			this.txtMonster.Name = "txtMonster";
			this.txtMonster.Size = new System.Drawing.Size(215, 32);
			this.txtMonster.TabIndex = 27;
			this.txtMonster.Text = "Monster (* = random)";
			this.txtMonster.WordWrap = false;
			this.txtMonster.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtMonster.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// boxSkillSet
			// 
			this.boxSkillSet.Controls.Add(this.darkLabel7);
			this.boxSkillSet.Controls.Add(this.numSkill);
			this.boxSkillSet.Controls.Add(this.btnAddSkill);
			this.boxSkillSet.Controls.Add(this.btnAddSafe);
			this.boxSkillSet.Controls.Add(this.btnAllSkill);
			this.boxSkillSet.Controls.Add(this.numSafe);
			this.boxSkillSet.Controls.Add(this.label2);
			this.boxSkillSet.Controls.Add(this.label17);
			this.boxSkillSet.Controls.Add(this.chkSafeMp);
			this.boxSkillSet.Controls.Add(this.cbSafeType);
			this.boxSkillSet.Controls.Add(this.label13);
			this.boxSkillSet.Controls.Add(this.numSkillD);
			this.boxSkillSet.Location = new System.Drawing.Point(278, 6);
			this.boxSkillSet.Name = "boxSkillSet";
			this.boxSkillSet.Size = new System.Drawing.Size(270, 177);
			this.boxSkillSet.TabIndex = 75;
			this.boxSkillSet.TabStop = false;
			this.boxSkillSet.Text = "Skill Set";
			// 
			// darkLabel7
			// 
			this.darkLabel7.AutoSize = true;
			this.darkLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel7.Location = new System.Drawing.Point(168, 145);
			this.darkLabel7.Name = "darkLabel7";
			this.darkLabel7.Size = new System.Drawing.Size(30, 20);
			this.darkLabel7.TabIndex = 73;
			this.darkLabel7.Text = "ms";
			// 
			// numSkill
			// 
			this.numSkill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSkill.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSkill.Location = new System.Drawing.Point(9, 25);
			this.numSkill.LoopValues = false;
			this.numSkill.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numSkill.Name = "numSkill";
			this.numSkill.Size = new System.Drawing.Size(66, 28);
			this.numSkill.TabIndex = 37;
			this.numSkill.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnAddSkill
			// 
			this.btnAddSkill.Checked = false;
			this.btnAddSkill.Location = new System.Drawing.Point(82, 23);
			this.btnAddSkill.Name = "btnAddSkill";
			this.btnAddSkill.Size = new System.Drawing.Size(80, 33);
			this.btnAddSkill.TabIndex = 38;
			this.btnAddSkill.Text = "Add skill";
			this.btnAddSkill.Click += new System.EventHandler(this.btnAddSkill_Click);
			// 
			// btnAddSafe
			// 
			this.btnAddSafe.Checked = false;
			this.btnAddSafe.Location = new System.Drawing.Point(9, 64);
			this.btnAddSafe.Name = "btnAddSafe";
			this.btnAddSafe.Size = new System.Drawing.Size(105, 33);
			this.btnAddSafe.TabIndex = 39;
			this.btnAddSafe.Text = "Safe skill";
			this.btnAddSafe.Click += new System.EventHandler(this.btnAddSafe_Click);
			// 
			// btnAllSkill
			// 
			this.btnAllSkill.Checked = false;
			this.btnAllSkill.Location = new System.Drawing.Point(168, 23);
			this.btnAllSkill.Name = "btnAllSkill";
			this.btnAllSkill.Size = new System.Drawing.Size(56, 33);
			this.btnAllSkill.TabIndex = 72;
			this.btnAllSkill.Text = "All";
			this.btnAllSkill.Click += new System.EventHandler(this.btnAllSkill_Click);
			// 
			// numSafe
			// 
			this.numSafe.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSafe.Location = new System.Drawing.Point(168, 104);
			this.numSafe.LoopValues = false;
			this.numSafe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSafe.Name = "numSafe";
			this.numSafe.Size = new System.Drawing.Size(51, 26);
			this.numSafe.TabIndex = 41;
			this.numSafe.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label2.Location = new System.Drawing.Point(220, 110);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 20);
			this.label2.TabIndex = 42;
			this.label2.Text = "%";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label17.Location = new System.Drawing.Point(120, 71);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(49, 20);
			this.label17.TabIndex = 62;
			this.label17.Text = "HP or";
			// 
			// chkSafeMp
			// 
			this.chkSafeMp.AutoSize = true;
			this.chkSafeMp.Location = new System.Drawing.Point(175, 69);
			this.chkSafeMp.Name = "chkSafeMp";
			this.chkSafeMp.Size = new System.Drawing.Size(58, 24);
			this.chkSafeMp.TabIndex = 58;
			this.chkSafeMp.Text = "MP";
			// 
			// cbSafeType
			// 
			this.cbSafeType.FormattingEnabled = true;
			this.cbSafeType.ItemHeight = 24;
			this.cbSafeType.Items.AddRange(new object[] {
            "<= Lower than",
            ">= Greater than"});
			this.cbSafeType.Location = new System.Drawing.Point(9, 103);
			this.cbSafeType.Name = "cbSafeType";
			this.cbSafeType.Size = new System.Drawing.Size(153, 30);
			this.cbSafeType.TabIndex = 67;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label13.Location = new System.Drawing.Point(5, 143);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(78, 20);
			this.label13.TabIndex = 53;
			this.label13.Text = "Skill delay";
			// 
			// numSkillD
			// 
			this.numSkillD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numSkillD.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSkillD.Location = new System.Drawing.Point(84, 139);
			this.numSkillD.LoopValues = false;
			this.numSkillD.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numSkillD.Name = "numSkillD";
			this.numSkillD.Size = new System.Drawing.Size(78, 26);
			this.numSkillD.TabIndex = 45;
			// 
			// tabMap
			// 
			this.tabMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabMap.Controls.Add(this.darkLabel1);
			this.tabMap.Controls.Add(this.cbJoinMapSwf);
			this.tabMap.Controls.Add(this.tbJoinMapSwf);
			this.tabMap.Controls.Add(this.darkLabel10);
			this.tabMap.Controls.Add(this.numJoinTry);
			this.tabMap.Controls.Add(this.btnSetSpawn);
			this.tabMap.Controls.Add(this.btnWalkRdm);
			this.tabMap.Controls.Add(this.btnWalkCur);
			this.tabMap.Controls.Add(this.btnWalk);
			this.tabMap.Controls.Add(this.numWalkY);
			this.tabMap.Controls.Add(this.numWalkX);
			this.tabMap.Controls.Add(this.button2);
			this.tabMap.Controls.Add(this.btnCellSwap);
			this.tabMap.Controls.Add(this.btnJump);
			this.tabMap.Controls.Add(this.btnCurrCell);
			this.tabMap.Controls.Add(this.txtPad);
			this.tabMap.Controls.Add(this.txtCell);
			this.tabMap.Controls.Add(this.btnJoin);
			this.tabMap.Controls.Add(this.txtJoinPad);
			this.tabMap.Controls.Add(this.txtJoinCell);
			this.tabMap.Controls.Add(this.txtJoin);
			this.tabMap.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMap.Location = new System.Drawing.Point(4, 33);
			this.tabMap.Margin = new System.Windows.Forms.Padding(0);
			this.tabMap.Name = "tabMap";
			this.tabMap.Padding = new System.Windows.Forms.Padding(3);
			this.tabMap.Size = new System.Drawing.Size(818, 462);
			this.tabMap.TabIndex = 2;
			this.tabMap.Text = "Map";
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(8, 129);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(115, 20);
			this.darkLabel1.TabIndex = 175;
			this.darkLabel1.Text = "Join attempt(s)";
			// 
			// cbJoinMapSwf
			// 
			this.cbJoinMapSwf.AutoSize = true;
			this.cbJoinMapSwf.Location = new System.Drawing.Point(13, 92);
			this.cbJoinMapSwf.Name = "cbJoinMapSwf";
			this.cbJoinMapSwf.Size = new System.Drawing.Size(22, 21);
			this.cbJoinMapSwf.TabIndex = 174;
			this.cbJoinMapSwf.CheckedChanged += new System.EventHandler(this.cbJoinMapSwf_CheckedChanged);
			// 
			// tbJoinMapSwf
			// 
			this.tbJoinMapSwf.Enabled = false;
			this.tbJoinMapSwf.Location = new System.Drawing.Point(41, 87);
			this.tbJoinMapSwf.Multiline = true;
			this.tbJoinMapSwf.Name = "tbJoinMapSwf";
			this.tbJoinMapSwf.Size = new System.Drawing.Size(138, 32);
			this.tbJoinMapSwf.TabIndex = 146;
			this.tbJoinMapSwf.Text = "Map file (.swf)";
			this.tbJoinMapSwf.WordWrap = false;
			this.tbJoinMapSwf.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbJoinMapSwf.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// darkLabel10
			// 
			this.darkLabel10.AutoSize = true;
			this.darkLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel10.Location = new System.Drawing.Point(185, 133);
			this.darkLabel10.Name = "darkLabel10";
			this.darkLabel10.Size = new System.Drawing.Size(16, 20);
			this.darkLabel10.TabIndex = 145;
			this.darkLabel10.Text = "x";
			// 
			// numJoinTry
			// 
			this.numJoinTry.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numJoinTry.Location = new System.Drawing.Point(129, 127);
			this.numJoinTry.LoopValues = false;
			this.numJoinTry.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numJoinTry.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numJoinTry.Name = "numJoinTry";
			this.numJoinTry.Size = new System.Drawing.Size(50, 26);
			this.numJoinTry.TabIndex = 144;
			this.numJoinTry.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnSetSpawn
			// 
			this.btnSetSpawn.Checked = false;
			this.btnSetSpawn.Location = new System.Drawing.Point(292, 120);
			this.btnSetSpawn.MinimumSize = new System.Drawing.Size(111, 22);
			this.btnSetSpawn.Name = "btnSetSpawn";
			this.btnSetSpawn.Size = new System.Drawing.Size(145, 33);
			this.btnSetSpawn.TabIndex = 142;
			this.btnSetSpawn.Text = "Set Spawnpoint";
			this.btnSetSpawn.Click += new System.EventHandler(this.btnSetSpawn_Click);
			// 
			// btnWalkRdm
			// 
			this.btnWalkRdm.Checked = false;
			this.btnWalkRdm.Location = new System.Drawing.Point(492, 121);
			this.btnWalkRdm.Name = "btnWalkRdm";
			this.btnWalkRdm.Size = new System.Drawing.Size(152, 33);
			this.btnWalkRdm.TabIndex = 142;
			this.btnWalkRdm.Text = "Walk Randomly";
			this.btnWalkRdm.Click += new System.EventHandler(this.btnWalkRdm_Click);
			// 
			// btnWalkCur
			// 
			this.btnWalkCur.Checked = false;
			this.btnWalkCur.Location = new System.Drawing.Point(492, 84);
			this.btnWalkCur.Name = "btnWalkCur";
			this.btnWalkCur.Size = new System.Drawing.Size(152, 33);
			this.btnWalkCur.TabIndex = 38;
			this.btnWalkCur.Text = "Current position";
			this.btnWalkCur.Click += new System.EventHandler(this.btnWalkCur_Click);
			// 
			// btnWalk
			// 
			this.btnWalk.Checked = false;
			this.btnWalk.Location = new System.Drawing.Point(492, 48);
			this.btnWalk.Name = "btnWalk";
			this.btnWalk.Size = new System.Drawing.Size(152, 33);
			this.btnWalk.TabIndex = 37;
			this.btnWalk.Text = "Walk to";
			this.btnWalk.Click += new System.EventHandler(this.btnWalk_Click);
			// 
			// numWalkY
			// 
			this.numWalkY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numWalkY.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numWalkY.Location = new System.Drawing.Point(571, 15);
			this.numWalkY.LoopValues = false;
			this.numWalkY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numWalkY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numWalkY.Name = "numWalkY";
			this.numWalkY.Size = new System.Drawing.Size(73, 28);
			this.numWalkY.TabIndex = 36;
			this.numWalkY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numWalkX
			// 
			this.numWalkX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numWalkX.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numWalkX.Location = new System.Drawing.Point(492, 15);
			this.numWalkX.LoopValues = false;
			this.numWalkX.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numWalkX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numWalkX.Name = "numWalkX";
			this.numWalkX.Size = new System.Drawing.Size(73, 28);
			this.numWalkX.TabIndex = 35;
			this.numWalkX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// button2
			// 
			this.button2.Checked = false;
			this.button2.Location = new System.Drawing.Point(205, 12);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(54, 32);
			this.button2.TabIndex = 34;
			this.button2.Text = ">";
			this.button2.Click += new System.EventHandler(this.btnCellSwap_Click);
			// 
			// btnCellSwap
			// 
			this.btnCellSwap.Checked = false;
			this.btnCellSwap.Location = new System.Drawing.Point(206, 49);
			this.btnCellSwap.Name = "btnCellSwap";
			this.btnCellSwap.Size = new System.Drawing.Size(53, 33);
			this.btnCellSwap.TabIndex = 34;
			this.btnCellSwap.Text = "<";
			this.btnCellSwap.Click += new System.EventHandler(this.btnCellSwap_Click);
			// 
			// btnJump
			// 
			this.btnJump.Checked = false;
			this.btnJump.Location = new System.Drawing.Point(292, 84);
			this.btnJump.Name = "btnJump";
			this.btnJump.Size = new System.Drawing.Size(145, 33);
			this.btnJump.TabIndex = 33;
			this.btnJump.Text = "Jump";
			this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
			// 
			// btnCurrCell
			// 
			this.btnCurrCell.Checked = false;
			this.btnCurrCell.Location = new System.Drawing.Point(292, 48);
			this.btnCurrCell.Name = "btnCurrCell";
			this.btnCurrCell.Size = new System.Drawing.Size(145, 33);
			this.btnCurrCell.TabIndex = 32;
			this.btnCurrCell.Text = "Get current cell";
			this.btnCurrCell.Click += new System.EventHandler(this.btnCurrCell_Click);
			// 
			// txtPad
			// 
			this.txtPad.Location = new System.Drawing.Point(367, 12);
			this.txtPad.Multiline = true;
			this.txtPad.Name = "txtPad";
			this.txtPad.Size = new System.Drawing.Size(70, 32);
			this.txtPad.TabIndex = 31;
			this.txtPad.Text = "Pad";
			this.txtPad.WordWrap = false;
			this.txtPad.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtPad.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtCell
			// 
			this.txtCell.Location = new System.Drawing.Point(291, 12);
			this.txtCell.Multiline = true;
			this.txtCell.Name = "txtCell";
			this.txtCell.Size = new System.Drawing.Size(70, 32);
			this.txtCell.TabIndex = 30;
			this.txtCell.Text = "Cell";
			this.txtCell.WordWrap = false;
			this.txtCell.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtCell.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnJoin
			// 
			this.btnJoin.Checked = false;
			this.btnJoin.Location = new System.Drawing.Point(6, 159);
			this.btnJoin.Name = "btnJoin";
			this.btnJoin.Size = new System.Drawing.Size(173, 33);
			this.btnJoin.TabIndex = 29;
			this.btnJoin.Text = "Join map";
			this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
			// 
			// txtJoinPad
			// 
			this.txtJoinPad.Location = new System.Drawing.Point(95, 48);
			this.txtJoinPad.Multiline = true;
			this.txtJoinPad.Name = "txtJoinPad";
			this.txtJoinPad.Size = new System.Drawing.Size(85, 32);
			this.txtJoinPad.TabIndex = 28;
			this.txtJoinPad.Text = "Spawn";
			this.txtJoinPad.WordWrap = false;
			this.txtJoinPad.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoinPad.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtJoinCell
			// 
			this.txtJoinCell.Location = new System.Drawing.Point(7, 48);
			this.txtJoinCell.Multiline = true;
			this.txtJoinCell.Name = "txtJoinCell";
			this.txtJoinCell.Size = new System.Drawing.Size(84, 32);
			this.txtJoinCell.TabIndex = 27;
			this.txtJoinCell.Text = "Enter";
			this.txtJoinCell.WordWrap = false;
			this.txtJoinCell.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoinCell.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtJoin
			// 
			this.txtJoin.Location = new System.Drawing.Point(6, 12);
			this.txtJoin.Multiline = true;
			this.txtJoin.Name = "txtJoin";
			this.txtJoin.Size = new System.Drawing.Size(174, 32);
			this.txtJoin.TabIndex = 26;
			this.txtJoin.Text = "battleon-1e99";
			this.txtJoin.WordWrap = false;
			this.txtJoin.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoin.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tabItem
			// 
			this.tabItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabItem.Controls.Add(this.darkGroupBox5);
			this.tabItem.Controls.Add(this.darkGroupBox4);
			this.tabItem.Controls.Add(this.darkGroupBox3);
			this.tabItem.Controls.Add(this.darkGroupBox2);
			this.tabItem.Controls.Add(this.btnBoost);
			this.tabItem.Controls.Add(this.cbBoosts);
			this.tabItem.Controls.Add(this.btnSwap);
			this.tabItem.Controls.Add(this.txtSwapInv);
			this.tabItem.Controls.Add(this.txtSwapBank);
			this.tabItem.Controls.Add(this.btnWhitelist);
			this.tabItem.Controls.Add(this.btnBoth);
			this.tabItem.Controls.Add(this.txtWhitelist);
			this.tabItem.Controls.Add(this.btnItem);
			this.tabItem.Controls.Add(this.btnUnbanklst);
			this.tabItem.Controls.Add(this.txtItem);
			this.tabItem.Controls.Add(this.cbItemCmds);
			this.tabItem.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabItem.Location = new System.Drawing.Point(4, 33);
			this.tabItem.Margin = new System.Windows.Forms.Padding(0);
			this.tabItem.Name = "tabItem";
			this.tabItem.Padding = new System.Windows.Forms.Padding(3);
			this.tabItem.Size = new System.Drawing.Size(818, 462);
			this.tabItem.TabIndex = 1;
			this.tabItem.Text = "Item";
			// 
			// darkGroupBox5
			// 
			this.darkGroupBox5.Controls.Add(this.btnMapItemExe);
			this.darkGroupBox5.Controls.Add(this.btnMapItem);
			this.darkGroupBox5.Controls.Add(this.numMapItem);
			this.darkGroupBox5.Location = new System.Drawing.Point(410, 6);
			this.darkGroupBox5.Name = "darkGroupBox5";
			this.darkGroupBox5.Size = new System.Drawing.Size(153, 103);
			this.darkGroupBox5.TabIndex = 161;
			this.darkGroupBox5.TabStop = false;
			this.darkGroupBox5.Text = "Map Item";
			// 
			// btnMapItemExe
			// 
			this.btnMapItemExe.Checked = false;
			this.btnMapItemExe.Location = new System.Drawing.Point(78, 58);
			this.btnMapItemExe.Name = "btnMapItemExe";
			this.btnMapItemExe.Size = new System.Drawing.Size(65, 33);
			this.btnMapItemExe.TabIndex = 31;
			this.btnMapItemExe.Text = "Get";
			this.btnMapItemExe.Click += new System.EventHandler(this.btnMapItemExe_Click);
			// 
			// btnMapItem
			// 
			this.btnMapItem.Checked = false;
			this.btnMapItem.Location = new System.Drawing.Point(10, 58);
			this.btnMapItem.Name = "btnMapItem";
			this.btnMapItem.Size = new System.Drawing.Size(62, 33);
			this.btnMapItem.TabIndex = 29;
			this.btnMapItem.Text = "Cmd";
			this.btnMapItem.Click += new System.EventHandler(this.btnMapItem_Click);
			// 
			// numMapItem
			// 
			this.numMapItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numMapItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numMapItem.Location = new System.Drawing.Point(10, 26);
			this.numMapItem.LoopValues = false;
			this.numMapItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numMapItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numMapItem.Name = "numMapItem";
			this.numMapItem.Size = new System.Drawing.Size(133, 28);
			this.numMapItem.TabIndex = 30;
			this.numMapItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// darkGroupBox4
			// 
			this.darkGroupBox4.Controls.Add(this.chkInventOnStart);
			this.darkGroupBox4.Controls.Add(this.chkPickupAll);
			this.darkGroupBox4.Controls.Add(this.chkPickup);
			this.darkGroupBox4.Controls.Add(this.chkReject);
			this.darkGroupBox4.Controls.Add(this.chkPickupAcTag);
			this.darkGroupBox4.Controls.Add(this.chkBankOnStop);
			this.darkGroupBox4.Location = new System.Drawing.Point(580, 6);
			this.darkGroupBox4.Name = "darkGroupBox4";
			this.darkGroupBox4.Size = new System.Drawing.Size(231, 243);
			this.darkGroupBox4.TabIndex = 160;
			this.darkGroupBox4.TabStop = false;
			this.darkGroupBox4.Text = "Options";
			// 
			// chkInventOnStart
			// 
			this.chkInventOnStart.AutoSize = true;
			this.chkInventOnStart.Checked = true;
			this.chkInventOnStart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkInventOnStart.Location = new System.Drawing.Point(16, 161);
			this.chkInventOnStart.Name = "chkInventOnStart";
			this.chkInventOnStart.Size = new System.Drawing.Size(202, 24);
			this.chkInventOnStart.TabIndex = 158;
			this.chkInventOnStart.Text = "Items to Invent on Start";
			// 
			// chkPickupAll
			// 
			this.chkPickupAll.AutoSize = true;
			this.chkPickupAll.Location = new System.Drawing.Point(16, 28);
			this.chkPickupAll.Name = "chkPickupAll";
			this.chkPickupAll.Size = new System.Drawing.Size(147, 24);
			this.chkPickupAll.TabIndex = 149;
			this.chkPickupAll.Text = "Pick up all items";
			// 
			// chkPickup
			// 
			this.chkPickup.AutoSize = true;
			this.chkPickup.Checked = true;
			this.chkPickup.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPickup.Location = new System.Drawing.Point(16, 60);
			this.chkPickup.Name = "chkPickup";
			this.chkPickup.Size = new System.Drawing.Size(164, 24);
			this.chkPickup.TabIndex = 24;
			this.chkPickup.Text = "Pick up whitelisted";
			// 
			// chkReject
			// 
			this.chkReject.AutoSize = true;
			this.chkReject.Location = new System.Drawing.Point(16, 127);
			this.chkReject.Name = "chkReject";
			this.chkReject.Size = new System.Drawing.Size(191, 24);
			this.chkReject.TabIndex = 25;
			this.chkReject.Text = "Reject non-whitelisted";
			// 
			// chkPickupAcTag
			// 
			this.chkPickupAcTag.AutoSize = true;
			this.chkPickupAcTag.Location = new System.Drawing.Point(16, 93);
			this.chkPickupAcTag.Name = "chkPickupAcTag";
			this.chkPickupAcTag.Size = new System.Drawing.Size(194, 24);
			this.chkPickupAcTag.TabIndex = 157;
			this.chkPickupAcTag.Text = "Pick up ac-tagged only";
			// 
			// chkBankOnStop
			// 
			this.chkBankOnStop.AutoSize = true;
			this.chkBankOnStop.Location = new System.Drawing.Point(16, 195);
			this.chkBankOnStop.Name = "chkBankOnStop";
			this.chkBankOnStop.Size = new System.Drawing.Size(198, 24);
			this.chkBankOnStop.TabIndex = 148;
			this.chkBankOnStop.Text = "Items to Bank on Stop ";
			this.chkBankOnStop.CheckedChanged += new System.EventHandler(this.chkBankOnStop_CheckedChanged);
			// 
			// darkGroupBox3
			// 
			this.darkGroupBox3.Controls.Add(this.radBuyByName);
			this.darkGroupBox3.Controls.Add(this.radBuyByID);
			this.darkGroupBox3.Controls.Add(this.tbShopId);
			this.darkGroupBox3.Controls.Add(this.tbShopItemId);
			this.darkGroupBox3.Controls.Add(this.tbItemId);
			this.darkGroupBox3.Controls.Add(this.label1);
			this.darkGroupBox3.Controls.Add(this.numBuyQty);
			this.darkGroupBox3.Controls.Add(this.btnLoadShop);
			this.darkGroupBox3.Controls.Add(this.tbShopItemName);
			this.darkGroupBox3.Controls.Add(this.btnBuy);
			this.darkGroupBox3.Controls.Add(this.btnBuyFast);
			this.darkGroupBox3.Location = new System.Drawing.Point(11, 237);
			this.darkGroupBox3.Name = "darkGroupBox3";
			this.darkGroupBox3.Size = new System.Drawing.Size(228, 212);
			this.darkGroupBox3.TabIndex = 159;
			this.darkGroupBox3.TabStop = false;
			this.darkGroupBox3.Text = "Shop";
			// 
			// radBuyByName
			// 
			this.radBuyByName.AutoSize = true;
			this.radBuyByName.Checked = true;
			this.radBuyByName.ForeColor = System.Drawing.Color.Gainsboro;
			this.radBuyByName.Location = new System.Drawing.Point(10, 69);
			this.radBuyByName.Name = "radBuyByName";
			this.radBuyByName.Size = new System.Drawing.Size(21, 20);
			this.radBuyByName.TabIndex = 156;
			this.radBuyByName.TabStop = true;
			this.radBuyByName.CheckedChanged += new System.EventHandler(this.radBuyByName_CheckedChanged);
			// 
			// radBuyByID
			// 
			this.radBuyByID.AutoSize = true;
			this.radBuyByID.ForeColor = System.Drawing.Color.Gainsboro;
			this.radBuyByID.Location = new System.Drawing.Point(10, 106);
			this.radBuyByID.Name = "radBuyByID";
			this.radBuyByID.Size = new System.Drawing.Size(21, 20);
			this.radBuyByID.TabIndex = 155;
			// 
			// tbShopId
			// 
			this.tbShopId.Location = new System.Drawing.Point(8, 27);
			this.tbShopId.Multiline = true;
			this.tbShopId.Name = "tbShopId";
			this.tbShopId.Size = new System.Drawing.Size(128, 32);
			this.tbShopId.TabIndex = 154;
			this.tbShopId.Text = "Shop Id";
			this.tbShopId.WordWrap = false;
			this.tbShopId.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbShopId.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbShopItemId
			// 
			this.tbShopItemId.Enabled = false;
			this.tbShopItemId.Location = new System.Drawing.Point(109, 102);
			this.tbShopItemId.Multiline = true;
			this.tbShopItemId.Name = "tbShopItemId";
			this.tbShopItemId.Size = new System.Drawing.Size(110, 33);
			this.tbShopItemId.TabIndex = 153;
			this.tbShopItemId.Text = "Shop Item Id";
			this.tbShopItemId.WordWrap = false;
			this.tbShopItemId.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbShopItemId.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbItemId
			// 
			this.tbItemId.Enabled = false;
			this.tbItemId.Location = new System.Drawing.Point(33, 102);
			this.tbItemId.Multiline = true;
			this.tbItemId.Name = "tbItemId";
			this.tbItemId.Size = new System.Drawing.Size(73, 33);
			this.tbItemId.TabIndex = 151;
			this.tbItemId.Text = "Item Id";
			this.tbItemId.WordWrap = false;
			this.tbItemId.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbItemId.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 20);
			this.label1.TabIndex = 136;
			this.label1.Text = "Qty";
			// 
			// numBuyQty
			// 
			this.numBuyQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numBuyQty.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBuyQty.Location = new System.Drawing.Point(48, 139);
			this.numBuyQty.LoopValues = false;
			this.numBuyQty.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numBuyQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numBuyQty.Name = "numBuyQty";
			this.numBuyQty.Size = new System.Drawing.Size(58, 28);
			this.numBuyQty.TabIndex = 135;
			this.numBuyQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnLoadShop
			// 
			this.btnLoadShop.Checked = false;
			this.btnLoadShop.Location = new System.Drawing.Point(142, 27);
			this.btnLoadShop.Name = "btnLoadShop";
			this.btnLoadShop.Size = new System.Drawing.Size(77, 33);
			this.btnLoadShop.TabIndex = 134;
			this.btnLoadShop.Text = "Load";
			this.btnLoadShop.Click += new System.EventHandler(this.btnLoadShop_Click);
			// 
			// tbShopItemName
			// 
			this.tbShopItemName.Location = new System.Drawing.Point(33, 65);
			this.tbShopItemName.Multiline = true;
			this.tbShopItemName.Name = "tbShopItemName";
			this.tbShopItemName.Size = new System.Drawing.Size(186, 32);
			this.tbShopItemName.TabIndex = 41;
			this.tbShopItemName.Text = "Shop Item Name";
			this.tbShopItemName.WordWrap = false;
			this.tbShopItemName.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbShopItemName.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnBuy
			// 
			this.btnBuy.Checked = false;
			this.btnBuy.Location = new System.Drawing.Point(8, 172);
			this.btnBuy.Name = "btnBuy";
			this.btnBuy.Size = new System.Drawing.Size(98, 33);
			this.btnBuy.TabIndex = 38;
			this.btnBuy.Text = "Buy item";
			this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
			// 
			// btnBuyFast
			// 
			this.btnBuyFast.Checked = false;
			this.btnBuyFast.Location = new System.Drawing.Point(109, 172);
			this.btnBuyFast.Name = "btnBuyFast";
			this.btnBuyFast.Size = new System.Drawing.Size(110, 33);
			this.btnBuyFast.TabIndex = 133;
			this.btnBuyFast.Text = "Buy fast";
			this.btnBuyFast.Click += new System.EventHandler(this.btnBuyFast_Click);
			// 
			// darkGroupBox2
			// 
			this.darkGroupBox2.Controls.Add(this.btnWhitelistToggle);
			this.darkGroupBox2.Controls.Add(this.btnWhitelistOn);
			this.darkGroupBox2.Controls.Add(this.btnWhitelistOff);
			this.darkGroupBox2.Location = new System.Drawing.Point(410, 119);
			this.darkGroupBox2.Name = "darkGroupBox2";
			this.darkGroupBox2.Size = new System.Drawing.Size(153, 106);
			this.darkGroupBox2.TabIndex = 158;
			this.darkGroupBox2.TabStop = false;
			this.darkGroupBox2.Text = "Whitelist";
			// 
			// btnWhitelistToggle
			// 
			this.btnWhitelistToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistToggle.Checked = false;
			this.btnWhitelistToggle.Location = new System.Drawing.Point(10, 26);
			this.btnWhitelistToggle.Name = "btnWhitelistToggle";
			this.btnWhitelistToggle.Size = new System.Drawing.Size(133, 33);
			this.btnWhitelistToggle.TabIndex = 154;
			this.btnWhitelistToggle.Text = "Toggle";
			this.btnWhitelistToggle.Click += new System.EventHandler(this.btnWhitelistToggle_Click);
			// 
			// btnWhitelistOn
			// 
			this.btnWhitelistOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistOn.Checked = false;
			this.btnWhitelistOn.Location = new System.Drawing.Point(10, 63);
			this.btnWhitelistOn.Name = "btnWhitelistOn";
			this.btnWhitelistOn.Size = new System.Drawing.Size(62, 33);
			this.btnWhitelistOn.TabIndex = 155;
			this.btnWhitelistOn.Text = "On";
			this.btnWhitelistOn.Click += new System.EventHandler(this.btnWhitelistOn_Click);
			// 
			// btnWhitelistOff
			// 
			this.btnWhitelistOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistOff.Checked = false;
			this.btnWhitelistOff.Location = new System.Drawing.Point(78, 63);
			this.btnWhitelistOff.Name = "btnWhitelistOff";
			this.btnWhitelistOff.Size = new System.Drawing.Size(65, 33);
			this.btnWhitelistOff.TabIndex = 156;
			this.btnWhitelistOff.Text = "Off";
			this.btnWhitelistOff.Click += new System.EventHandler(this.btnWhitelistOff_Click);
			// 
			// btnBoost
			// 
			this.btnBoost.Checked = false;
			this.btnBoost.Location = new System.Drawing.Point(229, 191);
			this.btnBoost.Name = "btnBoost";
			this.btnBoost.Size = new System.Drawing.Size(155, 33);
			this.btnBoost.TabIndex = 37;
			this.btnBoost.Text = "Add boost";
			this.btnBoost.Click += new System.EventHandler(this.btnBoost_Click);
			// 
			// cbBoosts
			// 
			this.cbBoosts.FormattingEnabled = true;
			this.cbBoosts.ItemHeight = 27;
			this.cbBoosts.Location = new System.Drawing.Point(229, 156);
			this.cbBoosts.Name = "cbBoosts";
			this.cbBoosts.Size = new System.Drawing.Size(155, 33);
			this.cbBoosts.TabIndex = 36;
			this.cbBoosts.Click += new System.EventHandler(this.cbBoosts_Click);
			// 
			// btnSwap
			// 
			this.btnSwap.Checked = false;
			this.btnSwap.Location = new System.Drawing.Point(11, 191);
			this.btnSwap.Name = "btnSwap";
			this.btnSwap.Size = new System.Drawing.Size(180, 33);
			this.btnSwap.TabIndex = 28;
			this.btnSwap.Text = "Bank swap";
			this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
			// 
			// txtSwapInv
			// 
			this.txtSwapInv.Location = new System.Drawing.Point(11, 157);
			this.txtSwapInv.Multiline = true;
			this.txtSwapInv.Name = "txtSwapInv";
			this.txtSwapInv.Size = new System.Drawing.Size(180, 32);
			this.txtSwapInv.TabIndex = 27;
			this.txtSwapInv.Text = "Inventory item name";
			this.txtSwapInv.WordWrap = false;
			// 
			// txtSwapBank
			// 
			this.txtSwapBank.Location = new System.Drawing.Point(11, 123);
			this.txtSwapBank.Multiline = true;
			this.txtSwapBank.Name = "txtSwapBank";
			this.txtSwapBank.Size = new System.Drawing.Size(180, 32);
			this.txtSwapBank.TabIndex = 26;
			this.txtSwapBank.Text = "Bank item name";
			this.txtSwapBank.WordWrap = false;
			// 
			// btnWhitelist
			// 
			this.btnWhitelist.Checked = false;
			this.btnWhitelist.Location = new System.Drawing.Point(229, 44);
			this.btnWhitelist.Name = "btnWhitelist";
			this.btnWhitelist.Size = new System.Drawing.Size(155, 33);
			this.btnWhitelist.TabIndex = 23;
			this.btnWhitelist.Text = "Add to whitelist";
			this.btnWhitelist.Click += new System.EventHandler(this.btnWhitelist_Click);
			// 
			// btnBoth
			// 
			this.btnBoth.Checked = false;
			this.btnBoth.Location = new System.Drawing.Point(229, 115);
			this.btnBoth.Name = "btnBoth";
			this.btnBoth.Size = new System.Drawing.Size(155, 33);
			this.btnBoth.TabIndex = 23;
			this.btnBoth.Text = "Add to both";
			this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);
			// 
			// txtWhitelist
			// 
			this.txtWhitelist.Location = new System.Drawing.Point(229, 10);
			this.txtWhitelist.Multiline = true;
			this.txtWhitelist.Name = "txtWhitelist";
			this.txtWhitelist.Size = new System.Drawing.Size(155, 32);
			this.txtWhitelist.TabIndex = 22;
			this.txtWhitelist.Text = "Item name";
			this.txtWhitelist.WordWrap = false;
			this.txtWhitelist.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtWhitelist.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnItem
			// 
			this.btnItem.Checked = false;
			this.btnItem.Location = new System.Drawing.Point(11, 76);
			this.btnItem.Name = "btnItem";
			this.btnItem.Size = new System.Drawing.Size(180, 33);
			this.btnItem.TabIndex = 21;
			this.btnItem.Text = "Add command";
			this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
			// 
			// btnUnbanklst
			// 
			this.btnUnbanklst.Checked = false;
			this.btnUnbanklst.Location = new System.Drawing.Point(229, 79);
			this.btnUnbanklst.Name = "btnUnbanklst";
			this.btnUnbanklst.Size = new System.Drawing.Size(155, 33);
			this.btnUnbanklst.TabIndex = 147;
			this.btnUnbanklst.Text = "Add to Unbank list";
			this.btnUnbanklst.Click += new System.EventHandler(this.btnUnbanklst_Click);
			// 
			// txtItem
			// 
			this.txtItem.Location = new System.Drawing.Point(11, 41);
			this.txtItem.Multiline = true;
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(180, 32);
			this.txtItem.TabIndex = 20;
			this.txtItem.Text = "Item name";
			this.txtItem.WordWrap = false;
			this.txtItem.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtItem.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// cbItemCmds
			// 
			this.cbItemCmds.FormattingEnabled = true;
			this.cbItemCmds.ItemHeight = 27;
			this.cbItemCmds.Items.AddRange(new object[] {
            "Get drop",
            "Sell",
            "Equip",
            "Unsafe Equip",
            "To bank from inv",
            "To inv from bank",
            "Equip Set",
            "Add to Whitelist",
            "Remove from Whitelist"});
			this.cbItemCmds.Location = new System.Drawing.Point(11, 6);
			this.cbItemCmds.Name = "cbItemCmds";
			this.cbItemCmds.Size = new System.Drawing.Size(180, 33);
			this.cbItemCmds.TabIndex = 19;
			// 
			// tabQuest
			// 
			this.tabQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabQuest.Controls.Add(this.darkGroupBox26);
			this.tabQuest.Controls.Add(this.darkGroupBox25);
			this.tabQuest.Controls.Add(this.darkGroupBox24);
			this.tabQuest.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabQuest.Location = new System.Drawing.Point(4, 33);
			this.tabQuest.Margin = new System.Windows.Forms.Padding(0);
			this.tabQuest.Name = "tabQuest";
			this.tabQuest.Padding = new System.Windows.Forms.Padding(3);
			this.tabQuest.Size = new System.Drawing.Size(818, 462);
			this.tabQuest.TabIndex = 3;
			this.tabQuest.Text = "Quest";
			// 
			// darkGroupBox26
			// 
			this.darkGroupBox26.Controls.Add(this.darkLabel4);
			this.darkGroupBox26.Controls.Add(this.numQQuestId);
			this.darkGroupBox26.Controls.Add(this.btnQAddToWhitelist);
			this.darkGroupBox26.Controls.Add(this.chkQRequirements);
			this.darkGroupBox26.Controls.Add(this.chkQRewards);
			this.darkGroupBox26.Location = new System.Drawing.Point(12, 214);
			this.darkGroupBox26.Name = "darkGroupBox26";
			this.darkGroupBox26.Size = new System.Drawing.Size(182, 196);
			this.darkGroupBox26.TabIndex = 31;
			this.darkGroupBox26.TabStop = false;
			this.darkGroupBox26.Text = "Grabber";
			// 
			// darkLabel4
			// 
			this.darkLabel4.AutoSize = true;
			this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel4.Location = new System.Drawing.Point(6, 27);
			this.darkLabel4.Name = "darkLabel4";
			this.darkLabel4.Size = new System.Drawing.Size(73, 20);
			this.darkLabel4.TabIndex = 19;
			this.darkLabel4.Text = "Quest ID";
			// 
			// numQQuestId
			// 
			this.numQQuestId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQQuestId.Location = new System.Drawing.Point(9, 54);
			this.numQQuestId.LoopValues = false;
			this.numQQuestId.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQQuestId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQQuestId.Name = "numQQuestId";
			this.numQQuestId.Size = new System.Drawing.Size(105, 26);
			this.numQQuestId.TabIndex = 20;
			this.numQQuestId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnQAddToWhitelist
			// 
			this.btnQAddToWhitelist.Checked = false;
			this.btnQAddToWhitelist.Location = new System.Drawing.Point(9, 153);
			this.btnQAddToWhitelist.Name = "btnQAddToWhitelist";
			this.btnQAddToWhitelist.Size = new System.Drawing.Size(165, 33);
			this.btnQAddToWhitelist.TabIndex = 21;
			this.btnQAddToWhitelist.Text = "Add item whitelist";
			this.btnQAddToWhitelist.Click += new System.EventHandler(this.btnQAddToWhitelist_Click);
			// 
			// chkQRequirements
			// 
			this.chkQRequirements.AutoSize = true;
			this.chkQRequirements.Checked = true;
			this.chkQRequirements.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkQRequirements.Location = new System.Drawing.Point(9, 120);
			this.chkQRequirements.Name = "chkQRequirements";
			this.chkQRequirements.Size = new System.Drawing.Size(126, 24);
			this.chkQRequirements.TabIndex = 23;
			this.chkQRequirements.Text = "Requirments";
			// 
			// chkQRewards
			// 
			this.chkQRewards.AutoSize = true;
			this.chkQRewards.Checked = true;
			this.chkQRewards.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkQRewards.Location = new System.Drawing.Point(9, 88);
			this.chkQRewards.Name = "chkQRewards";
			this.chkQRewards.Size = new System.Drawing.Size(98, 24);
			this.chkQRewards.TabIndex = 22;
			this.chkQRewards.Text = "Rewards";
			// 
			// darkGroupBox25
			// 
			this.darkGroupBox25.Controls.Add(this.chkReloginCompleteQuest);
			this.darkGroupBox25.Controls.Add(this.darkLabel13);
			this.darkGroupBox25.Controls.Add(this.darkLabel8);
			this.darkGroupBox25.Controls.Add(this.numQuestListID);
			this.darkGroupBox25.Controls.Add(this.numQuestDelay);
			this.darkGroupBox25.Controls.Add(this.btnAddQuestList);
			this.darkGroupBox25.Controls.Add(this.chkQuestListItem);
			this.darkGroupBox25.Controls.Add(this.darkLabel5);
			this.darkGroupBox25.Controls.Add(this.numQuestListItem);
			this.darkGroupBox25.Controls.Add(this.btnRemoveQuestList);
			this.darkGroupBox25.Controls.Add(this.btnQuestAdd);
			this.darkGroupBox25.Location = new System.Drawing.Point(253, 13);
			this.darkGroupBox25.Name = "darkGroupBox25";
			this.darkGroupBox25.Size = new System.Drawing.Size(417, 222);
			this.darkGroupBox25.TabIndex = 30;
			this.darkGroupBox25.TabStop = false;
			this.darkGroupBox25.Text = "Quest List";
			// 
			// chkReloginCompleteQuest
			// 
			this.chkReloginCompleteQuest.Checked = true;
			this.chkReloginCompleteQuest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkReloginCompleteQuest.Location = new System.Drawing.Point(182, 106);
			this.chkReloginCompleteQuest.Name = "chkReloginCompleteQuest";
			this.chkReloginCompleteQuest.Size = new System.Drawing.Size(222, 32);
			this.chkReloginCompleteQuest.TabIndex = 18;
			this.chkReloginCompleteQuest.Text = "Logout after 5 times failed to complete a quest";
			this.chkReloginCompleteQuest.MouseHover += new System.EventHandler(this.chkReloginCompleteQuest_MouseHover);
			// 
			// darkLabel13
			// 
			this.darkLabel13.AutoSize = true;
			this.darkLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel13.Location = new System.Drawing.Point(11, 29);
			this.darkLabel13.Name = "darkLabel13";
			this.darkLabel13.Size = new System.Drawing.Size(73, 20);
			this.darkLabel13.TabIndex = 7;
			this.darkLabel13.Text = "Quest ID";
			// 
			// darkLabel8
			// 
			this.darkLabel8.AutoSize = true;
			this.darkLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel8.Location = new System.Drawing.Point(178, 29);
			this.darkLabel8.Name = "darkLabel8";
			this.darkLabel8.Size = new System.Drawing.Size(49, 20);
			this.darkLabel8.TabIndex = 26;
			this.darkLabel8.Text = "Delay";
			// 
			// numQuestListID
			// 
			this.numQuestListID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numQuestListID.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQuestListID.Location = new System.Drawing.Point(14, 57);
			this.numQuestListID.LoopValues = false;
			this.numQuestListID.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQuestListID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQuestListID.Name = "numQuestListID";
			this.numQuestListID.Size = new System.Drawing.Size(70, 28);
			this.numQuestListID.TabIndex = 8;
			this.numQuestListID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numQuestDelay
			// 
			this.numQuestDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numQuestDelay.IncrementAlternate = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numQuestDelay.Location = new System.Drawing.Point(178, 57);
			this.numQuestDelay.LoopValues = false;
			this.numQuestDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numQuestDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numQuestDelay.Name = "numQuestDelay";
			this.numQuestDelay.Size = new System.Drawing.Size(76, 28);
			this.numQuestDelay.TabIndex = 24;
			this.numQuestDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// btnAddQuestList
			// 
			this.btnAddQuestList.Checked = false;
			this.btnAddQuestList.Location = new System.Drawing.Point(14, 126);
			this.btnAddQuestList.Name = "btnAddQuestList";
			this.btnAddQuestList.Size = new System.Drawing.Size(145, 33);
			this.btnAddQuestList.TabIndex = 28;
			this.btnAddQuestList.Text = "Add (cmd)";
			this.btnAddQuestList.Click += new System.EventHandler(this.btnAddQuestList_Click);
			// 
			// chkQuestListItem
			// 
			this.chkQuestListItem.AutoSize = true;
			this.chkQuestListItem.Location = new System.Drawing.Point(90, 27);
			this.chkQuestListItem.Name = "chkQuestListItem";
			this.chkQuestListItem.Size = new System.Drawing.Size(88, 24);
			this.chkQuestListItem.TabIndex = 9;
			this.chkQuestListItem.Text = "Item ID";
			this.chkQuestListItem.CheckedChanged += new System.EventHandler(this.chkQuestListItem_CheckedChanged);
			// 
			// darkLabel5
			// 
			this.darkLabel5.AutoSize = true;
			this.darkLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel5.Location = new System.Drawing.Point(257, 62);
			this.darkLabel5.Name = "darkLabel5";
			this.darkLabel5.Size = new System.Drawing.Size(30, 20);
			this.darkLabel5.TabIndex = 25;
			this.darkLabel5.Text = "ms";
			// 
			// numQuestListItem
			// 
			this.numQuestListItem.Enabled = false;
			this.numQuestListItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numQuestListItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQuestListItem.Location = new System.Drawing.Point(89, 57);
			this.numQuestListItem.LoopValues = false;
			this.numQuestListItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQuestListItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQuestListItem.Name = "numQuestListItem";
			this.numQuestListItem.Size = new System.Drawing.Size(70, 28);
			this.numQuestListItem.TabIndex = 10;
			this.numQuestListItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnRemoveQuestList
			// 
			this.btnRemoveQuestList.Checked = false;
			this.btnRemoveQuestList.Location = new System.Drawing.Point(14, 163);
			this.btnRemoveQuestList.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.btnRemoveQuestList.Name = "btnRemoveQuestList";
			this.btnRemoveQuestList.Size = new System.Drawing.Size(145, 33);
			this.btnRemoveQuestList.TabIndex = 12;
			this.btnRemoveQuestList.Text = "Remove (cmd)";
			this.btnRemoveQuestList.Click += new System.EventHandler(this.btnRemoveQuestList_Click);
			// 
			// btnQuestAdd
			// 
			this.btnQuestAdd.Checked = false;
			this.btnQuestAdd.Location = new System.Drawing.Point(14, 89);
			this.btnQuestAdd.Name = "btnQuestAdd";
			this.btnQuestAdd.Size = new System.Drawing.Size(145, 33);
			this.btnQuestAdd.TabIndex = 11;
			this.btnQuestAdd.Text = "Add to quest list";
			this.btnQuestAdd.Click += new System.EventHandler(this.btnQuestAdd_Click);
			// 
			// darkGroupBox24
			// 
			this.darkGroupBox24.Controls.Add(this.darkLabel12);
			this.darkGroupBox24.Controls.Add(this.label4);
			this.darkGroupBox24.Controls.Add(this.numQuestID);
			this.darkGroupBox24.Controls.Add(this.chkQuestItem);
			this.darkGroupBox24.Controls.Add(this.numQuestItem);
			this.darkGroupBox24.Controls.Add(this.btnQuestComplete);
			this.darkGroupBox24.Controls.Add(this.btnQuestAccept);
			this.darkGroupBox24.Controls.Add(this.numEnsureTries);
			this.darkGroupBox24.Location = new System.Drawing.Point(12, 12);
			this.darkGroupBox24.Name = "darkGroupBox24";
			this.darkGroupBox24.Size = new System.Drawing.Size(222, 187);
			this.darkGroupBox24.TabIndex = 29;
			this.darkGroupBox24.TabStop = false;
			this.darkGroupBox24.Text = "Quest";
			// 
			// darkLabel12
			// 
			this.darkLabel12.AutoSize = true;
			this.darkLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel12.Location = new System.Drawing.Point(161, 108);
			this.darkLabel12.Name = "darkLabel12";
			this.darkLabel12.Size = new System.Drawing.Size(30, 20);
			this.darkLabel12.TabIndex = 25;
			this.darkLabel12.Text = "qty";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label4.Location = new System.Drawing.Point(6, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "Quest ID";
			// 
			// numQuestID
			// 
			this.numQuestID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numQuestID.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQuestID.Location = new System.Drawing.Point(10, 57);
			this.numQuestID.LoopValues = false;
			this.numQuestID.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQuestID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQuestID.Name = "numQuestID";
			this.numQuestID.Size = new System.Drawing.Size(66, 28);
			this.numQuestID.TabIndex = 8;
			this.numQuestID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// chkQuestItem
			// 
			this.chkQuestItem.AutoSize = true;
			this.chkQuestItem.Location = new System.Drawing.Point(86, 24);
			this.chkQuestItem.Name = "chkQuestItem";
			this.chkQuestItem.Size = new System.Drawing.Size(88, 24);
			this.chkQuestItem.TabIndex = 9;
			this.chkQuestItem.Text = "Item ID";
			this.chkQuestItem.CheckedChanged += new System.EventHandler(this.chkQuestItem_CheckedChanged);
			// 
			// numQuestItem
			// 
			this.numQuestItem.Enabled = false;
			this.numQuestItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numQuestItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQuestItem.Location = new System.Drawing.Point(79, 57);
			this.numQuestItem.LoopValues = false;
			this.numQuestItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQuestItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQuestItem.Name = "numQuestItem";
			this.numQuestItem.Size = new System.Drawing.Size(65, 28);
			this.numQuestItem.TabIndex = 10;
			this.numQuestItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnQuestComplete
			// 
			this.btnQuestComplete.Checked = false;
			this.btnQuestComplete.Location = new System.Drawing.Point(10, 129);
			this.btnQuestComplete.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.btnQuestComplete.Name = "btnQuestComplete";
			this.btnQuestComplete.Size = new System.Drawing.Size(134, 33);
			this.btnQuestComplete.TabIndex = 12;
			this.btnQuestComplete.Text = "Complete (cmd)";
			this.btnQuestComplete.Click += new System.EventHandler(this.btnQuestComplete_Click);
			// 
			// btnQuestAccept
			// 
			this.btnQuestAccept.Checked = false;
			this.btnQuestAccept.Location = new System.Drawing.Point(10, 90);
			this.btnQuestAccept.Name = "btnQuestAccept";
			this.btnQuestAccept.Size = new System.Drawing.Size(134, 33);
			this.btnQuestAccept.TabIndex = 13;
			this.btnQuestAccept.Text = "Accept (cmd)";
			this.btnQuestAccept.Click += new System.EventHandler(this.btnQuestAccept_Click);
			// 
			// numEnsureTries
			// 
			this.numEnsureTries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numEnsureTries.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numEnsureTries.Location = new System.Drawing.Point(148, 132);
			this.numEnsureTries.LoopValues = false;
			this.numEnsureTries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numEnsureTries.Name = "numEnsureTries";
			this.numEnsureTries.Size = new System.Drawing.Size(47, 28);
			this.numEnsureTries.TabIndex = 15;
			this.numEnsureTries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// tabMisc
			// 
			this.tabMisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabMisc.Controls.Add(this.darkGroupBox15);
			this.tabMisc.Controls.Add(this.darkGroupBox11);
			this.tabMisc.Controls.Add(this.darkGroupBox10);
			this.tabMisc.Controls.Add(this.btnAddLabel);
			this.tabMisc.Controls.Add(this.btnGotoLabel);
			this.tabMisc.Controls.Add(this.txtLabel);
			this.tabMisc.Controls.Add(this.numSpamTimes);
			this.tabMisc.Controls.Add(this.darkGroupBox14);
			this.tabMisc.Controls.Add(this.darkGroupBox12);
			this.tabMisc.Controls.Add(this.btnReturnCmd);
			this.tabMisc.Controls.Add(this.darkGroupBox8);
			this.tabMisc.Controls.Add(this.darkGroupBox7);
			this.tabMisc.Controls.Add(this.btnBlank);
			this.tabMisc.Controls.Add(this.btnLogout);
			this.tabMisc.Controls.Add(this.btnBeep);
			this.tabMisc.Controls.Add(this.numBeepTimes);
			this.tabMisc.Controls.Add(this.btnDelay);
			this.tabMisc.Controls.Add(this.numDelay);
			this.tabMisc.Controls.Add(this.btnStatementAdd);
			this.tabMisc.Controls.Add(this.txtStatement2);
			this.tabMisc.Controls.Add(this.txtStatement1);
			this.tabMisc.Controls.Add(this.cbStatement);
			this.tabMisc.Controls.Add(this.cbCategories);
			this.tabMisc.Controls.Add(this.txtPacket);
			this.tabMisc.Controls.Add(this.btnClientPacket);
			this.tabMisc.Controls.Add(this.btnPacket);
			this.tabMisc.Controls.Add(this.btnLoadCmd);
			this.tabMisc.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMisc.Location = new System.Drawing.Point(4, 33);
			this.tabMisc.Margin = new System.Windows.Forms.Padding(0);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(818, 462);
			this.tabMisc.TabIndex = 4;
			this.tabMisc.Text = "Misc";
			// 
			// darkGroupBox15
			// 
			this.darkGroupBox15.Controls.Add(this.btnFollowCmd);
			this.darkGroupBox15.Controls.Add(this.tbFollowPlayer);
			this.darkGroupBox15.Location = new System.Drawing.Point(499, 376);
			this.darkGroupBox15.Name = "darkGroupBox15";
			this.darkGroupBox15.Size = new System.Drawing.Size(300, 68);
			this.darkGroupBox15.TabIndex = 173;
			this.darkGroupBox15.TabStop = false;
			this.darkGroupBox15.Text = "Follow and Kills";
			// 
			// btnFollowCmd
			// 
			this.btnFollowCmd.Checked = false;
			this.btnFollowCmd.Location = new System.Drawing.Point(152, 27);
			this.btnFollowCmd.Name = "btnFollowCmd";
			this.btnFollowCmd.Size = new System.Drawing.Size(142, 33);
			this.btnFollowCmd.TabIndex = 151;
			this.btnFollowCmd.Text = "Follow";
			this.btnFollowCmd.Click += new System.EventHandler(this.btnFollowCmd_Click);
			// 
			// tbFollowPlayer
			// 
			this.tbFollowPlayer.Location = new System.Drawing.Point(11, 27);
			this.tbFollowPlayer.Multiline = true;
			this.tbFollowPlayer.Name = "tbFollowPlayer";
			this.tbFollowPlayer.Size = new System.Drawing.Size(137, 33);
			this.tbFollowPlayer.TabIndex = 147;
			this.tbFollowPlayer.Text = "Player name";
			this.tbFollowPlayer.WordWrap = false;
			this.tbFollowPlayer.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbFollowPlayer.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// darkGroupBox11
			// 
			this.darkGroupBox11.Controls.Add(this.lbLabels);
			this.darkGroupBox11.Location = new System.Drawing.Point(496, 9);
			this.darkGroupBox11.Name = "darkGroupBox11";
			this.darkGroupBox11.Size = new System.Drawing.Size(306, 202);
			this.darkGroupBox11.TabIndex = 172;
			this.darkGroupBox11.TabStop = false;
			this.darkGroupBox11.Text = "Labels";
			// 
			// lbLabels
			// 
			this.lbLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.lbLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLabels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbLabels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lbLabels.FormattingEnabled = true;
			this.lbLabels.ItemHeight = 18;
			this.lbLabels.Location = new System.Drawing.Point(3, 22);
			this.lbLabels.Name = "lbLabels";
			this.lbLabels.Size = new System.Drawing.Size(300, 177);
			this.lbLabels.TabIndex = 114;
			this.lbLabels.DoubleClick += new System.EventHandler(this.lbLabels_DoubleClick);
			// 
			// darkGroupBox10
			// 
			this.darkGroupBox10.Controls.Add(this.btnStop);
			this.darkGroupBox10.Controls.Add(this.btnRestart);
			this.darkGroupBox10.Location = new System.Drawing.Point(269, 261);
			this.darkGroupBox10.Name = "darkGroupBox10";
			this.darkGroupBox10.Size = new System.Drawing.Size(208, 70);
			this.darkGroupBox10.TabIndex = 168;
			this.darkGroupBox10.TabStop = false;
			this.darkGroupBox10.Text = "Bot Control";
			// 
			// btnStop
			// 
			this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnStop.Checked = false;
			this.btnStop.Location = new System.Drawing.Point(106, 25);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(96, 33);
			this.btnStop.TabIndex = 65;
			this.btnStop.Text = "Stop bot";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRestart
			// 
			this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnRestart.Checked = false;
			this.btnRestart.Location = new System.Drawing.Point(6, 25);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(94, 33);
			this.btnRestart.TabIndex = 66;
			this.btnRestart.Text = "Restart";
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// btnAddLabel
			// 
			this.btnAddLabel.Checked = false;
			this.btnAddLabel.Location = new System.Drawing.Point(651, 250);
			this.btnAddLabel.Name = "btnAddLabel";
			this.btnAddLabel.Size = new System.Drawing.Size(148, 33);
			this.btnAddLabel.TabIndex = 171;
			this.btnAddLabel.Text = "Add";
			this.btnAddLabel.Click += new System.EventHandler(this.btnAddLabel_Click);
			// 
			// btnGotoLabel
			// 
			this.btnGotoLabel.Checked = false;
			this.btnGotoLabel.Location = new System.Drawing.Point(499, 250);
			this.btnGotoLabel.Name = "btnGotoLabel";
			this.btnGotoLabel.Size = new System.Drawing.Size(148, 33);
			this.btnGotoLabel.TabIndex = 170;
			this.btnGotoLabel.Text = "Goto";
			this.btnGotoLabel.Click += new System.EventHandler(this.btnGotoLabel_Click);
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(499, 216);
			this.txtLabel.Multiline = true;
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(300, 32);
			this.txtLabel.TabIndex = 169;
			this.txtLabel.Text = "Label name";
			this.txtLabel.WordWrap = false;
			// 
			// numSpamTimes
			// 
			this.numSpamTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSpamTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSpamTimes.Location = new System.Drawing.Point(423, 11);
			this.numSpamTimes.LoopValues = false;
			this.numSpamTimes.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numSpamTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSpamTimes.Name = "numSpamTimes";
			this.numSpamTimes.Size = new System.Drawing.Size(54, 28);
			this.numSpamTimes.TabIndex = 167;
			this.numSpamTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// darkGroupBox14
			// 
			this.darkGroupBox14.Controls.Add(this.txtPlayer);
			this.darkGroupBox14.Controls.Add(this.btnGoto);
			this.darkGroupBox14.Location = new System.Drawing.Point(499, 294);
			this.darkGroupBox14.Name = "darkGroupBox14";
			this.darkGroupBox14.Size = new System.Drawing.Size(300, 74);
			this.darkGroupBox14.TabIndex = 166;
			this.darkGroupBox14.TabStop = false;
			this.darkGroupBox14.Text = "Player";
			// 
			// txtPlayer
			// 
			this.txtPlayer.Location = new System.Drawing.Point(11, 27);
			this.txtPlayer.Multiline = true;
			this.txtPlayer.Name = "txtPlayer";
			this.txtPlayer.Size = new System.Drawing.Size(142, 33);
			this.txtPlayer.TabIndex = 69;
			this.txtPlayer.Text = "Player name";
			this.txtPlayer.WordWrap = false;
			this.txtPlayer.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtPlayer.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnGoto
			// 
			this.btnGoto.Checked = false;
			this.btnGoto.Location = new System.Drawing.Point(157, 27);
			this.btnGoto.Name = "btnGoto";
			this.btnGoto.Size = new System.Drawing.Size(137, 33);
			this.btnGoto.TabIndex = 68;
			this.btnGoto.Text = "Goto";
			this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
			// 
			// darkGroupBox12
			// 
			this.darkGroupBox12.Controls.Add(this.txtSetInt);
			this.darkGroupBox12.Controls.Add(this.btnSetInt);
			this.darkGroupBox12.Controls.Add(this.numSetInt);
			this.darkGroupBox12.Controls.Add(this.btnIncreaseInt);
			this.darkGroupBox12.Controls.Add(this.btnDecreaseInt);
			this.darkGroupBox12.Location = new System.Drawing.Point(10, 261);
			this.darkGroupBox12.Name = "darkGroupBox12";
			this.darkGroupBox12.Size = new System.Drawing.Size(253, 107);
			this.darkGroupBox12.TabIndex = 164;
			this.darkGroupBox12.TabStop = false;
			this.darkGroupBox12.Text = "Temp Integer";
			// 
			// txtSetInt
			// 
			this.txtSetInt.Location = new System.Drawing.Point(10, 24);
			this.txtSetInt.Multiline = true;
			this.txtSetInt.Name = "txtSetInt";
			this.txtSetInt.Size = new System.Drawing.Size(176, 32);
			this.txtSetInt.TabIndex = 156;
			this.txtSetInt.WordWrap = false;
			// 
			// btnSetInt
			// 
			this.btnSetInt.Checked = false;
			this.btnSetInt.Location = new System.Drawing.Point(10, 61);
			this.btnSetInt.Name = "btnSetInt";
			this.btnSetInt.Size = new System.Drawing.Size(75, 33);
			this.btnSetInt.TabIndex = 155;
			this.btnSetInt.Text = "Set";
			this.btnSetInt.Click += new System.EventHandler(this.btnSetInt_Click);
			// 
			// numSetInt
			// 
			this.numSetInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSetInt.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSetInt.Location = new System.Drawing.Point(192, 26);
			this.numSetInt.LoopValues = false;
			this.numSetInt.Name = "numSetInt";
			this.numSetInt.Size = new System.Drawing.Size(55, 28);
			this.numSetInt.TabIndex = 157;
			// 
			// btnIncreaseInt
			// 
			this.btnIncreaseInt.Checked = false;
			this.btnIncreaseInt.Location = new System.Drawing.Point(91, 61);
			this.btnIncreaseInt.Name = "btnIncreaseInt";
			this.btnIncreaseInt.Size = new System.Drawing.Size(75, 33);
			this.btnIncreaseInt.TabIndex = 158;
			this.btnIncreaseInt.Text = "++";
			this.btnIncreaseInt.Click += new System.EventHandler(this.btnIncreaseInt_Click);
			// 
			// btnDecreaseInt
			// 
			this.btnDecreaseInt.Checked = false;
			this.btnDecreaseInt.Location = new System.Drawing.Point(172, 61);
			this.btnDecreaseInt.Name = "btnDecreaseInt";
			this.btnDecreaseInt.Size = new System.Drawing.Size(75, 33);
			this.btnDecreaseInt.TabIndex = 158;
			this.btnDecreaseInt.Text = "--";
			this.btnDecreaseInt.Click += new System.EventHandler(this.btnDecreaseInt_Click);
			// 
			// btnReturnCmd
			// 
			this.btnReturnCmd.Checked = false;
			this.btnReturnCmd.Location = new System.Drawing.Point(269, 150);
			this.btnReturnCmd.Name = "btnReturnCmd";
			this.btnReturnCmd.Size = new System.Drawing.Size(208, 33);
			this.btnReturnCmd.TabIndex = 160;
			this.btnReturnCmd.Text = "Return (command)";
			this.btnReturnCmd.Click += new System.EventHandler(this.btnReturnCmd_Click);
			// 
			// darkGroupBox8
			// 
			this.darkGroupBox8.Controls.Add(this.numIndexCmd);
			this.darkGroupBox8.Controls.Add(this.btnGotoIndex);
			this.darkGroupBox8.Controls.Add(this.btnGoUpIndex);
			this.darkGroupBox8.Controls.Add(this.btnGoDownIndex);
			this.darkGroupBox8.Location = new System.Drawing.Point(10, 374);
			this.darkGroupBox8.Name = "darkGroupBox8";
			this.darkGroupBox8.Size = new System.Drawing.Size(253, 65);
			this.darkGroupBox8.TabIndex = 162;
			this.darkGroupBox8.TabStop = false;
			this.darkGroupBox8.Text = "Index";
			// 
			// numIndexCmd
			// 
			this.numIndexCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numIndexCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numIndexCmd.Location = new System.Drawing.Point(10, 26);
			this.numIndexCmd.LoopValues = false;
			this.numIndexCmd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numIndexCmd.Name = "numIndexCmd";
			this.numIndexCmd.Size = new System.Drawing.Size(66, 28);
			this.numIndexCmd.TabIndex = 152;
			// 
			// btnGotoIndex
			// 
			this.btnGotoIndex.Checked = false;
			this.btnGotoIndex.Location = new System.Drawing.Point(82, 24);
			this.btnGotoIndex.Name = "btnGotoIndex";
			this.btnGotoIndex.Size = new System.Drawing.Size(73, 33);
			this.btnGotoIndex.TabIndex = 153;
			this.btnGotoIndex.Text = "Goto";
			this.btnGotoIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// btnGoUpIndex
			// 
			this.btnGoUpIndex.Checked = false;
			this.btnGoUpIndex.Location = new System.Drawing.Point(161, 24);
			this.btnGoUpIndex.Name = "btnGoUpIndex";
			this.btnGoUpIndex.Size = new System.Drawing.Size(40, 33);
			this.btnGoUpIndex.TabIndex = 153;
			this.btnGoUpIndex.Text = "++";
			this.btnGoUpIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// btnGoDownIndex
			// 
			this.btnGoDownIndex.Checked = false;
			this.btnGoDownIndex.Location = new System.Drawing.Point(207, 24);
			this.btnGoDownIndex.Name = "btnGoDownIndex";
			this.btnGoDownIndex.Size = new System.Drawing.Size(40, 33);
			this.btnGoDownIndex.TabIndex = 153;
			this.btnGoDownIndex.Text = "--";
			this.btnGoDownIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// darkGroupBox7
			// 
			this.darkGroupBox7.Controls.Add(this.btnProvokeOff);
			this.darkGroupBox7.Controls.Add(this.btnProvokeOn);
			this.darkGroupBox7.Location = new System.Drawing.Point(269, 348);
			this.darkGroupBox7.Name = "darkGroupBox7";
			this.darkGroupBox7.Size = new System.Drawing.Size(208, 73);
			this.darkGroupBox7.TabIndex = 161;
			this.darkGroupBox7.TabStop = false;
			this.darkGroupBox7.Text = "Provoke";
			// 
			// btnProvokeOff
			// 
			this.btnProvokeOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProvokeOff.Checked = false;
			this.btnProvokeOff.Location = new System.Drawing.Point(106, 25);
			this.btnProvokeOff.Name = "btnProvokeOff";
			this.btnProvokeOff.Size = new System.Drawing.Size(96, 33);
			this.btnProvokeOff.TabIndex = 150;
			this.btnProvokeOff.Text = "Off";
			this.btnProvokeOff.Click += new System.EventHandler(this.btnProvokeOff_Click);
			// 
			// btnProvokeOn
			// 
			this.btnProvokeOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProvokeOn.Checked = false;
			this.btnProvokeOn.Location = new System.Drawing.Point(6, 26);
			this.btnProvokeOn.Name = "btnProvokeOn";
			this.btnProvokeOn.Size = new System.Drawing.Size(94, 33);
			this.btnProvokeOn.TabIndex = 149;
			this.btnProvokeOn.Text = "On";
			this.btnProvokeOn.Click += new System.EventHandler(this.btnProvokeOn_Click);
			// 
			// btnBlank
			// 
			this.btnBlank.Checked = false;
			this.btnBlank.Location = new System.Drawing.Point(8, 219);
			this.btnBlank.Name = "btnBlank";
			this.btnBlank.Size = new System.Drawing.Size(255, 33);
			this.btnBlank.TabIndex = 151;
			this.btnBlank.Text = "Blank Command";
			this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
			// 
			// btnLogout
			// 
			this.btnLogout.Checked = false;
			this.btnLogout.Location = new System.Drawing.Point(269, 184);
			this.btnLogout.Name = "btnLogout";
			this.btnLogout.Size = new System.Drawing.Size(208, 33);
			this.btnLogout.TabIndex = 114;
			this.btnLogout.Text = "Logout";
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			// 
			// btnBeep
			// 
			this.btnBeep.Checked = false;
			this.btnBeep.Location = new System.Drawing.Point(269, 219);
			this.btnBeep.Name = "btnBeep";
			this.btnBeep.Size = new System.Drawing.Size(122, 33);
			this.btnBeep.TabIndex = 74;
			this.btnBeep.Text = "Play Sound";
			this.btnBeep.Click += new System.EventHandler(this.btnBeep_Click);
			// 
			// numBeepTimes
			// 
			this.numBeepTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numBeepTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBeepTimes.Location = new System.Drawing.Point(397, 222);
			this.numBeepTimes.LoopValues = false;
			this.numBeepTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numBeepTimes.Name = "numBeepTimes";
			this.numBeepTimes.Size = new System.Drawing.Size(80, 28);
			this.numBeepTimes.TabIndex = 73;
			this.numBeepTimes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// btnDelay
			// 
			this.btnDelay.Checked = false;
			this.btnDelay.Location = new System.Drawing.Point(269, 80);
			this.btnDelay.Name = "btnDelay";
			this.btnDelay.Size = new System.Drawing.Size(122, 33);
			this.btnDelay.TabIndex = 74;
			this.btnDelay.Text = "Delay";
			this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
			// 
			// numDelay
			// 
			this.numDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numDelay.Location = new System.Drawing.Point(397, 82);
			this.numDelay.LoopValues = false;
			this.numDelay.Maximum = new decimal(new int[] {
            71000,
            0,
            0,
            0});
			this.numDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numDelay.Name = "numDelay";
			this.numDelay.Size = new System.Drawing.Size(80, 28);
			this.numDelay.TabIndex = 73;
			this.numDelay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			// 
			// btnStatementAdd
			// 
			this.btnStatementAdd.Checked = false;
			this.btnStatementAdd.Location = new System.Drawing.Point(8, 184);
			this.btnStatementAdd.MinimumSize = new System.Drawing.Size(167, 33);
			this.btnStatementAdd.Name = "btnStatementAdd";
			this.btnStatementAdd.Size = new System.Drawing.Size(255, 33);
			this.btnStatementAdd.TabIndex = 61;
			this.btnStatementAdd.Text = "Add statement";
			this.btnStatementAdd.Click += new System.EventHandler(this.btnStatementAdd_Click);
			// 
			// txtStatement2
			// 
			this.txtStatement2.Location = new System.Drawing.Point(8, 150);
			this.txtStatement2.MinimumSize = new System.Drawing.Size(167, 20);
			this.txtStatement2.Multiline = true;
			this.txtStatement2.Name = "txtStatement2";
			this.txtStatement2.Size = new System.Drawing.Size(255, 33);
			this.txtStatement2.TabIndex = 60;
			this.txtStatement2.WordWrap = false;
			// 
			// txtStatement1
			// 
			this.txtStatement1.Location = new System.Drawing.Point(8, 115);
			this.txtStatement1.MinimumSize = new System.Drawing.Size(167, 32);
			this.txtStatement1.Multiline = true;
			this.txtStatement1.Name = "txtStatement1";
			this.txtStatement1.Size = new System.Drawing.Size(255, 33);
			this.txtStatement1.TabIndex = 59;
			this.txtStatement1.WordWrap = false;
			// 
			// cbStatement
			// 
			this.cbStatement.DisplayMember = "Text";
			this.cbStatement.FormattingEnabled = true;
			this.cbStatement.ItemHeight = 27;
			this.cbStatement.Location = new System.Drawing.Point(8, 80);
			this.cbStatement.MaxDropDownItems = 15;
			this.cbStatement.MinimumSize = new System.Drawing.Size(167, 0);
			this.cbStatement.Name = "cbStatement";
			this.cbStatement.Size = new System.Drawing.Size(255, 33);
			this.cbStatement.TabIndex = 58;
			this.cbStatement.SelectedIndexChanged += new System.EventHandler(this.cbStatement_SelectedIndexChanged);
			// 
			// cbCategories
			// 
			this.cbCategories.FormattingEnabled = true;
			this.cbCategories.ItemHeight = 27;
			this.cbCategories.Items.AddRange(new object[] {
            "Aura",
            "Item",
            "This player",
            "Player",
            "Map",
            "Monster",
            "Quest",
            "Misc"});
			this.cbCategories.Location = new System.Drawing.Point(8, 45);
			this.cbCategories.MinimumSize = new System.Drawing.Size(167, 0);
			this.cbCategories.Name = "cbCategories";
			this.cbCategories.Size = new System.Drawing.Size(255, 33);
			this.cbCategories.TabIndex = 57;
			this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
			// 
			// txtPacket
			// 
			this.txtPacket.Location = new System.Drawing.Point(7, 9);
			this.txtPacket.Multiline = true;
			this.txtPacket.Name = "txtPacket";
			this.txtPacket.Size = new System.Drawing.Size(410, 32);
			this.txtPacket.TabIndex = 53;
			this.txtPacket.Text = "%xt%zm%.........";
			this.txtPacket.WordWrap = false;
			this.txtPacket.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtPacket.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnClientPacket
			// 
			this.btnClientPacket.Checked = false;
			this.btnClientPacket.Location = new System.Drawing.Point(357, 45);
			this.btnClientPacket.Name = "btnClientPacket";
			this.btnClientPacket.Size = new System.Drawing.Size(120, 33);
			this.btnClientPacket.TabIndex = 52;
			this.btnClientPacket.Text = "Client Packet";
			this.btnClientPacket.Click += new System.EventHandler(this.btnClientPacket_Click);
			// 
			// btnPacket
			// 
			this.btnPacket.Checked = false;
			this.btnPacket.Location = new System.Drawing.Point(269, 45);
			this.btnPacket.Name = "btnPacket";
			this.btnPacket.Size = new System.Drawing.Size(82, 33);
			this.btnPacket.TabIndex = 52;
			this.btnPacket.Text = "Packet";
			this.btnPacket.Click += new System.EventHandler(this.btnPacket_Click);
			// 
			// btnLoadCmd
			// 
			this.btnLoadCmd.Checked = false;
			this.btnLoadCmd.Location = new System.Drawing.Point(269, 115);
			this.btnLoadCmd.Name = "btnLoadCmd";
			this.btnLoadCmd.Size = new System.Drawing.Size(208, 33);
			this.btnLoadCmd.TabIndex = 63;
			this.btnLoadCmd.Text = "Load bot (command)";
			this.btnLoadCmd.Click += new System.EventHandler(this.btnLoadCmd_Click);
			// 
			// tabMisc2
			// 
			this.tabMisc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabMisc2.Controls.Add(this.darkGroupBox21);
			this.tabMisc2.Controls.Add(this.darkGroupBox13);
			this.tabMisc2.Controls.Add(this.darkGroupBox9);
			this.tabMisc2.Controls.Add(this.chkMerge);
			this.tabMisc2.Controls.Add(this.darkGroupBox1);
			this.tabMisc2.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMisc2.Location = new System.Drawing.Point(4, 33);
			this.tabMisc2.Margin = new System.Windows.Forms.Padding(0);
			this.tabMisc2.Name = "tabMisc2";
			this.tabMisc2.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc2.Size = new System.Drawing.Size(818, 462);
			this.tabMisc2.TabIndex = 8;
			this.tabMisc2.Text = "Misc 2";
			// 
			// darkGroupBox21
			// 
			this.darkGroupBox21.Controls.Add(this.chkSpecial);
			this.darkGroupBox21.Controls.Add(this.cmbSpecials);
			this.darkGroupBox21.Location = new System.Drawing.Point(6, 166);
			this.darkGroupBox21.Name = "darkGroupBox21";
			this.darkGroupBox21.Size = new System.Drawing.Size(359, 72);
			this.darkGroupBox21.TabIndex = 171;
			this.darkGroupBox21.TabStop = false;
			this.darkGroupBox21.Text = "Special handlers";
			// 
			// chkSpecial
			// 
			this.chkSpecial.AutoSize = true;
			this.chkSpecial.Location = new System.Drawing.Point(10, 33);
			this.chkSpecial.Name = "chkSpecial";
			this.chkSpecial.Size = new System.Drawing.Size(22, 21);
			this.chkSpecial.TabIndex = 173;
			this.chkSpecial.CheckedChanged += new System.EventHandler(this.chkSpecial_CheckedChanged);
			// 
			// cmbSpecials
			// 
			this.cmbSpecials.FormattingEnabled = true;
			this.cmbSpecials.ItemHeight = 27;
			this.cmbSpecials.Items.AddRange(new object[] {
            "Auto Zone - Ultradage",
            "Auto Zone - Dark Carnax",
            "Auto Zone - Astral Empyrean",
            "Auto Zone - Queen Iona",
            "Auto Zone - Colossal Vordred"});
			this.cmbSpecials.Location = new System.Drawing.Point(39, 27);
			this.cmbSpecials.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cmbSpecials.Name = "cmbSpecials";
			this.cmbSpecials.Size = new System.Drawing.Size(314, 33);
			this.cmbSpecials.TabIndex = 172;
			// 
			// darkGroupBox13
			// 
			this.darkGroupBox13.Controls.Add(this.chkRestartAFK);
			this.darkGroupBox13.Controls.Add(this.label3);
			this.darkGroupBox13.Controls.Add(this.chkSkip);
			this.darkGroupBox13.Controls.Add(this.btnBotDelay);
			this.darkGroupBox13.Controls.Add(this.numBotDelay);
			this.darkGroupBox13.Controls.Add(this.chkRestartDeath);
			this.darkGroupBox13.Location = new System.Drawing.Point(3, 265);
			this.darkGroupBox13.Name = "darkGroupBox13";
			this.darkGroupBox13.Size = new System.Drawing.Size(362, 192);
			this.darkGroupBox13.TabIndex = 161;
			this.darkGroupBox13.TabStop = false;
			this.darkGroupBox13.Text = "Bot Delay";
			// 
			// chkRestartAFK
			// 
			this.chkRestartAFK.AutoSize = true;
			this.chkRestartAFK.Checked = true;
			this.chkRestartAFK.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRestartAFK.Location = new System.Drawing.Point(17, 155);
			this.chkRestartAFK.Name = "chkRestartAFK";
			this.chkRestartAFK.Size = new System.Drawing.Size(172, 24);
			this.chkRestartAFK.TabIndex = 117;
			this.chkRestartAFK.Text = "Restart bot on AFK";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label3.Location = new System.Drawing.Point(13, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 20);
			this.label3.TabIndex = 72;
			this.label3.Text = "Overall Delay";
			// 
			// chkSkip
			// 
			this.chkSkip.AutoSize = true;
			this.chkSkip.Checked = true;
			this.chkSkip.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSkip.Location = new System.Drawing.Point(17, 95);
			this.chkSkip.Name = "chkSkip";
			this.chkSkip.Size = new System.Drawing.Size(210, 24);
			this.chkSkip.TabIndex = 62;
			this.chkSkip.Text = "Skip bot delay for index/if";
			// 
			// btnBotDelay
			// 
			this.btnBotDelay.Checked = false;
			this.btnBotDelay.Location = new System.Drawing.Point(120, 25);
			this.btnBotDelay.Name = "btnBotDelay";
			this.btnBotDelay.Size = new System.Drawing.Size(97, 33);
			this.btnBotDelay.TabIndex = 70;
			this.btnBotDelay.Text = "Set (cmd)";
			this.btnBotDelay.Click += new System.EventHandler(this.btnBotDelay_Click);
			// 
			// numBotDelay
			// 
			this.numBotDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numBotDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numBotDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBotDelay.Location = new System.Drawing.Point(20, 48);
			this.numBotDelay.LoopValues = false;
			this.numBotDelay.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numBotDelay.Name = "numBotDelay";
			this.numBotDelay.Size = new System.Drawing.Size(76, 28);
			this.numBotDelay.TabIndex = 71;
			this.numBotDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// chkRestartDeath
			// 
			this.chkRestartDeath.AutoSize = true;
			this.chkRestartDeath.Checked = true;
			this.chkRestartDeath.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRestartDeath.Location = new System.Drawing.Point(17, 125);
			this.chkRestartDeath.Name = "chkRestartDeath";
			this.chkRestartDeath.Size = new System.Drawing.Size(196, 24);
			this.chkRestartDeath.TabIndex = 116;
			this.chkRestartDeath.Text = "Restart bot upon dying";
			// 
			// darkGroupBox9
			// 
			this.darkGroupBox9.Controls.Add(this.txtAuthor);
			this.darkGroupBox9.Controls.Add(this.splitContainer3);
			this.darkGroupBox9.Controls.Add(this.txtDescription);
			this.darkGroupBox9.Location = new System.Drawing.Point(371, 6);
			this.darkGroupBox9.Name = "darkGroupBox9";
			this.darkGroupBox9.Size = new System.Drawing.Size(441, 456);
			this.darkGroupBox9.TabIndex = 116;
			this.darkGroupBox9.TabStop = false;
			this.darkGroupBox9.Text = "Save/Load";
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.Location = new System.Drawing.Point(7, 67);
			this.txtAuthor.Multiline = true;
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(429, 33);
			this.txtAuthor.TabIndex = 119;
			this.txtAuthor.Text = "Author";
			this.txtAuthor.WordWrap = false;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(7, 27);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.btnSave);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnLoad);
			this.splitContainer3.Size = new System.Drawing.Size(428, 33);
			this.splitContainer3.SplitterDistance = 198;
			this.splitContainer3.TabIndex = 118;
			// 
			// btnSave
			// 
			this.btnSave.Checked = false;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSave.Location = new System.Drawing.Point(0, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(198, 33);
			this.btnSave.TabIndex = 75;
			this.btnSave.Text = "Save bot";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Checked = false;
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Location = new System.Drawing.Point(0, 0);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(226, 33);
			this.btnLoad.TabIndex = 67;
			this.btnLoad.Text = "Load bot";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(7, 104);
			this.txtDescription.MaxLength = 2147483647;
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(428, 340);
			this.txtDescription.TabIndex = 109;
			this.txtDescription.Text = "Description (Write in RTF)";
			this.txtDescription.WordWrap = false;
			this.txtDescription.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtDescription.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// chkMerge
			// 
			this.chkMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMerge.AutoSize = true;
			this.chkMerge.Location = new System.Drawing.Point(-17543, 56);
			this.chkMerge.Name = "chkMerge";
			this.chkMerge.Size = new System.Drawing.Size(80, 24);
			this.chkMerge.TabIndex = 115;
			this.chkMerge.Text = "Merge";
			// 
			// darkGroupBox1
			// 
			this.darkGroupBox1.Controls.Add(this.btnBSStop);
			this.darkGroupBox1.Controls.Add(this.label8);
			this.darkGroupBox1.Controls.Add(this.tbBSLabel);
			this.darkGroupBox1.Controls.Add(this.numBSDelay);
			this.darkGroupBox1.Controls.Add(this.tbBSPacket);
			this.darkGroupBox1.Controls.Add(this.btnBSAdd);
			this.darkGroupBox1.Location = new System.Drawing.Point(6, 6);
			this.darkGroupBox1.Name = "darkGroupBox1";
			this.darkGroupBox1.Size = new System.Drawing.Size(359, 143);
			this.darkGroupBox1.TabIndex = 59;
			this.darkGroupBox1.TabStop = false;
			this.darkGroupBox1.Text = "Packet Spammer";
			// 
			// btnBSStop
			// 
			this.btnBSStop.Checked = false;
			this.btnBSStop.Location = new System.Drawing.Point(276, 100);
			this.btnBSStop.Name = "btnBSStop";
			this.btnBSStop.Size = new System.Drawing.Size(77, 33);
			this.btnBSStop.TabIndex = 166;
			this.btnBSStop.Text = "Stop";
			this.btnBSStop.Click += new System.EventHandler(this.btnBSStop_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(106, 104);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30, 20);
			this.label8.TabIndex = 165;
			this.label8.Text = "ms";
			// 
			// tbBSLabel
			// 
			this.tbBSLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbBSLabel.Location = new System.Drawing.Point(10, 29);
			this.tbBSLabel.Multiline = true;
			this.tbBSLabel.Name = "tbBSLabel";
			this.tbBSLabel.Size = new System.Drawing.Size(343, 32);
			this.tbBSLabel.TabIndex = 164;
			this.tbBSLabel.Text = "Packet Name";
			this.tbBSLabel.WordWrap = false;
			this.tbBSLabel.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbBSLabel.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// numBSDelay
			// 
			this.numBSDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numBSDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numBSDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBSDelay.Location = new System.Drawing.Point(10, 100);
			this.numBSDelay.LoopValues = false;
			this.numBSDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numBSDelay.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numBSDelay.Name = "numBSDelay";
			this.numBSDelay.Size = new System.Drawing.Size(90, 28);
			this.numBSDelay.TabIndex = 164;
			this.numBSDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// tbBSPacket
			// 
			this.tbBSPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbBSPacket.Location = new System.Drawing.Point(10, 64);
			this.tbBSPacket.Multiline = true;
			this.tbBSPacket.Name = "tbBSPacket";
			this.tbBSPacket.Size = new System.Drawing.Size(343, 32);
			this.tbBSPacket.TabIndex = 163;
			this.tbBSPacket.Text = "%xt%zm%.........";
			this.tbBSPacket.WordWrap = false;
			this.tbBSPacket.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbBSPacket.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnBSAdd
			// 
			this.btnBSAdd.Checked = false;
			this.btnBSAdd.Location = new System.Drawing.Point(200, 100);
			this.btnBSAdd.Name = "btnBSAdd";
			this.btnBSAdd.Size = new System.Drawing.Size(73, 33);
			this.btnBSAdd.TabIndex = 162;
			this.btnBSAdd.Text = "Add";
			this.btnBSAdd.Click += new System.EventHandler(this.btnBSStart_Click);
			// 
			// tabOptions
			// 
			this.tabOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabOptions.Controls.Add(this.chkAntiCounter);
			this.tabOptions.Controls.Add(this.chkFollowOnly);
			this.tabOptions.Controls.Add(this.tbFollowPlayer2);
			this.tabOptions.Controls.Add(this.chkWalkSpeed);
			this.tabOptions.Controls.Add(this.darkGroupBox6);
			this.tabOptions.Controls.Add(this.label6);
			this.tabOptions.Controls.Add(this.numOptionsTimer);
			this.tabOptions.Controls.Add(this.chkEnableSettings);
			this.tabOptions.Controls.Add(this.chkDisableAnims);
			this.tabOptions.Controls.Add(this.txtSoundItem);
			this.tabOptions.Controls.Add(this.btnSoundAdd);
			this.tabOptions.Controls.Add(this.btnSoundDelete);
			this.tabOptions.Controls.Add(this.btnSoundTest);
			this.tabOptions.Controls.Add(this.lstSoundItems);
			this.tabOptions.Controls.Add(this.label9);
			this.tabOptions.Controls.Add(this.numWalkSpeed);
			this.tabOptions.Controls.Add(this.chkSkipCutscenes);
			this.tabOptions.Controls.Add(this.chkHidePlayers);
			this.tabOptions.Controls.Add(this.chkLag);
			this.tabOptions.Controls.Add(this.chkMagnet);
			this.tabOptions.Controls.Add(this.chkProvoke);
			this.tabOptions.Controls.Add(this.chkInfiniteRange);
			this.tabOptions.Controls.Add(this.grpLogin);
			this.tabOptions.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabOptions.Location = new System.Drawing.Point(4, 33);
			this.tabOptions.Margin = new System.Windows.Forms.Padding(0);
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabOptions.Size = new System.Drawing.Size(818, 462);
			this.tabOptions.TabIndex = 5;
			this.tabOptions.Text = "Options";
			// 
			// chkAntiCounter
			// 
			this.chkAntiCounter.AutoSize = true;
			this.chkAntiCounter.Checked = true;
			this.chkAntiCounter.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAntiCounter.Location = new System.Drawing.Point(295, 258);
			this.chkAntiCounter.Name = "chkAntiCounter";
			this.chkAntiCounter.Size = new System.Drawing.Size(169, 24);
			this.chkAntiCounter.TabIndex = 163;
			this.chkAntiCounter.Text = "Anti counter attack";
			this.chkAntiCounter.CheckedChanged += new System.EventHandler(this.chkAntiCounter_CheckedChanged);
			this.chkAntiCounter.MouseHover += new System.EventHandler(this.chkAntiCounter_MouseHover);
			// 
			// chkFollowOnly
			// 
			this.chkFollowOnly.AutoSize = true;
			this.chkFollowOnly.Location = new System.Drawing.Point(294, 224);
			this.chkFollowOnly.Name = "chkFollowOnly";
			this.chkFollowOnly.Size = new System.Drawing.Size(80, 24);
			this.chkFollowOnly.TabIndex = 162;
			this.chkFollowOnly.Text = "Follow";
			// 
			// tbFollowPlayer2
			// 
			this.tbFollowPlayer2.Location = new System.Drawing.Point(378, 222);
			this.tbFollowPlayer2.Multiline = true;
			this.tbFollowPlayer2.Name = "tbFollowPlayer2";
			this.tbFollowPlayer2.Size = new System.Drawing.Size(113, 32);
			this.tbFollowPlayer2.TabIndex = 161;
			this.tbFollowPlayer2.Text = "Player name";
			this.tbFollowPlayer2.WordWrap = false;
			this.tbFollowPlayer2.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbFollowPlayer2.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// chkWalkSpeed
			// 
			this.chkWalkSpeed.AutoSize = true;
			this.chkWalkSpeed.Location = new System.Drawing.Point(295, 193);
			this.chkWalkSpeed.Name = "chkWalkSpeed";
			this.chkWalkSpeed.Size = new System.Drawing.Size(118, 24);
			this.chkWalkSpeed.TabIndex = 160;
			this.chkWalkSpeed.Text = "Walk speed";
			this.chkWalkSpeed.CheckedChanged += new System.EventHandler(this.chkWalkSpeed_CheckedChanged);
			// 
			// darkGroupBox6
			// 
			this.darkGroupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.darkGroupBox6.Controls.Add(this.splitContainer5);
			this.darkGroupBox6.Controls.Add(this.lstLogText);
			this.darkGroupBox6.Controls.Add(this.txtLog);
			this.darkGroupBox6.Controls.Add(this.label5);
			this.darkGroupBox6.Location = new System.Drawing.Point(-2613, 3);
			this.darkGroupBox6.Name = "darkGroupBox6";
			this.darkGroupBox6.Size = new System.Drawing.Size(298, 456);
			this.darkGroupBox6.TabIndex = 159;
			this.darkGroupBox6.TabStop = false;
			this.darkGroupBox6.Text = "Logs";
			// 
			// splitContainer5
			// 
			this.splitContainer5.Location = new System.Drawing.Point(6, 149);
			this.splitContainer5.Name = "splitContainer5";
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.btnLog);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.btnLogDebug);
			this.splitContainer5.Size = new System.Drawing.Size(285, 33);
			this.splitContainer5.SplitterDistance = 134;
			this.splitContainer5.SplitterWidth = 2;
			this.splitContainer5.TabIndex = 156;
			// 
			// btnLog
			// 
			this.btnLog.Checked = false;
			this.btnLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLog.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLog.Location = new System.Drawing.Point(0, 0);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(134, 33);
			this.btnLog.TabIndex = 148;
			this.btnLog.Text = "Log Script";
			this.btnLog.Click += new System.EventHandler(this.logScript);
			// 
			// btnLogDebug
			// 
			this.btnLogDebug.Checked = false;
			this.btnLogDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLogDebug.Location = new System.Drawing.Point(0, 0);
			this.btnLogDebug.Name = "btnLogDebug";
			this.btnLogDebug.Size = new System.Drawing.Size(149, 33);
			this.btnLogDebug.TabIndex = 152;
			this.btnLogDebug.Text = "Log Debug";
			this.btnLogDebug.Click += new System.EventHandler(this.logDebug);
			// 
			// lstLogText
			// 
			this.lstLogText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstLogText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.lstLogText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstLogText.ForeColor = System.Drawing.Color.Gainsboro;
			this.lstLogText.FormattingEnabled = true;
			this.lstLogText.ItemHeight = 20;
			this.lstLogText.Items.AddRange(new object[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}",
            "{INT VALUE: int}",
            "{ROOM_ID}"});
			this.lstLogText.Location = new System.Drawing.Point(6, 226);
			this.lstLogText.Name = "lstLogText";
			this.lstLogText.Size = new System.Drawing.Size(285, 222);
			this.lstLogText.TabIndex = 153;
			this.lstLogText.DoubleClick += new System.EventHandler(this.lstLogText_DoubleClick);
			this.lstLogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogText_KeyDown);
			// 
			// txtLog
			// 
			this.txtLog.AcceptsReturn = true;
			this.txtLog.AcceptsTab = true;
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.AutoCompleteCustomSource.AddRange(new string[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}"});
			this.txtLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtLog.Location = new System.Drawing.Point(6, 25);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(285, 122);
			this.txtLog.TabIndex = 147;
			this.txtLog.Text = "Text";
			this.txtLog.WordWrap = false;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label5.Location = new System.Drawing.Point(6, 199);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(171, 20);
			this.label5.TabIndex = 155;
			this.label5.Text = "Viable Log References";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label6.Location = new System.Drawing.Point(374, 409);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107, 20);
			this.label6.TabIndex = 157;
			this.label6.Text = "Options Timer";
			// 
			// numOptionsTimer
			// 
			this.numOptionsTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numOptionsTimer.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numOptionsTimer.Location = new System.Drawing.Point(298, 407);
			this.numOptionsTimer.LoopValues = false;
			this.numOptionsTimer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numOptionsTimer.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numOptionsTimer.Name = "numOptionsTimer";
			this.numOptionsTimer.Size = new System.Drawing.Size(70, 28);
			this.numOptionsTimer.TabIndex = 156;
			this.numOptionsTimer.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numOptionsTimer.ValueChanged += new System.EventHandler(this.numOptionsTimer_ValueChanged);
			// 
			// chkEnableSettings
			// 
			this.chkEnableSettings.AutoSize = true;
			this.chkEnableSettings.Location = new System.Drawing.Point(294, 352);
			this.chkEnableSettings.Name = "chkEnableSettings";
			this.chkEnableSettings.Size = new System.Drawing.Size(143, 44);
			this.chkEnableSettings.TabIndex = 132;
			this.chkEnableSettings.Text = "Enable options\r\nwithout starting";
			this.chkEnableSettings.CheckedChanged += new System.EventHandler(this.chkEnableSettings_CheckedChanged);
			this.chkEnableSettings.Click += new System.EventHandler(this.chkEnableSettings_Click);
			// 
			// chkDisableAnims
			// 
			this.chkDisableAnims.AutoSize = true;
			this.chkDisableAnims.Location = new System.Drawing.Point(294, 288);
			this.chkDisableAnims.Name = "chkDisableAnims";
			this.chkDisableAnims.Size = new System.Drawing.Size(180, 24);
			this.chkDisableAnims.TabIndex = 131;
			this.chkDisableAnims.Text = "Disable player anims";
			this.chkDisableAnims.Visible = false;
			this.chkDisableAnims.CheckedChanged += new System.EventHandler(this.chkDisableAnims_CheckedChanged);
			// 
			// txtSoundItem
			// 
			this.txtSoundItem.Location = new System.Drawing.Point(10, 395);
			this.txtSoundItem.Multiline = true;
			this.txtSoundItem.Name = "txtSoundItem";
			this.txtSoundItem.Size = new System.Drawing.Size(232, 32);
			this.txtSoundItem.TabIndex = 130;
			this.txtSoundItem.WordWrap = false;
			// 
			// btnSoundAdd
			// 
			this.btnSoundAdd.Checked = false;
			this.btnSoundAdd.Location = new System.Drawing.Point(89, 428);
			this.btnSoundAdd.Name = "btnSoundAdd";
			this.btnSoundAdd.Size = new System.Drawing.Size(75, 33);
			this.btnSoundAdd.TabIndex = 129;
			this.btnSoundAdd.Text = "Add";
			this.btnSoundAdd.Click += new System.EventHandler(this.btnSoundAdd_Click);
			// 
			// btnSoundDelete
			// 
			this.btnSoundDelete.Checked = false;
			this.btnSoundDelete.Location = new System.Drawing.Point(167, 428);
			this.btnSoundDelete.Name = "btnSoundDelete";
			this.btnSoundDelete.Size = new System.Drawing.Size(75, 33);
			this.btnSoundDelete.TabIndex = 128;
			this.btnSoundDelete.Text = "Delete";
			this.btnSoundDelete.Click += new System.EventHandler(this.btnSoundDelete_Click);
			// 
			// btnSoundTest
			// 
			this.btnSoundTest.Checked = false;
			this.btnSoundTest.Location = new System.Drawing.Point(10, 428);
			this.btnSoundTest.Name = "btnSoundTest";
			this.btnSoundTest.Size = new System.Drawing.Size(75, 33);
			this.btnSoundTest.TabIndex = 126;
			this.btnSoundTest.Text = "Test";
			this.btnSoundTest.Click += new System.EventHandler(this.btnSoundTest_Click);
			// 
			// lstSoundItems
			// 
			this.lstSoundItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.lstSoundItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstSoundItems.ForeColor = System.Drawing.Color.Gainsboro;
			this.lstSoundItems.FormattingEnabled = true;
			this.lstSoundItems.ItemHeight = 20;
			this.lstSoundItems.Location = new System.Drawing.Point(10, 310);
			this.lstSoundItems.Name = "lstSoundItems";
			this.lstSoundItems.Size = new System.Drawing.Size(232, 82);
			this.lstSoundItems.TabIndex = 125;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label9.Location = new System.Drawing.Point(6, 262);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(200, 40);
			this.label9.TabIndex = 124;
			this.label9.Text = "If any of the following items\r\nare dropped, play a sound";
			// 
			// numWalkSpeed
			// 
			this.numWalkSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numWalkSpeed.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numWalkSpeed.Location = new System.Drawing.Point(414, 192);
			this.numWalkSpeed.LoopValues = false;
			this.numWalkSpeed.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.numWalkSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numWalkSpeed.Name = "numWalkSpeed";
			this.numWalkSpeed.Size = new System.Drawing.Size(60, 28);
			this.numWalkSpeed.TabIndex = 123;
			this.numWalkSpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numWalkSpeed.ValueChanged += new System.EventHandler(this.numWalkSpeed_ValueChanged);
			// 
			// chkSkipCutscenes
			// 
			this.chkSkipCutscenes.AutoSize = true;
			this.chkSkipCutscenes.Location = new System.Drawing.Point(295, 163);
			this.chkSkipCutscenes.Name = "chkSkipCutscenes";
			this.chkSkipCutscenes.Size = new System.Drawing.Size(143, 24);
			this.chkSkipCutscenes.TabIndex = 121;
			this.chkSkipCutscenes.Text = "Skip cutscenes";
			this.chkSkipCutscenes.CheckedChanged += new System.EventHandler(this.chkSkipCutscenes_CheckedChanged);
			// 
			// chkHidePlayers
			// 
			this.chkHidePlayers.AutoSize = true;
			this.chkHidePlayers.Location = new System.Drawing.Point(295, 133);
			this.chkHidePlayers.Name = "chkHidePlayers";
			this.chkHidePlayers.Size = new System.Drawing.Size(122, 24);
			this.chkHidePlayers.TabIndex = 120;
			this.chkHidePlayers.Text = "Hide players";
			this.chkHidePlayers.CheckedChanged += new System.EventHandler(this.chkHidePlayers_CheckedChanged);
			// 
			// chkLag
			// 
			this.chkLag.AutoSize = true;
			this.chkLag.Location = new System.Drawing.Point(295, 106);
			this.chkLag.Name = "chkLag";
			this.chkLag.Size = new System.Drawing.Size(97, 24);
			this.chkLag.TabIndex = 119;
			this.chkLag.Text = "Lag killer";
			this.chkLag.CheckedChanged += new System.EventHandler(this.chkLag_CheckedChanged);
			// 
			// chkMagnet
			// 
			this.chkMagnet.AutoSize = true;
			this.chkMagnet.Location = new System.Drawing.Point(295, 75);
			this.chkMagnet.Name = "chkMagnet";
			this.chkMagnet.Size = new System.Drawing.Size(142, 24);
			this.chkMagnet.TabIndex = 118;
			this.chkMagnet.Text = "Enemy magnet";
			this.chkMagnet.CheckedChanged += new System.EventHandler(this.chkMagnet_CheckedChanged);
			// 
			// chkProvoke
			// 
			this.chkProvoke.AutoSize = true;
			this.chkProvoke.Location = new System.Drawing.Point(295, 45);
			this.chkProvoke.Name = "chkProvoke";
			this.chkProvoke.Size = new System.Drawing.Size(162, 24);
			this.chkProvoke.TabIndex = 117;
			this.chkProvoke.Text = "Provoke monsters";
			this.chkProvoke.CheckedChanged += new System.EventHandler(this.chkProvoke_CheckedChanged);
			// 
			// chkInfiniteRange
			// 
			this.chkInfiniteRange.AutoSize = true;
			this.chkInfiniteRange.Location = new System.Drawing.Point(295, 15);
			this.chkInfiniteRange.Name = "chkInfiniteRange";
			this.chkInfiniteRange.Size = new System.Drawing.Size(176, 24);
			this.chkInfiniteRange.TabIndex = 116;
			this.chkInfiniteRange.Text = "Infinite attack range";
			this.chkInfiniteRange.CheckedChanged += new System.EventHandler(this.chkInfiniteRange_CheckedChanged);
			// 
			// grpLogin
			// 
			this.grpLogin.Controls.Add(this.chkAFK);
			this.grpLogin.Controls.Add(this.cbServers);
			this.grpLogin.Controls.Add(this.chkRelogRetry);
			this.grpLogin.Controls.Add(this.chkRelog);
			this.grpLogin.Controls.Add(this.numRelogDelay);
			this.grpLogin.Controls.Add(this.label7);
			this.grpLogin.Location = new System.Drawing.Point(4, 3);
			this.grpLogin.Name = "grpLogin";
			this.grpLogin.Size = new System.Drawing.Size(238, 227);
			this.grpLogin.TabIndex = 115;
			this.grpLogin.TabStop = false;
			this.grpLogin.Text = "Auto relogin";
			// 
			// chkAFK
			// 
			this.chkAFK.AutoSize = true;
			this.chkAFK.Enabled = false;
			this.chkAFK.Location = new System.Drawing.Point(13, 96);
			this.chkAFK.Name = "chkAFK";
			this.chkAFK.Size = new System.Drawing.Size(146, 24);
			this.chkAFK.TabIndex = 159;
			this.chkAFK.Text = "Relogin on AFK";
			this.chkAFK.CheckedChanged += new System.EventHandler(this.chkAFK_CheckedChanged);
			// 
			// cbServers
			// 
			this.cbServers.DisplayMember = "Name";
			this.cbServers.FormattingEnabled = true;
			this.cbServers.ItemHeight = 27;
			this.cbServers.Location = new System.Drawing.Point(13, 27);
			this.cbServers.Name = "cbServers";
			this.cbServers.Size = new System.Drawing.Size(212, 33);
			this.cbServers.TabIndex = 0;
			this.cbServers.ValueMember = "Name";
			// 
			// chkRelogRetry
			// 
			this.chkRelogRetry.AutoSize = true;
			this.chkRelogRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkRelogRetry.Location = new System.Drawing.Point(15, 193);
			this.chkRelogRetry.Name = "chkRelogRetry";
			this.chkRelogRetry.Size = new System.Drawing.Size(210, 24);
			this.chkRelogRetry.TabIndex = 88;
			this.chkRelogRetry.Text = "Relog again if in battleon";
			// 
			// chkRelog
			// 
			this.chkRelog.AutoSize = true;
			this.chkRelog.Location = new System.Drawing.Point(13, 66);
			this.chkRelog.Name = "chkRelog";
			this.chkRelog.Size = new System.Drawing.Size(120, 24);
			this.chkRelog.TabIndex = 1;
			this.chkRelog.Text = "Auto relogin";
			this.chkRelog.CheckedChanged += new System.EventHandler(this.chkRelog_CheckedChanged);
			// 
			// numRelogDelay
			// 
			this.numRelogDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numRelogDelay.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numRelogDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numRelogDelay.Location = new System.Drawing.Point(15, 157);
			this.numRelogDelay.LoopValues = false;
			this.numRelogDelay.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.numRelogDelay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numRelogDelay.Name = "numRelogDelay";
			this.numRelogDelay.Size = new System.Drawing.Size(84, 28);
			this.numRelogDelay.TabIndex = 86;
			this.numRelogDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label7.Location = new System.Drawing.Point(12, 131);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(210, 20);
			this.label7.TabIndex = 87;
			this.label7.Text = "Delay before starting the bot";
			// 
			// tabOptions2
			// 
			this.tabOptions2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabOptions2.Controls.Add(this.darkGroupBox27);
			this.tabOptions2.Controls.Add(this.darkLabel11);
			this.tabOptions2.Controls.Add(this.btnSetFPSCmd);
			this.tabOptions2.Controls.Add(this.btnSetSpawn2);
			this.tabOptions2.Controls.Add(this.darkGroupBox23);
			this.tabOptions2.Controls.Add(this.btnReloadMap);
			this.tabOptions2.Controls.Add(this.btnHideLoading);
			this.tabOptions2.Controls.Add(this.darkGroupBox22);
			this.tabOptions2.Controls.Add(this.colorfulCommands);
			this.tabOptions2.Controls.Add(this.darkGroupBox16);
			this.tabOptions2.Controls.Add(this.chkGender);
			this.tabOptions2.Controls.Add(this.numSetFPS);
			this.tabOptions2.Controls.Add(this.btnSetFPS);
			this.tabOptions2.Controls.Add(this.btnSetLevelCmd);
			this.tabOptions2.Controls.Add(this.btnSetLevel);
			this.tabOptions2.Controls.Add(this.tbLevel);
			this.tabOptions2.Controls.Add(this.groupBox1);
			this.tabOptions2.Controls.Add(this.grpAccessLevel);
			this.tabOptions2.Controls.Add(this.grpAlignment);
			this.tabOptions2.Controls.Add(this.txtUsername);
			this.tabOptions2.Controls.Add(this.btnChangeNameCmd);
			this.tabOptions2.Controls.Add(this.btnchangeName);
			this.tabOptions2.Controls.Add(this.btnChangeGuildCmd);
			this.tabOptions2.Controls.Add(this.btnchangeGuild);
			this.tabOptions2.Controls.Add(this.txtGuild);
			this.tabOptions2.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabOptions2.Location = new System.Drawing.Point(4, 33);
			this.tabOptions2.Margin = new System.Windows.Forms.Padding(0);
			this.tabOptions2.Name = "tabOptions2";
			this.tabOptions2.Padding = new System.Windows.Forms.Padding(3);
			this.tabOptions2.Size = new System.Drawing.Size(818, 462);
			this.tabOptions2.TabIndex = 7;
			this.tabOptions2.Text = "Client";
			// 
			// darkGroupBox27
			// 
			this.darkGroupBox27.Controls.Add(this.tbLoadMap);
			this.darkGroupBox27.Controls.Add(this.btnLoadMap);
			this.darkGroupBox27.Location = new System.Drawing.Point(294, 251);
			this.darkGroupBox27.Name = "darkGroupBox27";
			this.darkGroupBox27.Size = new System.Drawing.Size(150, 101);
			this.darkGroupBox27.TabIndex = 176;
			this.darkGroupBox27.TabStop = false;
			this.darkGroupBox27.Text = "Load Map";
			// 
			// tbLoadMap
			// 
			this.tbLoadMap.Location = new System.Drawing.Point(6, 25);
			this.tbLoadMap.Multiline = true;
			this.tbLoadMap.Name = "tbLoadMap";
			this.tbLoadMap.Size = new System.Drawing.Size(138, 32);
			this.tbLoadMap.TabIndex = 146;
			this.tbLoadMap.Text = "Map file (.swf)";
			this.tbLoadMap.WordWrap = false;
			this.tbLoadMap.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbLoadMap.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnLoadMap
			// 
			this.btnLoadMap.Checked = false;
			this.btnLoadMap.Location = new System.Drawing.Point(6, 59);
			this.btnLoadMap.Name = "btnLoadMap";
			this.btnLoadMap.Size = new System.Drawing.Size(138, 33);
			this.btnLoadMap.TabIndex = 147;
			this.btnLoadMap.Text = "Load";
			this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
			// 
			// darkLabel11
			// 
			this.darkLabel11.AutoSize = true;
			this.darkLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel11.Location = new System.Drawing.Point(79, 312);
			this.darkLabel11.Name = "darkLabel11";
			this.darkLabel11.Size = new System.Drawing.Size(40, 20);
			this.darkLabel11.TabIndex = 168;
			this.darkLabel11.Text = "FPS";
			// 
			// btnSetFPSCmd
			// 
			this.btnSetFPSCmd.Checked = false;
			this.btnSetFPSCmd.Location = new System.Drawing.Point(183, 301);
			this.btnSetFPSCmd.Name = "btnSetFPSCmd";
			this.btnSetFPSCmd.Size = new System.Drawing.Size(72, 33);
			this.btnSetFPSCmd.TabIndex = 176;
			this.btnSetFPSCmd.Text = "(cmd)";
			this.btnSetFPSCmd.Click += new System.EventHandler(this.btnSetFPSCmd_Click);
			// 
			// btnSetSpawn2
			// 
			this.btnSetSpawn2.Checked = false;
			this.btnSetSpawn2.Location = new System.Drawing.Point(460, 158);
			this.btnSetSpawn2.Name = "btnSetSpawn2";
			this.btnSetSpawn2.Size = new System.Drawing.Size(138, 33);
			this.btnSetSpawn2.TabIndex = 168;
			this.btnSetSpawn2.Text = "Set Spawnpoint";
			this.btnSetSpawn2.Click += new System.EventHandler(this.btnSetSpawn2_Click);
			// 
			// darkGroupBox23
			// 
			this.darkGroupBox23.Controls.Add(this.txtSearchCmd);
			this.darkGroupBox23.Controls.Add(this.btnSearchCmd);
			this.darkGroupBox23.Location = new System.Drawing.Point(294, 6);
			this.darkGroupBox23.Name = "darkGroupBox23";
			this.darkGroupBox23.Size = new System.Drawing.Size(150, 98);
			this.darkGroupBox23.TabIndex = 175;
			this.darkGroupBox23.TabStop = false;
			this.darkGroupBox23.Text = "Search";
			// 
			// txtSearchCmd
			// 
			this.txtSearchCmd.Location = new System.Drawing.Point(6, 26);
			this.txtSearchCmd.Multiline = true;
			this.txtSearchCmd.Name = "txtSearchCmd";
			this.txtSearchCmd.Size = new System.Drawing.Size(138, 32);
			this.txtSearchCmd.TabIndex = 146;
			this.txtSearchCmd.Text = "Command Text";
			this.txtSearchCmd.WordWrap = false;
			// 
			// btnSearchCmd
			// 
			this.btnSearchCmd.Checked = false;
			this.btnSearchCmd.Location = new System.Drawing.Point(6, 60);
			this.btnSearchCmd.Name = "btnSearchCmd";
			this.btnSearchCmd.Size = new System.Drawing.Size(138, 33);
			this.btnSearchCmd.TabIndex = 147;
			this.btnSearchCmd.Text = "Search";
			this.btnSearchCmd.Click += new System.EventHandler(this.btnSearchCmd_Click);
			// 
			// btnReloadMap
			// 
			this.btnReloadMap.Checked = false;
			this.btnReloadMap.Location = new System.Drawing.Point(604, 158);
			this.btnReloadMap.Name = "btnReloadMap";
			this.btnReloadMap.Size = new System.Drawing.Size(138, 33);
			this.btnReloadMap.TabIndex = 173;
			this.btnReloadMap.Text = "Reload Map";
			this.btnReloadMap.Click += new System.EventHandler(this.btnReloadMap_Click);
			// 
			// btnHideLoading
			// 
			this.btnHideLoading.Checked = false;
			this.btnHideLoading.Location = new System.Drawing.Point(459, 197);
			this.btnHideLoading.Name = "btnHideLoading";
			this.btnHideLoading.Size = new System.Drawing.Size(139, 33);
			this.btnHideLoading.TabIndex = 174;
			this.btnHideLoading.Text = "Hide Loading";
			this.btnHideLoading.Click += new System.EventHandler(this.btnHideLoading_Click);
			// 
			// darkGroupBox22
			// 
			this.darkGroupBox22.Controls.Add(this.chkSaveProgress);
			this.darkGroupBox22.Controls.Add(this.numSaveProgress);
			this.darkGroupBox22.Controls.Add(this.darkLabel6);
			this.darkGroupBox22.Location = new System.Drawing.Point(450, 89);
			this.darkGroupBox22.Name = "darkGroupBox22";
			this.darkGroupBox22.Size = new System.Drawing.Size(329, 63);
			this.darkGroupBox22.TabIndex = 172;
			this.darkGroupBox22.TabStop = false;
			this.darkGroupBox22.Text = "Save state";
			// 
			// chkSaveProgress
			// 
			this.chkSaveProgress.AutoSize = true;
			this.chkSaveProgress.Location = new System.Drawing.Point(10, 26);
			this.chkSaveProgress.Name = "chkSaveProgress";
			this.chkSaveProgress.Size = new System.Drawing.Size(130, 24);
			this.chkSaveProgress.TabIndex = 165;
			this.chkSaveProgress.Text = "Relogin every";
			this.chkSaveProgress.CheckedChanged += new System.EventHandler(this.chkSaveProgress_CheckedChanged);
			this.chkSaveProgress.MouseHover += new System.EventHandler(this.chkSaveProgress_MouseHover);
			// 
			// numSaveProgress
			// 
			this.numSaveProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSaveProgress.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSaveProgress.Location = new System.Drawing.Point(142, 25);
			this.numSaveProgress.LoopValues = false;
			this.numSaveProgress.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this.numSaveProgress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSaveProgress.Name = "numSaveProgress";
			this.numSaveProgress.Size = new System.Drawing.Size(59, 28);
			this.numSaveProgress.TabIndex = 166;
			this.numSaveProgress.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// darkLabel6
			// 
			this.darkLabel6.AutoSize = true;
			this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel6.Location = new System.Drawing.Point(202, 30);
			this.darkLabel6.Name = "darkLabel6";
			this.darkLabel6.Size = new System.Drawing.Size(65, 20);
			this.darkLabel6.TabIndex = 167;
			this.darkLabel6.Text = "minutes";
			// 
			// colorfulCommands
			// 
			this.colorfulCommands.Checked = true;
			this.colorfulCommands.CheckState = System.Windows.Forms.CheckState.Checked;
			this.colorfulCommands.Location = new System.Drawing.Point(6, 431);
			this.colorfulCommands.Name = "colorfulCommands";
			this.colorfulCommands.Size = new System.Drawing.Size(363, 24);
			this.colorfulCommands.TabIndex = 169;
			this.colorfulCommands.Text = "Enable custom commands (CPU intensive)\r\n";
			// 
			// darkGroupBox16
			// 
			this.darkGroupBox16.Controls.Add(this.chkAMStopBot);
			this.darkGroupBox16.Controls.Add(this.chkAMLogout);
			this.darkGroupBox16.Controls.Add(this.chkAntiMod);
			this.darkGroupBox16.Location = new System.Drawing.Point(450, 6);
			this.darkGroupBox16.Name = "darkGroupBox16";
			this.darkGroupBox16.Size = new System.Drawing.Size(329, 71);
			this.darkGroupBox16.TabIndex = 151;
			this.darkGroupBox16.TabStop = false;
			this.darkGroupBox16.Text = "Anti Mod";
			// 
			// chkAMStopBot
			// 
			this.chkAMStopBot.AutoSize = true;
			this.chkAMStopBot.Checked = true;
			this.chkAMStopBot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAMStopBot.Location = new System.Drawing.Point(220, 28);
			this.chkAMStopBot.Name = "chkAMStopBot";
			this.chkAMStopBot.Size = new System.Drawing.Size(98, 24);
			this.chkAMStopBot.TabIndex = 171;
			this.chkAMStopBot.Text = "Stop Bot";
			// 
			// chkAMLogout
			// 
			this.chkAMLogout.AutoSize = true;
			this.chkAMLogout.Checked = true;
			this.chkAMLogout.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAMLogout.Location = new System.Drawing.Point(113, 28);
			this.chkAMLogout.Name = "chkAMLogout";
			this.chkAMLogout.Size = new System.Drawing.Size(85, 24);
			this.chkAMLogout.TabIndex = 170;
			this.chkAMLogout.Text = "Logout";
			// 
			// chkAntiMod
			// 
			this.chkAntiMod.AutoSize = true;
			this.chkAntiMod.Checked = true;
			this.chkAntiMod.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAntiMod.Location = new System.Drawing.Point(11, 28);
			this.chkAntiMod.Name = "chkAntiMod";
			this.chkAntiMod.Size = new System.Drawing.Size(85, 24);
			this.chkAntiMod.TabIndex = 169;
			this.chkAntiMod.Text = "Enable";
			this.chkAntiMod.MouseHover += new System.EventHandler(this.chkAntiMod_MouseHover);
			// 
			// chkGender
			// 
			this.chkGender.AutoSize = true;
			this.chkGender.Location = new System.Drawing.Point(6, 343);
			this.chkGender.Name = "chkGender";
			this.chkGender.Size = new System.Drawing.Size(130, 24);
			this.chkGender.TabIndex = 155;
			this.chkGender.Text = "Gender swap";
			this.chkGender.CheckedChanged += new System.EventHandler(this.chkGender_CheckedChanged);
			// 
			// numSetFPS
			// 
			this.numSetFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.numSetFPS.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSetFPS.Location = new System.Drawing.Point(12, 306);
			this.numSetFPS.LoopValues = false;
			this.numSetFPS.Name = "numSetFPS";
			this.numSetFPS.Size = new System.Drawing.Size(61, 28);
			this.numSetFPS.TabIndex = 154;
			this.numSetFPS.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// btnSetFPS
			// 
			this.btnSetFPS.Checked = false;
			this.btnSetFPS.Location = new System.Drawing.Point(136, 301);
			this.btnSetFPS.Name = "btnSetFPS";
			this.btnSetFPS.Size = new System.Drawing.Size(45, 33);
			this.btnSetFPS.TabIndex = 153;
			this.btnSetFPS.Text = "Set";
			this.btnSetFPS.Click += new System.EventHandler(this.btnSetFPS_Click);
			// 
			// btnSetLevelCmd
			// 
			this.btnSetLevelCmd.Checked = false;
			this.btnSetLevelCmd.Location = new System.Drawing.Point(183, 266);
			this.btnSetLevelCmd.Name = "btnSetLevelCmd";
			this.btnSetLevelCmd.Size = new System.Drawing.Size(72, 33);
			this.btnSetLevelCmd.TabIndex = 151;
			this.btnSetLevelCmd.Text = "(cmd)";
			this.btnSetLevelCmd.Click += new System.EventHandler(this.btnSetLevelCmd_Click);
			// 
			// btnSetLevel
			// 
			this.btnSetLevel.Checked = false;
			this.btnSetLevel.Location = new System.Drawing.Point(136, 266);
			this.btnSetLevel.Name = "btnSetLevel";
			this.btnSetLevel.Size = new System.Drawing.Size(45, 33);
			this.btnSetLevel.TabIndex = 150;
			this.btnSetLevel.Text = "Set";
			this.btnSetLevel.Click += new System.EventHandler(this.btnSetLevel_Click);
			// 
			// tbLevel
			// 
			this.tbLevel.Location = new System.Drawing.Point(6, 266);
			this.tbLevel.Multiline = true;
			this.tbLevel.Name = "tbLevel";
			this.tbLevel.Size = new System.Drawing.Size(128, 32);
			this.tbLevel.TabIndex = 149;
			this.tbLevel.Text = "Level";
			this.tbLevel.WordWrap = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnAddInfoMsg);
			this.groupBox1.Controls.Add(this.btnAddWarnMsg);
			this.groupBox1.Controls.Add(this.inputMsgClient);
			this.groupBox1.Location = new System.Drawing.Point(294, 110);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(150, 135);
			this.groupBox1.TabIndex = 148;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Messages";
			// 
			// btnAddInfoMsg
			// 
			this.btnAddInfoMsg.Checked = false;
			this.btnAddInfoMsg.Location = new System.Drawing.Point(6, 95);
			this.btnAddInfoMsg.Name = "btnAddInfoMsg";
			this.btnAddInfoMsg.Size = new System.Drawing.Size(138, 33);
			this.btnAddInfoMsg.TabIndex = 150;
			this.btnAddInfoMsg.Text = "Add Info";
			this.btnAddInfoMsg.Click += new System.EventHandler(this.btnAddInfoMsg_Click);
			// 
			// btnAddWarnMsg
			// 
			this.btnAddWarnMsg.Checked = false;
			this.btnAddWarnMsg.Location = new System.Drawing.Point(6, 60);
			this.btnAddWarnMsg.Name = "btnAddWarnMsg";
			this.btnAddWarnMsg.Size = new System.Drawing.Size(138, 33);
			this.btnAddWarnMsg.TabIndex = 149;
			this.btnAddWarnMsg.Text = "Add Warning";
			this.btnAddWarnMsg.Click += new System.EventHandler(this.btnAddWarnMsg_Click);
			// 
			// inputMsgClient
			// 
			this.inputMsgClient.Location = new System.Drawing.Point(6, 26);
			this.inputMsgClient.Multiline = true;
			this.inputMsgClient.Name = "inputMsgClient";
			this.inputMsgClient.Size = new System.Drawing.Size(138, 32);
			this.inputMsgClient.TabIndex = 147;
			this.inputMsgClient.Text = "Hello World!";
			this.inputMsgClient.WordWrap = false;
			// 
			// grpAccessLevel
			// 
			this.grpAccessLevel.Controls.Add(this.chkToggleMute);
			this.grpAccessLevel.Controls.Add(this.btnSetMem);
			this.grpAccessLevel.Controls.Add(this.btnSetModerator);
			this.grpAccessLevel.Controls.Add(this.btnSetNonMem);
			this.grpAccessLevel.Location = new System.Drawing.Point(151, 6);
			this.grpAccessLevel.Name = "grpAccessLevel";
			this.grpAccessLevel.Size = new System.Drawing.Size(137, 168);
			this.grpAccessLevel.TabIndex = 6;
			this.grpAccessLevel.TabStop = false;
			this.grpAccessLevel.Text = "Access";
			// 
			// chkToggleMute
			// 
			this.chkToggleMute.AutoSize = true;
			this.chkToggleMute.Location = new System.Drawing.Point(6, 134);
			this.chkToggleMute.Name = "chkToggleMute";
			this.chkToggleMute.Size = new System.Drawing.Size(123, 24);
			this.chkToggleMute.TabIndex = 6;
			this.chkToggleMute.Text = "Toggle Mute";
			this.chkToggleMute.CheckedChanged += new System.EventHandler(this.chkToggleMute_CheckedChanged);
			// 
			// btnSetMem
			// 
			this.btnSetMem.Checked = false;
			this.btnSetMem.Location = new System.Drawing.Point(6, 23);
			this.btnSetMem.Name = "btnSetMem";
			this.btnSetMem.Size = new System.Drawing.Size(123, 33);
			this.btnSetMem.TabIndex = 3;
			this.btnSetMem.Text = "Member";
			this.btnSetMem.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// btnSetModerator
			// 
			this.btnSetModerator.Checked = false;
			this.btnSetModerator.Location = new System.Drawing.Point(6, 93);
			this.btnSetModerator.Name = "btnSetModerator";
			this.btnSetModerator.Size = new System.Drawing.Size(123, 33);
			this.btnSetModerator.TabIndex = 5;
			this.btnSetModerator.Text = "Moderator";
			this.btnSetModerator.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// btnSetNonMem
			// 
			this.btnSetNonMem.Checked = false;
			this.btnSetNonMem.Location = new System.Drawing.Point(6, 58);
			this.btnSetNonMem.Name = "btnSetNonMem";
			this.btnSetNonMem.Size = new System.Drawing.Size(123, 33);
			this.btnSetNonMem.TabIndex = 4;
			this.btnSetNonMem.Text = "Non-Mem";
			this.btnSetNonMem.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// grpAlignment
			// 
			this.grpAlignment.Controls.Add(this.btnSetChaos);
			this.grpAlignment.Controls.Add(this.btnSetUndecided);
			this.grpAlignment.Controls.Add(this.btnSetGood);
			this.grpAlignment.Controls.Add(this.btnSetEvil);
			this.grpAlignment.Location = new System.Drawing.Point(6, 6);
			this.grpAlignment.Name = "grpAlignment";
			this.grpAlignment.Size = new System.Drawing.Size(135, 168);
			this.grpAlignment.TabIndex = 1;
			this.grpAlignment.TabStop = false;
			this.grpAlignment.Text = "Alignment";
			// 
			// btnSetChaos
			// 
			this.btnSetChaos.Checked = false;
			this.btnSetChaos.Location = new System.Drawing.Point(6, 92);
			this.btnSetChaos.Name = "btnSetChaos";
			this.btnSetChaos.Size = new System.Drawing.Size(123, 33);
			this.btnSetChaos.TabIndex = 0;
			this.btnSetChaos.Text = "Chaos";
			this.btnSetChaos.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// btnSetUndecided
			// 
			this.btnSetUndecided.Checked = false;
			this.btnSetUndecided.Location = new System.Drawing.Point(6, 127);
			this.btnSetUndecided.Name = "btnSetUndecided";
			this.btnSetUndecided.Size = new System.Drawing.Size(123, 33);
			this.btnSetUndecided.TabIndex = 0;
			this.btnSetUndecided.Text = "Undecided";
			this.btnSetUndecided.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// btnSetGood
			// 
			this.btnSetGood.Checked = false;
			this.btnSetGood.Location = new System.Drawing.Point(6, 22);
			this.btnSetGood.Name = "btnSetGood";
			this.btnSetGood.Size = new System.Drawing.Size(123, 33);
			this.btnSetGood.TabIndex = 0;
			this.btnSetGood.Text = "Good";
			this.btnSetGood.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// btnSetEvil
			// 
			this.btnSetEvil.Checked = false;
			this.btnSetEvil.Location = new System.Drawing.Point(6, 57);
			this.btnSetEvil.Name = "btnSetEvil";
			this.btnSetEvil.Size = new System.Drawing.Size(123, 33);
			this.btnSetEvil.TabIndex = 0;
			this.btnSetEvil.Text = "Evil";
			this.btnSetEvil.Click += new System.EventHandler(this.btnSetHero_Click);
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(6, 196);
			this.txtUsername.Multiline = true;
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(128, 33);
			this.txtUsername.TabIndex = 135;
			this.txtUsername.Text = "Username";
			this.txtUsername.WordWrap = false;
			// 
			// btnChangeNameCmd
			// 
			this.btnChangeNameCmd.Checked = false;
			this.btnChangeNameCmd.Location = new System.Drawing.Point(183, 196);
			this.btnChangeNameCmd.Name = "btnChangeNameCmd";
			this.btnChangeNameCmd.Size = new System.Drawing.Size(72, 33);
			this.btnChangeNameCmd.TabIndex = 142;
			this.btnChangeNameCmd.Text = "(cmd)";
			this.btnChangeNameCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
			// 
			// btnchangeName
			// 
			this.btnchangeName.Checked = false;
			this.btnchangeName.Location = new System.Drawing.Point(136, 196);
			this.btnchangeName.Name = "btnchangeName";
			this.btnchangeName.Size = new System.Drawing.Size(45, 33);
			this.btnchangeName.TabIndex = 142;
			this.btnchangeName.Text = "Set";
			this.btnchangeName.Click += new System.EventHandler(this.btnchangeName_Click);
			// 
			// btnChangeGuildCmd
			// 
			this.btnChangeGuildCmd.Checked = false;
			this.btnChangeGuildCmd.Location = new System.Drawing.Point(183, 231);
			this.btnChangeGuildCmd.Name = "btnChangeGuildCmd";
			this.btnChangeGuildCmd.Size = new System.Drawing.Size(72, 33);
			this.btnChangeGuildCmd.TabIndex = 143;
			this.btnChangeGuildCmd.Text = "(cmd)";
			this.btnChangeGuildCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
			// 
			// btnchangeGuild
			// 
			this.btnchangeGuild.Checked = false;
			this.btnchangeGuild.Location = new System.Drawing.Point(136, 231);
			this.btnchangeGuild.Name = "btnchangeGuild";
			this.btnchangeGuild.Size = new System.Drawing.Size(45, 33);
			this.btnchangeGuild.TabIndex = 143;
			this.btnchangeGuild.Text = "Set";
			this.btnchangeGuild.Click += new System.EventHandler(this.btnchangeGuild_Click);
			// 
			// txtGuild
			// 
			this.txtGuild.Location = new System.Drawing.Point(6, 231);
			this.txtGuild.Multiline = true;
			this.txtGuild.Name = "txtGuild";
			this.txtGuild.Size = new System.Drawing.Size(128, 33);
			this.txtGuild.TabIndex = 136;
			this.txtGuild.Text = "Guild";
			this.txtGuild.WordWrap = false;
			// 
			// tabHunt
			// 
			this.tabHunt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabHunt.Controls.Add(this.pictureBox1);
			this.tabHunt.Controls.Add(this.btnAddCmdHunt);
			this.tabHunt.Controls.Add(this.chkIsTempF);
			this.tabHunt.Controls.Add(this.chkAddToWhitelistF);
			this.tabHunt.Controls.Add(this.tbItemQtyF);
			this.tabHunt.Controls.Add(this.tbItemNameF);
			this.tabHunt.Controls.Add(this.tbMonNameF);
			this.tabHunt.Controls.Add(this.cbBlankFirstF);
			this.tabHunt.Controls.Add(this.btnGetMapF);
			this.tabHunt.Controls.Add(this.tbPadF);
			this.tabHunt.Controls.Add(this.tbCellF);
			this.tabHunt.Controls.Add(this.tbMapF);
			this.tabHunt.Controls.Add(this.darkLabel2);
			this.tabHunt.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabHunt.Location = new System.Drawing.Point(4, 33);
			this.tabHunt.Margin = new System.Windows.Forms.Padding(0);
			this.tabHunt.Name = "tabHunt";
			this.tabHunt.Padding = new System.Windows.Forms.Padding(3);
			this.tabHunt.Size = new System.Drawing.Size(818, 462);
			this.tabHunt.TabIndex = 3;
			this.tabHunt.Text = "Hunt";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.pictureBox1.Image = global::Properties.Resources.astolfo_head;
			this.pictureBox1.Location = new System.Drawing.Point(637, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(178, 456);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 178;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// btnAddCmdHunt
			// 
			this.btnAddCmdHunt.Checked = false;
			this.btnAddCmdHunt.Location = new System.Drawing.Point(11, 280);
			this.btnAddCmdHunt.Name = "btnAddCmdHunt";
			this.btnAddCmdHunt.Size = new System.Drawing.Size(183, 33);
			this.btnAddCmdHunt.TabIndex = 170;
			this.btnAddCmdHunt.Text = "Add Command";
			this.btnAddCmdHunt.Click += new System.EventHandler(this.btnAddCmdHunt_Click);
			// 
			// chkIsTempF
			// 
			this.chkIsTempF.AutoSize = true;
			this.chkIsTempF.Location = new System.Drawing.Point(201, 186);
			this.chkIsTempF.Name = "chkIsTempF";
			this.chkIsTempF.Size = new System.Drawing.Size(90, 24);
			this.chkIsTempF.TabIndex = 169;
			this.chkIsTempF.Text = "is Temp";
			// 
			// chkAddToWhitelistF
			// 
			this.chkAddToWhitelistF.AutoSize = true;
			this.chkAddToWhitelistF.Checked = true;
			this.chkAddToWhitelistF.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAddToWhitelistF.Location = new System.Drawing.Point(13, 249);
			this.chkAddToWhitelistF.Name = "chkAddToWhitelistF";
			this.chkAddToWhitelistF.Size = new System.Drawing.Size(144, 24);
			this.chkAddToWhitelistF.TabIndex = 165;
			this.chkAddToWhitelistF.Text = "add to Whitelist";
			// 
			// tbItemQtyF
			// 
			this.tbItemQtyF.Location = new System.Drawing.Point(11, 211);
			this.tbItemQtyF.Multiline = true;
			this.tbItemQtyF.Name = "tbItemQtyF";
			this.tbItemQtyF.Size = new System.Drawing.Size(184, 32);
			this.tbItemQtyF.TabIndex = 164;
			this.tbItemQtyF.Text = "Quantity (* = any)";
			this.tbItemQtyF.WordWrap = false;
			this.tbItemQtyF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbItemQtyF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbItemNameF
			// 
			this.tbItemNameF.Location = new System.Drawing.Point(11, 178);
			this.tbItemNameF.Multiline = true;
			this.tbItemNameF.Name = "tbItemNameF";
			this.tbItemNameF.Size = new System.Drawing.Size(184, 32);
			this.tbItemNameF.TabIndex = 163;
			this.tbItemNameF.Text = "Item name";
			this.tbItemNameF.WordWrap = false;
			this.tbItemNameF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbItemNameF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbMonNameF
			// 
			this.tbMonNameF.Location = new System.Drawing.Point(11, 144);
			this.tbMonNameF.Multiline = true;
			this.tbMonNameF.Name = "tbMonNameF";
			this.tbMonNameF.Size = new System.Drawing.Size(184, 32);
			this.tbMonNameF.TabIndex = 162;
			this.tbMonNameF.Text = "Monster (* = random)";
			this.tbMonNameF.WordWrap = false;
			this.tbMonNameF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbMonNameF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// cbBlankFirstF
			// 
			this.cbBlankFirstF.AutoSize = true;
			this.cbBlankFirstF.Checked = true;
			this.cbBlankFirstF.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbBlankFirstF.Location = new System.Drawing.Point(12, 113);
			this.cbBlankFirstF.Name = "cbBlankFirstF";
			this.cbBlankFirstF.Size = new System.Drawing.Size(183, 24);
			this.cbBlankFirstF.TabIndex = 161;
			this.cbBlankFirstF.Text = "Blank first before join";
			// 
			// btnGetMapF
			// 
			this.btnGetMapF.Checked = false;
			this.btnGetMapF.Location = new System.Drawing.Point(201, 42);
			this.btnGetMapF.Name = "btnGetMapF";
			this.btnGetMapF.Size = new System.Drawing.Size(66, 33);
			this.btnGetMapF.TabIndex = 160;
			this.btnGetMapF.Text = "Get";
			this.btnGetMapF.Click += new System.EventHandler(this.btnGetMapF_Click);
			// 
			// tbPadF
			// 
			this.tbPadF.Location = new System.Drawing.Point(104, 76);
			this.tbPadF.Multiline = true;
			this.tbPadF.Name = "tbPadF";
			this.tbPadF.Size = new System.Drawing.Size(91, 32);
			this.tbPadF.TabIndex = 159;
			this.tbPadF.Text = "Spawn";
			this.tbPadF.WordWrap = false;
			this.tbPadF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbPadF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbCellF
			// 
			this.tbCellF.Location = new System.Drawing.Point(11, 76);
			this.tbCellF.Multiline = true;
			this.tbCellF.Name = "tbCellF";
			this.tbCellF.Size = new System.Drawing.Size(91, 32);
			this.tbCellF.TabIndex = 158;
			this.tbCellF.Text = "Enter";
			this.tbCellF.WordWrap = false;
			this.tbCellF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbCellF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbMapF
			// 
			this.tbMapF.Location = new System.Drawing.Point(11, 42);
			this.tbMapF.Multiline = true;
			this.tbMapF.Name = "tbMapF";
			this.tbMapF.Size = new System.Drawing.Size(184, 32);
			this.tbMapF.TabIndex = 157;
			this.tbMapF.Text = "battleon-1e99";
			this.tbMapF.WordWrap = false;
			this.tbMapF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbMapF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// darkLabel2
			// 
			this.darkLabel2.AutoSize = true;
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(9, 12);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(108, 20);
			this.darkLabel2.TabIndex = 19;
			this.darkLabel2.Text = "Short Hunting";
			// 
			// tabBots
			// 
			this.tabBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabBots.Controls.Add(this.btnSetBotsDir);
			this.tabBots.Controls.Add(this.txtSavedDesc);
			this.tabBots.Controls.Add(this.darkPanel1);
			this.tabBots.Controls.Add(this.lblBoosts);
			this.tabBots.Controls.Add(this.lblDrops);
			this.tabBots.Controls.Add(this.lblQuests);
			this.tabBots.Controls.Add(this.lblSkills);
			this.tabBots.Controls.Add(this.lblCommands);
			this.tabBots.Controls.Add(this.lblItems);
			this.tabBots.Controls.Add(this.txtSavedAuthor);
			this.tabBots.Controls.Add(this.lblBots);
			this.tabBots.Controls.Add(this.txtSavedAdd);
			this.tabBots.Controls.Add(this.btnSavedAdd);
			this.tabBots.Controls.Add(this.txtSaved);
			this.tabBots.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabBots.Location = new System.Drawing.Point(4, 33);
			this.tabBots.Margin = new System.Windows.Forms.Padding(0);
			this.tabBots.Name = "tabBots";
			this.tabBots.Padding = new System.Windows.Forms.Padding(3);
			this.tabBots.Size = new System.Drawing.Size(818, 462);
			this.tabBots.TabIndex = 6;
			this.tabBots.Text = "Bots";
			// 
			// btnSetBotsDir
			// 
			this.btnSetBotsDir.Checked = false;
			this.btnSetBotsDir.Location = new System.Drawing.Point(744, 4);
			this.btnSetBotsDir.Name = "btnSetBotsDir";
			this.btnSetBotsDir.Size = new System.Drawing.Size(73, 33);
			this.btnSetBotsDir.TabIndex = 149;
			this.btnSetBotsDir.Text = "Set";
			this.btnSetBotsDir.Click += new System.EventHandler(this.btnSetBotsDir_Click);
			// 
			// txtSavedDesc
			// 
			this.txtSavedDesc.Location = new System.Drawing.Point(429, 142);
			this.txtSavedDesc.MaxLength = 2147483647;
			this.txtSavedDesc.Multiline = true;
			this.txtSavedDesc.Name = "txtSavedDesc";
			this.txtSavedDesc.ReadOnly = true;
			this.txtSavedDesc.Size = new System.Drawing.Size(388, 321);
			this.txtSavedDesc.TabIndex = 21;
			this.txtSavedDesc.Text = "Description";
			this.txtSavedDesc.WordWrap = false;
			// 
			// darkPanel1
			// 
			this.darkPanel1.Controls.Add(this.treeBots);
			this.darkPanel1.Location = new System.Drawing.Point(3, 40);
			this.darkPanel1.Name = "darkPanel1";
			this.darkPanel1.Size = new System.Drawing.Size(420, 425);
			this.darkPanel1.TabIndex = 148;
			// 
			// treeBots
			// 
			this.treeBots.AllowDrop = true;
			this.treeBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.treeBots.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeBots.ForeColor = System.Drawing.Color.Gainsboro;
			this.treeBots.LineColor = System.Drawing.Color.DarkGray;
			this.treeBots.Location = new System.Drawing.Point(0, 0);
			this.treeBots.Name = "treeBots";
			this.treeBots.Size = new System.Drawing.Size(420, 425);
			this.treeBots.TabIndex = 17;
			this.treeBots.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterExpand);
			this.treeBots.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterSelect);
			// 
			// lblBoosts
			// 
			this.lblBoosts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblBoosts.AutoSize = true;
			this.lblBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblBoosts.Location = new System.Drawing.Point(254, -11353);
			this.lblBoosts.Name = "lblBoosts";
			this.lblBoosts.Size = new System.Drawing.Size(63, 20);
			this.lblBoosts.TabIndex = 25;
			this.lblBoosts.Text = "Boosts:";
			this.lblBoosts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblDrops
			// 
			this.lblDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDrops.AutoSize = true;
			this.lblDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblDrops.Location = new System.Drawing.Point(198, -11353);
			this.lblDrops.Name = "lblDrops";
			this.lblDrops.Size = new System.Drawing.Size(56, 20);
			this.lblDrops.TabIndex = 24;
			this.lblDrops.Text = "Drops:";
			this.lblDrops.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblQuests
			// 
			this.lblQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblQuests.AutoSize = true;
			this.lblQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblQuests.Location = new System.Drawing.Point(140, -11353);
			this.lblQuests.Name = "lblQuests";
			this.lblQuests.Size = new System.Drawing.Size(64, 20);
			this.lblQuests.TabIndex = 23;
			this.lblQuests.Text = "Quests:";
			this.lblQuests.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblSkills
			// 
			this.lblSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblSkills.AutoSize = true;
			this.lblSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblSkills.Location = new System.Drawing.Point(87, -11353);
			this.lblSkills.Name = "lblSkills";
			this.lblSkills.Size = new System.Drawing.Size(49, 20);
			this.lblSkills.TabIndex = 22;
			this.lblSkills.Text = "Skills:";
			this.lblSkills.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblCommands
			// 
			this.lblCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCommands.AutoSize = true;
			this.lblCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblCommands.Location = new System.Drawing.Point(6, -11366);
			this.lblCommands.Name = "lblCommands";
			this.lblCommands.Size = new System.Drawing.Size(94, 40);
			this.lblCommands.TabIndex = 21;
			this.lblCommands.Text = "Number of\r\nCommands:";
			this.lblCommands.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblItems
			// 
			this.lblItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblItems.AutoSize = true;
			this.lblItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblItems.Location = new System.Drawing.Point(316, -11353);
			this.lblItems.Name = "lblItems";
			this.lblItems.Size = new System.Drawing.Size(53, 20);
			this.lblItems.TabIndex = 146;
			this.lblItems.Text = "Items:";
			this.lblItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// txtSavedAuthor
			// 
			this.txtSavedAuthor.Location = new System.Drawing.Point(429, 107);
			this.txtSavedAuthor.Multiline = true;
			this.txtSavedAuthor.Name = "txtSavedAuthor";
			this.txtSavedAuthor.ReadOnly = true;
			this.txtSavedAuthor.Size = new System.Drawing.Size(388, 32);
			this.txtSavedAuthor.TabIndex = 19;
			this.txtSavedAuthor.Text = "Author";
			this.txtSavedAuthor.WordWrap = false;
			// 
			// lblBots
			// 
			this.lblBots.AutoSize = true;
			this.lblBots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblBots.Location = new System.Drawing.Point(425, 80);
			this.lblBots.Name = "lblBots";
			this.lblBots.Size = new System.Drawing.Size(124, 20);
			this.lblBots.TabIndex = 18;
			this.lblBots.Text = "Number of Bots:";
			// 
			// txtSavedAdd
			// 
			this.txtSavedAdd.Location = new System.Drawing.Point(429, 40);
			this.txtSavedAdd.Multiline = true;
			this.txtSavedAdd.Name = "txtSavedAdd";
			this.txtSavedAdd.Size = new System.Drawing.Size(284, 32);
			this.txtSavedAdd.TabIndex = 16;
			this.txtSavedAdd.WordWrap = false;
			// 
			// btnSavedAdd
			// 
			this.btnSavedAdd.Checked = false;
			this.btnSavedAdd.Location = new System.Drawing.Point(719, 40);
			this.btnSavedAdd.Name = "btnSavedAdd";
			this.btnSavedAdd.Size = new System.Drawing.Size(98, 33);
			this.btnSavedAdd.TabIndex = 15;
			this.btnSavedAdd.Text = "Add folder";
			this.btnSavedAdd.Click += new System.EventHandler(this.btnSavedAdd_Click);
			// 
			// txtSaved
			// 
			this.txtSaved.Location = new System.Drawing.Point(4, 4);
			this.txtSaved.Multiline = true;
			this.txtSaved.Name = "txtSaved";
			this.txtSaved.Size = new System.Drawing.Size(735, 32);
			this.txtSaved.TabIndex = 13;
			this.txtSaved.WordWrap = false;
			this.txtSaved.TextChanged += new System.EventHandler(this.txtSaved_TextChanged);
			// 
			// tabInfo
			// 
			this.tabInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.tabInfo.Controls.Add(this.panel5);
			this.tabInfo.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabInfo.Location = new System.Drawing.Point(4, 33);
			this.tabInfo.Name = "tabInfo";
			this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tabInfo.Size = new System.Drawing.Size(818, 462);
			this.tabInfo.TabIndex = 9;
			this.tabInfo.Text = "Info";
			this.tabInfo.ToolTipText = "The Info about the bot you\'ve loaded";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.richTextBox2);
			this.panel5.Controls.Add(this.rtbInfo);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(3, 3);
			this.panel5.Margin = new System.Windows.Forms.Padding(5);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(812, 456);
			this.panel5.TabIndex = 0;
			// 
			// richTextBox2
			// 
			this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
			this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox2.ForeColor = System.Drawing.Color.Silver;
			this.richTextBox2.Location = new System.Drawing.Point(0, 418);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size(129, 38);
			this.richTextBox2.TabIndex = 1;
			this.richTextBox2.Text = "Test here";
			this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
			// 
			// rtbInfo
			// 
			this.rtbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbInfo.ForeColor = System.Drawing.Color.Gainsboro;
			this.rtbInfo.Location = new System.Drawing.Point(0, 0);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.ReadOnly = true;
			this.rtbInfo.Size = new System.Drawing.Size(812, 456);
			this.rtbInfo.TabIndex = 0;
			this.rtbInfo.Text = "This is where information about a bot will be shown in RichTextFormat";
			this.rtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInfo_LinkClicked);
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 397);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.splitContainer1.Panel1.Controls.Add(this.cbLists);
			this.splitContainer1.Panel1.Controls.Add(this.panel3);
			this.splitContainer1.Panel1.Controls.Add(this.chkAll);
			this.splitContainer1.Panel1.Controls.Add(this.btnClear);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.chkEnable);
			this.splitContainer1.Panel2.Controls.Add(this.panel4);
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(381, 102);
			this.splitContainer1.SplitterDistance = 180;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 149;
			// 
			// cbLists
			// 
			this.cbLists.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cbLists.FormattingEnabled = true;
			this.cbLists.ItemHeight = 27;
			this.cbLists.Items.AddRange(new object[] {
            "Commands",
            "Skills",
            "Quests",
            "Drops",
            "Boosts",
            "Items"});
			this.cbLists.Location = new System.Drawing.Point(0, 69);
			this.cbLists.Name = "cbLists";
			this.cbLists.Size = new System.Drawing.Size(180, 33);
			this.cbLists.TabIndex = 169;
			this.cbLists.SelectedIndexChanged += new System.EventHandler(this.cbLists_SelectedIndexChanged);
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.btnDown);
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(174, 32);
			this.panel3.TabIndex = 148;
			// 
			// btnDown
			// 
			this.btnDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnDown.Checked = false;
			this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDown.Location = new System.Drawing.Point(0, 0);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(174, 32);
			this.btnDown.TabIndex = 166;
			this.btnDown.Text = "▼";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// chkAll
			// 
			this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAll.AutoSize = true;
			this.chkAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
			this.chkAll.Location = new System.Drawing.Point(124, 37);
			this.chkAll.Name = "chkAll";
			this.chkAll.Size = new System.Drawing.Size(50, 24);
			this.chkAll.TabIndex = 168;
			this.chkAll.Text = "all";
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Checked = false;
			this.btnClear.Location = new System.Drawing.Point(0, 34);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(116, 33);
			this.btnClear.TabIndex = 167;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// chkEnable
			// 
			this.chkEnable.AutoSize = true;
			this.chkEnable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.chkEnable.Location = new System.Drawing.Point(37, 72);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(114, 24);
			this.chkEnable.TabIndex = 169;
			this.chkEnable.Text = "Enable Bot";
			this.chkEnable.UseVisualStyleBackColor = true;
			this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnableBot_CheckedChanged);
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.panel4.Controls.Add(this.btnRemove);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 32);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(200, 34);
			this.panel4.TabIndex = 148;
			// 
			// btnRemove
			// 
			this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnRemove.Checked = false;
			this.btnRemove.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnRemove.Location = new System.Drawing.Point(0, 0);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(200, 33);
			this.btnRemove.TabIndex = 166;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// panel2
			// 
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.Controls.Add(this.btnUp);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 32);
			this.panel2.TabIndex = 147;
			// 
			// btnUp
			// 
			this.btnUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnUp.Checked = false;
			this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnUp.Location = new System.Drawing.Point(0, 0);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(200, 32);
			this.btnUp.TabIndex = 165;
			this.btnUp.Text = "▲";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.panel1.Controls.Add(this.splitContainer1);
			this.panel1.Controls.Add(this.lstCommands);
			this.panel1.Controls.Add(this.lstDrops);
			this.panel1.Controls.Add(this.lstBoosts);
			this.panel1.Controls.Add(this.lstQuests);
			this.panel1.Controls.Add(this.lstSkills);
			this.panel1.Controls.Add(this.lstItems);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(381, 499);
			this.panel1.TabIndex = 150;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(7, 7);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(10);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.panel1);
			this.splitContainer2.Panel1MinSize = 30;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.splitContainer2.Panel2.Controls.Add(this.mainTabControl);
			this.splitContainer2.Size = new System.Drawing.Size(1211, 499);
			this.splitContainer2.SplitterDistance = 381;
			this.splitContainer2.TabIndex = 150;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(150, 184);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(82, 17);
			this.checkBox1.TabIndex = 158;
			this.checkBox1.Text = "Placeholder";
			// 
			// chkBuffup
			// 
			this.chkBuffup.Location = new System.Drawing.Point(0, 0);
			this.chkBuffup.Name = "chkBuffup";
			this.chkBuffup.Size = new System.Drawing.Size(104, 24);
			this.chkBuffup.TabIndex = 0;
			// 
			// BotManagerMenuStrip
			// 
			this.BotManagerMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.BotManagerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontsToolStripMenuItem,
            this.multilineToggleToolStripMenuItem,
            this.toggleTabpagesToolStripMenuItem,
            this.commandColorsToolStripMenuItem});
			this.BotManagerMenuStrip.Name = "contextMenuStrip1";
			this.BotManagerMenuStrip.Size = new System.Drawing.Size(263, 132);
			// 
			// changeFontsToolStripMenuItem
			// 
			this.changeFontsToolStripMenuItem.Name = "changeFontsToolStripMenuItem";
			this.changeFontsToolStripMenuItem.Size = new System.Drawing.Size(262, 32);
			this.changeFontsToolStripMenuItem.Text = "Change Fonts";
			this.changeFontsToolStripMenuItem.Click += new System.EventHandler(this.changeFontsToolStripMenuItem_Click);
			// 
			// multilineToggleToolStripMenuItem
			// 
			this.multilineToggleToolStripMenuItem.Name = "multilineToggleToolStripMenuItem";
			this.multilineToggleToolStripMenuItem.Size = new System.Drawing.Size(262, 32);
			this.multilineToggleToolStripMenuItem.Text = "Multiline Toggle";
			this.multilineToggleToolStripMenuItem.Click += new System.EventHandler(this.multilineToggleToolStripMenuItem_Click);
			// 
			// toggleTabpagesToolStripMenuItem
			// 
			this.toggleTabpagesToolStripMenuItem.Name = "toggleTabpagesToolStripMenuItem";
			this.toggleTabpagesToolStripMenuItem.Size = new System.Drawing.Size(262, 32);
			this.toggleTabpagesToolStripMenuItem.Text = "Toggle Tabpages";
			this.toggleTabpagesToolStripMenuItem.Click += new System.EventHandler(this.toggleTabpagesToolStripMenuItem_Click);
			// 
			// commandColorsToolStripMenuItem
			// 
			this.commandColorsToolStripMenuItem.Name = "commandColorsToolStripMenuItem";
			this.commandColorsToolStripMenuItem.Size = new System.Drawing.Size(262, 32);
			this.commandColorsToolStripMenuItem.Text = "Command Customizer";
			this.commandColorsToolStripMenuItem.Click += new System.EventHandler(this.commandColorsToolStripMenuItem_Click);
			// 
			// BotManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.ClientSize = new System.Drawing.Size(1225, 513);
			this.ContextMenuStrip = this.BotManagerMenuStrip;
			this.Controls.Add(this.splitContainer2);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = global::Properties.Resources.GrimoireIcon;
			this.Name = "BotManager";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = " Bot";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BotManager_FormClosing);
			this.Load += new System.EventHandler(this.BotManager_Load);
			this.mainTabControl.ResumeLayout(false);
			this.tabCombat.ResumeLayout(false);
			this.tabCombat.PerformLayout();
			this.darkGroupBox20.ResumeLayout(false);
			this.darkGroupBox20.PerformLayout();
			this.darkGroupBox19.ResumeLayout(false);
			this.darkGroupBox19.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRestMP)).EndInit();
			this.darkGroupBox17.ResumeLayout(false);
			this.darkGroupBox17.PerformLayout();
			this.darkGroupBox18.ResumeLayout(false);
			this.darkGroupBox18.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkillCmd)).EndInit();
			this.boxSkillSet.ResumeLayout(false);
			this.boxSkillSet.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSkill)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSafe)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillD)).EndInit();
			this.tabMap.ResumeLayout(false);
			this.tabMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numJoinTry)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkX)).EndInit();
			this.tabItem.ResumeLayout(false);
			this.tabItem.PerformLayout();
			this.darkGroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numMapItem)).EndInit();
			this.darkGroupBox4.ResumeLayout(false);
			this.darkGroupBox4.PerformLayout();
			this.darkGroupBox3.ResumeLayout(false);
			this.darkGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBuyQty)).EndInit();
			this.darkGroupBox2.ResumeLayout(false);
			this.tabQuest.ResumeLayout(false);
			this.darkGroupBox26.ResumeLayout(false);
			this.darkGroupBox26.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQQuestId)).EndInit();
			this.darkGroupBox25.ResumeLayout(false);
			this.darkGroupBox25.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQuestListID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestListItem)).EndInit();
			this.darkGroupBox24.ResumeLayout(false);
			this.darkGroupBox24.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQuestID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).EndInit();
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			this.darkGroupBox15.ResumeLayout(false);
			this.darkGroupBox15.PerformLayout();
			this.darkGroupBox11.ResumeLayout(false);
			this.darkGroupBox10.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).EndInit();
			this.darkGroupBox14.ResumeLayout(false);
			this.darkGroupBox14.PerformLayout();
			this.darkGroupBox12.ResumeLayout(false);
			this.darkGroupBox12.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetInt)).EndInit();
			this.darkGroupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).EndInit();
			this.darkGroupBox7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
			this.tabMisc2.ResumeLayout(false);
			this.tabMisc2.PerformLayout();
			this.darkGroupBox21.ResumeLayout(false);
			this.darkGroupBox21.PerformLayout();
			this.darkGroupBox13.ResumeLayout(false);
			this.darkGroupBox13.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).EndInit();
			this.darkGroupBox9.ResumeLayout(false);
			this.darkGroupBox9.PerformLayout();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.darkGroupBox1.ResumeLayout(false);
			this.darkGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBSDelay)).EndInit();
			this.tabOptions.ResumeLayout(false);
			this.tabOptions.PerformLayout();
			this.darkGroupBox6.ResumeLayout(false);
			this.darkGroupBox6.PerformLayout();
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
			this.splitContainer5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).EndInit();
			this.grpLogin.ResumeLayout(false);
			this.grpLogin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).EndInit();
			this.tabOptions2.ResumeLayout(false);
			this.tabOptions2.PerformLayout();
			this.darkGroupBox27.ResumeLayout(false);
			this.darkGroupBox27.PerformLayout();
			this.darkGroupBox23.ResumeLayout(false);
			this.darkGroupBox23.PerformLayout();
			this.darkGroupBox22.ResumeLayout(false);
			this.darkGroupBox22.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSaveProgress)).EndInit();
			this.darkGroupBox16.ResumeLayout(false);
			this.darkGroupBox16.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpAccessLevel.ResumeLayout(false);
			this.grpAccessLevel.PerformLayout();
			this.grpAlignment.ResumeLayout(false);
			this.tabHunt.ResumeLayout(false);
			this.tabHunt.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabBots.ResumeLayout(false);
			this.tabBots.PerformLayout();
			this.darkPanel1.ResumeLayout(false);
			this.tabInfo.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.BotManagerMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private DarkTextBox txtMonsterSkillCmd;
		private DarkGroupBox boxSkillSet;
		private DarkGroupBox darkGroupBox18;
		private DarkCheckBox cbSkillCmdWait;
		private DarkNumericUpDown numSkillCmd;
		private DarkButton btnReloadMap;
		private DarkComboBox cmbSpecials;
		private DarkCheckBox chkSpecial;
		private DarkGroupBox darkGroupBox19;
		private DarkGroupBox darkGroupBox17;
		private DarkGroupBox darkGroupBox20;
		private DarkLabel darkLabel7;
		private DarkGroupBox darkGroupBox21;
		private DarkCheckBox chkInventOnStart;
		private DarkButton btnSetBotsDir;
		private DarkGroupBox darkGroupBox22;
		private DarkButton btnHideLoading;
		private DarkGroupBox darkGroupBox23;
		private DarkListBox lbLabels;
		public ListBox lstCommands;
		public ListBox lstSkills;
		private ListBox lstQuests;
		public ListBox lstDrops;
		private ListBox lstBoosts;
		private ListBox lstItems;
		private DarkCheckBox chkAntiCounter;
		private DarkLabel darkLabel10;
		private DarkNumericUpDown numJoinTry;
		private DarkButton btnAddQuestList;
		private Label label1;
		private DarkNumericUpDown numBuyQty;
		private DarkLabel darkLabel8;
		public DarkNumericUpDown numQuestDelay;
		private DarkLabel darkLabel5;
		private DarkGroupBox darkGroupBox24;
		private DarkLabel darkLabel12;
		private DarkGroupBox darkGroupBox26;
		private DarkGroupBox darkGroupBox25;
		private DarkLabel darkLabel13;
		private DarkNumericUpDown numQuestListID;
		private DarkCheckBox chkQuestListItem;
		private DarkNumericUpDown numQuestListItem;
		private DarkButton btnRemoveQuestList;
		private DarkRadioButton radBuyByName;
		private DarkRadioButton radBuyByID;
		private DarkTextBox tbShopId;
		private DarkTextBox tbShopItemId;
		private DarkTextBox tbItemId;
		private DarkGroupBox darkGroupBox15;
		private DarkButton btnFollowCmd;
		private DarkTextBox tbFollowPlayer;
		private DarkLabel darkLabel11;
		private DarkButton btnSetFPSCmd;
		private DarkButton btnStopAttack;
		private DarkButton btnLeaveCombat;
		public DarkCheckBox chkRestartAFK;
		private DarkGroupBox darkGroupBox27;
		private DarkTextBox tbLoadMap;
		private DarkButton btnLoadMap;
		private DarkLabel darkLabel1;
		private DarkCheckBox cbJoinMapSwf;
		private DarkTextBox tbJoinMapSwf;
		private DarkButton btnMapItemExe;
		private FlatTabControl.FlatTabControl mainTabControl;
		private PictureBox pictureBox1;
	}
}