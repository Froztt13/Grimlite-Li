using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
	public class Hotkeys : DarkForm
	{
		public static readonly Action[] Actions = new Action[]
		{
			delegate
			{
				Root.Instance.chkStartBot.Checked = !Root.Instance.chkStartBot.Checked;
			},
			delegate
			{
				Root.Instance.ShowForm(BotManager.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(Loaders.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(PacketLogger.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(PacketSpammer.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(PacketTamperer.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(Travel.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(Maid.MaidRemake.Instance);
			},
			delegate
			{
				Maid.MaidRemake.Instance.cbEnablePlugin.Checked = !Maid.MaidRemake.Instance.cbEnablePlugin.Checked;
			},
			delegate
			{
				Root.Instance.ToggleLauncherSkin();
			},
			delegate
			{
				Player.CancelTarget();
			},
			delegate
			{
				Root.Instance.ShowForm(Root.Instance);
			},
			delegate
			{
				Player.Bank.Show();
			},
			delegate
			{
				Root.Instance.ShowForm(CosmeticForm.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(LogForm.Instance);
			},
			delegate
			{
				Root.Instance.ShowForm(Notepad.Instance);
			},
			delegate
			{
				Shop.LoadHairShop(1);
			},
			delegate
			{
				Shop.LoadArmorCustomizer();
			},
			delegate
			{
				if (!OptionsManager.IsRunning)
					OptionsManager.Start();
				else
					OptionsManager.Stop();
			},
			delegate
			{
				//execute debug
			}
		};

		public static readonly List<Hotkey> InstalledHotkeys = new List<Hotkey>();

		private int _processId;

		private IContainer components;

		private DarkListBox lstKeys;
		private DarkComboBox cbKeys;
		private DarkComboBox cbActions;
		private DarkButton btnAdd;
		private DarkButton btnRemove;
		private DarkButton btnSave;

		public static Hotkeys Instance
		{
			get;
		} = new Hotkeys();

		private string configPath => Path.Combine(Application.StartupPath, "hotkeys.json");

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

		private Hotkeys()
		{
			InitializeComponent();
		}

		private void Hotkeys_Load(object sender, EventArgs e)
		{
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			string font = c.Get("font");
			float? fontSize = float.Parse(c.Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			lstKeys.DisplayMember = "Text";
			cbActions.SelectedIndex = 0;
			cbKeys.SelectedIndex = 0;
			if (font != null && fontSize != null)
				this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
		}

		private static CmdTravel CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn")
		{
			return new CmdTravel
			{
				Map = map,
				Cell = cell,
				Pad = pad
			};
		}

		private static async void ExecuteTravel(List<IBotCommand> cmds)
		{
			foreach (IBotCommand cmd in cmds)
			{
				await cmd.Execute(null);
				await Task.Delay(1000);
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			Hotkey hotkey = (Hotkey)lstKeys.SelectedItem;
			if (hotkey != null)
			{
				hotkey.Uninstall();
				InstalledHotkeys.Remove(hotkey);
				lstKeys.Items.Remove(hotkey);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			int selectedIndex = cbActions.SelectedIndex;
			if (selectedIndex > -1 && cbKeys.SelectedIndex > -1)
			{
				Keys keys = (Keys)Enum.Parse(typeof(Keys), cbKeys.SelectedItem.ToString());
				if (!KeyboardHook.Instance.TargetedKeys.Contains(keys))
				{
					Hotkey hotkey = new Hotkey
					{
						ActionIndex = selectedIndex,
						Key = keys,
						Text = $"{keys}: {cbActions.Items[selectedIndex]}"
					};
					hotkey.Install();
					InstalledHotkeys.Add(hotkey);
					lstKeys.Items.Add(hotkey);
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			File.WriteAllText(configPath, JsonConvert.SerializeObject(InstalledHotkeys));
		}

		public void LoadHotkeys()
		{
			if (File.Exists(configPath))
			{
				Hotkey[] array = JsonConvert.DeserializeObject<Hotkey[]>(File.ReadAllText(configPath));
				if (array != null)
				{
					InstalledHotkeys.AddRange(array);
					foreach (Hotkey installedHotkey in InstalledHotkeys)
					{
						lstKeys.Items.Add(installedHotkey);
						installedHotkey.Install();
					}
				}
			}
			KeyboardHook.Instance.KeyDown += OnKeyDown;
			_processId = Process.GetCurrentProcess().Id;
		}

		public void OnKeyDown(Keys key)
		{
			Hotkey hotkey = InstalledHotkeys.First((Hotkey h) => h.Key == key);
			if (ApplicationContainsFocus() || (string)cbActions.Items[hotkey.ActionIndex] == "Minimize to tray")
			{
				Actions[hotkey.ActionIndex]();
			}
		}

		public bool ApplicationContainsFocus()
		{
			IntPtr foregroundWindow = GetForegroundWindow();
			if (foregroundWindow == IntPtr.Zero)
			{
				return false;
			}
			GetWindowThreadProcessId(foregroundWindow, out int processId);
			return processId == _processId;
		}

		private void Hotkeys_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotkeys));
			this.lstKeys = new DarkUI.Controls.DarkListBox(this.components);
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.cbKeys = new DarkUI.Controls.DarkComboBox();
			this.cbActions = new DarkUI.Controls.DarkComboBox();
			this.btnAdd = new DarkUI.Controls.DarkButton();
			this.btnRemove = new DarkUI.Controls.DarkButton();
			this.SuspendLayout();
			// 
			// lstKeys
			// 
			this.lstKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstKeys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.lstKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstKeys.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstKeys.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstKeys.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lstKeys.FormattingEnabled = true;
			this.lstKeys.HorizontalScrollbar = true;
			this.lstKeys.ItemHeight = 18;
			this.lstKeys.Location = new System.Drawing.Point(18, 60);
			this.lstKeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lstKeys.Name = "lstKeys";
			this.lstKeys.Size = new System.Drawing.Size(420, 290);
			this.lstKeys.TabIndex = 28;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Checked = false;
			this.btnSave.Location = new System.Drawing.Point(20, 417);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(420, 35);
			this.btnSave.TabIndex = 33;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// cbKeys
			// 
			this.cbKeys.FormattingEnabled = true;
			this.cbKeys.ItemHeight = 27;
			this.cbKeys.Items.AddRange(new object[] {
            "Escape",
            "Left",
            "Up",
            "Right",
            "Down",
            "D0",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "D6",
            "D7",
            "D8",
            "D9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12",
            "Alt",
            "Tab",
            "Shift"});
			this.cbKeys.Location = new System.Drawing.Point(18, 18);
			this.cbKeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbKeys.MaxDropDownItems = 20;
			this.cbKeys.Name = "cbKeys";
			this.cbKeys.Size = new System.Drawing.Size(157, 33);
			this.cbKeys.TabIndex = 35;
			// 
			// cbActions
			// 
			this.cbActions.FormattingEnabled = true;
			this.cbActions.ItemHeight = 27;
			this.cbActions.Items.AddRange(new object[] {
            "Start/Stop Bot",
            "Show Bot",
            "Show Hotkeys",
            "Show Loaders",
            "Show Packet Logger",
            "Show Packet Spammer",
            "Show Packet Tamperer",
            "Show Fast Travels",
            "Show Maid",
            "Start/Stop Maid",
            "Change Launcher Skin",
            "Cancel Target",
            "Minimize to tray",
            "Show Bank",
            "Show Cosmetics form",
            "Show Logs",
            "Show Notepad",
            "Load Hair shop",
            "Load Armor Customizer",
            "Toggle Options"});
			this.cbActions.Location = new System.Drawing.Point(186, 18);
			this.cbActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cbActions.MaxDropDownItems = 20;
			this.cbActions.Name = "cbActions";
			this.cbActions.Size = new System.Drawing.Size(252, 33);
			this.cbActions.TabIndex = 36;
			// 
			// btnAdd
			// 
			this.btnAdd.Checked = false;
			this.btnAdd.Location = new System.Drawing.Point(20, 375);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(207, 33);
			this.btnAdd.TabIndex = 39;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Checked = false;
			this.btnRemove.Location = new System.Drawing.Point(237, 375);
			this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(202, 33);
			this.btnRemove.TabIndex = 40;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// Hotkeys
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 471);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.cbKeys);
			this.Controls.Add(this.cbActions);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lstKeys);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Hotkeys";
			this.Text = "Hotkeys";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hotkeys_FormClosing);
			this.Load += new System.EventHandler(this.Hotkeys_Load);
			this.ResumeLayout(false);

		}
	}
}