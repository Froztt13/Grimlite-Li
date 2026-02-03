using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Networking;
using DarkUI.Forms;
using Grimoire.Tools;
using MaidRemake.LockedMapHandle;
using MaidRemake.WhitelistMap;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Grimoire.Tools.Maid;
using Grimoire.Networking.Handlers.Maid;
using System.Drawing;
using Grimoire.Game.Data;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Grimoire.Botting;
using Grimoire.Utils;

namespace Grimoire.UI.Maid
{
    public partial class MaidRemake : DarkForm
    {
        public static MaidRemake Instance { get; } = new MaidRemake();

        public string targetUsername => (MaidRemake.Instance.cmbGotoUsername.Text ?? "").Trim().ToLower();

        public bool isPlayerInMyCell
        {
            get
            {
                try
                {
                    string res = Flash.Call<string>("GetCellPlayers", new string[] { targetUsername });
                    return bool.TryParse(res, out bool v) && v;
                }
                catch { return false; }
            }
        }

        public bool isPlayerInMyRoom => IsPlayerInMap(targetUsername);

        //Avoiding unwanted stuck
        public bool shouldFollow => !(isPlayerInMyRoom && isPlayerInMyCell);
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
        bool balance = false;   //Used on Gramiel crystal HP balancing
        bool locktarget = false;//Used for Special cases like Breaking shield/safeguard
        private string selectedMode => cmbUltraBoss.SelectedItem?.ToString() ?? "None";
        private string selectedPreset => cmbPreset.SelectedItem?.ToString() ?? "";
        Stopwatch stopwatch = new Stopwatch();

        public MaidRemake()
        {
            InitializeComponent();

            KeyPreview = true;

            //KeyListener non Global Hook
            this.KeyDown += new KeyEventHandler(this.hotkey);
            if (Player.IsLoggedIn) cmbGotoUsername.Text = Player.Username;
            cmbUltraBoss.SelectedIndex = 0;
            AutoLoadConfig();
            this.Text = $"Maid Remake";

            if (cbAntiCounter.Checked)
                Flash.FlashCall2 += AntiCounterHandler;
            if (cbPartyCmd.Checked)
            {
                Proxy.Instance.RegisterHandler(PartyInvitationHandler);
                Proxy.Instance.RegisterHandler(PartyChatHandler);
            }
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
            bool toggle = cbEnablePlugin.Checked;
            // OptionsManager.SetSkipCutscenes();
            OptionsManager.SetLagKiller(toggle);
            if (toggle)
            {
                startUI();

                int gotoTry = 0;
                var skillproperties = Flash.Instance.GetGameObject<List<JObject>>("world.actions.active") ?? new List<JObject>();

                string[] skillList = (tbSkillList.Text ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (skillList.Length == 0)
                    skillList = new[] { "1", "2", "3", "4" };
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

                if (!cbUnfollow.Checked && Player.IsLoggedIn && !World.IsMapLoading && shouldFollow)//isPlayerInMyRoom && !isPlayerInMyCell)
                {
					if (!isPlayerInMyRoom)
					{
						await gotoTarget(targetUsername);
						if (isPlayerInMyRoom && !isPlayerInMyCell)
							await chaseTargetCell(targetUsername);
					}
					else
						await chaseTargetCell(targetUsername);
                }

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
                if (selectedMode != "None" && selectedMode != "Auto Potion")
                {
                    equipEnrage();
                }
                //if (skillproperties[5]?["sArg1"]?.ToString() != "")
                //    useSkill("5", true);
                OptionsManager.SetInfiniteRange();

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

                        if (cbEnablePlugin.Checked && !cbUnfollow.Checked && !onPause && Player.IsLoggedIn && !World.IsMapLoading && isPlayerInMyRoom && !isPlayerInMyCell)
                        {
                            await chaseTargetCell(targetUsername);
                            continue;
                        }

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
                            string selfskill = null;
                            if (int.TryParse(skillList[skillIndex], out int skillIdx) && skillIdx >= 0 && skillIdx < skillproperties.Count)
                                selfskill = skillproperties[skillIdx]?["tgt"]?.ToString();
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
                                //For class with some aura detection or Ultragramiel boss fight
                                await SpecialCombo();
                                if (tauntTask == null)
                                    taunt();

                                if (cbWaitSkill.Checked && Player.SkillAvailable(skillList[skillIndex]) > 0)
                                    continue;
                                //Preventing SpecialCombo interfere skill combo after consuming potion

                                // force, to ensure a skill is REALLY executed 
                                if (forceSkill)
                                {
                                    string skillAct = numSkillAct.Value.ToString();
                                    await waitSkill(skillAct);
                                    /* await Task.Delay(1000);
                                    await Task.Delay(Player.SkillAvailable(skillAct));
                                    Player.UseSkill(skillAct);
                                    useSkill(skillAct, true);*/
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
                            _ = gotoTarget(targetUsername);
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

                            // wait 3 second before try to goto or join to different map (when locked map handler is enabled)
                            for (int i = 0; i < 6 && cbEnablePlugin.Checked && cbHandleLockedMap.Checked && Player.IsLoggedIn && !World.IsMapLoading; i++)
                                await Task.Delay(500);

                            // goto target current cell when in the same room
                            // Actively chase leader cell (works even when leader is idle and sends no uotls packet)
                            while (!cbUnfollow.Checked && cbEnablePlugin.Checked && Player.IsLoggedIn && /*shouldFollow)*/ isPlayerInMyRoom && !isPlayerInMyCell)
                            {
                                await chaseTargetCell(targetUsername);
                            }
                            BotData.BotState = BotData.State.Combat;
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
        private async Task waitSkill(string index)
        {
            int cd = Player.SkillAvailable(index);
            await Task.Delay(Math.Min(cd, 1500));
            useSkill(index, true);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(50); //Ensure its going to cooldown
                if (Player.SkillAvailable(index) > 0)
                    return;
            }
            useSkill(index, true);
        }
        private void useSkill(string skillIndex, bool force = false)
        {
            if (isUsingCSH() || force)
            {
                Player.ForceUseSkill(skillIndex);
                return;
            }
            Player.UseSkill(skillIndex);
        }

        private void equipEnrage()
        {
            InventoryItem item = Player.Inventory.Items.FirstOrDefault((InventoryItem i) => i.Name.Equals("Scroll of Enrage") && i.IsEquippable);
            if (item != null && !item.IsEquipped)
            {
                Player.EquipPotion(item.Id, item.Description, item.File, item.Name);
                _ = Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    useSkill("5", true);
                });
            }
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
                        throw new Exception("Cycle isn't a valid number!");

                    if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]))
                        mon = parts[2];

