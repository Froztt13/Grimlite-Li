using AxShockwaveFlashObjects;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Botting;
using Grimoire.Game.Data;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using DarkUI.Controls;
using System.Reflection;
using System.Linq;
using Grimoire.FlashTools;
using Grimoire.Utils;
using System.Diagnostics;
using Grimoire.Botting.Commands.Combat;

namespace Grimoire.UI
{
    public class Root : Form
    {
        private IContainer components;

        private NotifyIcon nTray;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private Button btnMax;
        private Button btnExit;
        private Button btnMin;
        private ToolStripMenuItem managerToolStripMenuItem;
        private ToolStripMenuItem loadBotToolStripMenuItem;
        private Panel panel1;
        public MenuStrip MenuMain;
        private DarkComboBox cbPads;
        private DarkComboBox cbCells;
        private DarkButton btnBank;
        private DarkCheckBox chkAutoAttack;
        public DarkCheckBox chkStartBot;
        private DarkButton btnGetCell;
        public SplitContainer splitContainer1;
        public DarkMenuStrip darkMenuStrip1;
        private ToolStripMenuItem botToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem fastTravelsToolStripMenuItem;
        private ToolStripMenuItem loadersgrabbersToolStripMenuItem;
        private ToolStripMenuItem hotkeysToolStripMenuItem;
        private ToolStripMenuItem pluginManagerToolStripMenuItem;
        private ToolStripMenuItem cosmeticsToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem;
        private ToolStripMenuItem setsToolStripMenuItem;
        private ToolStripMenuItem eyeDropperToolStripMenuItem;
        private ToolStripMenuItem logsToolStripMenuItem1;
        private ToolStripMenuItem notepadToolStripMenuItem1;
        private ToolStripMenuItem changeServerMenuItem;
        public ToolStripComboBox changeServerList;
        private ToolStripMenuItem DPSMeterToolStripMenuItem;
        private ToolStripMenuItem commandeditornodeToolStripMenuItem;
        private ToolStripMenuItem packetsToolStripMenuItem;
        private ToolStripMenuItem snifferToolStripMenuItem;
        private ToolStripMenuItem spammerToolStripMenuItem;
        private ToolStripMenuItem tampererToolStripMenuItem;
        public ToolStripMenuItem optionsToolStripMenuItem;
        public ToolStripMenuItem infRangeToolStripMenuItem;
        public ToolStripMenuItem provokeToolStripMenuItem1;
        public ToolStripMenuItem enemyMagnetToolStripMenuItem;
        public ToolStripMenuItem lagKillerToolStripMenuItem;
        public ToolStripMenuItem hidePlayersToolStripMenuItem;
        public ToolStripMenuItem skipCutscenesToolStripMenuItem;
        public ToolStripMenuItem disableAnimationsToolStripMenuItem;
        public ToolStripMenuItem pluginsStrip;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem pvptoolStripMenuItem1;
        private Panel gameContainer;

        public static Root Instance
        {
            get;
            private set;
        }

        public AxShockwaveFlash flashPlayer = new AxShockwaveFlash();
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem devTestToolStripMenuItem;
        public ToolStripMenuItem walkspeedToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox1;
        public ToolStripMenuItem enableOptionsToolStripMenuItem;
        private ToolStripMenuItem menuCharSelect;
        public ToolStripMenuItem maidStrip;

        //public AxShockwaveFlash Client => flashPlayer;

        private string title = $"Grimlite Li";

        public Root()
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            InitializeComponent();
            this.CenterToScreen();
            Instance = this;
            this.Text = title;
        }

        private void Root_Load(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(Proxy.Instance.Start, TaskCreationOptions.LongRunning);
            InitFlashMovie(ClientConfig.GetValue(ClientConfig.C_FLASH));
            Hotkeys.Instance.LoadHotkeys();
            LoadPlugins();
            LoadCharSelect();
            auraClearer();
        }

        private void LoadPlugins()
        {
            Program.PluginsManager.LoadRange(Directory.GetFiles(Program.PluginsPath));
        }

        class CharInfo
        {
            public string Username;
            public string Password;
        }

        private List<CharInfo> chars = new List<CharInfo>();
        public void LoadCharSelect()
        {
            menuCharSelect.Visible = true;
            if (chars.Count > 0) return;
            Config config = Config.Load(Application.StartupPath + "\\CharSelect.cfg");
            int i = 0;
            while (i >= 0)
            {
                try
                {
                    string data = config.Get(i.ToString());
                    if (data == null)
                    {
                        i = -1;
                        return;
                    }
                    CharInfo charInfo = new CharInfo
                    {
                        Username = data.Split(',')[0],
                        Password = data.Split(',')[1]
                    };
                    chars.Add(charInfo);
                    menuCharSelect.DropDownItems.Add(charInfo.Username);
                    menuCharSelect.DropDownItems[i].Click -= new EventHandler(menuCharSelectItem_Click);
                    menuCharSelect.DropDownItems[i].Click += new EventHandler(menuCharSelectItem_Click);
                }
                catch
                {
                    i = -1;
                }
                i++;
            }
        }

        public void HideCharSelect()
        {
            menuCharSelect.Visible = false;
        }

