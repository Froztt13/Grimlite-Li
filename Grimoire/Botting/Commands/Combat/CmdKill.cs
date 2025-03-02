using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grimoire.Botting.Commands.Combat
{
	public class CmdKill : IBotCommand
	{
		public string Monster { get; set; }
		public string KillPriority { get; set; } = "";
		public bool AntiCounter { get; set; } = false;

		private bool onPause = false;

		public async Task Execute(IBotEngine instance)
		{
			BotData.BotState = BotData.State.Combat;

			onPause = false;

			if (instance.Configuration.SkipAttack)
			{
				if (Player.HasTarget) Player.CancelTarget();
				return;
			}

			string Monster = instance.IsVar(this.Monster) ? Configuration.Tempvariable[instance.GetVar(this.Monster)] : this.Monster;

			await instance.WaitUntil(() => World.IsMonsterAvailable(Monster), null, 3);

			if (instance.Configuration.WaitForAllSkills)
			{
				await Task.Delay(Player.AllSkillsAvailable);
			}

			if (!instance.IsRunning || !Player.IsAlive || !Player.IsLoggedIn)
				return;

			bool disableAnims = OptionsManager.DisableAnimations;
			if (AntiCounter)
			{
				OptionsManager.DisableAnimations = false;
				Flash.FlashCall += AntiCounterHandler;
			}

			//Console.WriteLine("Mon:" + Monster);
			Player.AttackMonster(Monster);

			if (instance.Configuration.Skills.Count > 0)
				Task.Run(() => UseSkillsSet(instance));

			await instance.WaitUntil(() => !Player.HasTarget && !onPause, null, 360);
			Player.CancelTarget();

			if (AntiCounter)
			{
				OptionsManager.DisableAnimations = disableAnims;
				Flash.FlashCall -= AntiCounterHandler;
			}

			_cts?.Cancel(false);
		}

		private CancellationTokenSource _cts;

		private int _skillIndex;

		private int Index;
		private async Task UseSkillsSet(IBotEngine instance)
		{
			this._cts = new CancellationTokenSource();
			int ClassIndex = -1;
			bool flag = BotData.SkillSet != null && BotData.SkillSet.ContainsKey("[" + BotData.BotSkill + "]");
			if (flag)
			{
				ClassIndex = BotData.SkillSet["[" + BotData.BotSkill + "]"] + 1;
			}
			int Count = instance.Configuration.Skills.Count - 1;
			this.Index = ClassIndex;

			bool hasTarget = Player.HasTarget;
			while (!this._cts.IsCancellationRequested && !onPause && hasTarget)
			{
				switch (this.Monster.ToLower())
				{
					case "escherion":
						if (World.IsMonsterAvailable("Staff of Inversion"))
							Player.AttackMonster("Staff of Inversion");
						break;
					case "vath":
						if (World.IsMonsterAvailable("Stalagbite"))
							Player.AttackMonster("Stalagbite");
						break;
					case "ultra avatar tyndarius":
						if (World.IsMonsterAvailable("Ultra Fire Orb"))
							Player.AttackMonster("Ultra Fire Orb");
						break;
				}

				if (KillPriority.Length > 0)
				{
					List<string> priorities = new List<string>();
					if (KillPriority.Contains(","))
					{
						foreach (string p in KillPriority.Split(','))
						{
							priorities.Add(p);
						}
					}
					else
					{
						priorities.Add(KillPriority);
					}

					foreach (string p in priorities)
					{
						if (World.IsMonsterAvailable(p))
						{
							Player.AttackMonster(p);
							break;
						}
					}
				}

				if (ClassIndex != -1)
				{
					//with label
					Skill s = instance.Configuration.Skills[this.Index];
					if (s.Type == Skill.SkillType.Label)
					{
						this.Index = ClassIndex;
						continue;
					}

					if (instance.Configuration.WaitForSkill)
					{
						BotManager.Instance.OnSkillIndexChanged(Index);
						await Task.Delay(Player.SkillAvailable(s.Index));
					}

					s.ExecuteSkill();

					int index;
					if (this.Index < Count)
					{
						int num3 = this.Index + 1;
						this.Index = num3;
						index = num3;
					}
					else
					{
						index = ClassIndex;
					}
					this.Index = index;
					s = null;
				}
				else
				{
					//non label
					Skill s = instance.Configuration.Skills[_skillIndex];

					if (instance.Configuration.WaitForSkill)
					{
						BotManager.Instance.OnSkillIndexChanged(Index);
						await Task.Delay(Player.SkillAvailable(s.Index));
					}

					s.ExecuteSkill();

					int count = instance.Configuration.Skills.Count - 1;

					_skillIndex = _skillIndex >= count ? 0 : ++_skillIndex;
					await Task.Delay(instance.Configuration.SkillDelay);
				}
				await Task.Delay(instance.Configuration.SkillDelay);
				hasTarget = Player.HasTarget;
			}

			if (Player.HasTarget)
			{
				Player.CancelTarget();
			}
		}

		private void AntiCounterHandler(AxShockwaveFlashObjects.AxShockwaveFlash flash, string function, params object[] args)
		{
			string msg = args[0].ToString();
			if (!msg.StartsWith("{")) return;
			if (function == "pext")
			{
				dynamic packet = JsonConvert.DeserializeObject<dynamic>(msg);
				string type = packet["params"].type;
				dynamic data = packet["params"].dataObj;
				if (type == "json")
					if (data.cmd == "ct")
					{
						JArray anims = (JArray)data.anims;
						if (anims != null)
							if (anims[0]["msg"].ToString().ToLower().Contains("prepares a counter attack"))
							{
								Player.CancelAutoAttack();
								Player.CancelTarget();
								onPause = true;
								Console.WriteLine("Counter Attack: active");
							}
						JArray a = (JArray)data.a;
						if (a != null)
							foreach (JObject aura in a)
							{
								JObject aura2 = (JObject)aura["aura"];
								if (aura2.GetValue("nam")?.ToString() == "Counter Attack" && aura.GetValue("cmd")?.ToString() == "aura--")
								{
									onPause = false;
									Console.WriteLine("Counter Attack: fades");
									break;
								}
							}
					}
			}
		}

		public override string ToString()
		{
			return $"Kill {Monster}";
		}
	}
}
