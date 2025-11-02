using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Game;
using Grimoire.Networking;
using DarkUI.Forms;
using Grimoire.Tools;
using MaidRemake.LockedMapHandle;
using MaidRemake.WhitelistMap;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Grimoire.UI;
using Grimoire.Tools.Maid;
using Grimoire.Networking.Handlers.Maid;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Interop;
using System.Security.Cryptography;
using Grimoire.Game.Data;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Grimoire.UI.Maid
{
    public partial class MaidRemake : DarkForm
    {
        public static MaidRemake Instance { get; } = new MaidRemake();

        public string targetUsername => MaidRemake.Instance.cmbGotoUsername.Text.ToLower();

        public bool isPlayerInMyCell => bool.Parse(Flash.Call<string>("GetCellPlayers", new string[] { targetUsername }) ?? "False");

        public bool isPlayerInMyRoom => IsPlayerInMap(targetUsername);

        public int skillDelay => (int)MaidRemake.Instance.numSkillDelay.Value;

        LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();

        public CellJumperHandler CJHandler { get; } = new CellJumperHandler();

        public JoinMapHandler JoinMapHandler { get; } = new JoinMapHandler();

        public WarningMsgHandler RedMsgHandler { get; } = new WarningMsgHandler();

        public CopyWalkHandler CopyWalkHandler { get; } = new CopyWalkHandler();

        public PartyChatHandler PartyChatHandler { get; } = new PartyChatHandler();

        public PartyInvitationHandler PartyInvitationHandler { get; } = new PartyInvitationHandler();

        private int healthPercent => (int)MaidRemake.Instance.numHealthPercent.Value;

        string[] buffSkill = null;
        int buffIndex = 0;

        string[] healSkill = null;
        int healIndex = 0;

        string[] monsterList = null;

        bool onPause = false;

        bool forceSkill = false;

        Stopwatch stopwatch = new Stopwatch();

        public MaidRemake()
        {
            InitializeComponent();

            KeyPreview = true;

            //KeyListener non Global Hook
            //this.KeyDown += new KeyEventHandler(this.hotkey);
            if (Player.IsLoggedIn) cmbGotoUsername.Text = Player.Username;
            cmbUltraBoss.SelectedIndex = 0;
            this.Text = $"Maid Remake";

			ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.cbPartyCmd,
                "[Auto accept any party invitation when checked]" +
                "\n\nEnter /p party chat's to use the commands below" +
                "\n.join {mapname-room}" +
                "\n.acc {questIds}" +
                "\n.turnin {questIds}" +
                "\n.target {playername} => change Maid's master target" +
                "\n.start => turn on Maid" +
                "\n.stop => turn off Maid"
                );
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private async void cbEnablePlugin_CheckedChanged(object sender, EventArgs e)
        {
            resetSpecials();
            if (cbEnablePlugin.Checked)
            {
                startUI();

                int gotoTry = 0;
                string equippedclass = Player.EquippedClass.ToLower();
                var skillproperties = Flash.Instance.GetGameObject<List<JObject>>("world.actions.active");

                string[] skillList = tbSkillList.Text.Split(',');
                int skillIndex = 0;

                if (cbHandleLockedMap.Checked && AlternativeMap.Count() > 0)
                    AlternativeMap.Init();
                else if (cbHandleLockedMap.Checked)
                    cbHandleLockedMap.Checked = false;

                Proxy.Instance.RegisterHandler(RedMsgHandler);

                Proxy.Instance.RegisterHandler(JoinMapHandler);

                if (!cbUnfollow.Checked)
                    Proxy.Instance.RegisterHandler(CJHandler);

                if (cbCopyWalk.Checked)
                    Proxy.Instance.RegisterHandler(CopyWalkHandler);

                if (cbSpecialAnims.Checked)
                    Flash.FlashCall2 += AnimsMsgHandler;

                if (cbAntiCounter.Checked)
					Flash.FlashCall2 += AntiCounterHandler;

                if (cbPartyCmd.Checked)
                {
					Proxy.Instance.RegisterHandler(PartyInvitationHandler);
					Proxy.Instance.RegisterHandler(PartyChatHandler);
				}

				if (!cbUnfollow.Checked && Player.IsLoggedIn && !World.IsMapLoading && isPlayerInMyRoom && !isPlayerInMyCell)
                    Player.GoToPlayer(targetUsername);

                if (cbAttackPriority.Checked)
                    monsterList = tbAttPriority.Text.Split(',');

                if (cbUseHeal.Checked)
                    healSkill = tbHealSkill.Text.Split(',');

                if (cbBuffIfStop.Checked)
                {
                    buffSkill = tbBuffSkill.Text.Split(',');
                    buffIndex = 0;
                }

                // auto equip Scroll of Enrage
                if (cmbUltraBoss.SelectedItem.ToString() != "None")
                {
                    equipEnrage();
                }

                while (cbEnablePlugin.Checked)
                {
                    try
                    {
                        // while player is logout -> do delay (2s), wait first join, do first join delay
                        if (cbEnablePlugin.Checked && !Player.IsLoggedIn)
                            await waitForFirstJoin();

                        // plugin disabled
                        if (!cbEnablePlugin.Checked)
                            return;

                        // starting the plugin
                        if ((isPlayerInMyRoom || cbUnfollow.Checked) && Player.IsLoggedIn && !World.IsMapLoading && !onPause)
                        {
                            gotoTry = 0;

                            if (!Player.IsAlive)
                            {
                                skillIndex = 0;
                                World.SetSpawnPoint();
                                await Task.Delay(500);
                                forceSkill = false;
                                continue;
                            }

                            if (cbUseHeal.Checked && tbHealSkill.Text != String.Empty && isHealthUnder(healthPercent))
                            {
                                useSkill(healSkill[healIndex]);
                                //Player.UseSkill(healSkill[healIndex]);
                                healIndex++;

                                if (healIndex >= healSkill.Length)
                                    healIndex = 0;

                                await Task.Delay(skillDelay);
                                continue;
                            }

                            if (cbStopAttack.Checked)
                            {
                                if (Player.HasTarget)
                                {
                                    Player.CancelAutoAttack();
                                    Player.CancelTarget();
                                }

                                if (cbBuffIfStop.Checked && tbBuffSkill.Text != String.Empty)
                                {
                                    useSkill(buffSkill[buffIndex]);
                                    //Player.UseSkill(buffSkill[buffIndex]);
                                    buffIndex++;

                                    if (buffIndex >= buffSkill.Length)
                                        buffIndex = 0;
                                }

                                await Task.Delay(skillDelay);
                                continue;
                            }

                            if (cbAttackPriority.Checked && !forceSkill)
                                doPriorityAttack();

                            // set targetting to availabe monster in current cell
                            if (World.IsMonsterAvailable("*") && !Player.HasTarget)
                                Player.AttackMonster("*");

                            //keep using buff when no enemy target & checking waitskill
                            string selfskill = skillproperties[int.Parse(skillList[skillIndex])]?["tgt"]?.ToString();
                            //debug($"{selfskill}");
                            if (cbWaitSkill.Checked && selfskill != "h") //H or F [H - monster/hostile, F - Friendly/self]
                            {
                                await Task.Delay(Player.SkillAvailable(skillList[skillIndex]));
                                Player.ForceUseSkill(skillList[skillIndex]);
                                await Task.Delay(150);
                                skillIndex = (skillIndex + 1) % skillList.Length;
                            }

                            // waiting for skill CD if 'Wait' skill checked
                            if (cbWaitSkill.Checked && (Player.SkillAvailable(skillList[skillIndex]) > 0 || !Player.HasTarget))
                            {
                                await Task.Delay(150);
                                continue;
                            }

                            // do attack with skills
                            if (Player.HasTarget)
							{
								//general loop taunt
								DoLoopTaunt();

								//For class with some aura detection
								await SpecialCombo();
                                if (tauntTask == null)
                                    taunt();
                                // force, to ensure a skill is REALLY executed 
                                if (forceSkill)
                                {
                                    string skillAct = numSkillAct.Value.ToString();
                                    await Task.Delay(1000);
                                    await Task.Delay(Player.SkillAvailable(skillAct));
                                    //Player.UseSkill(skillAct);
                                    useSkill(skillAct);
                                    forceSkill = false;
                                    await Task.Delay(500);
                                }
                                else
                                {   // normal skill spamming
                                    // Previously Player.UseSkill(skillList[skillIndex]); 
                                    useSkill(skillList[skillIndex]);
                                }
                            }

                            skillIndex++;

                            if (skillIndex >= skillList.Length)
                                skillIndex = 0;
                        }
                        else if (Player.IsLoggedIn && !World.IsMapLoading)
                        {
                            gotoTarget(targetUsername);
                            if (cbStopIf.Checked)
                            {
                                gotoTry++;
                                if (gotoTry >= 5)
                                {
                                    gotoTry = 0;
                                    stopMaid();
                                }
                            }

                            // wait loading screen before try to goto again (max: 5100 ms)
                            for (int i = 0; i < 36 && cbEnablePlugin.Checked && Player.IsLoggedIn && !World.IsMapLoading; i++)
                                await Task.Delay(150);

                            // wait map loading end
                            while (cbEnablePlugin.Checked && Player.IsLoggedIn && World.IsMapLoading)
                                await Task.Delay(500);

                            // wait 2 second before try to goto or join to different map (when locked map handler is enabled)
                            for (int i = 0; i < 8 && cbEnablePlugin.Checked && cbHandleLockedMap.Checked && Player.IsLoggedIn && !World.IsMapLoading; i++)
                                await Task.Delay(250);

                            // goto target current cell when in the same room
                            while (cbEnablePlugin.Checked && Player.IsLoggedIn && isPlayerInMyRoom && !isPlayerInMyCell)
                            {
                                Player.GoToPlayer(targetUsername);
                                if (cbEnablePlugin.Checked && Player.IsLoggedIn && isPlayerInMyRoom && !isPlayerInMyCell)
                                    await Task.Delay(1000);
                                else break;
                            }
                        }

                        await Task.Delay(skillDelay);
                    }
                    catch { }
                }
            }
            else
            {
                stopMaid();
            }
		}

		private void DoLoopTaunt()
		{
			// ultra gramiel
			if (Player.Map == "ultragramiel")
			{
				if (World.IsMonsterAvailable("Grace Crystal")) return;
				if (Player.GetAuras(false, "Focus") < 1 &&
					Player.GetAuras(true, "Vendetta") < 1 &&
					Player.SkillAvailable("5") == 0)
				{
					Player.UseSkill("5");
				}
			}
		}

		private void useSkill(string skillIndex)
        {
            if (isUsingCSH())
            {
                Player.ForceUseSkill(skillIndex);
                return;
            }
            Player.UseSkill(skillIndex);
        }

        private void equipEnrage()
        {
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals("Scroll of Enrage") && i.IsEquippable);
            Player.EquipPotion(item.Id, item.Description, item.File, item.Name);
            Task.Delay(1000);
        }

        private void taunt()
        {
            if (tbSpecialMsg.Text.StartsWith("tc;", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string[] parts = tbSpecialMsg.Text.Split(';');

                    // Default value
                    int cycle = 2;
                    string mon = "*";
                    int second = 12;
                    int order = -1;

                    if (parts.Length > 1 && !int.TryParse(parts[1], out cycle))
                        throw new Exception("Cycle harus berupa angka!");

                    if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]))
                        mon = parts[2];

                    if (parts.Length > 3 && !int.TryParse(parts[3], out second))
                        throw new Exception("Second harus berupa angka!");
                    if (parts.Length > 4 && !int.TryParse(parts[4], out order))
                        order -= 1;

                    if (tauntTask == null && World.IsMonsterAvailable(mon))
                    {
                        ResetToken(true);
                        tauntTask = Task.Run(() =>
                            cycleTaunt(cycle, mon, second, order)
                        );
                    }
                }
                catch (Exception ex)
                {
                    Task.Run(() =>
                    MessageBox.Show(
                        $"Wrong format:\n{ex.Message}" +
                        $"\n\nExample: tc;<cycle>;<monster>;<second>" +
                        $"\nHere, i'll fix that for u",
                        "TauntCycle Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    )
                    );
                    Task.Delay(2000);
                    stopMaid();
                    Task.Delay(200);
                    Player.MoveToCell(Player.Cell,Player.Pad);
                    tbSpecialMsg.Text = $"tc;2;*;14;<order 1-4>(optional)";
                }
            }
        }

        private bool isUsingCSH()
        {
            return Player.EquippedClass.Contains("CHRONO SHADOW");
        }

        private InventoryItem potion;

        private string msgTemp;
        private async Task SpecialCombo()
        {
            potion = Player.Inventory.Items.FirstOrDefault((InventoryItem i)
                => i.IsEquipped && i.Name.Equals("Potent Honor Potion") || i.Name.Equals("Potent Malice Potion"));
            if (potion != null)
            {
                if (potion.IsEquipped && potion.Quantity >= 1 && Player.GetAuras(true, "Potent Honor Malice") == 0)
                {
                    useSkill("5");
                    await Task.Delay(200);
                    return;
                }
            }
            if (Player.Map == "voidxyfrag" && Player.EquippedClass == "LEGION REVENANT")
            {
                if (!string.IsNullOrWhiteSpace(msgTemp))
                    return;
                //Save and set specialMsg
                msgTemp = tbSpecialMsg.Text;
                tbSpecialMsg.Text = "bleeee";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(msgTemp))
                {
                    tbSpecialMsg.Text = msgTemp;
                    msgTemp = string.Empty; // Reset setelah keluar map
                }
            }

            if (isUsingCSH() && cmbPreset.SelectedItem.ToString() == "CSH")
            {
                if (Player.GetAuras(true, "Rounds Empty") == 1 || Player.Mana < 15)
                {
                    useSkill("4");
                    await Task.Delay(Player.SkillAvailable("1"));
                    await Task.Delay(100);
                    useSkill("1");
                    await Task.Delay(200);
                }
            }
            else if (Player.EquippedClass == "ARCANA INVOKER")
            {
                if (Player.GetAuras(true, "XX - Judgement") == 1 ||
                    Player.GetAuras(true, "End of the world") >= 13 ||
                    Player.GetAuras(true, "XXI - The World") == 0 && Player.GetAuras(true, "0 - The Fool") == 0)
                {
                    useSkill("1");
                    await Task.Delay(200);
                }
            }
            else if (Player.EquippedClass == "ARCHMAGE")
            {
                if (Player.GetAuras(true, "Arcane Flux") == 1 &&
                    Player.GetAuras(true, "Corporeal Ascension") == 0 ||
                    Player.GetAuras(true, "Arcane Sigil") == 0)
                {
                    useSkill("4");
                    await Task.Delay(200);
                }
            }

            // DOESNT WORK PROPERLY
            // ultra gramiel
            /*if (Player.Map == "ultragramiel")
            {
                if (World.IsMonsterAvailable("Grace Crystal"))
                    return;
                if (tauntTask == null)
                {
                    ResetToken(true);
                    tauntTask = Task.Run(() => cycleTaunt(4, "Gramiel", 20));
                } else if (!World.IsMonsterAvailable("Gramiel"))
                {
                    ResetToken(false);
                }
            }*/
        }
        private Task tauntTask = null;
        private CancellationTokenSource cts;
        private void ResetToken(bool createNew)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                tauntTask = null;
                cts = null;
            }
            if (createNew)
                cts = new CancellationTokenSource();
        }
        private async Task cycleTaunt(int cycle = 2, string mon = "*", int second = 12, int count = -1)
        {
            if (count == -1)
            {
                count = cmbGotoUsername.Items.IndexOf(Player.Username.ToLower());
            }
            if (count > cycle)
            {
                count %= cycle;
            }
            debug($"Executing tauntcycle with Cycle = {cycle}, Every {second}s, initial taunt at {(count+1)*second}s");
            while (World.IsMonsterAvailable(mon) && !cts.IsCancellationRequested)
            {
                if (count <= 0)
                {
                    debug($"Count = {count} forcing to taunt");
                    count = cycle;
                    forceSkill = true;
                    Player.AttackMonster(mon);
                }
                else
                    debug($"Count = {count}");
                count--;
                await Task.Delay(second / cycle * 1000);
            }
            debug($"Monster no longer detected, stopping taunt cycle");
            ResetToken(false);
        }

        private Grimoire.Networking.Message CreateMessage(string raw)
        {
            if (raw != null && raw.Length > 0)
            {
                switch (raw.Trim()[0])
                {
                    case '%':
                        return new XtMessage(raw);
                    case '<':
                        return new XmlMessage(raw);
                    case '{':
                        return new JsonMessage(raw);
                }
            }

            return null;
        }


        private void AnimsMsgHandler(string function, params object[] args)
        {
            if (function != "packetFromServer") return;
            try
            {
                Networking.Message message = CreateMessage((string)args[0]);
                if (message is JsonMessage)
                {
                    JsonMessage jsonMessage = message as JsonMessage;
                    if (jsonMessage.DataObject?["anims"] != null)
                    {
                        JArray anims = (JArray)jsonMessage.DataObject["anims"];
                        if (anims != null)
                        {
                            //System.Console.WriteLine("anims: " + anims);
                            foreach (JObject anim in anims)
                            {
                                string msg = anim?["msg"]?.ToString()?.ToLower();

                                if (msg != null)
                                {
                                    int monId = 0;

                                    int.TryParse(anim?["tInf"]?.ToString()?.Split(':')[1], out monId);
                                    string[] inputMsg = tbSpecialMsg.Text?.ToLower().Split(',');
                                    foreach (string m in inputMsg)
                                    {
                                        string specialMsg = m.Trim();
                                        if (!string.IsNullOrEmpty(specialMsg))
                                        {
                                            if (msg.Contains(specialMsg) && ultraBossHandler(msg))
                                            {
                                                forceSkill = true;
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                debug($"e: {e}");
            }
        }

        private bool counterAttack = false;

        private void AntiCounterHandler(string function, params object[] args)
        {
            if (function != "packetFromServer") return;
            try
            {
                Grimoire.Networking.Message message = CreateMessage((string)args[0]);
                JsonMessage jsonMessage = message as JsonMessage;
                if (jsonMessage != null)
                {
                    if (jsonMessage.DataObject?["anims"] != null)
                    {
                        JArray anims = (JArray)jsonMessage.DataObject["anims"];
                        if (anims != null)
                        {
                            foreach (JObject anim in anims)
                            {
                                string msg = anim?["msg"]?.ToString()?.ToLower();
                                if (msg != null)
                                {
                                    if (msg.Contains("prepares a counter attack"))
                                    {
                                        //debug("Counter Attack: active");
                                        counterAttack = true;
                                        cbStopAttack.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                    if (jsonMessage.DataObject?["a"] != null)
                    {
                        JArray a = (JArray)jsonMessage.DataObject?["a"];
                        if (a != null)
                        {
                            cbStopAttack.Checked = Player.GetAuras(true, "Sun's Heat") > 0 || counterAttack;
                            foreach (JObject aura in a)
                            {
                                JObject aura2 = (JObject)aura["aura"];
                                if (aura2?["nam"]?.ToString() == "Counter Attack" && aura.GetValue("cmd")?.ToString() == "aura--")
                                {
                                    counterAttack = false;
                                    cbStopAttack.Checked = false;
                                    //debug("Counter Attack: fades");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                debug($"e: {e}");
            }
        }

        private void debug(string text)
        {
            LogForm.Instance.AppendDebug($"[Maid] {text}");
        }

        private async Task waitForFirstJoin()
        {
            // wait player to join the map
            while (cbEnablePlugin.Checked && World.IsMapLoading)
                await Task.Delay(2000);

            // do first join delay
            if (cbEnablePlugin.Checked)
                await Task.Delay((int)numRelogDelay.Value);
        }

        private void doPriorityAttack()
        {
            string currentTarget = Player.GetTargetName();
            for (int i = 0; i < monsterList.Length; i++)
            {
                //if (monsterList[i].Equals(Player.GetTargetName(), StringComparison.OrdinalIgnoreCase))
                if (currentTarget?.IndexOf(monsterList[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    return; //Made special for CSH non autoattack cases
                if (World.IsMonsterAvailable(monsterList[i]))
                {
                    Player.AttackMonster(monsterList[i]);
                    return;
                }
            }
        }

        private bool isPlayerInCombat()
        {
            return (Player.CurrentState == Player.State.InCombat ? true : false);
        }

        private bool IsPlayerInMap(string targetUsername)
        {
            foreach (string player in World.PlayersInMap)
            {
                if (player.ToLower() == targetUsername)
                    return true;
            }
            return false;
        }

        private bool isHealthUnder(int percentage)
        {
            int healthBoundary = Player.HealthMax * percentage / 100;
            return Player.Health <= healthBoundary ? true : false;
        }

        private async void gotoTarget(string targetUsername)
        {
            if (Player.CurrentState != Player.State.Idle)
                Player.MoveToCell("Enter", "Spawn");
            await Task.Delay(500);
            Player.GoToPlayer(targetUsername);
            //await Proxy.Instance.SendToServer($"%xt%zm%cmd%1%goto%{targetUsername}%");
        }

        /* UI state */

        public void startUI()
        {
            cbSpecialAnims.Enabled = false;
            tbSpecialMsg.Enabled = false;
            numSkillAct.Enabled = false;
            cmbGotoUsername.Enabled = false;
            tbSkillList.Enabled = false;
            gbOptions.Enabled = false;
            cbWaitSkill.Enabled = false;
            btnMe.Enabled = false;
            cbCopyWalk.Enabled = false;
            cmbUltraBoss.Enabled = false;
            Root.Instance.maidStrip.Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Underline);
            if (LockedMapForm.Instance.Visible)
            {
                if (LockedMapForm.Instance.WindowState == FormWindowState.Minimized)
                    LockedMapForm.Instance.WindowState = FormWindowState.Normal;
                LockedMapForm.Instance.Hide();
            }
            if (WhitelistMapForm.Instance.Visible)
            {
                if (WhitelistMapForm.Instance.WindowState == FormWindowState.Minimized)
                    WhitelistMapForm.Instance.WindowState = FormWindowState.Normal;
                WhitelistMapForm.Instance.Hide();
            }
        }

        public void stopMaid()
        {
            Proxy.Instance.UnregisterHandler(RedMsgHandler);
            Proxy.Instance.UnregisterHandler(CJHandler);
            Proxy.Instance.UnregisterHandler(JoinMapHandler);
            Proxy.Instance.UnregisterHandler(CopyWalkHandler);
            if (cbSpecialAnims.Checked)
                Flash.FlashCall2 -= AnimsMsgHandler;

            cbSpecialAnims.Enabled = true;
            tbSpecialMsg.Enabled = true;
            numSkillAct.Enabled = true;
            cmbGotoUsername.Enabled = true;
            tbSkillList.Enabled = true;
            gbOptions.Enabled = true;
            cbWaitSkill.Enabled = true;
            btnMe.Enabled = true;
            cbCopyWalk.Enabled = true;
            cmbUltraBoss.Enabled = true;
            cbEnablePlugin.Checked = false;
            onPause = false;
            ResetToken(false);
            if (!string.IsNullOrWhiteSpace(msgTemp))
            {
                tbSpecialMsg.Text = msgTemp;
                msgTemp = string.Empty; // Reset setelah keluar map
            }
            Root.Instance.maidStrip.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        }

        public void resetSpecials()
        {
            if (Player.Map == "ascendeclipse" && Player.Cell != "r3")
            {
                sunConvergenceCount = 0;
                moonConvergenceCount = 0;
            }
            if (Player.Map == "astralshrine" && Player.Cell != "r2")
            {
                beholdOurStarfireCount = 0;
            }
            if (Player.Map == "ultragramiel" && Player.Cell != "r2")
            {
                crystalCount = 0;
            }
            counterAttack = false;
        }

        /* Hotkey */

        private void cbEnableGlobalHotkey_CheckedChanged(object sender, EventArgs e)
        {
            //cbUnfollow.Enabled = cbEnableGlobalHotkey.Checked;
            //cbStopAttack.Enabled = cbEnableGlobalHotkey.Checked;
            if (cbEnableGlobalHotkey.Checked)
            {
                kbh.OnKeyPressed += globalHotkey;
                kbh.OnKeyUnpressed += (s, ek) => { };
                this.KeyDown -= hotkey;

                kbh.HookKeyboard();
            }
            else
            {
                cbStopAttack.Checked = false;
                cbUnfollow.Checked = false;
                kbh.OnKeyPressed -= globalHotkey;
                kbh.OnKeyUnpressed -= (s, ek) => { };
                this.KeyDown += new KeyEventHandler(this.hotkey);

                kbh.UnHookKeyboard();
            }
        }

        private void hotkey(object sender, KeyEventArgs e)
        {
            if (cmbGotoUsername.Focused || tbAttPriority.Focused || tbSpecialMsg.Focused)
                return;

            switch (e.KeyCode)
            {
                case Keys.R:
                    // LockCell: R
                    e.SuppressKeyPress = true;
                    cbUnfollow.Checked = cbUnfollow.Checked ? false : true;
                    break;
                case Keys.T:
                    // StopAttack: T
                    e.SuppressKeyPress = true;
                    cbStopAttack.Checked = cbStopAttack.Checked ? false : true;
                    break;
            }
        }

        private void globalHotkey(object sender, Keys e)
        {
            if (cmbGotoUsername.Focused || tbAttPriority.Focused || tbSpecialMsg.Focused)
                return;

            switch (e)
            {
                case Keys.R:
                    // LockCell: R
                    cbUnfollow.Checked = cbUnfollow.Checked ? false : true;
                    break;
                case Keys.T:
                    // StopAttack: T
                    cbStopAttack.Checked = cbStopAttack.Checked ? false : true;
                    break;
            }
        }

        /* Other Control */

        public void pauseFollow()
        {
            if (onPause) return;
            if (cbCopyWalk.Checked)
                Proxy.Instance.UnregisterHandler(CopyWalkHandler);
            onPause = true;
            //debug("onPause: true");
        }

        public void resumeFollow()
        {
            if (!onPause) return;
            if (cbCopyWalk.Checked)
                Proxy.Instance.RegisterHandler(CopyWalkHandler);
            onPause = false;
            //debug("onPause: false");
        }

        private void cbLockCell_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableGlobalHotkey.Checked == false) return;
            if (cbUnfollow.Checked)
            {
                Proxy.Instance.UnregisterHandler(CJHandler);
                if (cbCopyWalk.Checked) Proxy.Instance.UnregisterHandler(CopyWalkHandler);
            }
            else
            {
                Proxy.Instance.RegisterHandler(CJHandler);
                if (cbCopyWalk.Checked) Proxy.Instance.RegisterHandler(CopyWalkHandler);
            }
        }

        private void cbStopAttack_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableGlobalHotkey.Checked == false) return;
            if (cbStopAttack.Checked)
            {
                lbStopAttackBg.BackColor = System.Drawing.Color.DeepPink;
                stopwatch.Reset();
                stopwatch.Start();
                timerStopAttack.Enabled = true;
                cbStopAttack.BackColor = System.Drawing.Color.Magenta;
                Player.CancelAutoAttack();
                Player.CancelTarget();
                Player.Rest();
            }
            else
            {
                lbStopAttackBg.BackColor = System.Drawing.Color.Transparent;
                stopwatch.Stop();
                this.Text = "Maid Remake";
                timerStopAttack.Enabled = false;
                cbStopAttack.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void cbUseHeal_CheckedChanged(object sender, EventArgs e)
        {
            tbHealSkill.Enabled = !cbUseHeal.Checked;
            numHealthPercent.Enabled = !cbUseHeal.Checked;
            if (cbUseHeal.Checked)
            {
                healSkill = tbHealSkill.Text.Split(',');
            }
        }

        private void cbBuffIfStop_CheckedChanged(object sender, EventArgs e)
        {
            tbBuffSkill.Enabled = !cbBuffIfStop.Checked;
            if (cbBuffIfStop.Checked)
            {
                buffSkill = tbBuffSkill.Text.Split(',');
                buffIndex = 0;
            }
        }

        private void cbAttackPriority_CheckedChanged(object sender, EventArgs e)
        {
            tbAttPriority.Enabled = !cbAttackPriority.Checked;
            if (cbAttackPriority.Checked)
            {
                monsterList = tbAttPriority.Text.Split(',');
            }
        }

        private void timerStopAttack_Tick(object sender, EventArgs e)
        {
            this.Text = $"Maid Remake ({string.Format("{0:hh\\:mm\\:ss}", stopwatch.Elapsed)})";
        }

        private int sunConvergenceCount = 0;
        private int moonConvergenceCount = 0;
        private int crystalCount = 0;
        private int beholdOurStarfireCount = 0;

        private bool ultraBossHandler(string msg, int monId = 0)
        {
            bool act = true;
            if (msg.Contains("shattering"))
            {
                switch (cmbUltraBoss.SelectedItem.ToString())
                {
                    case "Gramiel L1":
                    case "Gramiel L2":
                        crystalCount++;
                        debug($"Defense shattering 'Left Crystal' count: {crystalCount}");
                        break;
                    case "Gramiel R1":
                    case "Gramiel R2":
                        crystalCount++;
                        debug($"Defense shattering 'Right Crystal' count: {crystalCount}");
                        break;
                }
            }

            if (msg.Contains("sun converge"))
            {
                sunConvergenceCount++;
                debug($"Sun Converges count: {sunConvergenceCount}");
            }
            if (msg.Contains("moon converge"))
            {
                moonConvergenceCount++;
                debug($"Moon Converges count: {moonConvergenceCount}");
            }
            if (msg.Contains("behold our starfire"))
            {
                beholdOurStarfireCount++;
                debug($"Behold our starfire count: {beholdOurStarfireCount}");
            }
            switch (cmbUltraBoss.SelectedItem.ToString())
            {
                case "Asc.Solstice P1":
                    act = sunConvergenceCount % 2 != 0 || !msg.Contains("sun converge");
                    break;
                case "Asc.Solstice P2":
                    act = sunConvergenceCount % 2 == 0 || !msg.Contains("sun converge");
                    break;
                case "Asc.Midnight P1":
                    act = moonConvergenceCount % 2 != 0 || !msg.Contains("moon converge");
                    break;
                case "Asc.Midnight P2":
                    act = moonConvergenceCount % 2 == 0 || !msg.Contains("moon converge");
                    break;
                case "Ast.Empyrean P1":
                    act = beholdOurStarfireCount % 2 != 0 || !msg.Contains("behold our starfire");
                    break;
                case "Ast.Empyrean P2":
                    act = beholdOurStarfireCount % 2 == 0 || !msg.Contains("behold our starfire");
                    break;
                case "Gramiel L1":
                case "Gramiel R1":
                    act = crystalCount % 4 == 2 || !msg.Contains("shattering");
                    break;
                case "Gramiel L2":
                case "Gramiel R2":
                    act = crystalCount % 4 == 0 || !msg.Contains("shattering");
                    break;
            }
            return act;
        }

        private void cmbUltraBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbUltraBoss.SelectedItem.ToString())
            {
                case "None":
                    tbSpecialMsg.Enabled = true;
                    cbAttackPriority.Enabled = true;
                    numSkillAct.Enabled = true;
                    break;
                case "Asc.Solstice P1":
                case "Asc.Solstice P2":
                    cbAttackPriority.Checked = true;
                    tbAttPriority.Text = "Ascended Solstice";
                    tbSpecialMsg.Text = "sun converges";
                    numSkillAct.Value = 5;
                    break;
                case "Asc.Midnight P1":
                case "Asc.Midnight P2":
                    cbAttackPriority.Checked = true;
                    tbAttPriority.Text = "Ascended Midnight";
                    tbSpecialMsg.Text = "moon converges";
                    numSkillAct.Value = 5;
                    break;
                case "Ast.Empyrean P1":
                case "Ast.Empyrean P2":
                    cbAttackPriority.Checked = true;
                    tbAttPriority.Text = "Astral Empyrean";
                    tbSpecialMsg.Text = "behold our starfire";
                    numSkillAct.Value = 5;
                    break;
                case "Gramiel L1":
                case "Gramiel L2":
                    tbAttPriority.Text = "id.2,crystal";
                    cbAttackPriority.Checked = true;
                    tbSpecialMsg.Text = "shattering";
                    numSkillAct.Value = 5;
                    break;
                case "Gramiel R1":
                case "Gramiel R2":
                    cbAttackPriority.Checked = true;
                    tbAttPriority.Text = "id.3,crystal";
                    tbSpecialMsg.Text = "shattering";
                    numSkillAct.Value = 5;
                    break;
            }
        }

        private void cmbPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassPreset.cbClear();
            switch (cmbPreset.SelectedItem.ToString())
            {
                case "LR":
                    ClassPreset.LR();
                    break;
                case "LC":
                    ClassPreset.LC();
                    break;
                case "LOO":
                    ClassPreset.LOO();
                    break;
                case "SC":
                    ClassPreset.SC();
                    break;
                case "AP":
                    ClassPreset.AP();
                    break;
                case "CCMD":
                    ClassPreset.CCMD();
                    break;
                case "SSOT":
                    ClassPreset.SSOT();
                    break;
                case "NCM":
                    ClassPreset.NCM();
                    break;
                case "TK":
                    ClassPreset.TK();
                    break;
                case "AI":
                    ClassPreset.AI();
                    break;
                case "AM":
                    ClassPreset.AM();
                    break;
                case "CSH":
                    ClassPreset.CSH();
                    break;
                case "CSH v2":
                    ClassPreset.CSHGunslinger();
                    break;
            }
            ClassPreset.cbSet();
        }

        // get username in cell
        private void cmbGotoUsername_Clicked(object sender, EventArgs e)
        {
            if (World.IsMapLoading)
                return;
            cmbGotoUsername.Items.Clear();
            foreach (string player in World.PlayersInMap)
                cmbGotoUsername.Items.Add(player);
        }

        private void lblLockedMapSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LockedMapForm.Instance.Visible || LockedMapForm.Instance.WindowState == FormWindowState.Minimized)
            {
                LockedMapForm.Instance.WindowState = FormWindowState.Normal;
                LockedMapForm.Instance.Hide();
            }
            else if (!LockedMapForm.Instance.Visible)
            {
                LockedMapForm.Instance.Show(this);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MaidConfig maidConfig = new MaidConfig
            {
                Target = cmbGotoUsername.Text,
                SkillList = tbSkillList.Text,
                SkillDelay = (int)numSkillDelay.Value,
                WaitSkill = cbWaitSkill.Checked,
                StopFailedGoto = cbStopIf.Checked,
                LockedZoneHandler = cbHandleLockedMap.Checked,
                LockedZoneHandlerMaps = LockedMapForm.Instance.tbLockedMapAlternative.Text,
                WhitelistMap = cbWhitelistMap.Checked,
                WhitelistMapMaps = WhitelistMapForm.Instance.tbWhitelistMap.Text,
                RelogDelay = (int)numRelogDelay.Value,
                GlobalHotkey = cbEnableGlobalHotkey.Checked,
                SafeSkill = cbUseHeal.Checked,
                SafeSkillList = tbHealSkill.Text,
                SafeSkillHP = (int)numHealthPercent.Value,
                BuffStopAttack = cbBuffIfStop.Checked,
                BuffStopAttackList = tbBuffSkill.Text,
                AttackPriority = cbAttackPriority.Checked,
                AttackPriorityMonster = tbAttPriority.Text,
                CopyWalk = cbCopyWalk.Checked,
                SpecialMsg = tbSpecialMsg.Text,
                SpecialAct = (int)numSkillAct.Value,
                AntiCounter = cbAntiCounter.Checked,
                UltraBossExtra = cmbUltraBoss.SelectedIndex,
            };
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save config";
                saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Config");
                saveFileDialog.Filter = "Maid config|*.json";
                saveFileDialog.DefaultExt = ".json";
                saveFileDialog.CheckFileExists = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(maidConfig, Formatting.Indented));
                        string[] path = saveFileDialog.FileName.Split('\\');
                        gbConfig.Text = $"Config : {path[path.Length - 1]}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to save config: " + ex.Message);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load config";
                openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Config");
                openFileDialog.Filter = "Maid config|*.json";
                openFileDialog.DefaultExt = ".json";
                if (openFileDialog.ShowDialog() == DialogResult.OK &&
                    TryDeserialize(File.ReadAllText(openFileDialog.FileName), out MaidConfig config))
                {
                    gbConfig.Text = $"Config : {openFileDialog.SafeFileName}";
                    cmbUltraBoss.SelectedIndex = config.UltraBossExtra;
                    cmbGotoUsername.Text = config.Target;
                    tbSkillList.Text = config.SkillList;
                    numSkillDelay.Value = config.SkillDelay;
                    cbWaitSkill.Checked = config.WaitSkill;
                    cbStopIf.Checked = config.StopFailedGoto;
                    cbHandleLockedMap.Checked = config.LockedZoneHandler;
                    LockedMapForm.Instance.tbLockedMapAlternative.Text = config.LockedZoneHandlerMaps;
                    cbWhitelistMap.Checked = config.WhitelistMap;
                    WhitelistMapForm.Instance.tbWhitelistMap.Text = config.WhitelistMapMaps;
                    numRelogDelay.Value = config.RelogDelay;
                    cbEnableGlobalHotkey.Checked = config.GlobalHotkey;
                    cbUseHeal.Checked = config.SafeSkill;
                    tbHealSkill.Text = config.SafeSkillList;
                    numHealthPercent.Value = config.SafeSkillHP;
                    cbBuffIfStop.Checked = config.BuffStopAttack;
                    tbBuffSkill.Text = config.BuffStopAttackList;
                    cbAttackPriority.Checked = config.AttackPriority;
                    tbAttPriority.Text = config.AttackPriorityMonster;
                    cbCopyWalk.Checked = config.CopyWalk;
                    tbSpecialMsg.Text = config.SpecialMsg;
                    numSkillAct.Value = config.SpecialAct;
                    cbAntiCounter.Checked = config.AntiCounter;
                }
            }
        }

        private bool TryDeserialize(string json, out MaidConfig config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<MaidConfig>(json);
                return true;
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            config = null;
            return false;
        }

        private void cbPartyCmd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPartyCmd.Checked)
            {
                Proxy.Instance.RegisterHandler(PartyInvitationHandler);
                Proxy.Instance.RegisterHandler(PartyChatHandler);
            }
            else
            {
                Proxy.Instance.UnregisterHandler(PartyInvitationHandler);
                Proxy.Instance.UnregisterHandler(PartyChatHandler);
            }
        }

        private void lblWhitelistMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (WhitelistMapForm.Instance.Visible || WhitelistMapForm.Instance.WindowState == FormWindowState.Minimized)
            {
                WhitelistMapForm.Instance.WindowState = FormWindowState.Normal;
                WhitelistMapForm.Instance.Hide();
            }
            else if (!WhitelistMapForm.Instance.Visible)
            {
                WhitelistMapForm.Instance.Show(this);
            }
        }

        private void btnMe_Click(object sender, EventArgs e)
        {
            if (Player.IsLoggedIn) cmbGotoUsername.Text = Player.Username;
        }

        private void cbAntiCounter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAntiCounter.Checked)
            {
                Flash.FlashCall2 += AntiCounterHandler;
            }
            else
            {
                Flash.FlashCall2 -= AntiCounterHandler;
            }
        }

        private void cbSpecialAnims_CheckedChanged(object sender, EventArgs e)
        {
            tbSpecialMsg.Enabled = cbSpecialAnims.Checked;
            numSkillAct.Enabled = cbSpecialAnims.Checked;
        }
    }
}