        private void menuCharSelectItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            var menuText = menuItem.Text;
            foreach (var item in chars)
            {
                if (item.Username == menuText)
                    AutoRelogin.Login(item.Username, item.Password);
            }
        }

        private void Root_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hotkeys.InstalledHotkeys.ForEach(delegate (Hotkey h)
            {
                h.Uninstall();
            });
            KeyboardHook.Instance.Dispose();
            Proxy.Instance.Stop(appClosing: true);
            CommandColorForm.Instance.Dispose();
            nTray.Visible = false;
            nTray.Icon.Dispose();
            nTray.Dispose();
        }

        public void InitFlashMovie(string gameSwf)
        {
            //Hook.EoLHook.Hook();
            Flash.flash?.Dispose();
            Flash.SwfLoadProgress -= OnLoadProgress;
            Flash.SwfLoadProgress += OnLoadProgress;

            flashPlayer.BeginInit();
            flashPlayer.Name = "flash";
            flashPlayer.Dock = DockStyle.Fill;
            flashPlayer.TabIndex = 0;
            //flashPlayer.FlashCall += Flash.ProcessFlashCall;
            flashPlayer.FlashCall -= Flash.CallHandler;
            flashPlayer.FlashCall += Flash.CallHandler;
            flashPlayer.Visible = true;
            gameContainer.Controls.Add(flashPlayer);
            flashPlayer.EndInit();

            byte[] swf = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), gameSwf));

            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(8 + swf.Length);
                writer.Write(1432769894);
                writer.Write(swf.Length);
                writer.Write(swf);
                writer.Seek(0, SeekOrigin.Begin);
                flashPlayer.OcxState = new AxHost.State(stream, 1, false, null);
            }

            Flash.flash = flashPlayer;
            //Hook.EoLHook.Unhook();
        }

        private void OnLoadProgress(int progress)
        {
            if (progress < 100) return;
            FlashUtil.SwfLoadProgress -= OnLoadProgress;
            //flashPlayer.Visible = true;
        }

        public BotManager botManager = BotManager.Instance;

        private void botToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowForm(BotManager.Instance);
        }

        private void fastTravelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Travel.Instance);
        }

        private void loadersgrabbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Loaders.Instance);
        }

        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Hotkeys.Instance);
        }

        private void pluginManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PluginManager.Instance);
        }

        private void snifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketLogger.Instance);
        }

        private void spammerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketSpammer.Instance);
        }

        private void tampererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketTamperer.Instance);
        }

        public static Form EggsForm = null;

        public void ShowForm(Form form)
        {
            if (EggsForm != null) form = EggsForm;
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
                form.Show();
                form.BringToFront();
                form.Focus();
                return;
            }
            else if (form.Visible)
            {
                form.Hide();
                return;
            }
            form.Show();
            form.BringToFront();
            form.Focus();
        }

        private void cbCells_Click(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn) return;
            cbCells.Items.Clear();
            ComboBox.ObjectCollection items = cbCells.Items;
            object[] cells = World.Cells;
            object[] items2 = cells;
            items.AddRange(items2);
        }

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Root));
			this.nTray = new System.Windows.Forms.NotifyIcon(this.components);
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.managerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMax = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnMin = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.MenuMain = new System.Windows.Forms.MenuStrip();
			this.gameContainer = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnGetCell = new DarkUI.Controls.DarkButton();
			this.chkStartBot = new DarkUI.Controls.DarkCheckBox();
			this.chkAutoAttack = new DarkUI.Controls.DarkCheckBox();
			this.btnBank = new DarkUI.Controls.DarkButton();
			this.cbCells = new DarkUI.Controls.DarkComboBox();
			this.cbPads = new DarkUI.Controls.DarkComboBox();
			this.darkMenuStrip1 = new DarkUI.Controls.DarkMenuStrip();
			this.botToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fastTravelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadersgrabbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cosmeticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.eyeDropperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.notepadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.changeServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeServerList = new System.Windows.Forms.ToolStripComboBox();
			this.DPSMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commandeditornodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pvptoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.devTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.packetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.snifferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.spammerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tampererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.infRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.provokeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.enemyMagnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lagKillerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hidePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skipCutscenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableAnimationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.walkspeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.enableOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maidStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsStrip = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuCharSelect = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.darkMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// nTray
			// 
			this.nTray.Icon = ((System.Drawing.Icon)(resources.GetObject("nTray.Icon")));
			this.nTray.Text = "GrimLite";
			this.nTray.Visible = true;
			this.nTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nTray_MouseClick);
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.startToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.stopToolStripMenuItem.Enabled = false;
			this.stopToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// managerToolStripMenuItem
			// 
			this.managerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.managerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.managerToolStripMenuItem.Name = "managerToolStripMenuItem";
			this.managerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.managerToolStripMenuItem.Text = "Manager";
			this.managerToolStripMenuItem.Click += new System.EventHandler(this.managerToolStripMenuItem_Click);
			// 
			// loadBotToolStripMenuItem
			// 
			this.loadBotToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.loadBotToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.loadBotToolStripMenuItem.Name = "loadBotToolStripMenuItem";
			this.loadBotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadBotToolStripMenuItem.Text = "Load bot";
			this.loadBotToolStripMenuItem.Click += new System.EventHandler(this.loadBotToolStripMenuItem_Click);
			// 
			// btnMax
			// 
			this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
			this.btnMax.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMax.ForeColor = System.Drawing.Color.Black;
			this.btnMax.Location = new System.Drawing.Point(49, 0);
			this.btnMax.Name = "btnMax";
			this.btnMax.Size = new System.Drawing.Size(37, 27);
			this.btnMax.TabIndex = 32;
			this.btnMax.Text = "🗖";
			this.btnMax.UseVisualStyleBackColor = true;
			this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
			this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.ForeColor = System.Drawing.Color.Black;
			this.btnExit.Location = new System.Drawing.Point(86, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(49, 27);
			this.btnExit.TabIndex = 33;
			this.btnExit.Text = "×";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnMin
			// 
			this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
			this.btnMin.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMin.ForeColor = System.Drawing.Color.Black;
			this.btnMin.Location = new System.Drawing.Point(0, 0);
			this.btnMin.Name = "btnMin";
			this.btnMin.Size = new System.Drawing.Size(49, 27);
			this.btnMin.TabIndex = 34;
			this.btnMin.Text = "🗕";
			this.btnMin.UseVisualStyleBackColor = true;
			this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
			// 
			// panel1
			// 
			this.panel1.ImeMode = System.Windows.Forms.ImeMode.On;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 100);
			this.panel1.TabIndex = 0;
			// 
			// MenuMain
			// 
			this.MenuMain.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
			this.MenuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.MenuMain.Location = new System.Drawing.Point(0, 0);
			this.MenuMain.Name = "MenuMain";
			this.MenuMain.Size = new System.Drawing.Size(202, 24);
			this.MenuMain.TabIndex = 37;
			this.MenuMain.Text = "pluginHolder";
			this.MenuMain.Visible = false;
			this.MenuMain.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.pluginAdded);
			this.MenuMain.ItemRemoved += new System.Windows.Forms.ToolStripItemEventHandler(this.pluginRemoved);
			// 
			// gameContainer
			// 
			this.gameContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
			this.gameContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gameContainer.Location = new System.Drawing.Point(0, 42);
			this.gameContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gameContainer.Name = "gameContainer";
			this.gameContainer.Size = new System.Drawing.Size(1443, 849);
			this.gameContainer.TabIndex = 40;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnGetCell);
			this.splitContainer1.Panel1.Controls.Add(this.chkStartBot);
			this.splitContainer1.Panel1.Controls.Add(this.chkAutoAttack);
			this.splitContainer1.Panel1.Controls.Add(this.btnBank);
			this.splitContainer1.Panel1.Controls.Add(this.cbCells);
			this.splitContainer1.Panel1.Controls.Add(this.cbPads);
			this.splitContainer1.Panel1.Controls.Add(this.darkMenuStrip1);
			this.splitContainer1.Panel1.ImeMode = System.Windows.Forms.ImeMode.On;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Size = new System.Drawing.Size(1443, 42);
			this.splitContainer1.SplitterDistance = 824;
			this.splitContainer1.SplitterWidth = 2;
			this.splitContainer1.TabIndex = 38;
			// 
			// btnGetCell
			// 
			this.btnGetCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
			this.btnGetCell.Checked = false;
			this.btnGetCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.btnGetCell.Location = new System.Drawing.Point(1341, 5);
			this.btnGetCell.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnGetCell.Name = "btnGetCell";
			this.btnGetCell.Size = new System.Drawing.Size(27, 33);
			this.btnGetCell.TabIndex = 39;
			this.btnGetCell.Text = "x";
			this.btnGetCell.Click += new System.EventHandler(this.btnGetCell_Click);
			// 
			// chkStartBot
			// 
			this.chkStartBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkStartBot.AutoSize = true;
			this.chkStartBot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
			this.chkStartBot.Location = new System.Drawing.Point(854, 9);
			this.chkStartBot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkStartBot.Name = "chkStartBot";
			this.chkStartBot.Size = new System.Drawing.Size(99, 24);
			this.chkStartBot.TabIndex = 38;
			this.chkStartBot.Text = "Start Bot";
			this.chkStartBot.CheckedChanged += new System.EventHandler(this.chkStartBot_CheckedChanged);
			// 
			// chkAutoAttack
			// 
			this.chkAutoAttack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAutoAttack.AutoSize = true;
			this.chkAutoAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
			this.chkAutoAttack.Location = new System.Drawing.Point(962, 9);
			this.chkAutoAttack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkAutoAttack.Name = "chkAutoAttack";
			this.chkAutoAttack.Size = new System.Drawing.Size(119, 24);
			this.chkAutoAttack.TabIndex = 37;
			this.chkAutoAttack.Text = "Auto Attack";
			this.chkAutoAttack.CheckedChanged += new System.EventHandler(this.chkAutoAttack_CheckedChanged);
			// 
			// btnBank
			// 
			this.btnBank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
			this.btnBank.Checked = false;
			this.btnBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.btnBank.Location = new System.Drawing.Point(1374, 4);
			this.btnBank.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnBank.Name = "btnBank";
			this.btnBank.Size = new System.Drawing.Size(63, 33);
			this.btnBank.TabIndex = 36;
			this.btnBank.Text = "Bank";
			this.btnBank.Click += new System.EventHandler(this.btnBank_Click_1);
			// 
			// cbCells
			// 
			this.cbCells.FormattingEnabled = true;
			this.cbCells.ItemHeight = 27;
			this.cbCells.Location = new System.Drawing.Point(1214, 5);
			this.cbCells.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbCells.MaxDropDownItems = 50;
			this.cbCells.Name = "cbCells";
			this.cbCells.Size = new System.Drawing.Size(126, 33);
			this.cbCells.TabIndex = 18;
			this.cbCells.SelectedIndexChanged += new System.EventHandler(this.cbCells_SelectedIndexChanged);
			this.cbCells.Click += new System.EventHandler(this.cbCells_Click);
			// 
			// cbPads
			// 
			this.cbPads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbPads.FormattingEnabled = true;
			this.cbPads.ItemHeight = 27;
			this.cbPads.Items.AddRange(new object[] {
            "Center",
            "Spawn",
            "Left",
            "Right",
            "Top",
            "Bottom",
            "Up",
            "Down"});
			this.cbPads.Location = new System.Drawing.Point(1087, 5);
			this.cbPads.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbPads.MaxDropDownItems = 50;
			this.cbPads.Name = "cbPads";
			this.cbPads.Size = new System.Drawing.Size(126, 33);
			this.cbPads.TabIndex = 17;
			this.cbPads.SelectedIndexChanged += new System.EventHandler(this.cbPads_SelectedIndexChanged);
			// 
			// darkMenuStrip1
			// 
			this.darkMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.darkMenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.darkMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkMenuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.darkMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.darkMenuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.botToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.packetsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.maidStrip,
            this.pluginsStrip,
            this.aboutToolStripMenuItem,
            this.menuCharSelect});
			this.darkMenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.darkMenuStrip1.Name = "darkMenuStrip1";
			this.darkMenuStrip1.Padding = new System.Windows.Forms.Padding(3);
			this.darkMenuStrip1.Size = new System.Drawing.Size(1443, 42);
			this.darkMenuStrip1.Stretch = false;
			this.darkMenuStrip1.TabIndex = 35;
			this.darkMenuStrip1.Text = "darkMenuStrip1";
			this.darkMenuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuMain_MouseDown);
			// 
			// botToolStripMenuItem
			// 
			this.botToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.botToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.botToolStripMenuItem.Name = "botToolStripMenuItem";
			this.botToolStripMenuItem.Size = new System.Drawing.Size(55, 36);
			this.botToolStripMenuItem.Text = "Bot";
			this.botToolStripMenuItem.Click += new System.EventHandler(this.botToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastTravelsToolStripMenuItem,
            this.loadersgrabbersToolStripMenuItem,
            this.hotkeysToolStripMenuItem,
            this.pluginManagerToolStripMenuItem,
            this.cosmeticsToolStripMenuItem,
            this.bankToolStripMenuItem,
            this.setsToolStripMenuItem,
            this.eyeDropperToolStripMenuItem,
            this.logsToolStripMenuItem1,
            this.notepadToolStripMenuItem1,
            this.changeServerMenuItem,
            this.DPSMeterToolStripMenuItem,
            this.commandeditornodeToolStripMenuItem,
            this.pvptoolStripMenuItem1,
            this.devTestToolStripMenuItem});
			this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(69, 36);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// fastTravelsToolStripMenuItem
			// 
			this.fastTravelsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.fastTravelsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.fastTravelsToolStripMenuItem.Name = "fastTravelsToolStripMenuItem";
			this.fastTravelsToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.fastTravelsToolStripMenuItem.Text = "Fast travels";
			this.fastTravelsToolStripMenuItem.Click += new System.EventHandler(this.fastTravelsToolStripMenuItem_Click);
			// 
			// loadersgrabbersToolStripMenuItem
			// 
			this.loadersgrabbersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.loadersgrabbersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.loadersgrabbersToolStripMenuItem.Name = "loadersgrabbersToolStripMenuItem";
			this.loadersgrabbersToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.loadersgrabbersToolStripMenuItem.Text = "Loaders/grabbers";
			this.loadersgrabbersToolStripMenuItem.Click += new System.EventHandler(this.loadersgrabbersToolStripMenuItem_Click);
			// 
			// hotkeysToolStripMenuItem
			// 
			this.hotkeysToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.hotkeysToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
			this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.hotkeysToolStripMenuItem.Text = "Hotkeys";
			this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
			// 
			// pluginManagerToolStripMenuItem
			// 
			this.pluginManagerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.pluginManagerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.pluginManagerToolStripMenuItem.Name = "pluginManagerToolStripMenuItem";
			this.pluginManagerToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.pluginManagerToolStripMenuItem.Text = "Plugin manager";
			this.pluginManagerToolStripMenuItem.Click += new System.EventHandler(this.pluginManagerToolStripMenuItem_Click);
			// 
			// cosmeticsToolStripMenuItem
			// 
			this.cosmeticsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.cosmeticsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.cosmeticsToolStripMenuItem.Name = "cosmeticsToolStripMenuItem";
			this.cosmeticsToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.cosmeticsToolStripMenuItem.Text = "Cosmetics";
			this.cosmeticsToolStripMenuItem.Click += new System.EventHandler(this.cosmeticsToolStripMenuItem_Click);
			// 
			// bankToolStripMenuItem
			// 
			this.bankToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.bankToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.bankToolStripMenuItem.Name = "bankToolStripMenuItem";
			this.bankToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.bankToolStripMenuItem.Text = "Bank";
			this.bankToolStripMenuItem.Click += new System.EventHandler(this.bankToolStripMenuItem_Click);
			// 
			// setsToolStripMenuItem
			// 
			this.setsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.setsToolStripMenuItem.Enabled = false;
			this.setsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.setsToolStripMenuItem.Name = "setsToolStripMenuItem";
			this.setsToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.setsToolStripMenuItem.Text = "Sets";
			this.setsToolStripMenuItem.Visible = false;
			this.setsToolStripMenuItem.Click += new System.EventHandler(this.setsToolStripMenuItem_Click);
			// 
			// eyeDropperToolStripMenuItem
			// 
			this.eyeDropperToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.eyeDropperToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.eyeDropperToolStripMenuItem.Name = "eyeDropperToolStripMenuItem";
			this.eyeDropperToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.eyeDropperToolStripMenuItem.Text = "Eye Dropper";
			this.eyeDropperToolStripMenuItem.Click += new System.EventHandler(this.eyeDropperToolStripMenuItem_Click_1);
			// 
			// logsToolStripMenuItem1
			// 
			this.logsToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.logsToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.logsToolStripMenuItem1.Name = "logsToolStripMenuItem1";
			this.logsToolStripMenuItem1.Size = new System.Drawing.Size(283, 34);
			this.logsToolStripMenuItem1.Text = "Logs";
			this.logsToolStripMenuItem1.Click += new System.EventHandler(this.logsToolStripMenuItem1_Click);
			// 
			// notepadToolStripMenuItem1
			// 
			this.notepadToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.notepadToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.notepadToolStripMenuItem1.Name = "notepadToolStripMenuItem1";
			this.notepadToolStripMenuItem1.Size = new System.Drawing.Size(283, 34);
			this.notepadToolStripMenuItem1.Text = "Notepad";
			this.notepadToolStripMenuItem1.Click += new System.EventHandler(this.notepadToolStripMenuItem1_Click);
			// 
			// changeServerMenuItem
			// 
			this.changeServerMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.changeServerMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeServerList});
			this.changeServerMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.changeServerMenuItem.Name = "changeServerMenuItem";
			this.changeServerMenuItem.Size = new System.Drawing.Size(283, 34);
			this.changeServerMenuItem.Text = "Change Server";
			this.changeServerMenuItem.Visible = false;
			// 
			// changeServerList
			// 
			this.changeServerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.changeServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.changeServerList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.changeServerList.Name = "changeServerList";
			this.changeServerList.Size = new System.Drawing.Size(180, 33);
			this.changeServerList.SelectedIndexChanged += new System.EventHandler(this.changeServerList_SelectedIndexChanged);
			// 
			// DPSMeterToolStripMenuItem
			// 
			this.DPSMeterToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.DPSMeterToolStripMenuItem.Enabled = false;
			this.DPSMeterToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.DPSMeterToolStripMenuItem.Name = "DPSMeterToolStripMenuItem";
			this.DPSMeterToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.DPSMeterToolStripMenuItem.Text = "DPS Meter";
			this.DPSMeterToolStripMenuItem.Visible = false;
			this.DPSMeterToolStripMenuItem.Click += new System.EventHandler(this.dPSMeterToolStripMenuItem_Click);
			// 
			// commandeditornodeToolStripMenuItem
			// 
			this.commandeditornodeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.commandeditornodeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.commandeditornodeToolStripMenuItem.Name = "commandeditornodeToolStripMenuItem";
			this.commandeditornodeToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.commandeditornodeToolStripMenuItem.Text = "commandeditornode";
			this.commandeditornodeToolStripMenuItem.Visible = false;
			this.commandeditornodeToolStripMenuItem.Click += new System.EventHandler(this.commandeditornodeToolStripMenuItem_Click);
			// 
			// pvptoolStripMenuItem1
			// 
			this.pvptoolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.pvptoolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.pvptoolStripMenuItem1.Name = "pvptoolStripMenuItem1";
			this.pvptoolStripMenuItem1.Size = new System.Drawing.Size(283, 34);
			this.pvptoolStripMenuItem1.Text = "PvP";
			this.pvptoolStripMenuItem1.Visible = false;
			this.pvptoolStripMenuItem1.Click += new System.EventHandler(this.pvptoolStripMenuItem1_Click);
			// 
			// devTestToolStripMenuItem
			// 
			this.devTestToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.devTestToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.devTestToolStripMenuItem.Name = "devTestToolStripMenuItem";
			this.devTestToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
			this.devTestToolStripMenuItem.Text = "Dev Test";
			this.devTestToolStripMenuItem.Click += new System.EventHandler(this.devTestToolStripMenuItem_Click);
			// 
			// packetsToolStripMenuItem
			// 
			this.packetsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.packetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snifferToolStripMenuItem,
            this.spammerToolStripMenuItem,
            this.tampererToolStripMenuItem,
            this.toolStripMenuItem1});
			this.packetsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.packetsToolStripMenuItem.Name = "packetsToolStripMenuItem";
			this.packetsToolStripMenuItem.Size = new System.Drawing.Size(86, 36);
			this.packetsToolStripMenuItem.Text = "Packets";
			// 
			// snifferToolStripMenuItem
			// 
			this.snifferToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.snifferToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.snifferToolStripMenuItem.Name = "snifferToolStripMenuItem";
			this.snifferToolStripMenuItem.Size = new System.Drawing.Size(201, 34);
			this.snifferToolStripMenuItem.Text = "Sniffer";
			this.snifferToolStripMenuItem.Click += new System.EventHandler(this.snifferToolStripMenuItem_Click);
			// 
			// spammerToolStripMenuItem
			// 
			this.spammerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.spammerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.spammerToolStripMenuItem.Name = "spammerToolStripMenuItem";
			this.spammerToolStripMenuItem.Size = new System.Drawing.Size(201, 34);
			this.spammerToolStripMenuItem.Text = "Spammer";
			this.spammerToolStripMenuItem.Click += new System.EventHandler(this.spammerToolStripMenuItem_Click);
			// 
			// tampererToolStripMenuItem
			// 
			this.tampererToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tampererToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.tampererToolStripMenuItem.Name = "tampererToolStripMenuItem";
			this.tampererToolStripMenuItem.Size = new System.Drawing.Size(201, 34);
			this.tampererToolStripMenuItem.Text = "Tamperer";
			this.tampererToolStripMenuItem.Click += new System.EventHandler(this.tampererToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(201, 34);
			this.toolStripMenuItem1.Text = "Interceptor";
			this.toolStripMenuItem1.Visible = false;
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infRangeToolStripMenuItem,
            this.provokeToolStripMenuItem1,
            this.enemyMagnetToolStripMenuItem,
            this.lagKillerToolStripMenuItem,
            this.hidePlayersToolStripMenuItem,
            this.skipCutscenesToolStripMenuItem,
            this.disableAnimationsToolStripMenuItem,
            this.walkspeedToolStripMenuItem,
            this.enableOptionsToolStripMenuItem});
			this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(92, 36);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// infRangeToolStripMenuItem
			// 
			this.infRangeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.infRangeToolStripMenuItem.CheckOnClick = true;
			this.infRangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.infRangeToolStripMenuItem.Name = "infRangeToolStripMenuItem";
			this.infRangeToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.infRangeToolStripMenuItem.Text = "Infinite Range";
			this.infRangeToolStripMenuItem.Click += new System.EventHandler(this.infRangeToolStripMenuItem_Click);
			// 
			// provokeToolStripMenuItem1
			// 
			this.provokeToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.provokeToolStripMenuItem1.CheckOnClick = true;
			this.provokeToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.provokeToolStripMenuItem1.Name = "provokeToolStripMenuItem1";
			this.provokeToolStripMenuItem1.Size = new System.Drawing.Size(267, 34);
			this.provokeToolStripMenuItem1.Text = "Provoke";
			this.provokeToolStripMenuItem1.Click += new System.EventHandler(this.provokeToolStripMenuItem1_Click);
			// 
			// enemyMagnetToolStripMenuItem
			// 
			this.enemyMagnetToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.enemyMagnetToolStripMenuItem.CheckOnClick = true;
			this.enemyMagnetToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.enemyMagnetToolStripMenuItem.Name = "enemyMagnetToolStripMenuItem";
			this.enemyMagnetToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.enemyMagnetToolStripMenuItem.Text = "Enemy Magnet";
			this.enemyMagnetToolStripMenuItem.Click += new System.EventHandler(this.enemyMagnetToolStripMenuItem_Click);
			// 
			// lagKillerToolStripMenuItem
			// 
			this.lagKillerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lagKillerToolStripMenuItem.CheckOnClick = true;
			this.lagKillerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lagKillerToolStripMenuItem.Name = "lagKillerToolStripMenuItem";
			this.lagKillerToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.lagKillerToolStripMenuItem.Text = "Lag Killer";
			this.lagKillerToolStripMenuItem.Click += new System.EventHandler(this.lagKillerToolStripMenuItem_Click);
			// 
			// hidePlayersToolStripMenuItem
			// 
			this.hidePlayersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.hidePlayersToolStripMenuItem.CheckOnClick = true;
			this.hidePlayersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.hidePlayersToolStripMenuItem.Name = "hidePlayersToolStripMenuItem";
			this.hidePlayersToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.hidePlayersToolStripMenuItem.Text = "Hide Players";
			this.hidePlayersToolStripMenuItem.Click += new System.EventHandler(this.hidePlayersToolStripMenuItem_Click);
			// 
			// skipCutscenesToolStripMenuItem
			// 
			this.skipCutscenesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.skipCutscenesToolStripMenuItem.CheckOnClick = true;
			this.skipCutscenesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.skipCutscenesToolStripMenuItem.Name = "skipCutscenesToolStripMenuItem";
			this.skipCutscenesToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.skipCutscenesToolStripMenuItem.Text = "Skip Cutscenes";
			this.skipCutscenesToolStripMenuItem.Click += new System.EventHandler(this.skipCutscenesToolStripMenuItem_Click);
			// 
			// disableAnimationsToolStripMenuItem
			// 
			this.disableAnimationsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.disableAnimationsToolStripMenuItem.CheckOnClick = true;
			this.disableAnimationsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.disableAnimationsToolStripMenuItem.Name = "disableAnimationsToolStripMenuItem";
			this.disableAnimationsToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.disableAnimationsToolStripMenuItem.Text = "Disable Animations";
			this.disableAnimationsToolStripMenuItem.Visible = false;
			this.disableAnimationsToolStripMenuItem.Click += new System.EventHandler(this.disableAnimationsToolStripMenuItem_Click);
			// 
			// walkspeedToolStripMenuItem
			// 
			this.walkspeedToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.walkspeedToolStripMenuItem.CheckOnClick = true;
			this.walkspeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
			this.walkspeedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.walkspeedToolStripMenuItem.Name = "walkspeedToolStripMenuItem";
			this.walkspeedToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.walkspeedToolStripMenuItem.Text = "Walkspeed";
			this.walkspeedToolStripMenuItem.Click += new System.EventHandler(this.walkspeedToolStripMenuItem_Click);
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.toolStripTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 31);
			this.toolStripTextBox1.Text = "20";
			// 
			// enableOptionsToolStripMenuItem
			// 
			this.enableOptionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.enableOptionsToolStripMenuItem.CheckOnClick = true;
			this.enableOptionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.enableOptionsToolStripMenuItem.Name = "enableOptionsToolStripMenuItem";
			this.enableOptionsToolStripMenuItem.Size = new System.Drawing.Size(267, 34);
			this.enableOptionsToolStripMenuItem.Text = "Enable Options";
			this.enableOptionsToolStripMenuItem.Visible = false;
			this.enableOptionsToolStripMenuItem.Click += new System.EventHandler(this.enableOptionsToolStripMenuItem_Click);
			// 
			// maidStrip
			// 
			this.maidStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.maidStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.maidStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.maidStrip.Name = "maidStrip";
			this.maidStrip.Size = new System.Drawing.Size(68, 36);
			this.maidStrip.Text = "Maid";
			this.maidStrip.Click += new System.EventHandler(this.maidStrip_Click);
			// 
			// pluginsStrip
			// 
			this.pluginsStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.pluginsStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.pluginsStrip.Name = "pluginsStrip";
			this.pluginsStrip.Size = new System.Drawing.Size(85, 36);
			this.pluginsStrip.Text = "Plugins";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(78, 36);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// menuCharSelect
			// 
			this.menuCharSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.menuCharSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.menuCharSelect.Name = "menuCharSelect";
			this.menuCharSelect.Size = new System.Drawing.Size(115, 36);
			this.menuCharSelect.Text = "Char Select";
			// 
			// Root
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
			this.ClientSize = new System.Drawing.Size(1443, 891);
			this.Controls.Add(this.gameContainer);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.MenuMain);
			this.ForeColor = System.Drawing.SystemColors.Window;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Root";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Grimlite Li";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Root_FormClosing);
			this.Load += new System.EventHandler(this.Root_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Root_KeyPress);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.darkMenuStrip1.ResumeLayout(false);
			this.darkMenuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private void Instance_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            string Cell = (string)this.cbCells.SelectedItem;
            string Pad = (string)this.cbPads.SelectedItem;
            Player.MoveToCell(Cell ?? Player.Cell, Pad ?? Player.Pad);
        }

        private void cosmeticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(CosmeticForm.Instance);
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(BankForm.Instance);
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Set.Instance);
        }

        private void grimoireSuggestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void BotStateChanged(bool IsRunning)
        {
            if (IsRunning)
            {
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        private async void changeServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn) return;
            string server = changeServerList.Items[changeServerList.SelectedIndex].ToString();
            changeServerList.Visible = false;
            Player.Logout();
            await AutoRelogin.Login(new Server() { Name = server }, 3000, cts: new System.Threading.CancellationTokenSource(), ensureSuccess: false);
            changeServerList.Visible = true;
        }

        private void nTray_MouseClick(object sender, MouseEventArgs e)
        {
            ShowForm(this);
        }

        private void eyeDropperToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowForm(EyeDropper.Instance);
        }

        private void logsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void notepadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(Notepad.Instance);
        }

        private void infRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = infRangeToolStripMenuItem.Checked;
            OptionsManager.InfiniteRange = check;
            botManager.chkInfiniteRange.Checked = check;
        }

        private void provokeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool check = provokeToolStripMenuItem1.Checked;
            OptionsManager.ProvokeMonsters = check;
            botManager.chkProvoke.Checked = check;
        }

        private void enemyMagnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = enemyMagnetToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkMagnet.Checked = check;
        }

        private void lagKillerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = lagKillerToolStripMenuItem.Checked;
            OptionsManager.LagKiller = check;
            OptionsManager.SetLagKiller();
            botManager.chkLag.Checked = check;
        }

        private void hidePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = hidePlayersToolStripMenuItem.Checked;
            OptionsManager.HidePlayers = check;
            botManager.chkHidePlayers.Checked = check;
        }

        private void skipCutscenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = skipCutscenesToolStripMenuItem.Checked;
            if (check) OptionsManager.SetSkipCutscenes();
            OptionsManager.SkipCutscenes = check;
            botManager.chkSkipCutscenes.Checked = check;
        }

        private void disableAnimationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = disableAnimationsToolStripMenuItem.Checked;
            OptionsManager.DisableAnimations = check;
            botManager.chkDisableAnims.Checked = check;
        }

        private void enableOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            botManager.chkEnableSettings.Checked = enableOptionsToolStripMenuItem.Checked;
        }

        private void walkspeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (walkspeedToolStripMenuItem.Checked)
            {
                BotManager.Instance.numWalkSpeed.Value = int.Parse(walkspeedToolStripMenuItem.DropDownItems[0].Text);
                BotManager.Instance.chkWalkSpeed.Checked = true;
            }
            else
            {
                BotManager.Instance.chkWalkSpeed.Checked = false;
            }
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void MenuMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                //Message.Create(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            const UInt32 HTLEFT = 10;
            const UInt32 HTRIGHT = 11;
            const UInt32 HTBOTTOMRIGHT = 17;
            const UInt32 HTBOTTOM = 15;
            const UInt32 HTBOTTOMLEFT = 16;
            const UInt32 HTTOP = 12;
            const UInt32 HTTOPLEFT = 13;
            const UInt32 HTTOPRIGHT = 14;

            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                Dictionary<UInt32, Rectangle> boxes = new Dictionary<UInt32, Rectangle>() {
            {HTBOTTOMLEFT, new Rectangle(0, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOM, new Rectangle(RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOMRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE)},
            {HTTOPRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOP, new Rectangle(RESIZE_HANDLE_SIZE, 0, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOPLEFT, new Rectangle(0, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTLEFT, new Rectangle(0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE) }
        };

                foreach (KeyValuePair<UInt32, Rectangle> hitBox in boxes)
                {
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)hitBox.Key;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        private async void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && BotManager.Instance.lstCommands.Items.Count > 0)
            {
                BotManager.Instance.MultiMode();
                BotManager.Instance.ActiveBotEngine.IsRunningChanged += BotManager.Instance.OnIsRunningChanged;
                BotManager.Instance.ActiveBotEngine.IndexChanged += BotManager.Instance.OnIndexChanged;
                BotManager.Instance.ActiveBotEngine.ConfigurationChanged += BotManager.Instance.OnConfigurationChanged;
                BotManager.Instance.ActiveBotEngine.Start(BotManager.Instance.GenerateConfiguration());
                BotManager.Instance.BotStateChanged(IsRunning: true);
                this.BotStateChanged(IsRunning: true);
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuration.Instance.Items != null && Configuration.Instance.BankOnStop)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && Configuration.Instance.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
            BotManager.Instance.ActiveBotEngine.Stop();
            this.stopToolStripMenuItem.Enabled = false;
            BotManager.Instance.MultiMode();
            await Task.Delay(2000);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            this.BotStateChanged(IsRunning: false);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                {
                    cp.Style |= 0x20000 | 0x80000 | 0x40000; //WS_MINIMIZEBOX | WS_SYSMENU | WS_SIZEBOX;
                }
                return cp;
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pluginAdded(object sender, ToolStripItemEventArgs e)
        {
            pluginsStrip.DropDownItems.Add(e.Item);
        }

        private void pluginRemoved(object sender, ToolStripItemEventArgs e)
        {
            pluginsStrip.DropDownItems.Remove(e.Item);
        }

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(botManager);
        }

        private void Root_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dPSMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(DPSForm.Instance);
        }

        private void commandeditornodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(NodeEditor.Instance);
        }

        private void loadBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            botManager.btnLoad_Click(sender, e);
        }

        private void btnBank_Click_1(object sender, EventArgs e)
        {
            Player.Bank.Show();
        }

        private void cbCells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn) return;
            string cell = (string)this.cbCells.SelectedItem;
            if (cell != null)
            {
                string pad = (string)this.cbPads.SelectedItem;
                Player.MoveToCell(cell, pad ?? "Spawn");
            }
        }
        private void cbPads_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pad = (string)cbPads.SelectedItem;
            if (pad != null)
            {
                string cell = (string)this.cbCells.SelectedItem;
                Player.MoveToCell(cell ?? Player.Cell, pad);
            }
        }

        private List<Skill> getSkillPreset()
        {
            List<Skill> listSkill = new List<Skill>();
            try
            {
                bool wait = false;
                string playerClass = Player.EquippedClass;
                string skillPreset = ClientConfig.GetValue($"{ClientConfig.C_SKILL_PRESET_PREFIX}{playerClass.ToUpper()}");
                if (skillPreset != null)
                {
                    if (skillPreset.Contains("wait"))
                    {
                        skillPreset = skillPreset.Replace("wait", "");
                        wait = true;
                    }
                    string[] skills = skillPreset.Split(';');
                    foreach (string skill in skills)
                    {
                        if (wait)
                        {
                            listSkill.Add(
                                new Skill { Type = Skill.SkillType.Wait, Index = skill }
                            );
                            continue;
                        }
                        if (skill.Contains(':'))
                        {
                            string skillIndex = skill.Split(':')[0];
                            string safer = skill.Split(':')[1];
                            string sType = safer.Remove(0, 2)[0].ToString();
                            int sValue = int.Parse(safer.Remove(0, 3));
                            switch (safer.Substring(0, 2))
                            {
                                case "SH":
                                    listSkill.Add(new Skill
                                    {
                                        Type = Skill.SkillType.Safe,
                                        SType = sType == ">" ? Skill.SafeType.GreaterThan : Skill.SafeType.LowerThan,
                                        SafeValue = sValue,
                                        IsSafeMp = false,
                                        Index = skillIndex,
                                    });
                                    break;
                                case "SM":
                                    listSkill.Add(
                                        new Skill
                                        {
                                            Type = Skill.SkillType.Safe,
                                            SType = sType == ">" ? Skill.SafeType.GreaterThan : Skill.SafeType.LowerThan,
                                            SafeValue = sValue,
                                            IsSafeMp = true,
                                            Index = skillIndex
                                        });
                                    break;
                            }
                        }
                        else
                        {
                            listSkill.Add(
                                new Skill { Type = Skill.SkillType.Normal, Index = skill }
                            );
                        }
                    }
                }
            }
            catch
            {
                LogForm.Instance.AppendDebug("Error while loading skill preset for " + Player.EquippedClass + "\r\n");
            }
            return listSkill;
        }

        private async void chkAutoAttack_CheckedChanged(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn || chkStartBot.Checked)
            {
                chkAutoAttack.Checked = false;
                return;
            }

            List<Skill> listSkill = getSkillPreset();
            if (listSkill.Count <= 0)
            {
                listSkill = new List<Skill> {
                    new Skill {Type = Skill.SkillType.Normal, Index = "1"},
                    new Skill {Type = Skill.SkillType.Normal, Index = "2"},
                    new Skill {Type = Skill.SkillType.Normal, Index = "3"},
                    new Skill {Type = Skill.SkillType.Normal, Index = "4"}
                };
            }

            int i = 0;
			BotData.BotState = BotData.State.Combat;
            while (chkAutoAttack.Checked)
            {
                if (BotData.BotState == BotData.State.Combat)
                {
                    if (!Player.HasTarget) Player.AttackMonster("*");
                    await SpecialClassCombo();
                    if (listSkill.Count > 0)
                    {
                        if (listSkill[i].Type != Skill.SkillType.Label && Player.HasTarget)
                            listSkill[i].ExecuteSkill();
                    }
                    await Task.Delay(100);
                    i++;
                    if (i >= listSkill.Count) i = 0;

                }
                else
                {
                    await Task.Delay(8000);
                    BotData.BotState = BotData.State.Combat;
                }
            }
        }

        private async Task SpecialClassCombo()
        {
            string playerClass = Player.EquippedClass.ToLower();

            if (playerClass.Contains("chrono shadow"))
            {
                if (Player.GetAuras(true, "Rounds Empty") == 1 || Player.Mana < 15)
                {
                    useSkill("4");
                    useSkill("1");
                    await Task.Delay(200);
                }
            }
            else if (playerClass.Equals("arcana invoker"))
            {
                if (Player.GetAuras(true, "XX - Judgement") == 1 ||
                    Player.GetAuras(true, "End of the world") >= 13 ||
                    Player.GetAuras(true, "XXI - The World") == 0 && Player.GetAuras(true, "0 - The Fool") == 0)
                {
                    useSkill("1");
                    await Task.Delay(200);
                }
            }
            else if (playerClass.Equals("archmage"))
            {
                if (Player.GetAuras(true, "Arcane Flux") == 1 &&
                    Player.GetAuras(true, "Corporeal Ascension") == 0 ||
                    Player.GetAuras(true, "Arcane Sigil") == 0)
                {
                    useSkill("4");
                    await Task.Delay(200);
                }
            }
        }

        private void useSkill(string skillIndex)
        {
            Task.Delay(Player.SkillAvailable(skillIndex));
            if (Player.EquippedClass.Contains("CHRONO SHADOW"))
                Player.ForceUseSkill(skillIndex);
            else Player.UseSkill(skillIndex);
        }

        private void chkStartBot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStartBot.Checked && (BotManager.Instance.lstCommands.Items.Count <= 0 || !Player.IsLoggedIn))
            {
                chkStartBot.Checked = false;
                return;
            }

            if (chkStartBot.Checked)
            {
                if (!BotManager.Instance.chkEnable.Checked)
                    BotManager.Instance.chkEnable.Checked = true;
            }
            else
            {
                if (BotManager.Instance.chkEnable.Checked)
                    BotManager.Instance.chkEnable.Checked = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(AboutForm.Instance);
        }

        private void btnGetCell_Click(object sender, EventArgs e)
        {
            if (!Player.IsLoggedIn || World.IsMapLoading) return;
            cbPads.SelectedIndexChanged -= cbPads_SelectedIndexChanged;
            cbCells.SelectedIndexChanged -= cbCells_SelectedIndexChanged;

            cbCells.Items.Clear();
            ComboBox.ObjectCollection items = cbCells.Items;
            object[] cells = World.Cells;
            object[] items2 = cells;
            items.AddRange(items2);

            cbPads.SelectedItem = Player.Pad;
            cbCells.SelectedItem = Player.Cell;

            Player.MoveToCell(Player.Cell, Player.Pad);

            cbPads.SelectedIndexChanged += cbPads_SelectedIndexChanged;
            cbCells.SelectedIndexChanged += cbCells_SelectedIndexChanged;
        }

        private void btnReloadBank_Click(object sender, EventArgs e)
        {
            _ = Proxy.Instance.SendToServer($"%xt%zm%loadBank%{World.RoomId}%All%");
        }

        private void pvptoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(PvP.Instance);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(PacketInterceptor.Instance);
        }

        private void devTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(DevTest.Instance);
        }

        public void ToggleLauncherSkin()
        {
            if (this.Text == title)
            {
                this.Text = "";
                this.Icon = global::Properties.Resources.Artix;
                this.nTray.Icon = global::Properties.Resources.Artix;
                this.splitContainer1.Visible = false;
                this.Height -= this.darkMenuStrip1.Height;
            }
            else
            {
                this.Text = title;
                this.Icon = global::Properties.Resources.GrimoireIcon;
                this.nTray.Icon = global::Properties.Resources.GrimoireIcon;
                this.splitContainer1.Visible = true;
                this.Height += this.darkMenuStrip1.Height;
            }
        }

        private void maidStrip_Click(object sender, EventArgs e)
        {
            ShowForm(Maid.MaidRemake.Instance);
        }

        private DateTime _lastPacketSent = DateTime.MinValue;
        private void auraClearer()
        {
            bool _lastIsAlive = false;
            Task.Run(async () =>
            {
                while (true)
                {
                    if (Player.IsLoggedIn)
                    {
                        // Send every 15 minutes
                        if ((DateTime.Now - _lastPacketSent).Seconds >= 60 * 15)
                        {
                            await Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"clearAuras\"}}}");
                            _lastPacketSent = DateTime.Now;
                            //LogForm.Instance.AppendDebug($"[{DateTime.Now}] Executing Clear Aura");
                        }

                        // Death detection
                        bool isAlive = Player.IsAlive;
                        if (_lastIsAlive && !isAlive)
                        {
                            await Proxy.Instance.SendToClient("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"clearAuras\"}}}");
                            //LogForm.Instance.AppendDebug($"[{DateTime.Now}] Clearing Aura due to Death");
                        }
                        _lastIsAlive = isAlive;
                    }
                    await Task.Delay(9000);
                }
            });
        }
    }
}