                    if (parts.Length > 3 && !int.TryParse(parts[3], out second))
                        throw new Exception("Seconds isn't a valid number!");
                    if (parts.Length > 4 && !int.TryParse(parts[4], out order))
                        order -= 1;

                    if (tauntTask == null && World.IsMonsterAvailable(mon))
                    {
                        ResetToken(true);
                        tauntTask = cycleTaunt(cycle, mon, second, order);
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
                    Player.refreshCell();
                    tbSpecialMsg.Text = $"tc;2;*;12;<order 1-4>(optional)";
                }
            }
        }

        private bool isUsingCSH()
        {
            return (Player.EquippedClass ?? "").Contains("CHRONO SHADOW");
        }

        private string msgTemp;
        string[] potionNames = { "Felicitous Philtre", "Potent Malice", "Potent Honor" };
        private async Task SpecialCombo()
        {
            var potion = Player.Inventory.Items.FirstOrDefault((InventoryItem i) =>
                i.IsEquipped &&
                i.Quantity >= 1 &&
                potionNames.Any(pots => i.Name.ContainsIgnoreCase(pots)));
            if (selectedMode == "Auto Potion" && potion != null)
            {
                if (potion.Name.Contains("Potent") && Player.GetAuras(true, "Potent Honor Malice") == 0)
                {
                    await waitSkill("5");
                }
                else if (potion.Name.Contains("Felicitous") && Player.GetAuras(true, "Felicitous Philtre") == 0)
                {
                    await waitSkill("5");
                    //await Task.Delay(Player.SkillAvailable("5"));
                    //useSkill("5");
                }
            }

            /*if (Player.Map == "voidxyfrag" && Player.EquippedClass == "LEGION REVENANT")
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
            }*/
            string preset = selectedPreset;
            if (isUsingCSH() && preset == "CSH")
            {
                if (Player.GetAuras(true, "Temporal Rift") >= 4)
                {
                    await waitSkill("1");
                    await BotUtilities.WaitUntil(() => Player.GetAuras(true, "Gunslinger Stance") >= 1, 10, 10);
                    debug("Entering AA Spam");
                    do
                    {
                        if (!Player.HasTarget)
                        {
                            if (cbAttackPriority.Checked)
                                doPriorityAttack();
                            else
                                Player.AttackMonster("*");
                        }
                        useSkill("0");
                        await Task.Delay(30);
                    } while (Player.GetAuras(true, "Gunslinger Stance") >= 1
                        || Player.GetAuras(true, "Bang") <= 3);
                    debug("Spam Ends");
                    /*useSkill("4");
                    await Task.Delay(Player.SkillAvailable("1"));
                    await Task.Delay(100);
                    useSkill("1");
                    await Task.Delay(200);*/
                }
                //else
                //{
                //    LogForm.Instance.devDebug($"Temporal rift stacks : {Player.GetAuras(true, "Temporal Rift")}");
                //}
            }
            else if (Player.EquippedClass == "ARCANA INVOKER" &&
            preset == "AI")
            {
                if ((Player.GetAuras(true, "0 - The Fool") == 0 &&
                    Player.GetAuras(true, "XXI - The World") == 0) ||
                    Player.GetAuras(true, "XX - Judgement") == 1 ||
                    Player.GetAuras(true, "End of the world") >= 20)
                {
                    await waitSkill("1");
                }
            }
            else if (Player.EquippedClass == "ARCHMAGE" &&
            preset == "AM")
            {
                if (Player.GetAuras(true, "Corporeal Ascension") == 0 ||
                    Player.GetAuras(true, "Arcane Sigil") == 0)
                {
                    LogForm.Instance.devDebug("Attempting to enable Corporeal & Arcane Sigil");
                    await waitSkill("2");
                    await waitSkill("4");
                    await Task.Delay(350);
                }
            }
			else if (Player.EquippedClass == "ARCHPALADIN")
            {
                int Seal = AuraManager.EnemyAura(Player.CurrentTargetId)
									  .GetRemainingTime("Righteous Seal");
                if (Seal < 600 && Seal > 0)
                {
                    await waitSkill("4");
                    await Task.Delay(350);
                }
            }

            // ultra gramiel
            if (Player.Map == "ultragramiel")
            {
                // string target = Player.GetTargetName().ToLower();
                if (Player.GetTargetName.EqualsIgnoreCase("grace crystal") || World.IsMonsterAvailable("grace crystal"))
                {
                    CheckCrystalHealthBalance();
                    return;
                }

                if (tauntTask == null)
                {
                    ResetToken(true);
                    tauntTask = cycleTaunt(4, "Gramiel", 20);
                }
                else if (!World.IsMonsterAvailable("Gramiel"))
                {
                    ResetToken(false);
                }
            }
        }
        private void CheckCrystalHealthBalance()
        {
            var monsters = World.GetAllMonsters();
            var L_crystal = monsters.FirstOrDefault(m => m.MonMapID == 2);
            var R_crystal = monsters.FirstOrDefault(m => m.MonMapID == 3);

            if (forceSkill)
            {
                balance = false;
                doPriorityAttack();
                return;
            }//Ensure Taunt executed properly

            //Gramiel crystal HP threshold
            int[] thresholds = { 150, 50 };
            //Get Current target MonId
            int.TryParse(Flash.GetGameObject("world.myAvatar.target.objData.MonMapID").Replace("\"", ""), out int currentId);
            foreach (int threshold in thresholds)
            {
                if (currentId == 2 &&
                    L_crystal.Health <= threshold &&
                    R_crystal.Health > threshold)
                {
                    Player.AttackMonster("id.3");//Right
                    balance = true;
                    return;
                }

                if (currentId == 3 &&
                    R_crystal.Health <= threshold &&
                    L_crystal.Health > threshold)
                {
                    Player.AttackMonster("id.2");//Left
                    balance = true;
                    return;
                }
            }
            balance = false;
        }
        #region Tauntcycle
        private Task tauntTask = null;
        private CancellationTokenSource cts;
        private void ResetToken(bool createNew)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
            tauntTask = null;
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
            debug($"Executing tauntcycle with Cycle = {cycle}, Every {second}s, initial taunt at {second / cycle * count}s");
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
            ResetToken(true);
        }
        #endregion

        private void AnimsMsgHandler(string function, params object[] args)
        {
            if (function != "packetFromServer") return;
            try
            {
                Networking.Message message = NetworkUtils.CreateMessage((string)args[0]);
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
                                //catches anims[].msg
                                string msg = anim?["msg"]?.ToString()?.ToLower();

                                if (msg != null)
                                {
                                    int monId = 0;

                                    int.TryParse(anim?["tInf"]?.ToString()?.Split(':')[1], out monId);

                                    // Store animation message for bot statement commands
                                    Configuration.LastAnimationMessage = msg;
                                    Configuration.AnimationTriggered = true;

                                    string[] inputMsg = tbSpecialMsg.Text?.ToLower().Split(',');
                                    foreach (string m in inputMsg)
                                    {
                                        string specialMsg = m.Trim();
                                        if (!string.IsNullOrEmpty(specialMsg))
                                        {
                                            if (msg.Contains(specialMsg) && ultraBossHandler(msg, monId))
                                            {
                                                //LogForm.Instance.devDebug($"Forcing taunt into id:{monId}");
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
                Networking.Message message = NetworkUtils.CreateMessage((string)args[0]);
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
                                        Task.Run(async () =>
                                        {
                                            await Task.Delay(1500);
                                            counterAttack = true;
                                            cbStopAttack.Checked = true;
                                            await Task.Delay(6000);
                                            //This function will auto exit Counter/Stop Atk
                                            counterAttack = false;
                                            cbStopAttack.Checked = false;
                                        });
                                    }
                                }
                            }
                        }
                    }
                    if (jsonMessage.DataObject?["a"] != null)
                    {
                        JArray actions = (JArray)jsonMessage.DataObject?["a"];
                        if (actions != null)
                        {
                            if (Player.GetAuras(true, "Sun's Heat") > 0 || counterAttack)
                                cbStopAttack.Checked = true;
                            else if (Player.Map == "ascendedeclipse") //changed to avoid force uncheck
                                cbStopAttack.Checked = false;
                            foreach (JObject action in actions)
                            {
                                JObject aura = (JObject)action["aura"];
                                if (aura?["nam"]?.ToString() == "Counter Attack" && action.GetValue("cmd")?.ToString() == "aura--")
                                {
                                    counterAttack = false;
                                    cbStopAttack.Checked = false;
                                    //debug("Counter Attack: fades");
                                    break;
                                } //Commented Old Counter handling
                                JArray auras = (JArray)action["auras"];
                                if (auras != null)
                                {
                                    foreach (JObject _auras in auras)
                                    {
                                        string msg = (string)(_auras?["msgOn"]);
                                        if (msg == null) return;
                                        if (msg.Contains("Gramiel attempts to drain"))
                                        {
                                            Task.Run(async () =>
                                            {
                                                debug($"Switching to attack Gramiel for 4s to break his Safeguard");
                                                locktarget = true;
                                                Player.AttackMonster("Gramiel");
                                                await Task.Delay(4000);
                                                locktarget = false;
                                            });
                                            break;
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

        private void debug(string text)
        {
            LogForm.Instance.AppendDebug($"[Maid] {text}",doAnyway:true);
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
            if (balance || locktarget)
                return;

            JObject objData = Flash.Instance.GetGameObject<JObject>("world.myAvatar.target.objData");

            // string currentTarget = Player.GetTargetName;
            string currentTarget = objData?["strMonName"]?.ToString();
            string idTarget = objData?["MonMapID"]?.ToString();

            for (int i = 0; i < monsterList.Length; i++)
            {
                //if (monsterList[i].Equals(Player.GetTargetName(), StringComparison.OrdinalIgnoreCase))
                string monsterId = null;
                if (monsterList[i].StartsWith("id", StringComparison.OrdinalIgnoreCase) && monsterList[i].Length > 3)
                    monsterId = monsterList[i].Substring(3);

                if ((currentTarget?.ContainsIgnoreCase(monsterList[i]) ?? false) ||
                    (monsterId != null && monsterId == idTarget))
                    return; //Made special for CSH non autoattack cases

                if (World.IsMonsterAvailable(monsterList[i]))
                {
                    Player.AttackMonster(monsterList[i]);
                    return;
                }
            }
        }

        //private bool isPlayerInCombat => Player.CurrentState == Player.State.InCombat;

        private bool IsPlayerInMap(string targetUsername)
        {
            try
            {
                var players = World.PlayersInMap;
                if (players == null) return false;
                string t = (targetUsername ?? "").Trim().ToLower();
                if (string.IsNullOrEmpty(t)) return false;

                foreach (string player in players)
                {
                    if ((player ?? "").Trim().ToLower() == t)
                        return true;
                }
            }
            catch { }
            return false;
        }

        private bool isHealthUnder(int percentage)
        {
            int healthBoundary = Player.HealthMax * percentage / 100;
            return Player.Health <= healthBoundary ? true : false;
        }

        private async Task gotoTarget(string targetUsername)
        {
            BotData.BotState = BotData.State.Move;
            if (Player.CurrentState == Player.State.InCombat)
                await Player.ExitCombat(1500, true);
            if (!isPlayerInMyRoom)
            {
                string currentmap = Player.Map;
                await Proxy.Instance.SendToServer($"%xt%zm%cmd%1%goto%{targetUsername}%");
                await BotUtilities.WaitUntil(() => Player.Map != currentmap && !World.IsMapLoading, interval: 500);
            }
            // Player.GoToPlayer(targetUsername);
        }

        private (string cell, string pad)? GetTargetCellPad(string target)
        {
            try
            {
                World.RefreshDictionary();
                var info = World.Players?.FirstOrDefault(p => (p.Name ?? "").Trim().ToLower() == (target ?? "").Trim().ToLower());
                if (info != null && !string.IsNullOrWhiteSpace(info.Cell))
                    return (info.Cell, string.IsNullOrWhiteSpace(info.Pad) ? "Spawn" : info.Pad);
            }
            catch { }
            return null;
        }

        private async Task chaseTargetCell(string target)
        {
            try
            {
                var cp = GetTargetCellPad(target);
                if (cp != null && !string.Equals(Player.Cell ?? "", cp.Value.cell, StringComparison.OrdinalIgnoreCase))
                {
                    if (Player.CurrentState == Player.State.InCombat)
                        await Player.ExitCombat(500, true);
                    Player.MoveToCell(cp.Value.cell, cp.Value.pad);
                    await Task.Delay(800);
                    return;
                }
            }
            catch { }
            if (Player.CurrentState == Player.State.InCombat)
                await Player.ExitCombat(500, true);
            try { Player.GoToPlayer(target); } catch { }
            await Task.Delay(500);
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
            antiCounter();
        }

        public void stopMaid()
        {
            Proxy.Instance.UnregisterHandler(RedMsgHandler);
            Proxy.Instance.UnregisterHandler(CJHandler);
            Proxy.Instance.UnregisterHandler(JoinMapHandler);
            Proxy.Instance.UnregisterHandler(CopyWalkHandler);
            if (cbSpecialAnims.Checked)
                Flash.FlashCall2 -= AnimsMsgHandler;
            if (cbAntiCounter.Checked)
                Flash.FlashCall2 -= AntiCounterHandler;

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
            cbUnfollow.Checked = false;
            onPause = false;
            ResetToken(true);
            if (!string.IsNullOrWhiteSpace(msgTemp))
            {
                tbSpecialMsg.Text = msgTemp;
                msgTemp = string.Empty; // Reset setelah keluar map
            }
            Root.Instance.maidStrip.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        }

        public void resetSpecials()
        {
            string map = Player.Map;
            string cell = Player.Cell;
            if (map == "ascendeclipse" && cell != "r3")
            {
                sunConvergenceCount = 0;
                moonConvergenceCount = 0;
            }
            if (map == "astralshrine" && cell != "r2")
            {
                beholdOurStarfireCount = 0;
            }
            if (map == "ultragramiel" && cell != "r2")
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
            //if (cmbGotoUsername.Focused || tbAttPriority.Focused || tbSpecialMsg.Focused)
            if (IsFocusedOnBox)
                return;

            switch (e.KeyCode)
            {
                case Keys.R:
                    // LockCell: R
                    e.SuppressKeyPress = true;
                    cbUnfollow.Checked = !cbUnfollow.Checked;
                    break;
                case Keys.T:
                    // StopAttack: T
                    e.SuppressKeyPress = true;
                    cbStopAttack.Checked = !cbStopAttack.Checked;
                    break;
            }
        }

        private void globalHotkey(object sender, Keys e)
        {
            //if (cmbGotoUsername.Focused || tbAttPriority.Focused || tbSpecialMsg.Focused)
            if (IsFocusedOnBox)
                return;

            switch (e)
            {
                case Keys.R:
                    // LockCell: R
                    cbUnfollow.Checked = !cbUnfollow.Checked;
                    break;
                case Keys.T:
                    // StopAttack: T
                    cbStopAttack.Checked = !cbStopAttack.Checked;
                    break;
            }
        }
        /* Other Control */
        public bool IsFocusedOnBox => this.ActiveControl != null &&
        (this.ActiveControl is TextBox || this.ActiveControl is ComboBox) &&
        this.ActiveControl.Enabled;

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
            if (!cbEnablePlugin.Checked)
                return;
            Font prev = Root.Instance.maidStrip.Font;
            if (cbUnfollow.Checked)
            {
                Root.Instance.maidStrip.Font = new Font(prev, prev.Style | FontStyle.Italic);
                Proxy.Instance.UnregisterHandler(CJHandler);
                if (cbCopyWalk.Checked) Proxy.Instance.UnregisterHandler(CopyWalkHandler);
            }
            else
            {
                Root.Instance.maidStrip.Font = new Font(prev, prev.Style & ~FontStyle.Italic);
                Proxy.Instance.RegisterHandler(CJHandler);
                if (cbCopyWalk.Checked) Proxy.Instance.RegisterHandler(CopyWalkHandler);
            }
        }

        private void cbStopAttack_CheckedChanged(object sender, EventArgs e)
        {
            // if (cbEnableGlobalHotkey.Checked == false) return;
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
                Root.Instance.maidStrip.ForeColor = Color.FromArgb(240, 50, 50);
            }
            else
            {
                Root.Instance.maidStrip.ForeColor = Color.FromArgb(220, 220, 220);
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
                switch (selectedMode)
                {
                    case "Gramiel L1":
                    case "Gramiel L2":
                        if (monId != 2)
                            return false;
                        crystalCount++;
                        Player.AttackMonster("id." + monId);
                        debug($"Defense shattering 'Left Crystal' count: {crystalCount}");
                        break;
                    case "Gramiel R1":
                    case "Gramiel R2":
                        if (monId != 3)
                            return false;
                        Player.AttackMonster("id." + monId);
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
            switch (selectedMode)
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
                    act = crystalCount % 2 != 0 || !msg.Contains("shattering");
                    break;
                case "Gramiel L2":
                case "Gramiel R2":
                    act = crystalCount % 2 == 0 || !msg.Contains("shattering");
                    break;
            }
            return act;
        }

        private void cmbUltraBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectedMode)
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
            switch (selectedPreset)
            {
                case "EC":
                    ClassPreset.EC();
                    break;
                case "DOT":
                    ClassPreset.DOT();
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
                    ClassPreset.CSH_Gunslinger();
                    break;
                case "CSH v2":
                    ClassPreset.CSHNormal();
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
        #region configSave_Load
        private MaidConfig saveConfig()
        {
            return new MaidConfig
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
        }
        private void btnSave_Click(object sender, EventArgs e)
        {


            string configFolder = Path.Combine(Application.StartupPath, "Config");
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save config";
                saveFileDialog.InitialDirectory = configFolder;
                saveFileDialog.Filter = "Maid config|*.json";
                saveFileDialog.DefaultExt = ".json";
                saveFileDialog.CheckFileExists = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(saveConfig(), Formatting.Indented));
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
            string configFolder = Path.Combine(Application.StartupPath, "Config");
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load config";
                openFileDialog.InitialDirectory = configFolder;
                openFileDialog.Filter = "Maid config|*.json";
                openFileDialog.DefaultExt = ".json";
                if (openFileDialog.ShowDialog() == DialogResult.OK &&
                    TryDeserialize(File.ReadAllText(openFileDialog.FileName), out MaidConfig config))
                {
                    string file_name = openFileDialog.SafeFileName;
                    ApplyConfig(config, file_name);
                }
            }
        }
        private void AutoLoadConfig()
        {
            string configFolder = Path.Combine(Application.StartupPath, "Config");
            string autoConfig = Path.Combine(configFolder, "autoload.json");

            if (!File.Exists(autoConfig))
                return;

            if (!TryDeserialize(File.ReadAllText(autoConfig), out MaidConfig config))
                return;

            ApplyConfig(config, Path.GetFileName(autoConfig));
        }
        private void ApplyConfig(MaidConfig config, string fileName)
        {
            gbConfig.Text = $"Config : {fileName}";
            if (!string.IsNullOrEmpty(config.LockedZoneHandlerMaps))
                LockedMapForm.Instance.tbLockedMapAlternative.Text = config.LockedZoneHandlerMaps;
            if (!string.IsNullOrEmpty(config.WhitelistMapMaps))
                WhitelistMapForm.Instance.tbWhitelistMap.Text = config.WhitelistMapMaps;
            if (!string.IsNullOrEmpty(config.SkillList))
                tbSkillList.Text = config.SkillList;
            if (!string.IsNullOrEmpty(config.BuffStopAttackList))
                tbBuffSkill.Text = config.BuffStopAttackList;
            if (!string.IsNullOrEmpty(config.Target))
                cmbGotoUsername.Text = config.Target;
            if (!string.IsNullOrEmpty(config.SafeSkillList))
                tbHealSkill.Text = config.SafeSkillList;
            if (!string.IsNullOrEmpty(config.AttackPriorityMonster))
                tbAttPriority.Text = config.AttackPriorityMonster;
            if (!string.IsNullOrEmpty(config.SpecialMsg))
                tbSpecialMsg.Text = config.SpecialMsg;

            if (config.SkillDelay.HasValue)
                numSkillDelay.Value = config.SkillDelay.Value;
            if (config.UltraBossExtra.HasValue)
                cmbUltraBoss.SelectedIndex = config.UltraBossExtra.Value;
            if (config.RelogDelay.HasValue)
                numRelogDelay.Value = config.RelogDelay.Value;
            if (config.SafeSkillHP.HasValue)
                numHealthPercent.Value = config.SafeSkillHP.Value;
            if (config.SpecialAct.HasValue)
                numSkillAct.Value = config.SpecialAct.Value;

            cbWaitSkill.Checked = config.WaitSkill ?? cbWaitSkill.Checked;
            cbStopIf.Checked = config.StopFailedGoto ?? cbStopIf.Checked;
            cbHandleLockedMap.Checked = config.LockedZoneHandler ?? cbHandleLockedMap.Checked;
            cbWhitelistMap.Checked = config.WhitelistMap ?? cbWhitelistMap.Checked;
            cbEnableGlobalHotkey.Checked = config.GlobalHotkey ?? cbEnableGlobalHotkey.Checked;
            cbUseHeal.Checked = config.SafeSkill ?? cbUseHeal.Checked;
            cbBuffIfStop.Checked = config.BuffStopAttack ?? cbBuffIfStop.Checked;
            cbAttackPriority.Checked = config.AttackPriority ?? cbAttackPriority.Checked;
            cbCopyWalk.Checked = config.CopyWalk ?? cbCopyWalk.Checked;
            cbAntiCounter.Checked = config.AntiCounter ?? cbAntiCounter.Checked;
            /*cmbUltraBoss.SelectedIndex = config.UltraBossExtra;
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
			cbAntiCounter.Checked = config.AntiCounter;*/
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
        #endregion
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
            antiCounter();
        }
        private void antiCounter()
        {
            Flash.FlashCall2 -= AntiCounterHandler;
            if (cbAntiCounter.Checked)
                Flash.FlashCall2 += AntiCounterHandler;
        }
        private void cbSpecialAnims_CheckedChanged(object sender, EventArgs e)
        {
            tbSpecialMsg.Enabled = cbSpecialAnims.Checked;
            numSkillAct.Enabled = cbSpecialAnims.Checked;
        }
    }
}
