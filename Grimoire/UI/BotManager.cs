using Grimoire.Botting;
using Grimoire.Botting.Commands;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Map;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Properties;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Grimoire.UI;
using System.Drawing.Drawing2D;
using static Grimoire.Botting.Commands.Item.CmdWhitelist;
using System.Runtime.InteropServices;
using DarkUI.Forms;
using DarkUI.Controls;
using Newtonsoft.Json.Linq;
using Extensions = Grimoire.Botting.Extensions;
using Properties;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using Grimoire.Networking.Handlers;
using Grimoire.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Grimoire.UI
{
	public partial class BotManager : DarkForm
	{
		private IBotEngine _activeBotEngine = new Bot();

		private List<StatementCommand> _statementCommands;

		private Dictionary<string, string> _defaultControlText;

		public IJsonMessageHandler SpecialJsonHandler;

		public IXtMessageHandler SpecialXtHandler;

		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			TypeNameHandling = TypeNameHandling.All
		};

		private readonly JsonSerializerSettings _saveSerializerSettings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			TypeNameHandling = TypeNameHandling.All
		};

		public static BotManager Instance
		{
			get;
		}

		public IBotEngine ActiveBotEngine
		{
			get
			{
				return _activeBotEngine;
			}
			set
			{
				if (_activeBotEngine.IsRunning)
				{
					throw new InvalidOperationException("Cannot set a new bot engine while the current one is running");
				}

				_activeBotEngine = value ?? throw new ArgumentNullException("value");
			}
		}

		private ListBox SelectedList
		{
			get
			{
				switch (cbLists.SelectedIndex)
				{
					case 1:
						return lstSkills;

					case 2:
						return lstQuests;

					case 3:
						return lstDrops;

					case 4:
						return lstBoosts;

					case 5:
						return lstItems;

					default:
						return lstCommands;
				}
			}
		}

		public string CustomName
		{
			get => _customName;

			set
			{
				_customName = value;
				Flash.Call("ChangeName", _customName);
			}
		}

		public string CustomGuild
		{
			get => _customGuild;
			set
			{
				_customGuild = value;
				Flash.Call("ChangeGuild", txtGuild.Text);
			}
		}

		public static int SliderValue
		{
			get;
			set;
		}

		private DarkButton btnSetSpawn2;
		private DarkGroupBox darkGroupBox16;
		private DarkCheckBox chkAMStopBot;
		private DarkCheckBox chkAMLogout;
		private DarkCheckBox chkAntiMod;
		private DarkGroupBox darkGroupBox10;
		private DarkButton btnStop;
		private DarkButton btnRestart;
		private DarkCheckBox colorfulCommands;
		private DarkButton btnAddLabel;
		private DarkButton btnGotoLabel;
		private DarkTextBox txtLabel;
		private DarkTextBox txtAuthor;
		private DarkCheckBox chkForceSkill;
		public DarkCheckBox chkFollowOnly;
		public DarkTextBox tbFollowPlayer2;
		private DarkGroupBox darkGroupBox11;
		private DarkTextBox txtSavedDesc;
		public PacketSpammer botPacketSpammer;

		private BotManager()
		{
			InitializeComponent();
			cbSafeType.SelectedIndex = 0;
			botPacketSpammer = new PacketSpammer();
		}

		private void BotManager_Load(object sender, EventArgs e)
		{
			lstBoosts.DisplayMember = "Text";
			//lstQuests.DisplayMember = "Text";
			//lstSkills.DisplayMember = "Text";
			cbBoosts.DisplayMember = "Name";
			cbServers.DisplayMember = "Name";
			lstItems.DisplayMember = "Text";
			cbStatement.DisplayMember = "Text";
			cbLists.SelectedIndex = 0;
			_statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
			_defaultControlText = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.defaulttext, _serializerSettings);
			OptionsManager.StateChanged += OnOptionsStateChanged;
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			string fontName = c.Get("font") ?? "Microsoft Sans Serif";
			int finalItemHeight = int.Parse(c.Get("CommandsSize") ?? "21");
			float finalFontSize = (float)finalItemHeight * 0.38f;
			lstCommands.ItemHeight = finalItemHeight;
			lstCommands.Font = new Font(fontName, finalFontSize, FontStyle.Regular, GraphicsUnit.Point, 0);

			if (chkAntiCounter.Checked)
			{
				Proxy.Instance.ReceivedFromServer += CapturePlayerAura;
			}
		}

		private void TextboxEnter(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			if (t.Text == _defaultControlText[t.Name])
				t.Clear();
		}

		private void TextboxLeave(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			if (string.IsNullOrEmpty(t.Text))
			{
				if (_defaultControlText.TryGetValue(t.Name, out string def))
					t.Text = def;
			}
		}

		public void OnServersLoaded(Server[] servers)
		{
			if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
			{
				cbServers.Items.AddRange(servers);
				cbServers.SelectedIndex = 0;
				Root.Instance.changeServerList.Items.AddRange(servers);
				Root.Instance.changeServerList.SelectedIndex = 0;
			}
		}

		private void MoveListItem(int direction)
		{
			if (SelectedList.SelectedItem != null && SelectedList.SelectedIndex >= 0)
			{
				int num = SelectedList.SelectedIndex + direction;
				if (num >= 0 && num < SelectedList.Items.Count)
				{
					object selectedItem = SelectedList.SelectedItem;
					SelectedList.Items.Remove(selectedItem);
					SelectedList.Items.Insert(num, selectedItem);
					SelectedList.SetSelected(num, value: true);
				}
			}
		}

		private void MoveListItemByKey(int direction)
		{
			if (SelectedList.SelectedItem == null || SelectedList.SelectedIndex < 0)
			{
				return;
			}
			int num = SelectedList.SelectedIndex + direction;
			if (num >= 0 && num < SelectedList.Items.Count)
			{
				object selectedItem = SelectedList.SelectedItem;
				SelectedList.Items.Remove(selectedItem);
				SelectedList.Items.Insert(num, selectedItem);
				if (direction == 1)
				{
					SelectedList.SetSelected(num - 1, value: true);
				}
			}
		}

		public Configuration GenerateConfiguration()
		{
			return new Configuration
			{
				Author = txtAuthor.Text,
				Description = txtDescription.Text,
				Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
				Skills = lstSkills.Items.Cast<Skill>().ToList(),
				Quests = lstQuests.Items.Cast<Quest>().ToList(),
				Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
				Drops = lstDrops.Items.Cast<string>().ToList(),
				Items = lstItems.Items.Cast<string>().ToList(),
				SkillDelay = (int)numSkillD.Value,
				ExitCombatBeforeRest = chkExitRest.Checked,
				ExitCombatBeforeQuest = chkExistQuest.Checked,
				Server = (Server)cbServers.SelectedItem,
				AutoRelogin = chkRelog.Checked,
				RelogDelay = (int)numRelogDelay.Value,
				RelogRetryUponFailure = chkRelogRetry.Checked,
				BotDelay = (int)numBotDelay.Value,
				EnablePickup = chkPickup.Checked,
				EnableRejection = chkReject.Checked,
				EnablePickupAll = chkPickupAll.Checked,
				EnablePickupAcTagged = chkPickupAcTag.Checked,
				WaitForAllSkills = chkAllSkillsCD.Checked,
				WaitForSkill = chkSkillCD.Checked,
				SkipDelayIndexIf = chkSkip.Checked,
				InfiniteAttackRange = chkInfiniteRange.Checked,
				ProvokeMonsters = chkProvoke.Checked,
				EnemyMagnet = chkMagnet.Checked,
				LagKiller = chkLag.Checked,
				HidePlayers = chkHidePlayers.Checked,
				SkipCutscenes = chkSkipCutscenes.Checked,
				WalkSpeed = (int)numWalkSpeed.Value,
				NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
				RestIfMp = chkMP.Checked,
				RestIfHp = chkHP.Checked,
				BankOnStop = chkBankOnStop.Checked,
				RestMp = (int)numRestMP.Value,
				RestHp = (int)numRest.Value,
				RestartUponDeath = chkRestartDeath.Checked,
				RestartOnAFK = chkRestartAFK.Checked,
				AFK = chkAFK.Checked,
				AntiCounter = chkAntiCounter.Checked,
				DisableAnimations = chkDisableAnims.Checked,
				FollowCheck = chkFollowOnly.Checked,
				FollowName = tbFollowPlayer2.Text,
				AutoZone = cmbSpecials.SelectedItem != null ? cmbSpecials.SelectedItem.ToString() : string.Empty,
			};
		}

		public Configuration SaveConfiguration()
		{
			String author = txtAuthor.Text == "Author" ? "" : txtAuthor.Text;
			String desc = txtDescription.Text == "Description (Write in RTF)" ? "" : txtDescription.Text;

			var compressed = DocConvert.Zip<string>(desc);

			return new Configuration
			{
				Author = author,
				Description = compressed.Length > txtDescription.Text.Length ? txtDescription.Text : compressed,
				Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
				Skills = lstSkills.Items.Cast<Skill>().ToList(),
				Quests = lstQuests.Items.Cast<Quest>().ToList(),
				Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
				Drops = lstDrops.Items.Cast<string>().ToList(),
				Items = lstItems.Items.Cast<string>().ToList(),
				SkillDelay = (int)numSkillD.Value,
				ExitCombatBeforeRest = chkExitRest.Checked,
				ExitCombatBeforeQuest = chkExistQuest.Checked,
				Server = (Server)cbServers.SelectedItem,
				AutoRelogin = chkRelog.Checked,
				RelogDelay = (int)numRelogDelay.Value,
				RelogRetryUponFailure = chkRelogRetry.Checked,
				BotDelay = (int)numBotDelay.Value,
				EnablePickup = chkPickup.Checked,
				EnableRejection = chkReject.Checked,
				EnablePickupAll = chkPickupAll.Checked,
				EnablePickupAcTagged = chkPickupAcTag.Checked,
				WaitForAllSkills = chkAllSkillsCD.Checked,
				WaitForSkill = chkSkillCD.Checked,
				SkipDelayIndexIf = chkSkip.Checked,
				InfiniteAttackRange = chkInfiniteRange.Checked,
				ProvokeMonsters = chkProvoke.Checked,
				EnemyMagnet = chkMagnet.Checked,
				LagKiller = chkLag.Checked,
				HidePlayers = chkHidePlayers.Checked,
				SkipCutscenes = chkSkipCutscenes.Checked,
				WalkSpeed = (int)numWalkSpeed.Value,
				NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
				RestIfMp = chkMP.Checked,
				RestIfHp = chkHP.Checked,
				BankOnStop = chkBankOnStop.Checked,
				RestMp = (int)numRestMP.Value,
				RestHp = (int)numRest.Value,
				RestartUponDeath = chkRestartDeath.Checked,
				RestartOnAFK = chkRestartAFK.Checked,
				AFK = chkAFK.Checked,
				AntiCounter = chkAntiCounter.Checked,
				DisableAnimations = chkDisableAnims.Checked,
				FollowCheck = chkFollowOnly.Checked,
				FollowName = tbFollowPlayer2.Text,
				AutoZone = cmbSpecials.SelectedItem != null ? cmbSpecials.SelectedItem.ToString() : string.Empty,
			};
		}

		public void ApplyConfiguration(Configuration config)
		{
			if (config != null)
			{
				if (!chkMerge.Checked || ActiveBotEngine.IsRunning)
				{
					lstCommands.Items.Clear();
					lstBoosts.Items.Clear();
					lstDrops.Items.Clear();
					lstQuests.Items.Clear();
					lstSkills.Items.Clear();
					lstItems.Items.Clear();
					lstSoundItems.Items.Clear();
				}
				txtSavedAuthor.Text = config.Author ?? "Author";
				txtSavedDesc.Text = DocConvert.IsBase64Encoded(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description";
				List<IBotCommand> commands = config.Commands;
				if (commands != null && commands.Count > 0)
				{
					ListBox.ObjectCollection items = lstCommands.Items;
					object[] array = config.Commands.ToArray();
					items.AddRange(array);
				}
				List<Skill> skills = config.Skills;
				if (skills != null && skills.Count > 0)
				{
					ListBox.ObjectCollection items = lstSkills.Items;
					object[] array = config.Skills.ToArray();
					items.AddRange(array);
				}
				List<Quest> quests = config.Quests;
				if (quests != null && quests.Count > 0)
				{
					ListBox.ObjectCollection items = lstQuests.Items;
					object[] array = config.Quests.ToArray();
					items.AddRange(array);
				}
				List<InventoryItem> boosts = config.Boosts;
				if (boosts != null && boosts.Count > 0)
				{
					ListBox.ObjectCollection items = lstBoosts.Items;
					object[] array = config.Boosts.ToArray();
					items.AddRange(array);
				}
				List<string> drops = config.Drops;
				if (drops != null && drops.Count > 0)
				{
					ListBox.ObjectCollection items = lstDrops.Items;
					object[] array = config.Drops.ToArray();
					items.AddRange(array);
				}
				List<string> item = config.Items;
				if (item != null && item.Count > 0)
				{
					ListBox.ObjectCollection items = lstItems.Items;
					object[] array = config.Items.ToArray();
					items.AddRange(array);
				}
				numSkillD.Value = config.SkillDelay;
				chkExitRest.Checked = config.ExitCombatBeforeRest;
				chkExistQuest.Checked = config.ExitCombatBeforeQuest;
				if (config.Server != null)
				{
					cbServers.SelectedIndex = cbServers.Items.Cast<Server>().ToList().FindIndex((Server s) => s.Name == config.Server.Name);
				}
				chkRelog.Checked = config.AutoRelogin;
				numRelogDelay.Value = config.RelogDelay;
				chkRelogRetry.Checked = config.RelogRetryUponFailure;
				numBotDelay.Value = config.BotDelay;
				chkPickup.Checked = config.EnablePickup;
				chkReject.Checked = config.EnableRejection;
				chkPickupAll.Checked = config.EnablePickupAll;
				chkAllSkillsCD.Checked = config.WaitForAllSkills;
				chkSkillCD.Checked = config.WaitForSkill;
				chkSkip.Checked = config.SkipDelayIndexIf;
				chkInfiniteRange.Checked = config.InfiniteAttackRange;
				chkProvoke.Checked = config.ProvokeMonsters;
				chkLag.Checked = config.LagKiller;
				chkMagnet.Checked = config.EnemyMagnet;
				chkHidePlayers.Checked = config.HidePlayers;
				chkSkipCutscenes.Checked = config.SkipCutscenes;
				chkDisableAnims.Checked = config.DisableAnimations;
				numWalkSpeed.Value = (config.WalkSpeed <= 0) ? 8 : config.WalkSpeed;
				List<string> notifyUponDrop = config.NotifyUponDrop;
				if (notifyUponDrop != null && notifyUponDrop.Count > 0)
				{
					ListBox.ObjectCollection items14 = lstSoundItems.Items;
					object[] array = config.NotifyUponDrop.ToArray();
					object[] items15 = array;
					items14.AddRange(items15);
				}
				numRestMP.Value = config.RestMp;
				numRest.Value = config.RestHp;
				chkMP.Checked = config.RestIfMp;
				chkHP.Checked = config.RestIfHp;
				chkBankOnStop.Checked = config.BankOnStop;
				chkRestartDeath.Checked = config.RestartUponDeath;
				chkRestartAFK.Checked = config.RestartOnAFK;
				chkAFK.Checked = config.AFK;
				chkAntiCounter.Checked = config.AntiCounter;
				txtAuthor.Text = config.Author;
				txtDescription.Text =
				txtSavedDesc.Text = DocConvert.IsBase64Encoded(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description";
				var description = txtSavedDesc.Text ?? "Description";
				if (description.StartsWith("{\\rtf") || description.StartsWith("{\rtf"))
					rtbInfo.Rtf = description; //mainTabControl.SelectedTab = tabInfo;
				else
					rtbInfo.Text = config.Description;
				chkFollowOnly.Checked = config.FollowCheck;
				tbFollowPlayer2.Text = config.FollowName == null ? "Player" : config.FollowName;
				if (!string.IsNullOrEmpty(config.AutoZone))
				{
					int index = cmbSpecials.FindStringExact(config.AutoZone);
					cmbSpecials.SelectedIndex = index;
				}
				else
				{
					cmbSpecials.SelectedIndex = -1;
				}
			}
		}

		public void OnConfigurationChanged(Configuration config)
		{
			if (InvokeRequired)
			{
				Invoke((Action)delegate
				{
					ApplyConfiguration(config);
				});
			}
			else
			{
				ApplyConfiguration(config);
			}
		}

		public void OnIndexChanged(int index)
		{
			if (index > -1)
			{
				if (InvokeRequired)
				{
					Invoke((Action)delegate
					{
						lstCommands.SelectedIndex = index;
					});
				}
				else
				{
					lstCommands.SelectedIndex = index;
				}
			}
		}

		public void OnSkillIndexChanged(int index)
		{
			if (index > -1 && index < lstSkills.Items.Count)
			{
				Invoke((Action)delegate
				{
					lstSkills.SelectedIndex = index;
				});
			}
		}

		public void OnIsRunningChanged(bool isRunning)
		{
			if (!isRunning)
			{
				ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
				ActiveBotEngine.IndexChanged -= OnIndexChanged;
				ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;

				void Action()
				{
					btnUp.Enabled = true;
					btnDown.Enabled = true;
					btnClear.Enabled = true;
					btnRemove.Enabled = true;
				}

				if (InvokeRequired)
					Invoke((Action)Action);
				else
					Action();
			}

			if (InvokeRequired)
				Invoke(new Action(() => { chkEnable.Checked = isRunning; }));
			else
				chkEnable.Checked = isRunning;

			Root.Instance.chkStartBot.Checked = isRunning;

			selectionMode(isRunning ? SelectionMode.One : SelectionMode.MultiExtended);

			BotStateChanged(isRunning);

			/*Invoke((Action)delegate
			{
				if (!IsRunning)
				{
					ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
					ActiveBotEngine.IndexChanged -= OnIndexChanged;
					ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;
				}
				BotStateChanged(IsRunning);
			});*/
		}

		private void lstSkills_DoubleClick(object sender, EventArgs e)
		{
			/*int index = lstSkills.SelectedIndex;
			if (index > -1)
			{
				object cmd = lstSkills.Items[index];
				string result = JsonConvert.SerializeObject(cmd, Formatting.Indented, _serializerSettings);
				string mod = RawCommandEditor.Show(result);

				if (mod != null)
				{
					try
					{
						Skill modifiedCmd = (Skill)JsonConvert.DeserializeObject(mod, cmd.GetType());
						lstSkills.Items.Remove(cmd);
						lstSkills.Items.Insert(index, modifiedCmd);
					}
					catch { }
				}
			}*/


			if (lstSkills.Items.Count <= 0)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Title = "Load bot";
					openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
					openFileDialog.Filter = "Grimoire bots|*.gbot";
					openFileDialog.DefaultExt = ".gbot";
					if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
					{
						ApplyConfiguration(config);
					}
				}
			}
			else if (lstSkills.SelectedIndex > -1)
			{
				int selectedIndex = lstSkills.SelectedIndex;
				object obj = lstSkills.Items[selectedIndex];
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
				if (text != null)
				{
					try
					{
						Skill item = (Skill)JsonConvert.DeserializeObject(text, obj.GetType());
						lstSkills.Items.Remove(obj);
						lstSkills.Items.Insert(selectedIndex, item);
					}
					catch
					{

					}
				}
			}
		}

		private void lstQuests_DoubleClick(object sender, EventArgs e)
		{
			if (lstQuests.Items.Count <= 0)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Title = "Load bot";
					openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
					openFileDialog.Filter = "Grimoire bots|*.gbot";
					openFileDialog.DefaultExt = ".gbot";
					if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
					{
						ApplyConfiguration(config);
					}
				}
			}
			else if (lstQuests.SelectedIndex > -1)
			{
				int selectedIndex = lstQuests.SelectedIndex;
				object obj = lstQuests.Items[selectedIndex];
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
				if (text != null)
				{
					try
					{
						Quest item = (Quest)JsonConvert.DeserializeObject(text, obj.GetType());
						lstQuests.Items.Remove(obj);
						lstQuests.Items.Insert(selectedIndex, item);
					}
					catch
					{

					}
				}
			}
		}

		private void lstCommands_DoubleClick(object sender, EventArgs e)
		{
			if (lstCommands.Items.Count <= 0)
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					openFileDialog.Title = "Load bot";
					openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
					openFileDialog.Filter = "Grimoire bots|*.gbot";
					openFileDialog.DefaultExt = ".gbot";
					if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
					{
						ApplyConfiguration(config);
					}
				}
			}
			else if (lstCommands.SelectedIndex > -1)
			{
				int selectedIndex = lstCommands.SelectedIndex;
				object obj = lstCommands.Items[selectedIndex];
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
				if (text != null)
				{
					try
					{
						IBotCommand item = (IBotCommand)JsonConvert.DeserializeObject(text, obj.GetType());
						lstCommands.Items.Remove(obj);
						lstCommands.Items.Insert(selectedIndex, item);
					}
					catch
					{

					}
				}
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (SelectedList.SelectedItem != null)
			{
				int selectedIndex = SelectedList.SelectedIndex;
				if (selectedIndex > -1)
				{
					_RemoveListBoxItem();
				}
			}
			GetAllCommands<CmdLabel>(lbLabels);
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			_MoveListBoxDown();
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			_MoveListBoxUp();
		}

		private void _RemoveListBoxItem()
		{
			SelectedList.BeginUpdate();
			for (int x = SelectedList.SelectedIndices.Count - 1; x >= 0; x--)
			{
				int idx = SelectedList.SelectedIndices[x];
				SelectedList.Items.RemoveAt(idx);
			}
			SelectedList.EndUpdate();
		}

		private void _MoveListBoxUp()
		{
			//MoveListItem(-1);
			SelectedList.BeginUpdate();
			int numberOfSelectedItems = SelectedList.SelectedItems.Count;
			for (int i = 0; i < numberOfSelectedItems; i++)
			{
				// only if it's not the first item
				if (SelectedList.SelectedIndices[i] > 0)
				{
					// the index of the item above the item that we wanna move up
					int indexToInsertIn = SelectedList.SelectedIndices[i] - 1;
					// insert UP the item that we want to move up
					SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
					// removing it from its old place
					SelectedList.Items.RemoveAt(indexToInsertIn + 2);
					// highlighting it in its new place
					SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn];
				}
			}
			SelectedList.EndUpdate();
		}

		private void _MoveListBoxDown()
		{
			//MoveListItem(1);
			SelectedList.BeginUpdate();
			int numberOfSelectedItems = SelectedList.SelectedItems.Count;
			// when going down, instead of moving through the selected items from top to bottom
			// we'll go from bottom to top, it's easier to handle this way.
			for (int i = numberOfSelectedItems - 1; i >= 0; i--)
			{
				// only if it's not the last item
				if (SelectedList.SelectedIndices[i] < SelectedList.Items.Count - 1)
				{
					// the index of the item that is currently below the selected item
					int indexToInsertIn = SelectedList.SelectedIndices[i] + 2;
					// insert DOWN the item that we want to move down
					SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
					// removing it from its old place
					SelectedList.Items.RemoveAt(indexToInsertIn - 2);
					// highlighting it in its new place
					SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn - 1];
				}
			}
			SelectedList.EndUpdate();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (chkAll.Checked)
			{
				lstBoosts.Items.Clear();
				lstCommands.Items.Clear();
				lstDrops.Items.Clear();
				lstItems.Items.Clear();
				lstQuests.Items.Clear();
				lstSkills.Items.Clear();
			}
			else
			{
				SelectedList.Items.Clear();
			}
		}

		private void cbLists_SelectedIndexChanged(object sender, EventArgs e)
		{
			lstBoosts.Visible = SelectedList == lstBoosts;
			lstCommands.Visible = SelectedList == lstCommands;
			lstDrops.Visible = SelectedList == lstDrops;
			lstItems.Visible = SelectedList == lstItems;
			lstQuests.Visible = SelectedList == lstQuests;
			lstSkills.Visible = SelectedList == lstSkills;
		}

		private void BotManager_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void btnKill_Click(object sender, EventArgs e)
		{
			string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
			if (txtMonster.Text == "Monster (* = random)")
			{
				monster = "*";
			}
			AddCommand(new CmdKill
			{
				Monster = monster
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnKillF_Click(object sender, EventArgs e)
		{
			if (txtKillFItem.Text.Length > 0 && txtKillFQ.Text.Length > 0)
			{
				if (chkAddToWhiteList.Checked)
				{
					if (txtKillFItem.Text.Length <= 0) return;
					string[] items = txtKillFItem.Text.Split(new char[] {
						','
					});

					foreach (string _item in items)
					{
						if (!lstDrops.Items.Cast<string>().ToList().Any((string d) => d.Equals(_item, StringComparison.OrdinalIgnoreCase)))
							lstDrops.Items.Add(_item);
					}
				};

				string monster = string.IsNullOrEmpty(txtKillFMon.Text) || txtKillFMon.Text == "Monster (* = random)" ? "*" : txtKillFMon.Text;
				string itemName = txtKillFItem.Text;
				string itemQty = string.IsNullOrEmpty(txtKillFQ.Text) || txtKillFQ.Text == "Quantity (* = any)" ? "*" : txtKillFQ.Text;
				AddCommand(new CmdKillFor
				{
					ItemType = (!rbItems.Checked) ? ItemType.TempItems : ItemType.Items,
					Monster = monster,
					ItemName = itemName.Trim(),
					Quantity = itemQty.Trim()
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnAddSkill_Click(object sender, EventArgs e)
		{
			string index = numSkill.Text;
			AddSkill(new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				Type = Skill.SkillType.Normal
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddSafe_Click(object sender, EventArgs e)
		{
			string index = numSkill.Text;
			int safe = (int)numSafe.Value;

			Skill.SafeType safeType = Skill.SafeType.LowerThan;
			if (cbSafeType.SelectedIndex == 0)
			{
				safeType = Skill.SafeType.LowerThan;
			}
			else if (cbSafeType.SelectedIndex == 1)
			{
				safeType = Skill.SafeType.GreaterThan;
			}
			lstSkills.Items.Add(new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				SafeValue = safe,
				SType = safeType,
				Type = Skill.SkillType.Safe,
				IsSafeMp = chkSafeMp.Checked
			});
		}

		private void btnRest_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdRest(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnRestF_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdRest
			{
				Full = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnJoin_Click(object sender, EventArgs e)
		{
			string text = txtJoin.Text;
			string cell = string.IsNullOrEmpty(txtJoinCell.Text) ? "Enter" : txtJoinCell.Text;
			string pad = string.IsNullOrEmpty(txtJoinPad.Text) ? "Spawn" : txtJoinPad.Text;
			string swf = cbJoinMapSwf.Checked && !tbJoinMapSwf.Text.Contains("(.swf)") ? tbJoinMapSwf.Text : "";
			if (text.Length > 0)
			{
				AddCommand(new CmdJoin
				{
					Map = txtJoin.Text,
					Cell = cell,
					Pad = pad,
					Try = (int)numJoinTry.Value,
					MapSwf = swf
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void cbJoinMapSwf_CheckedChanged(object sender, EventArgs e)
		{
			tbJoinMapSwf.Enabled = cbJoinMapSwf.Checked;
		}

		private void btnCellSwap_Click(object sender, EventArgs e)
		{
			DarkButton s = sender as DarkButton;
			if (s.Text == "<")
			{
				txtJoin.Text = Player.Map + "-" + Flash.Call<string>("RoomNumber", new object[0]);
				txtJoinCell.Text = txtCell.Text;
				txtJoinPad.Text = txtPad.Text;
			}
			else if (s.Text == ">")
			{
				txtCell.Text = txtJoinCell.Text;
				txtPad.Text = txtJoinPad.Text;
			}
		}

		private void btnCurrCell_Click(object sender, EventArgs e)
		{
			txtCell.Text = Player.Cell;
			txtPad.Text = Player.Pad;
		}

		private void btnJump_Click(object sender, EventArgs e)
		{
			string cell = string.IsNullOrEmpty(txtCell.Text) ? "Enter" : txtCell.Text;
			string pad = string.IsNullOrEmpty(txtPad.Text) ? "Spawn" : txtPad.Text;
			AddCommand(new CmdMoveToCell
			{
				Cell = cell,
				Pad = pad
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnWalk_Click(object sender, EventArgs e)
		{
			string x = numWalkX.Value.ToString();
			string y = numWalkY.Value.ToString();
			AddCommand(new CmdWalk
			{
				X = x,
				Y = y
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnWalkCur_Click(object sender, EventArgs e)
		{
			string[] position = Player.Position;
			numWalkX.Value = decimal.Parse(position[0]);
			numWalkY.Value = decimal.Parse(position[1]);
		}

		private void btnWalkRdm_Click(object sender, EventArgs e)
		{
			string x = numWalkX.Value.ToString();
			string y = numWalkY.Value.ToString();
			AddCommand(new CmdWalk
			{
				Type = "Random",
				X = x,
				Y = y
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnItem_Click(object sender, EventArgs e)
		{
			string text = txtItem.Text;
			if (text.Length > 0 && cbItemCmds.SelectedIndex > -1)
			{
				IBotCommand cmd;
				switch (cbItemCmds.SelectedIndex)
				{
					case 0:
						cmd = new CmdGetDrop
						{
							ItemName = text
						};
						break;

					case 1:
						cmd = new CmdSell
						{
							ItemName = text
						};
						break;

					case 2:
						cmd = new CmdEquip
						{
							ItemName = text,
							Safe = true
						};
						break;

					case 3:
						cmd = new CmdEquip
						{
							ItemName = text,
							Safe = false
						};
						break;

					case 4:
						cmd = new CmdBankTransfer
						{
							ItemName = text,
							TransferFromBank = false
						};
						break;

					case 5:
						cmd = new CmdBankTransfer
						{
							ItemName = text,
							TransferFromBank = true
						};
						break;

					case 6:
						cmd = new CmdEquipSet
						{
							ItemName = text
						};
						break;

					case 7:
						cmd = new CmdWhitelist
						{
							ItemName = text,
							state = State.Add
						};
						break;

					case 8:
						cmd = new CmdWhitelist
						{
							ItemName = text,
							state = State.Remove
						};
						break;

					default:
						cmd = new CmdGetDrop
						{
							ItemName = text
						};
						break;
				}
				AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnMapItem_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdMapItem
			{
				ItemId = (int)numMapItem.Value
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnMapItemExe_Click(object sender, EventArgs e)
		{
			Player.GetMapItem((int)numMapItem.Value);
		}

		private void btnBoth_Click(object sender, EventArgs e)
		{
			string text = txtWhitelist.Text;
			if (text.Length > 0)
			{
				AddDrop(text);
				AddItem(text);
			}
		}

		private void btnWhitelist_Click(object sender, EventArgs e)
		{
			string text = txtWhitelist.Text;
			if (text.Length > 0)
			{
				AddDrop(text);
			}
		}

		private void btnSwap_Click(object sender, EventArgs e)
		{
			string text = txtSwapBank.Text;
			string text2 = txtSwapInv.Text;
			if (text.Length > 0 && text2.Length > 0)
			{
				AddCommand(new CmdBankSwap
				{
					InventoryItemName = text2,
					BankItemName = text
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnBoost_Click(object sender, EventArgs e)
		{
			if (cbBoosts.SelectedIndex > -1)
			{
				lstBoosts.Items.Add(cbBoosts.SelectedItem);
			}
		}

		private void cbBoosts_Click(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn) return;
			cbBoosts.Items.Clear();
			DarkComboBox.ObjectCollection items = cbBoosts.Items;
			object[] array = Player.Inventory.Items.Where((InventoryItem i) => i.Category == "ServerUse").ToArray();
			object[] items2 = array;
			items.AddRange(items2);
		}

		private void btnBuy_Click(object sender, EventArgs e)
		{
			if (tbShopItemName.TextLength > 0)
			{
				Int32.TryParse(tbShopId.Text, out int shopId);
				Int32.TryParse(tbItemId.Text, out int itemId);
				Int32.TryParse(tbShopItemId.Text, out int shopItemId);
				int qty = (int)numBuyQty.Value;

				if (radBuyByID.Checked)
				{
					if (shopId == 0 || itemId == 0 || shopItemId == 0) return;
					AddCommand(new CmdBuy
					{
						ShopId = shopId,
						ItemId = itemId,
						ShopItemId = shopItemId,
						Qty = qty,
						ByID = radBuyByID.Checked,
					}, (ModifierKeys & Keys.Control) == Keys.Control);

				}

				if (radBuyByName.Checked)
				{
					if (shopId == 0) return;
					AddCommand(new CmdBuy
					{
						ItemName = tbShopItemName.Text,
						ShopId = shopId,
						Qty = qty,
						ByID = radBuyByID.Checked,
					}, (ModifierKeys & Keys.Control) == Keys.Control);
				}
			}
		}

		private void radBuyByName_CheckedChanged(object sender, EventArgs e)
		{
			tbShopItemId.Enabled = !radBuyByName.Checked;
			tbItemId.Enabled = !radBuyByName.Checked;
			tbShopItemName.Enabled = radBuyByName.Checked;
		}

		private void btnQuestAdd_Click(object sender, EventArgs e)
		{
			AddQuest(
				(int)numQuestListID.Value,
				chkQuestListItem.Checked ? numQuestListItem.Value.ToString() : null, 
				chkReloginCompleteQuest.Checked
				);
		}

		private void btnQuestComplete_Click(object sender, EventArgs e)
		{
			Quest q = new Quest();
			CmdCompleteQuest cmd = new CmdCompleteQuest
			{
				CompleteTry = (int)numEnsureTries.Value,
			};
			q.Id = (int)numQuestID.Value;
			if (chkQuestItem.Checked)
				q.ItemId = numQuestItem.Value.ToString();
			cmd.Quest = q;
			this.AddCommand(cmd, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnQuestAccept_Click(object sender, EventArgs e)
		{
			Quest quest = new Quest
			{
				Id = (int)numQuestID.Value
			};
			AddCommand(new CmdAcceptQuest
			{
				Quest = quest
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void chkQuestItem_CheckedChanged(object sender, EventArgs e)
		{
			numQuestItem.Enabled = chkQuestItem.Checked;
		}

		private void btnPacket_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdPacket
			{
				Packet = txtPacket.Text,
				SpamTimes = Decimal.ToInt32(numSpamTimes.Value)
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnDelay_Click(object sender, EventArgs e)
		{
			int delay = (int)numDelay.Value;
			AddCommand(new CmdDelay
			{
				Delay = delay
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnGoto_Click(object sender, EventArgs e)
		{
			string text = txtPlayer.Text;
			if (text.Length > 0)
			{
				AddCommand(new CmdGotoPlayer
				{
					PlayerName = text
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnBotDelay_Click(object sender, EventArgs e)
		{
			int delay = (int)numBotDelay.Value;
			AddCommand(new CmdBotDelay
			{
				Delay = delay
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdStop(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnRestart_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdRestart(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		public void btnLoad_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Title = "Load bot";
				openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
				openFileDialog.Filter = "Grimoire bots|*.gbot";
				openFileDialog.DefaultExt = ".gbot";
				if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
				{
					ApplyConfiguration(config);
					GetAllCommands<CmdLabel>(lbLabels);
				}
			}
		}

		private bool TryDeserialize(string json, out Configuration config)
		{
			try
			{
				config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
				return true;
			}
			catch (Exception e) { MessageBox.Show(e.ToString()); }
			config = null;
			return false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Title = "Save bot";
				saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
				saveFileDialog.DefaultExt = ".gbot";
				saveFileDialog.Filter = "Grimoire bots|*.gbot";
				saveFileDialog.CheckFileExists = false;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Configuration value = SaveConfiguration();
					try
					{
						File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
					}
					catch (Exception ex)
					{
						MessageBox.Show("Unable to save bot: " + ex.Message);
					}
				}
			}
		}

		private void btnLoadCmd_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				string initialDirectory = Path.Combine(Application.StartupPath, "Bots");
				openFileDialog.Title = "Select bot to load";
				openFileDialog.Filter = "Grimoire bots|*.gbot";
				openFileDialog.InitialDirectory = initialDirectory;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					AddCommand(new CmdLoadBot
					{
						BotFilePath = Extensions.MakeRelativePathFrom(Application.StartupPath, openFileDialog.FileName), // Path.GetFullPath(openFileDialog.FileName)
						BotFileName = Path.GetFileName(openFileDialog.FileName)

					}, (ModifierKeys & Keys.Control) == Keys.Control);
				}
			}
		}

		private void cbStatement_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
			{
				StatementCommand statementCommand = (StatementCommand)cbStatement.SelectedItem;
				txtStatement1.Enabled = statementCommand.Description1 != null;
				txtStatement2.Enabled = statementCommand.Description2 != null;
				txtStatement1.Text = statementCommand.Description1;
				txtStatement2.Text = statementCommand.Description2;
			}
		}

		private void btnStatementAdd_Click(object sender, EventArgs e)
		{
			if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
			{
				string text = txtStatement1.Text;
				string text2 = txtStatement2.Text;
				StatementCommand statementCommand = (StatementCommand)Activator.CreateInstance(cbStatement.SelectedItem.GetType());
				statementCommand.Value1 = text;
				statementCommand.Value2 = text2;
				AddCommand((IBotCommand)statementCommand, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbCategories.SelectedIndex > -1)
			{
				cbStatement.Items.Clear();
				string text = cbCategories.SelectedItem.ToString();
				DarkComboBox.ObjectCollection items = cbStatement.Items;
				object[] array = _statementCommands.Where((StatementCommand s) => s.Tag == text).ToArray();
				object[] items2 = array;
				items.AddRange(items2);
			}
		}

		private void lbLabels_DoubleClick(object sender, EventArgs e)
		{
			string findCmd = this.lbLabels.SelectedItem.ToString();
			string data2 = this.lbLabels.SelectedItem.ToString().Replace("[", "").Replace("]", "");
			if ((ModifierKeys & Keys.Control) == Keys.Control)
			{ 
                for (int i = 0; i < lstCommands.Items.Count; i++)
				{
				    if (lstCommands.Items[i].ToString().Contains(findCmd))
				    {
				        lstCommands.SelectedIndex = i;
				        //lstCommands.TopIndex = i; // scroll to label index
				        break;
				    }
				}
			} else this.txtLabel.Text = $"{data2}";
        }

		private void btnGotoLabel_Click(object sender, EventArgs e)
		{
			if (txtLabel.TextLength > 0)
			{
				AddCommand(new CmdGotoLabel
				{
					Label = txtLabel.Text.ToUpper()
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
			GetAllCommands<CmdLabel>(lbLabels);
		}

		private void btnAddLabel_Click(object sender, EventArgs e)
		{
			if (txtLabel.TextLength > 0)
			{
				AddCommand(new CmdLabel
				{
					Name = txtLabel.Text.ToUpper()
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
			GetAllCommands<CmdLabel>(lbLabels);
		}

		private void GetAllCommands<T>(ListBox lb)
		{
			lb.Items.Clear();
			T[] allItems = lstCommands.Items.OfType<T>().ToArray();
			string[] allStrings = new string[allItems.Count()];
			for (int i = 0; i < allItems.Count(); i++)
				allStrings[i] = allItems[i].ToString();
			lb.Items.AddRange(allStrings);
		}

		private void btnLogout_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdLogout(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void UpdateTree()
		{
			if (!string.IsNullOrEmpty(txtSaved.Text) && Directory.Exists(txtSaved.Text))
			{
				lblBots.Text = string.Format("Number of Bots: {0}", Directory.EnumerateFiles(txtSaved.Text, "*.gbot", SearchOption.AllDirectories).Count());
				treeBots.Nodes.Clear();
				AddTreeNodes(treeBots, txtSaved.Text);
			}
		}

		private void treeBots_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
			if (File.Exists(path))
			{
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    string relativePath = "Bots" + path.Substring(txtSaved.Text.Length);
					LogForm.Instance.AppendDebug($"Path : {path}\r\nRelative path : {relativePath}\r\nNode : {e.Node.FullPath}");
                    AddCommand(new CmdLoadBot
                    {
                        BotFileName = Path.GetFileName(path),       // "06 - The lovers embrace.gbot"
                        BotFilePath = relativePath					// "...\Bots\Arcana"
                    }, Insert:true);
                    return;
                }

                if (!TryDeserialize(File.ReadAllText(path), out Configuration config))
				{
					return;
				}
				ApplyConfiguration(config);
			}
			lblCommands.Text = $"Number of{Environment.NewLine}Commands: {lstCommands.Items.Count}";
			lblSkills.Text = $"Skills: {lstSkills.Items.Count}";
			lblQuests.Text = $"Quests: {lstQuests.Items.Count}";
			lblDrops.Text = $"Drops: {lstDrops.Items.Count}";
			lblBoosts.Text = $"Boosts: {lstBoosts.Items.Count}";
			lblItems.Text = $"Items: {lstItems.Items.Count}";
		}

		private void treeBots_AfterExpand(object sender, TreeViewEventArgs e)
		{
			string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
			if (Directory.Exists(path))
			{
				AddTreeNodes(e.Node, path);
				if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
				{
					e.Node.Nodes.RemoveAt(0);
				}
			}
		}

		private void AddTreeNodes(TreeNode node, string path)
		{
			foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
			{
				string add = Path.GetFileName(item);
				if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
				{
					node.Nodes.Add(add).Nodes.Add("Loading...");
				}
			}
			foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
			{
				string add2 = Path.GetFileName(item2);
				if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
				{
					node.Nodes.Add(add2);
				}
			}
		}

		private void AddTreeNodes(TreeView tree, string path)
		{
			foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
			{
				string add = Path.GetFileName(item);
				if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
				{
					tree.Nodes.Add(add).Nodes.Add("Loading...");
				}
			}
			foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
			{
				string add2 = Path.GetFileName(item2);
				if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
				{
					tree.Nodes.Add(add2);
				}
			}
		}

		private void btnSavedAdd_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtSaved.Text))
			{
				string path = Path.Combine(txtSaved.Text, txtSavedAdd.Text);
				if (!Directory.Exists(path))
				{
					try
					{
						Directory.CreateDirectory(path);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Unable to create directory: " + ex.Message, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				UpdateTree();
			}
		}

		private void btnSoundAdd_Click(object sender, EventArgs e)
		{
			if (txtSoundItem.TextLength > 0)
			{
				lstSoundItems.Items.Add(txtSoundItem.Text);
			}
		}

		private void btnSoundDelete_Click(object sender, EventArgs e)
		{
			int selectedIndex = lstSoundItems.SelectedIndex;
			if (selectedIndex > -1)
			{
				lstSoundItems.Items.RemoveAt(selectedIndex);
			}
		}

		private void btnSoundTest_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 5; i++)
			{
				Console.Beep();
			}
		}

		private void chkInfiniteRange_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.InfiniteRange = chkInfiniteRange.Checked;
			Root.Instance.infRangeToolStripMenuItem.Checked = chkInfiniteRange.Checked;
		}

		private void chkProvoke_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.ProvokeMonsters = chkProvoke.Checked;
			Root.Instance.provokeToolStripMenuItem1.Checked = chkProvoke.Checked;
		}

		private void chkMagnet_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.EnemyMagnet = chkMagnet.Checked;
			Root.Instance.enemyMagnetToolStripMenuItem.Checked = chkMagnet.Checked;
		}

		private void chkLag_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.LagKiller = chkLag.Checked;
			Root.Instance.lagKillerToolStripMenuItem.Checked = chkLag.Checked;
		}

		private void chkHidePlayers_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.HidePlayers = chkHidePlayers.Checked;
			Root.Instance.hidePlayersToolStripMenuItem.Checked = chkHidePlayers.Checked;
		}

		private void chkSkipCutscenes_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.SkipCutscenes = chkSkipCutscenes.Checked;
			Root.Instance.skipCutscenesToolStripMenuItem.Checked = chkSkipCutscenes.Checked;
		}

		private void numWalkSpeed_ValueChanged(object sender, EventArgs e)
		{
			OptionsManager.WalkSpeed = (int)numWalkSpeed.Value;
		}

		public async void chkWalkSpeed_CheckedChanged(object sender, EventArgs e)
		{
			numWalkSpeed.Enabled = !chkWalkSpeed.Checked;
			OptionsManager.WalkSpeed = (int)numWalkSpeed.Value;
			Root.Instance.walkspeedToolStripMenuItem.Checked = chkWalkSpeed.Checked;
			while (chkWalkSpeed.Checked)
			{
				OptionsManager.SetWalkSpeed();
				await Task.Delay(250);
			}
			if (!chkWalkSpeed.Checked)
				Flash.Call("SetWalkSpeed", new string[] { "8" });
		}

		private void chkDisableAnims_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.DisableAnimations = chkDisableAnims.Checked;
			Root.Instance.disableAnimationsToolStripMenuItem.Checked = chkDisableAnims.Checked;
		}

		private void OnOptionsStateChanged(bool state)
		{
			if (InvokeRequired)
			{
				Invoke((Action)delegate
				{
					chkEnableSettings.Checked = state;
				});
			}
			else
			{
				chkEnableSettings.Checked = state;
			}
		}

		private void chkEnableSettings_Click(object sender, EventArgs e)
		{
			if (chkEnableSettings.Checked)
				OptionsManager.Start();
			else
				OptionsManager.Stop();
		}

		private void lstBoxs_KeyPress(object sender, KeyEventArgs e)
		{
			if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
			{
				_MoveListBoxUp();
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Down)
			{
				_MoveListBoxDown();
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Delete)
			{
				btnRemove.PerformClick();
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.D && SelectedList.SelectedIndex > -1)
			{
				var selectedItems = SelectedList.SelectedItems;
				for (int i = 0; selectedItems.Count > i; i++)
				{
					SelectedList.Items.Insert(SelectedList.SelectedIndex + selectedItems.Count + i, selectedItems[i]);
				}
				e.Handled = true;
			}
			else if ((ModifierKeys == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1))
			{
				Clipboard.Clear();
				Configuration items = new Configuration
				{
					Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList()
				};
				string[] itemsString = new string[items.Commands.Count];
				for (int i = 0; i < items.Commands.Count; i++)
				{
					itemsString[i] = items.Commands[i].ToString();
				}
				Clipboard.SetText(String.Join(Environment.NewLine, itemsString));
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1)
			{
				Clipboard.Clear();
				Configuration items = new Configuration
				{
					Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
					Skills = lstSkills.SelectedItems.Cast<Skill>().ToList(),
					Quests = lstQuests.SelectedItems.Cast<Quest>().ToList(),
					Boosts = lstBoosts.SelectedItems.Cast<InventoryItem>().ToList(),
					Drops = lstDrops.SelectedItems.Cast<string>().ToList(),
					Items = lstItems.SelectedItems.Cast<string>().ToList()
				};
				Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
			{
				TryDeserialize(Clipboard.GetText(), out Configuration config);
				List<IBotCommand> commands = config.Commands;
				if (commands != null && commands.Count > 0)
				{
					List<IBotCommand> items = lstCommands.Items.Cast<IBotCommand>().ToList();
					int selectedIndex = lstCommands.SelectedIndex;
					lstCommands.SelectedIndex = -1;
					items.InsertRange(++selectedIndex, commands);
					lstCommands.Items.Clear();
					lstCommands.Items.AddRange(items.ToArray());
					for (int i = 0; i < commands.Count; i++)
						lstCommands.SelectedIndex = selectedIndex + i;

					/* Deprecated
					ListBox.ObjectCollection items = lstCommands.Items;
					object[] array = config.Commands.ToArray();
					int selectedIndex = lstCommands.SelectedIndex;
					lstCommands.SelectedIndex = -1;
					for (int i = 0; array.Count() > i; i++)
					{
						items.Insert(selectedIndex + i + 1, array[i]);
						lstCommands.SelectedIndex = selectedIndex + i + 1;
					}
					*/
				}
				List<Skill> skills = config.Skills;
				if (skills != null && skills.Count > 0)
				{
					ListBox.ObjectCollection items = lstSkills.Items;
					object[] array = config.Skills.ToArray();
					items.AddRange(array);
				}
				List<Quest> quests = config.Quests;
				if (quests != null && quests.Count > 0)
				{
					ListBox.ObjectCollection items = lstQuests.Items;
					object[] array = config.Quests.ToArray();
					items.AddRange(array);
				}
				List<InventoryItem> boosts = config.Boosts;
				if (boosts != null && boosts.Count > 0)
				{
					ListBox.ObjectCollection items = lstBoosts.Items;
					object[] array = config.Boosts.ToArray();
					items.AddRange(array);
				}
				List<string> drops = config.Drops;
				if (drops != null && drops.Count > 0)
				{
					ListBox.ObjectCollection items = lstDrops.Items;
					object[] array = config.Drops.ToArray();
					foreach (String drop in array) 
					{
						if (!items.Contains(drop)) items.AddRange(array);
					}
				}
				List<string> item = config.Items;
				if (item != null && item.Count > 0)
				{
					ListBox.ObjectCollection items = lstItems.Items;
					object[] array = config.Items.ToArray();
					foreach (String drop in array) 
					{
						if (!items.Contains(drop)) items.AddRange(array);
					}
				}
				e.Handled = true;
			}
			else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.X && this.SelectedList.SelectedIndex > -1)
			{
				Clipboard.Clear();
				Configuration value = new Configuration
				{
					Commands = this.lstCommands.SelectedItems.Cast<IBotCommand>().ToList<IBotCommand>(),
					Skills = this.lstSkills.SelectedItems.Cast<Skill>().ToList<Skill>(),
					Quests = this.lstQuests.SelectedItems.Cast<Quest>().ToList<Quest>(),
					Boosts = this.lstBoosts.SelectedItems.Cast<InventoryItem>().ToList<InventoryItem>(),
					Drops = this.lstDrops.SelectedItems.Cast<string>().ToList<string>(),
				};
				Clipboard.SetText(JsonConvert.SerializeObject(value, Formatting.Indented, this._serializerSettings));
				this.btnRemove.PerformClick();
				e.Handled = true;
				return;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Title = "Save bot";
					saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
					saveFileDialog.DefaultExt = ".gbot";
					saveFileDialog.Filter = "Grimoire bots|*.gbot";
					saveFileDialog.CheckFileExists = false;
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						Configuration value = SaveConfiguration();
						try
						{
							File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
						}
						catch (Exception ex)
						{
							MessageBox.Show("Unable to save bot: " + ex.Message);
						}
					}
				}
				e.Handled = true;
			}
		}

		public void AddCommand(IBotCommand cmd, bool Insert = false)
		{
			if (Insert)
			{
				lstCommands.Items.Insert((lstCommands.SelectedIndex > -1) ? lstCommands.SelectedIndex : lstCommands.Items.Count, cmd);
			}
			else
			{
				lstCommands.Items.Add(cmd);
			}
		}

		private void AddSkill(Skill skill, bool Insert)
		{
			if (Insert)
			{
				lstSkills.Items.Insert((lstSkills.SelectedIndex > -1) ? lstSkills.SelectedIndex : lstSkills.Items.Count, skill);
			}
			else
			{
				lstSkills.Items.Add(skill);
			}
		}

		private async void SendInfoMessages(string message)
		{
			await Proxy.Instance.SendToClient($"%xt%server%-1%BOT : {message}%");
		}

		private async Task UnBankItems()
		{
			SendInfoMessages("Unbanking items to invent...");
			foreach (string item in lstItems.Items)
			{
				Player.Bank.TransferFromBank(item);
				await Task.Delay(200);
				//LogForm.Instance.AppendDebug("Transferred from Bank: " + item + "\r\n");
			}
			SendInfoMessages("Unbanking done.");
		}

		private async Task BankingItems()
		{
			SendInfoMessages("Banking items from invent...");
			foreach (InventoryItem item in Player.Inventory.Items)
			{
				if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && lstItems.Items.Contains(item.Name))
				{
					Player.Bank.TransferToBank(item.Name);
					await Task.Delay(100);
					//LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
				}
			}
			SendInfoMessages("Banking done.");
		}

		private void setPresetsSkills()
		{

		}

		private async void chkEnableBot_CheckedChanged(object sender, EventArgs e)
		{
			if (chkEnable.Checked && (lstCommands.Items.Count <= 0 || !Player.IsLoggedIn))
			{
				chkEnable.Checked = false;
				return;
			}

			chkAntiMod.Enabled = !chkEnable.Checked;
			chkAMLogout.Enabled = !chkEnable.Checked;
			chkAMStopBot.Enabled = !chkEnable.Checked;
			chkReloginCompleteQuest.Enabled = !chkEnable.Checked;
			numQuestDelay.Enabled = !chkEnable.Checked;
			numBotDelay.Enabled = !chkEnable.Checked;
			if (cmbSpecials.SelectedIndex != -1)
			{
				chkSpecial.Enabled = !chkEnable.Checked;
			}

			chkEnable.Enabled = false;
			Root.Instance.chkStartBot.Enabled = false;

			if (chkEnable.Checked)
			{
				if (cmbSpecials.SelectedIndex != -1 && !chkSpecial.Checked)
				{
					chkSpecial.Checked = true;
				}

				setPresetsSkills();

				if (lstItems.Items.Count > 0 && chkInventOnStart.Checked)
					await UnBankItems();
				selectionMode(SelectionMode.One);
				ActiveBotEngine.IsRunningChanged += OnIsRunningChanged;
				ActiveBotEngine.IndexChanged += OnIndexChanged;
				ActiveBotEngine.ConfigurationChanged += OnConfigurationChanged;
				ActiveBotEngine.Start(GenerateConfiguration());
				BotStateChanged(IsRunning: true);
				Root.Instance.BotStateChanged(IsRunning: true);
				Root.Instance.chkStartBot.Checked = true;
				if (chkAntiMod.Checked)
				{
					chkHidePlayers.Enabled = false;
					chkHidePlayers.Checked = false;
				}

				if (SpecialJsonHandler != null)
					Proxy.Instance.RegisterHandler(SpecialJsonHandler);
				if (SpecialXtHandler != null)
					Proxy.Instance.RegisterHandler(SpecialXtHandler);
			}
			else
			{
				ActiveBotEngine.Stop();
				selectionMode(SelectionMode.MultiExtended);
				BotStateChanged(IsRunning: false);
				Root.Instance.BotStateChanged(IsRunning: false);
				Root.Instance.chkStartBot.Checked = false;
				chkHidePlayers.Enabled = true;

				if (Player.CurrentState == Player.State.InCombat)
				{
					Player.CancelTarget();
					Player.CancelAutoAttack();
				}
				if (lstItems.Items.Count > 0 && this.chkBankOnStop.Checked)
				{
					Player.MoveToCell(Player.Cell, Player.Pad);
					await Task.Delay(2000);
					await BankingItems();
				}

				if (SpecialJsonHandler != null)
					Proxy.Instance.UnregisterHandler(SpecialJsonHandler);
				if (SpecialXtHandler != null)
					Proxy.Instance.UnregisterHandler(SpecialXtHandler);

				if (cmbSpecials.SelectedIndex != -1 && chkSpecial.Enabled)
				{
					chkSpecial.Checked = false;
					chkSpecial.Enabled = true;
				}
			}
			toggleAntiMod(chkAntiMod.Checked && chkEnable.Checked);

			chkEnable.Enabled = true;
			Root.Instance.chkStartBot.Enabled = true;
		}


		private void selectionMode(SelectionMode mode)
		{
			this.lstCommands.SelectionMode = mode;
			this.lstSkills.SelectionMode = mode;
			this.lstQuests.SelectionMode = mode;
			this.lstDrops.SelectionMode = mode;
			this.lstBoosts.SelectionMode = mode;
			this.lstItems.SelectionMode = mode;
		}

		public void BotStateChanged(bool IsRunning)
		{
			btnUp.Enabled = !IsRunning;
			btnDown.Enabled = !IsRunning;
			btnClear.Enabled = !IsRunning;
			btnRemove.Enabled = !IsRunning;
		}

		public void AddQuest(int QuestID, string ItemID = null, bool safeRelogin = false)
		{
			Quest quest = new Quest
			{
				Id = QuestID,
				ItemId = ItemID,
				SafeRelogin = safeRelogin
			};
			quest.Text = (quest.ItemId != null) ? $"{quest.Id}:{quest.ItemId}" : quest.Id.ToString();
			if (!lstQuests.Items.Contains(quest))
			{
				lstQuests.Items.Add(quest);
			}
		}

		public void RemoveQuest(int QuestID, string ItemID = null, bool safeRelogin = false)
		{
			var i = lstQuests.Items.Cast<Quest>().ToList().FirstOrDefault((Quest q) =>
				q.Id == QuestID && q.ItemId == ItemID && q.SafeRelogin == safeRelogin);
			lstQuests.Items.Remove(i);
		}

		public void AddDrop(string Name)
		{
			if (!lstDrops.Items.Contains(Name))
			{
				lstDrops.Items.Add(Name);
			}
		}

		static BotManager()
		{
			Instance = new BotManager();
			Log = new LogForm();
		}

		private void btnBuyFast_Click(object sender, EventArgs e)
		{
			if (tbShopItemName.TextLength > 0)
			{
				AddCommand(new CmdBuyFast
				{
					ItemName = tbShopItemName.Text,
					Qty = (int)numBuyQty.Value
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnLoadShop_Click(object sender, EventArgs e)
		{
			Int32.TryParse(tbShopId.Text, out int shopId);
			if (shopId == 0) return;
			AddCommand(new CmdLoad
			{
				ShopId = shopId,
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void logScript(object sender, EventArgs e)
		{
			AddCommand(new CmdLog
			{
				Text = txtLog.Text
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void logDebug(object sender, EventArgs e)
		{
			AddCommand(new CmdLog
			{
				Text = txtLog.Text,
				Debug = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnchangeName_Click(object sender, EventArgs e)
		{
			foreach (string n in Configuration.BlockedPlayers)
			{
				if (txtUsername.Text.ToLower() == n)
				{
					Environment.Exit(0);
				}
			}
			CustomName = txtUsername.Text;
		}

		private void btnchangeGuild_Click(object sender, EventArgs e)
		{
			foreach (string n in Configuration.BlockedPlayers)
			{
				if (txtUsername.Text.ToLower() == n)
				{
					Environment.Exit(0);
				}
			}
			CustomGuild = txtGuild.Text;
		}

		private void btnProvokeOn_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdProvoke
			{
				Set = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnProvokeOff_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdProvoke
			{
				Set = false
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		public void AddItem(string Name)
		{
			if (!lstItems.Items.Contains(Name))
			{
				lstItems.Items.Add(Name);
			}
		}

		private void btnUnbanklst_Click(object sender, EventArgs e)
		{
			string text = txtWhitelist.Text;
			if (text.Length > 0)
			{
				AddItem(text);
			}
		}

		private void lstLogText_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Control && e.KeyCode == Keys.C;
			if (flag)
			{
				string data = this.lstLogText.SelectedItem.ToString();
				Clipboard.SetData(DataFormats.StringFormat, data);
			}
		}

		private void numOptionsTimer_ValueChanged(object sender, EventArgs e)
		{
			OptionsManager.Timer = (int)numOptionsTimer.Value;
		}

		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == tabBots)
			{
				this.txtSaved.Text = Path.Combine(Application.StartupPath, "Bots");
				try
				{
					string botsDir = ClientConfig.GetValue(ClientConfig.C_BOTS_DIR);
					if (Directory.Exists(botsDir)) this.txtSaved.Text = botsDir;
				} catch{ }
				UpdateTree();
			}
			else if (e.TabPage == tabMisc)
			{
				GetAllCommands<CmdLabel>(lbLabels);
			}
		}

		private void txtSaved_TextChanged(object sender, EventArgs e)
		{
			UpdateTree();
		}

		private void lstLogText_DoubleClick(object sender, EventArgs e)
		{
			string data = this.txtLog.Text == "Logs" ? "" : txtLog.Text + " ";
			string data2 = this.lstLogText.SelectedItem.ToString();
			this.txtLog.Text = $"{data}{data2}";
		}

		public void MultiMode()
		{
			if (this.lstCommands.SelectionMode != SelectionMode.MultiExtended)
			{
				this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
				this.lstItems.SelectionMode = SelectionMode.MultiExtended;
				this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
				this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
				this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
				this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
			}
			else
			{
				this.lstCommands.SelectionMode = SelectionMode.One;
				this.lstItems.SelectionMode = SelectionMode.One;
				this.lstSkills.SelectionMode = SelectionMode.One;
				this.lstQuests.SelectionMode = SelectionMode.One;
				this.lstDrops.SelectionMode = SelectionMode.One;
				this.lstBoosts.SelectionMode = SelectionMode.One;
			}
		}

		private void lstCommands_DragDrop(object sender, DragEventArgs e)
		{
			Configuration config;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length > 1) return;
			if (TryDeserialize(File.ReadAllText(files[0]), out config))
			{
				ApplyConfiguration(config);
				GetAllCommands<CmdLabel>(lbLabels);
			}
		}

		private void lstCommands_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		private void lstCommands_Click(object sender, EventArgs e)
		{

		}

		private void btnBlank_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdBlank3 { Text = "...", Alpha = 1, R = 220, G = 220, B = 220 }, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void chkAFK_CheckedChanged(object sender, EventArgs e)
		{
			OptionsManager.AFK = chkAFK.Checked;
		}

		private void chkRelog_CheckedChanged(object sender, EventArgs e)
		{
			this.chkAFK.Enabled = chkRelog.Checked;
		}

		private void btnCurrBlank_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdMoveToCell
			{
				Cell = "Blank",
				Pad = "Spawn"
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}
		private void btnSetSpawn_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdSetSpawn(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnBeep_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdBeep
			{
				Times = (int)numBeepTimes.Value
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddSkillCmd_Click(object sender, EventArgs e)
		{
			string index = numSkillCmd.Text;
			string target = txtMonsterSkillCmd.Text;
			if (target == "Monster (* = random)") target = "*";
			Skill skill = new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				Type = Skill.SkillType.Normal,
			};

			AddCommand(new CmdUseSkill
			{
				Index = skill.Index,
				Wait = cbSkillCmdWait.Checked,
				Force = chkForceSkill.Checked,
				Monster = target
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private async void btnSetHero_Click(object sender, EventArgs e)
		{
			Button s = (Button)sender;
			switch (s.Name)
			{
				case "btnSetGood":
					Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%1%");
					break;
				case "btnSetEvil":
					Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%2%");
					break;
				case "btnSetChaos":
					Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%3%");
					break;
				case "btnSetUndecided":
					Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%5%");
					break;
				case "btnSetMem":
					Player.ChangeAccessLevel("Member");
					break;
				case "btnSetNonMem":
					Player.ChangeAccessLevel("Non Member");
					break;
				case "btnSetModerator":
					Player.ChangeAccessLevel("Moderator");
					break;
			}
		}

		private void chkToggleMute_CheckedChanged(object sender, EventArgs e)
		{
			Player.ToggleMute(!chkToggleMute.Checked);
		}

		private void changeFontsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontDialog fdlg = new FontDialog();
			if (fdlg.ShowDialog() == DialogResult.OK)
			{
				this.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
				foreach (Control control in this.Controls)
				{
					control.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
				}
				var selectedOption = MessageBox.Show("Would you like to Save style and have it load on the next startup?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (selectedOption == DialogResult.Yes)
				{
					Config c = Config.Load(Application.StartupPath + "\\config.cfg");
					c.Set("font", fdlg.Font.FontFamily.Name.ToString());
					c.Set("fontSize", fdlg.Font.Size.ToString());
					c.Save();
				}
			}
		}

		private void btnAttack_Click(object sender, EventArgs e)
		{
			string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
			AddCommand(new CmdAttack
			{
				Monster = txtMonster.Text == "Monster (* = random)" ? "*" : txtMonster.Text
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void chkBankOnStop_CheckedChanged(object sender, EventArgs e)
		{
			Configuration.Instance.BankOnStop = chkBankOnStop.Checked;
		}

		private void btnGotoIndex_Click(object sender, EventArgs e)
		{
			IBotCommand cmd;
			switch (((Button)sender).Text)
			{
				case "++":
					cmd = new CmdIndex
					{
						Type = CmdIndex.IndexCommand.Up,
						Index = (int)numIndexCmd.Value
					};
					break;
				case "--":
					cmd = new CmdIndex
					{
						Type = CmdIndex.IndexCommand.Down,
						Index = (int)numIndexCmd.Value
					};
					break;
				default:
					cmd = new CmdIndex
					{
						Type = CmdIndex.IndexCommand.Goto,
						Index = (int)numIndexCmd.Value
					};
					break;
			}
			AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void TogglePages()
		{
			Size p1 = splitContainer2.Panel1.ClientSize;
			int p1w = splitContainer2.Panel1.ClientSize.Width;
			Size p2 = splitContainer2.Panel2.ClientSize;
			int p2w = splitContainer2.Panel2.ClientSize.Width;
			if (mainTabControl.Visible)
			{
				this.ClientSize = new Size(p1w, ClientSize.Height);
				mainTabControl.Visible = false;
				splitContainer2.Panel2Collapsed = true;
			}
			else
			{
				this.ClientSize = new Size(p1w + 880, ClientSize.Height);
				splitContainer2.Panel2Collapsed = false;
				mainTabControl.Visible = true;
			}
		}

		public Dictionary<string, Color> CurrentColors = new Dictionary<string, Color>();

		public Color GetCurrentColor(string cmd)
		{
			if (!CurrentColors.ContainsKey(cmd))
				CurrentColors[cmd] = GetColor(cmd);
			return CurrentColors[cmd];
		}

		public Color GetColor(string name)
		{
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			return Color.FromArgb(int.Parse(c.Get(name + "Color") ?? "FFDCDCDC", System.Globalization.NumberStyles.HexNumber));
		}

		public Dictionary<string, bool> CurrentCentered = new Dictionary<string, bool>();

		private bool GetCurrentBoolCentered(string cmd)
		{
			if (!CurrentCentered.ContainsKey(cmd))
				CurrentCentered[cmd] = GetBoolCentered(cmd);
			return CurrentCentered[cmd];
		}

		private bool GetBoolCentered(string name)
		{
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			return bool.Parse(c.Get(name + "Centered") ?? "false");
		}

		private void lstCommands_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (!(e.Index > -1))
				return;
			e.DrawBackground();
			if (!colorfulCommands.Checked)
			{
				// Define the default color of the brush as black.
				Brush myBrush = Brushes.Gainsboro;
				// Draw the current item text based on the current Font 
				// and the custom brush settings.
				e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(),
					e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
				// If the ListBox has focus, draw a focus rectangle around the selected item.
				e.DrawFocusRectangle();
				return;
			}

			#region Settings
			IBotCommand cmd = (IBotCommand)lstCommands.Items[e.Index];
			string[] count = cmd.GetType().ToString().Split('.');
			string scmd = count[count.Count() - 1].Replace("Cmd", "");
			//string WindowText = SystemColors.WindowText.ToArgb().ToString();
			SolidBrush color = new SolidBrush(GetCurrentColor(scmd));
			SolidBrush varColor = new SolidBrush(GetCurrentColor("Variable"));
			SolidBrush eVarColor = new SolidBrush(GetCurrentColor("ExtendedVariable"));
			SolidBrush indexcolor = new SolidBrush(GetCurrentColor("Index"));
			RectangleF region = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
			Font font = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Regular);
			StringFormat centered = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			#endregion

			string[] LabelLess = new string[]
			{
				"Label",
				"GotoLabel",
				"Blank",
				"Blank2",
				"StatementCommand",
				"Index",
				"SkillSet"
			};

			if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
			{
				string txt = lstCommands.Items[e.Index].ToString();
				Font cmdFont = new Font("Arial", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel);
				if (cmd is CmdBlank2 && txt.Contains("[RGB]"))
					using (Font the_font = new Font("Times New Roman", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel))
					{
						var font_info = new FontInfo(e.Graphics, the_font);
						SizeF text_size = e.Graphics.MeasureString(txt, the_font);
						int x0 = (int)((this.ClientSize.Width - text_size.Width) / 2);
						int y0 = (int)((this.ClientSize.Height - text_size.Height) / 2);
						int brush_y0 = (int)(y0 + font_info.InternalLeadingPixels) + (int)font_info.InternalLeadingPixels;
						int brush_y1 = (int)(y0 + font_info.AscentPixels) + 5;
						using (LinearGradientBrush rainbowbrush = new LinearGradientBrush(new Point(x0, brush_y0), new Point(x0, brush_y1), Color.Red, Color.Violet))
						{
							Color[] colors = new Color[]
							{
								Color.FromArgb(255, 0, 0),
								Color.FromArgb(255, 0, 0),
								Color.FromArgb(255, 128, 0),
								Color.FromArgb(255, 255, 0),
								Color.FromArgb(0, 255, 0),
								Color.FromArgb(0, 255, 128),
								Color.FromArgb(0, 255, 255),
								Color.FromArgb(0, 128, 255),
								Color.FromArgb(0, 0, 255),
								Color.FromArgb(0, 0, 255),
							};
							int num_colors = colors.Length;
							float[] blend_positions = new float[num_colors];
							for (int i = 0; i < num_colors; i++)
								blend_positions[i] = i / (num_colors - 1f);
							ColorBlend color_blend = new ColorBlend
							{
								Colors = colors,
								Positions = blend_positions
							};
							rainbowbrush.InterpolationColors = color_blend;

							// Draw the text.
							txt = txt.Replace("[RGB]", "");
							e.Graphics.DrawString(txt, the_font, rainbowbrush, e.Bounds, centered);
						}
					}
				else if (cmd is CmdBlank2 && txt.StartsWith("["))
				{
					try
					{
						string[] rgbarray = txt.Replace("[", "").Split(']')[0].Split(',');
						SolidBrush b2b = new SolidBrush(Color.Black);
						if (rgbarray.Length == 3)
							b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2])));
						else if (rgbarray.Length == 4)
							b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2]), int.Parse(rgbarray[3])));
						txt = Regex.Replace(txt, @"\[.*?\]", "");
						if (txt.Contains("(TROLL)"))
							e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, b2b, e.Bounds, StringFormat.GenericDefault);
						else
							e.Graphics.DrawString(txt, cmdFont, b2b, e.Bounds, centered);
					}
					catch { }
				}
				else if (cmd is CmdBlank3)
				{
					var jsonObj = JsonConvert.DeserializeObject<CmdBlank3>(JsonConvert.SerializeObject(cmd));
					try
					{
						SolidBrush colorBrush = new SolidBrush(jsonObj.Argb());
						e.Graphics.DrawString(txt, cmdFont, colorBrush, e.Bounds, centered);
					}
					catch { }
				}
				else if (txt.Contains("(TROLL)"))
					e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
				return;
			}

			if (!LabelLess.Contains(scmd))
			{
				//Draw Index
				//Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", this.Font, indexcolor, region, new StringFormat(StringFormatFlags.DirectionRightToLeft));
				Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", font, indexcolor, region, StringFormat.GenericDefault);
				// Adjust the region we wish to print with a +3 offset.
				region = new RectangleF(region.X + first.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
			}

			// Draw the second string (rest of the string, in this case Command type).
			string cmdText = lstCommands.Items[e.Index].ToString();
			string[] toDraw;
			if (cmdText.Contains(':'))
			{
				toDraw = lstCommands.Items[e.Index].ToString().Split(':');
				Region second = DrawString(e.Graphics, toDraw[0] + ": ", font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
				region = new RectangleF(region.X + second.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
				if (toDraw[1].Contains(","))
				{
					toDraw = toDraw[1].Split(',');
					Region third = DrawString(e.Graphics, toDraw[0], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
					for (int i = 1; i < toDraw.Length; i++)
					{
						region = new RectangleF(region.X + third.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
						third = DrawString(e.Graphics, toDraw[i], font, eVarColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
					}
				}
				else
					DrawString(e.Graphics, toDraw[1], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
			}
			else
				DrawString(e.Graphics, cmdText, font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);

		}

		private Region DrawString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
			format.Alignment = StringAlignment.Near;
			g.DrawString(s, font, brush, layoutRectangle, format);
			return g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
		}

		private void multilineToggleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MultiMode();
		}

		private void toggleTabpagesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TogglePages();
		}

		private void commandColorsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Root.Instance.ShowForm(CommandColorForm.Instance);
		}

		private void btnChangeCmd_Click(object sender, EventArgs e)
		{
			IBotCommand cmd;
			switch (((Button)sender).Name)
			{
				case "btnChangeGuildCmd":
					cmd = new CmdChange
					{
						Guild = true,
						Text = txtGuild.Text
					};
					break;
				default:
					cmd = new CmdChange
					{
						Guild = false,
						Text = txtUsername.Text
					};
					break;
			}
			AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnClientPacket_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdPacket
			{
				Packet = txtPacket.Text,
				SpamTimes = Decimal.ToInt32(numSpamTimes.Value),
				ForClient = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnSetInt_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdInt
			{
				Int = txtSetInt.Text,
				Value = (int)numSetInt.Value,
				type = CmdInt.Types.Set
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnIncreaseInt_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdInt
			{
				Int = txtSetInt.Text,
				Value = (int)numSetInt.Value,
				type = CmdInt.Types.Upper
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnDecreaseInt_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdInt
			{
				Int = txtSetInt.Text,
				Value = (int)numSetInt.Value,
				type = CmdInt.Types.Lower
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void lstCommands_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void btnWhitelistOn_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdWhitelist
			{
				state = CmdWhitelist.State.On
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnWhitelistOff_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdWhitelist
			{
				state = CmdWhitelist.State.Off
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnWhitelistToggle_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdWhitelist
			{
				state = CmdWhitelist.State.Toggle
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		public int LastIndexedSearch = 0;
		public string SKeyword = "";
		public List<int> Filtered = new List<int>();

		private void btnSearchCmd_Click(object sender, EventArgs e)
		{
			string Keyword = this.txtSearchCmd.Text;
			ListBox.ObjectCollection lists = lstCommands.Items;

			if (Keyword != SKeyword)
			{
				LastIndexedSearch = 0;
				SKeyword = "";
				Filtered.Clear();
			}

			for (int i = 0; i < lists.Count; i++)
			{
				if (Keyword == SKeyword) 
					break;
				if (lists[i].ToString().IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
					Filtered.Add(i);
			}

			SKeyword = Keyword;
			if (Filtered.Count > 0)
			{
				lstCommands.SelectedIndex = -1;
				lstCommands.SelectedIndex = Filtered[LastIndexedSearch];
				LastIndexedSearch++;
				if (LastIndexedSearch >= Filtered.Count)
					LastIndexedSearch = 0;
			}
		}

		private void btnReturnCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdReturn(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnClearTempVar_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdClearTemp(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{
			try
			{
				rtbInfo.Rtf = richTextBox2.Text;
				return;

				var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
				var tempDoc = $@"{currDir}/tempdoc";
				var target = $@"{currDir}/temprtf";
				if (File.Exists(tempDoc))
				{
					File.Delete(tempDoc);
				}
				using (FileStream newFile = File.Create(tempDoc))
				{
					byte[] toWrite = new UTF8Encoding(true).GetBytes(richTextBox2.Text);
					newFile.Write(toWrite, 0, toWrite.Length);
				}
				var tempRtf = "";
				Task.WaitAll(Task.Run(() => DocConvert.toRtf(tempDoc, target, out tempRtf)));
				rtbInfo.Rtf = File.ReadAllText(tempRtf);
			}
			catch { };
		}

		private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		private async void btnSetLevel_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(tbLevel.Text, out _)) return;

			string packet =
				"{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"2000\",\"intLevel\":" + tbLevel.Text + "}}}";
			await Proxy.Instance.SendToClient(packet);
		}

		private void btnSetLevelCmd_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(tbLevel.Text, out _)) return;

			string packet = "Level " + tbLevel.Text;
			this.AddCommand(new CmdPacket
			{
				Packet = packet,
				SpamTimes = 1,
				ForClient = true
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddCmdHunt_Click(object sender, EventArgs e)
		{
			if (tbItemNameF.Text.Length > 0 && tbItemQtyF.Text.Length > 0)
			{
				string monster = string.IsNullOrEmpty(this.tbMonNameF.Text) ? "*" : this.tbMonNameF.Text;
				string monId = "";
				if (monster.Split(' ')[0].Equals("/id"))
				{
					monId = monster.Split(' ')[1];
				}
				bool flag = this.tbMonNameF.Text == "Monster (* = random)" || string.IsNullOrEmpty(tbMonNameF.Text);
				if (flag) monster = "*";
				string itemName = this.tbItemNameF.Text;
				string itemQty = this.tbItemQtyF.Text;

				if (chkAddToWhitelistF.Checked)
				{
					if (tbItemNameF.Text.Length <= 0) return;
					string[] items = tbItemNameF.Text.Split(new char[] {
						','
					});

					foreach (string item in items)
					{
						if (!lstDrops.Items.Cast<string>().ToList().Any((string d) => d.Equals(item, StringComparison.OrdinalIgnoreCase)))
							lstDrops.Items.Add(item);
					}
				}
                if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
					this.AddCommand(new CmdQuestHunt
					{
						Map = tbMapF.Text,
						Cell = tbCellF.Text,
						Pad = tbPadF.Text,
						ItemName = itemName.Trim(),
						Quantity = itemQty.Trim(),
						Monster = monster.Trim()
					}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
					return;
                }

                CmdShortHunt cmd = new CmdShortHunt
				{
					Map = tbMapF.Text,
					Cell = tbCellF.Text,
					Pad = tbPadF.Text,
					ItemType = (chkIsTempF.Checked ? ItemType.TempItems : ItemType.Items),
					Monster = monster.Trim(),
					ItemName = itemName.Trim(),
					Quantity = itemQty.Trim(),
					BlankFirst = cbBlankFirstF.Checked
				};

				this.AddCommand(cmd, (Control.ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnGetMapF_Click(object sender, EventArgs e)
		{
			tbMapF.Text = Player.Map;
			tbCellF.Text = Player.Cell;
			tbPadF.Text = Player.Pad;
		}

		private void btnAllSkill_Click(object sender, EventArgs e)
		{
			for (int i = 1; i <= 4; i++)
			{
				AddSkill(new Skill
				{
					Text = Skill.GetSkillName(i.ToString()),
					Index = i.ToString(),
					Type = Skill.SkillType.Normal
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnQAddToWhitelist_Click(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn) return;
			int idQuest = Decimal.ToInt32(numQQuestId.Value);
			Player.Quests.Load(idQuest);

			if (chkQRewards.Checked)
			{
				List<String> items = Grabber.GetQuestRewards(idQuest);
				foreach (String item in items)
				{
					if (item.Length > 0)
						lstDrops.Items.Add(item);
				}
			}

			if (chkQRequirements.Checked)
			{
				List<String> items = Grabber.GetQuestRequirment(idQuest);
				foreach (String item in items)
				{
					if (item.Length > 0)
						lstDrops.Items.Add(item);
				}
			}
		}

		private void btnAddWarnMsg_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdClientMessage
			{
				Messages = inputMsgClient.Text,
				IsWarning = true
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddInfoMsg_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdClientMessage
			{
				Messages = inputMsgClient.Text,
				IsWarning = false
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnSetFPS_Click(object sender, EventArgs e)
		{
			Flash.Call("SetFPS", (int)numSetFPS.Value);
		}

		private void btnSetFPSCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdSetFPS
			{
				FPS = (int)numSetFPS.Value
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnBSStart_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdBackgroundPacket
			{
				ActionW = CmdBackgroundPacket.Action.ADD,
				Label = tbBSLabel.Text,
				Packet = tbBSPacket.Text,
				Delay = (int)numBSDelay.Value
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnBSStop_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdBackgroundPacket
			{
				ActionW = CmdBackgroundPacket.Action.STOP,
				Label = tbBSLabel.Text,
				Packet = "-LEAVE ME BLANK-",
				Delay = 6969
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private System.Timers.Timer SaveProgressTimer = new System.Timers.Timer();
		private void chkSaveProgress_CheckedChanged(object sender, EventArgs e)
		{
			numSaveProgress.Enabled = !chkSaveProgress.Checked;
			if (chkSaveProgress.Checked)
			{
				SaveProgressTimer.Interval = 1000 * 60 * (int)numSaveProgress.Value;
				SaveProgressTimer.Elapsed += OnTimedEvent;
				SaveProgressTimer.Start();
				Console.WriteLine($"SaveProgress: Timer start");
			}
			else
			{
				SaveProgressTimer.Elapsed -= OnTimedEvent;
				SaveProgressTimer.Stop();
				Console.WriteLine("SaveProgress: Stoped");
			}
		}

		private void chkSaveProgress_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkSaveProgress, "Just logout every...");
		}

		private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			if (Player.IsLoggedIn)
			{
				Player.Logout();
				LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Progress saved.");
			}
		}

		private async void btnSetSpawn2_Click(object sender, EventArgs e)
		{
			btnSetSpawn2.Enabled = false;
			Player.SetSpawnPoint();
			string Messages = $"Spawnpoint set: {Player.Cell}, {Player.Pad}";
			await Proxy.Instance.SendToClient($"%xt%server%-1%{Messages}%");
			await Task.Delay(500);
			btnSetSpawn2.Enabled = true;
		}

		private void btnFollowCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdFollow
			{
				PlayerName = tbFollowPlayer.Text.ToLower(),
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void toggleAntiMod(bool enable)
		{
			if (enable)
			{
				Flash.FlashCall += AntiMODHandler;
				chkHidePlayers.Checked = false;
			}
			else
			{
				Flash.FlashCall -= AntiMODHandler;
			}
			chkAMLogout.Enabled = !chkAntiMod.Checked;
			chkAMStopBot.Enabled = !chkAntiMod.Checked;
		}

		private void chkAntiMod_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiMod, "Auto stop and logout when you see any staff/mod.");
		}

		private void btnAMTest_Click(object sender, EventArgs e)
		{
			showLog("MOD Username", "60");
			modAction();
		}

		private void AntiMODHandler(AxShockwaveFlashObjects.AxShockwaveFlash flash, string function, params object[] args)
		{
			string msg = args[0].ToString();
			if (!msg.StartsWith("{")) return;
			dynamic packet = JsonConvert.DeserializeObject<dynamic>(msg);
			dynamic data = packet["params"].dataObj;
			if (packet["params"].type == "json")
			{
				if (data.cmd == "initUserDatas")
				{
					JArray a = (JArray)data.a;
					if (a != null)
						foreach (JObject player in a)
						{
							JObject playerData = (JObject)player["data"];
							showLog(playerData["strUsername"]?.ToString(), playerData["intAccessLevel"]?.ToString());
						}
				}

				if (data.cmd == "uotls")
				{
					string unm = data.unm;
					if (unm != null) showLog(unm, Player.GetAccessLevel(unm));
				}
			}
		}

		private void showLog(string username, string accessLevel)
		{
			int _accessLevel = int.Parse(accessLevel);
			string color, log = "";
			switch (_accessLevel)
			{
				case 100:
				case 30:
					color = "Dark Green";
					break;
				case 40:
					color = "Light Green";
					break;
				case 50:
					color = "Purple";
					break;
				case 60:
					color = "Yellow";
					break;
				default:
					color = "Unknown";
					break;
			}

			if (_accessLevel >= 30)
			{
				string text = $"==> [STAFF  | {_accessLevel}/{color} | {username}] <==";
				string strip = "";
				for (int i = 0; i < text.Length; i++)
					strip += "-";
				log += $"{strip} {Environment.NewLine} {text} {Environment.NewLine} {strip} {Environment.NewLine}";
				modAction();
				LogForm.Instance.AppendDebug(log);
			}
		}

		private void modAction()
		{
			if (chkAMStopBot.Checked)
				chkEnable.Checked = false;
			if (chkAMLogout.Checked)
				Player.Logout();
		}

		private void chkAntiCounter_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiCounter, "Auto cancel target when monster countering attack.");
		}
		
		private void chkAntiCounter_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDisableAnims.Checked && chkAntiCounter.Checked)
				chkDisableAnims.Checked = false;
			if (chkAntiCounter.Checked)
			{
				Proxy.Instance.ReceivedFromServer += CapturePlayerAura;
			}
			else
			{
				Proxy.Instance.ReceivedFromServer -= CapturePlayerAura;
			}
		}

		private void CapturePlayerAura(Networking.Message message)
		{
			string msg = message.ToString();

			try
			{
				//"cmd":"aura++","auras":[{"nam":"Counter Attack"
				//prepares a counter attack!!
				string c1 = "\"cmd\":\"aura++\",\"auras\":[{\"nam\":\"Counter Attack\"";
				string c2 = "prepares a counter attack";
				if (msg.Contains(c2))
				{
					Player.CancelAutoAttack();
					Player.CancelTarget();
					if (chkEnable.Checked)
					{
						ActiveBotEngine.Configuration.SkipAttack = true;
					}
					Console.WriteLine("Counter Attack: active");
				}

				//{"t":"xt","b":{"r":-1,"o":{"cmd":"ct","a":[{"cmd":"aura-","aura":{"nam":"Counter Attack"},"tInf":"m:7"}]}}}
				if (msg.Contains("\"cmd\":\"aura-\",\"aura\":{\"nam\":\"Counter Attack\""))
				{
					Console.WriteLine("Counter Attack: fades");
					ActiveBotEngine.Configuration.SkipAttack = false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("err: " + e.Message);
			}
		}

		private HandlerFollow HandlerFollow = new HandlerFollow();
		private async Task setFollowHandler()
		{
			if (chkFollowOnly.Checked && chkEnableSettings.Checked)
			{
				string PlayerName = tbFollowPlayer2.Text.ToLower();
				string[] safeCell = ClientConfig.GetValue(ClientConfig.C_SAFE_CELL).Split(',');
				Proxy.Instance.RegisterHandler(HandlerFollow);
				while (chkFollowOnly.Checked && chkEnableSettings.Checked)
				{
					if (Player.IsLoggedIn)
					{
						List<string> mapPlayers = World.PlayersInMap.ConvertAll<string>(a => a.ToLower());
						if (!mapPlayers.Contains(PlayerName))
						{
							Player.CancelAutoAttack();
							Player.CancelTarget();
							Player.MoveToCell(safeCell[0], safeCell[1]);
							await ActiveBotEngine.WaitUntil(() => Player.CurrentState != Player.State.InCombat);
							await Task.Delay(1000);
							Player.GoToPlayer(PlayerName);
						}
					}
					await Task.Delay(2000);
				}
			}
			else
			{
				Proxy.Instance.UnregisterHandler(HandlerFollow);
			}
		}

		private async void chkEnableSettings_CheckedChanged(object sender, EventArgs e)
		{
			Root.Instance.enableOptionsToolStripMenuItem.Checked = chkEnableSettings.Checked;
			tbFollowPlayer2.Enabled = !chkEnableSettings.Checked;
			chkFollowOnly.Enabled = !chkEnableSettings.Checked;
			await setFollowHandler();
		}

		private void chkUseSkillTargeted_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiMod, "Use skill when player has target only.");
		}

		private void btnAddSkillSet_Click(object sender, EventArgs e)
		{
			if (txtSkillSet.TextLength > 0)
			{
				AddSkill(new Skill
				{
					Text = "[" + txtSkillSet.Text.ToUpper() + "]",
					Type = Skill.SkillType.Label
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnUseSkillSet_Click(object sender, EventArgs e)
		{
			if (txtSkillSet.TextLength > 0)
			{
				AddCommand(new CmdSkillSet
				{
					Name = txtSkillSet.Text.ToUpper()
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void chkReloginCompleteQuest_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkReloginCompleteQuest, "Automatic logout when a quest take 5 times to complete.");
		}

		private void btnReloadMap_Click(object sender, EventArgs e)
		{
			World.ReloadMap();
		}

		private void chkSpecial_CheckedChanged(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn)
			{
				if (chkSpecial.Checked) chkSpecial.Checked = false;
				return;
			}
			cmbSpecials.Enabled = !chkSpecial.Checked;
			if (chkSpecial.Checked)
			{
				switch (cmbSpecials.SelectedItem.ToString())
				{
					case "Auto Zone - Ultradage":
						SpecialJsonHandler = new HandlerAutoZoneUltraDage();
						break;
					case "Auto Zone - Dark Carnax":
						SpecialJsonHandler = new HandlerAutoZoneDarkCarnax();
						break;
					case "Auto Zone - Astral Empyrean":
						SpecialJsonHandler = new HandlerAutoZoneAstralEmpyrean();
						break;
					case "Auto Zone - Queen Iona":
						SpecialJsonHandler = new HandlerAutoZoneQueenIona();
						break;
					case "Auto Zone - Colossal Vordred":
						SpecialJsonHandler = new HandlerAutoZoneVordred();
						break;
				}
			} 
			else
			{
				SpecialJsonHandler = null;
				SpecialXtHandler = null;
			}
		}

		private async void chkGender_CheckedChanged(object sender, EventArgs e)
		{
			int userId = Flash.Call<int>("UserID", new string[0]);
			string gender = Flash.Call<string>("Gender", new string[0]);
			gender = (gender.ToUpper() == "M") ? "F" : "M";
			string packet = $"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"genderSwap\",\"bitSuccess\":1,\"gender\":\"{gender}\",\"intCoins\":0,\"uid\":\"{userId}\",\"strHairFileName\":\"\",\"HairID\":\"\",\"strHairName\":\"\"}}}}}}";
			await Proxy.Instance.SendToClient(packet);
		}

		private void btnSetBotsDir_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog openFolderDialog = new FolderBrowserDialog())
			{
				openFolderDialog.Description = "Choose your bots directory";
				openFolderDialog.ShowNewFolderButton = true;
				openFolderDialog.SelectedPath = txtSaved.Text;
				if (openFolderDialog.ShowDialog() == DialogResult.OK)
				{
					txtSaved.Text = openFolderDialog.SelectedPath;
				}
			}
			ClientConfig.SetValue(ClientConfig.C_BOTS_DIR, txtSaved.Text);
		}

		private void btnHideLoading_Click(object sender, EventArgs e)
		{
			Flash.Call2("HideConnMC", new object[0]);
		}

		private void chkQuestListItem_CheckedChanged(object sender, EventArgs e)
		{
			numQuestListItem.Enabled = chkQuestListItem.Checked;
		}

		private void btnAddQuestList_Click(object sender, EventArgs e)
		{
			string itemId = null;
			if (chkQuestListItem.Checked)
			{
				itemId = numQuestListItem.Value.ToString();
			}
			AddCommand(new CmdAddQuestList
			{
				Id = (int)numQuestListID.Value,
				ItemId = itemId,
				SafeRelogin = chkReloginCompleteQuest.Checked
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnRemoveQuestList_Click(object sender, EventArgs e)
		{
			string itemId = null;
			if (chkQuestListItem.Checked)
			{
				itemId = numQuestListItem.Value.ToString();
			}
			AddCommand(new CmdRemoveQuestList
			{
				Id = (int)numQuestListID.Value,
				ItemId = itemId,
				SafeRelogin = chkReloginCompleteQuest.Checked
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		int clickCounter = 0;
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			clickCounter++;
			if (clickCounter >= 3)
			{
				Root.Instance.ShowForm(new Egg());
				clickCounter = 0;
			}
		}

		private void btnStopAttack_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdCancelTarget(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnLeaveCombat_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdCancelTarget
			{
				LeaveCombat = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private async void btnLoadMap_Click(object sender, EventArgs e)
		{
			if (!tbLoadMap.Text.Contains(".swf") || tbLoadMap.Text.Contains("(.swf)")) return;
			btnLoadMap.Enabled = false;
			World.LoadMap(tbLoadMap.Text);
			await Task.Delay(1000);
			btnLoadMap.Enabled = true;
		}
	}
}