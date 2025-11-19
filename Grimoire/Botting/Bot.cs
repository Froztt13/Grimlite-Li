using Grimoire.Botting.Commands.Misc;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public class Bot : IBotEngine
    {
        public bool IsVar(string value)
        {
            return Regex.IsMatch(value, @"\[([^\)]*)\]");
        }

        public string GetVar(string value)
        {
            return Regex.Replace(value, @"[\[\]']+", "");
        }

        public string ResolveVars(string value)
        {
            // Match and replace all Tempvariable ex : "[var1]*[var2]"
            string replaced = Regex.Replace(value, @"\[(.*?)\]", match =>
            {
                string key = match.Groups[1].Value;
                if (Configuration.Tempvariable.ContainsKey(key))
                    return Configuration.Tempvariable[key];
                if (Configuration.Tempvalues.ContainsKey(key))
                    return Configuration.Tempvalues[key].ToString();

                LogForm.Instance.AppendDebug($"[Var Error] Key for [{match.Value}] not found");
                return match.Value;
            });

            // Try to evaluate if the whole string is math operation (only support -,+,*,/)
            string evaluated = TryEvaluateExpression(replaced);

            //LogForm.Instance.AppendDebug($"Raw : {value}\r\nReplaced : {replaced}\r\nEvaluated/Final : {evaluated}");
            return evaluated;
        }

        private string TryEvaluateExpression(string input)
        {
            try
            {
                // kalau input hanya angka atau ekspresi matematis
                if (Regex.IsMatch(input, @"^[0-9\+\-\*\/\.\(\)\s]+$"))
                {
                    var dt = new DataTable();
                    var result = dt.Compute(input, "");
                    return result.ToString();
                }
            }
            catch { }
            return input;
        }

        public static Bot Instance = new Bot();

        private int _index;

        private Configuration _config;

        private bool _isRunning;

        private CancellationTokenSource _ctsQuestList;

        private bool _onCompletingQuest = false;

        private CancellationTokenSource _ctsBot;

        private Stopwatch _questDelayCounter;

        private Stopwatch _boostDelayCounter;

        private List<string> _bsLabels = new List<string>();

        private string lastCommand = null;

        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = (value < Configuration.Commands.Count) ? value : 0;
            }
        }

        public Configuration Configuration
        {
            get
            {
                return _config;
            }
            set
            {
                if (value != _config)
                {
                    _config = value;
                    this.ConfigurationChanged?.Invoke(_config);
                }
            }
        }

        public static Dictionary<int, Configuration> Configurations = new Dictionary<int, Configuration>();

        public static Dictionary<int, int> OldIndex = new Dictionary<int, int>();

        public int CurrentConfiguration { get; set; } = 0;

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.IsRunningChanged?.Invoke(_isRunning);
            }
        }

        public event Action<bool> IsRunningChanged;

        public event Action<int> IndexChanged;

        public event Action<Configuration> ConfigurationChanged;

        public void Start(Configuration config)
        {
            IsRunning = true;
            Configuration = config;
            Index = 0;
            BotData.BotState = BotData.State.Others;
            _ctsBot = new CancellationTokenSource();
            _questDelayCounter = new Stopwatch();
            _boostDelayCounter = new Stopwatch();
            World.ItemDropped += OnItemDropped;
            Player.Quests.QuestsLoaded += OnQuestsLoaded;
            Player.Quests.QuestCompleted += OnQuestCompleted;
            _questDelayCounter.Start();
            this.LoadAllQuests();
            this.LoadBankItems();
            CheckBoosts();
            _boostDelayCounter.Start();
            OptionsManager.Start();
            Task.Factory.StartNew(Activate, _ctsBot.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            BotData.BotMap = null;
            BotData.BotCell = null;
            BotData.BotPad = null;
            BotData.BotSkill = null;
            BotData.BotState = BotData.State.Others;
            BotData.SkillSet.Clear();
            for (int i = 0; i < Configuration.Skills.Count; i++)
            {
                if (Configuration.Skills[i].Type == Skill.SkillType.Label)
                {
                    BotData.SkillSet.Add(Configuration.Skills[i].Text.ToUpper(), i);
                }
            }
        }

        public void Stop()
        {
            _ctsBot?.Cancel(throwOnFirstException: false);
            World.ItemDropped -= OnItemDropped;
            Player.Quests.QuestsLoaded -= OnQuestsLoaded;
            Player.Quests.QuestCompleted -= OnQuestCompleted;
            _questDelayCounter.Stop();
            _boostDelayCounter.Stop();
            OptionsManager.Stop();
            StopBackGroundSpammer();
            IsRunning = false;
			_onCompletingQuest = false;
            paused = false;
            BotData.BotState = BotData.State.Others;
            this.StopCommands();
        }
        public bool paused
        {
            get;
            set;
        }
        private async Task Activate()
        {
            if (Configuration.Quests.Count > 0)
                StartQuestList();

            while (!_ctsBot.IsCancellationRequested)
            {
                if (Player.IsLoggedIn && !Player.IsAlive)
                {
                    World.SetSpawnPoint();
                    await this.WaitUntil(() => Player.IsAlive, () => IsRunning && Player.IsLoggedIn, -1);
                    await Task.Delay(1000);
                    Index = Configuration.RestartUponDeath ? 0 : Index - 1;
                }

                if (!Player.IsLoggedIn)
                {
                    LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Disconnected. Last cmd: [{Index}]{lastCommand}");
                    StopQuestList();
                    StopBackGroundSpammer();
                    World.LoadedShops.Clear();

                    if (Configuration.AutoRelogin)
                    {
                        bool infiniteRange = OptionsManager.InfiniteRange;
                        bool provoke = OptionsManager.ProvokeMonsters;
                        bool lagKiller = OptionsManager.LagKiller;
                        bool skipCutscene = OptionsManager.SkipCutscenes;
                        bool playerAnim = OptionsManager.DisableAnimations;
                        bool enemyMagnet = OptionsManager.EnemyMagnet;
                        bool reloginOnAFK = OptionsManager.AFK;

                        OptionsManager.Stop();
                        await AutoRelogin.Login(Configuration.Server, Configuration.RelogDelay, _ctsBot, Configuration.RelogRetryUponFailure);
                        Index = 0;
                        this.LoadAllQuests();
                        this.LoadBankItems();
                        if (Configuration.Quests.Count > 0)
                            StartQuestList();
                        LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Relogin success.");

                        OptionsManager.InfiniteRange = infiniteRange;
                        OptionsManager.ProvokeMonsters = provoke;
                        OptionsManager.LagKiller = lagKiller;
                        OptionsManager.SkipCutscenes = skipCutscene;
                        OptionsManager.DisableAnimations = playerAnim;
                        OptionsManager.EnemyMagnet = enemyMagnet;
                        OptionsManager.AFK = reloginOnAFK;
                        OptionsManager.Start();
                    }
                    else
                    {
                        Stop();
                        return;
                    }
                }

                if (_ctsBot.IsCancellationRequested)
                    return;

                if (Configuration.RestIfHp)
                    await RestHealth();

                if (_ctsBot.IsCancellationRequested)
                    return;

                if (Configuration.RestIfMp)
                    await RestMana();

                if (_ctsBot.IsCancellationRequested)
                    return;

                IndexChanged?.Invoke(Index);
                IBotCommand cmd = Configuration.Commands[Index];
                if (cmd is CmdBackgroundPacket)
                {
                    ToggleSpammer(cmd);
                }
                else
                {
                    lastCommand = cmd.ToString();
                    while (paused) //Might be useful for handler uses in the future
                        await Task.Delay(100);
                    await cmd.Execute(this);
                }

                if (_ctsBot.IsCancellationRequested)
                    return;

                if (Configuration.BotDelay > 0 &&
                    (!Configuration.SkipDelayIndexIf || Configuration.SkipDelayIndexIf && cmd.RequiresDelay()))
                    await Task.Delay(_config.BotDelay);

                if (_ctsBot.IsCancellationRequested)
                    return;

                if (Configuration.Boosts.Count > 0)
                    CheckBoosts();

                if (!_ctsBot.IsCancellationRequested)
                    Index++;
            }
        }

        private Dictionary<int, int> qFailures = new Dictionary<int, int>();

        public async void StartQuestList()
        {
            if (_ctsQuestList != null && !_ctsQuestList.IsCancellationRequested)
            {
                LogForm.Instance.devDebug($"QuestList Restarted");
                StopQuestList();
            }

            _ctsQuestList = new CancellationTokenSource();
            var token = _ctsQuestList.Token;

            int questDelay = (int)BotManager.Instance.numQuestDelay.Value;

            if (Configuration.Quests.Count == 0)
                return;

            qFailures.Clear();
            foreach (var quest in Configuration.Quests)
            {
                qFailures.Add(quest.Id, 0);
            }

            try
            {
                while (!_ctsBot.IsCancellationRequested && !token.IsCancellationRequested && Player.IsLoggedIn)
                {
                    Quest quest = Configuration.Quests.FirstOrDefault((Quest q) => q.CanComplete);
                    if (quest != null)
                    {
                        BotData.State TempState = BotData.BotState;
                        BotData.BotState = BotData.State.Quest;
                        _onCompletingQuest = true;

                        quest.Complete();
                        await Task.Delay(questDelay, token);

                        if (quest.CanComplete)
                        {
                            int f = qFailures[quest.Id];
                            if (f >= 5)
                            {
                                Player.Logout();
                                LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Failed to complete quest [{quest.Id}] {f} times.");
                            }
                            else
                            {
                                qFailures[quest.Id] = f++;
                                Console.WriteLine($"qFailures[{quest.Id}] : {f++}");
                            }
                        }
                        else
                        {
                            qFailures[quest.Id] = 0;
                        }

                        BotData.BotState = TempState;
                        _onCompletingQuest = false;
                    }
                    else
                    {
                        await Task.Delay(questDelay, token);
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        public void StopQuestList()
        {
			_onCompletingQuest = false;
            _ctsQuestList?.Cancel();
            _ctsQuestList?.Dispose();
        }

        public void StopBackGroundSpammer()
        {
            _bsLabels.Clear();
        }

        private async void ToggleSpammer(IBotCommand cmd)
        {
            CmdBackgroundPacket bSpammer = (CmdBackgroundPacket)cmd;
            switch (bSpammer.ActionW)
            {
                case CmdBackgroundPacket.Action.ADD:
                    _bsLabels.Add(bSpammer.Label);
                    break;

                case CmdBackgroundPacket.Action.STOP:
                    _bsLabels.RemoveAll(label => label == bSpammer.Label);
                    break;
            }

            while (_bsLabels.Contains(bSpammer.Label) && bSpammer.Packet != null)
            {
                if (!_onCompletingQuest)
                {
                    await Proxy.Instance.SendToServer(bSpammer.Packet);
                    await Task.Delay(bSpammer.Delay);
                }
            }
        }

        private async Task RestHealth()
        {
            if (Player.Health / (double)Player.HealthMax <= Configuration.RestHp / 100.0)
            {
                BotData.State TempState = BotData.BotState;
                BotData.BotState = BotData.State.Rest;
                bool provokeMons = this.Configuration.ProvokeMonsters;
                if (provokeMons) this.Configuration.ProvokeMonsters = false;
                if (Configuration.ExitCombatBeforeRest)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(2000);
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Health >= Player.HealthMax);
                BotData.BotState = TempState;
                if (provokeMons) this.Configuration.ProvokeMonsters = true;
            }
        }

        private async Task RestMana()
        {
            if (Player.Mana / (double)Player.ManaMax <= Configuration.RestMp / 100.0)
            {
                BotData.State TempState = BotData.BotState;
                BotData.BotState = BotData.State.Rest;
                bool provokeMons = this.Configuration.ProvokeMonsters;
                if (provokeMons) this.Configuration.ProvokeMonsters = false;
                if (Configuration.ExitCombatBeforeRest)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(2000);
                }
                Player.Rest();
                await this.WaitUntil(() => Player.Mana >= Player.ManaMax);
                BotData.BotState = TempState;
                if (provokeMons) this.Configuration.ProvokeMonsters = true;
            }
        }

        private void CheckBoosts()
        {
            if (_boostDelayCounter.ElapsedMilliseconds >= 10000)
            {
                foreach (InventoryItem boost in Configuration.Boosts)
                {
                    if (!Player.HasActiveBoost(boost.Name))
                    {
                        Player.UseBoost(boost.Id);
                    }
                }
                _boostDelayCounter.Restart();
            }
        }

        private void OnItemDropped(InventoryItem drop)
        {
            NotifyDrop(drop);
            bool flag = Configuration.Drops.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase));
            if (Configuration.EnablePickupAll)
            {
                World.DropStack.GetDrop(drop.Id);
            }
            else if (Configuration.EnablePickup && flag)
            {
                World.DropStack.GetDrop(drop.Id);
            }

            if (Configuration.EnablePickupAcTagged)
            {
                if (drop.IsAcItem)
                {
                    World.DropStack.GetDrop(drop.Id);
                }
            }
        }

        private void NotifyDrop(InventoryItem drop)
        {
            if (Configuration.NotifyUponDrop.Count > 0 && Configuration.NotifyUponDrop.Any((string d) => d.Equals(drop.Name, StringComparison.OrdinalIgnoreCase)))
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Beep();
                }
            }
        }

		private void OnQuestsLoaded(List<Quest> quests)
		{
			var matchingQuests = quests
				.Where(q => Configuration.Quests.Any(configQuest => configQuest.Id == q.Id))
				.ToList();
			switch (matchingQuests.Count)
			{
				case 0:
					return;

				case 1:
					matchingQuests[0].Accept();
					return;

				default:
					// for better performance, use GhostAccept for multiple quests accepting
					for (int i = 0; i < matchingQuests.Count; i++)
					{
						int index = i;
						Task.Run(async () =>
						{
							await Task.Delay(700 * index);
							matchingQuests[index].GhostAccept();
						});
					}
					break;
			}
		}

		private void OnQuestCompleted(CompletedQuest quest)
        {
            Task.Run(async () =>
            {
                await Task.Delay(600);
                Configuration.Quests.FirstOrDefault((Quest q) => q.Id == quest.Id)?.GhostAccept();
            });
        }
    }
}