using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Tools;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
				Flash.FlashCall2 += AntiCounterHandler;
			}

			//Console.WriteLine("Mon:" + Monster);
			Player.AttackMonster(Monster);

			if (instance.Configuration.Skills.Count > 0)
				await UseSkillsSet(instance);

			await instance.WaitUntil(() => !Player.HasTarget && !onPause, timeout:20);
			Player.CancelTarget(); //timeout increased to 20 for Autoattack/empty skills users

			if (AntiCounter)
			{
				OptionsManager.DisableAnimations = disableAnims;
				Flash.FlashCall2 -= AntiCounterHandler;
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

			while (!this._cts.IsCancellationRequested && !onPause && Player.HasTarget)
			{
				switch (this.Monster.ToLower())
				{
					case "escherion":
						if (World.IsMonsterAvailable("Staff of Inversion"))
							Player.AttackMonster("Staff of Inversion");
						break;
					case "commander gallaeon":
						if (World.IsMonsterAvailable("hydra crew"))
							Player.AttackMonster("hydra crew");
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

					if (instance.Configuration.WaitForSkill || s.Type == Skill.SkillType.Wait)
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
				}
				else
				{
					//non label
					
					Skill s = instance.Configuration.Skills[_skillIndex];
                    //LogForm.Instance.AppendDebug($"Trying to execute Skill-{s.Index} at index {_skillIndex}/{Count}");
                    if (instance.Configuration.WaitForSkill)
					{
						BotManager.Instance.OnSkillIndexChanged(Index);
						await Task.Delay(Player.SkillAvailable(s.Index));
					}

					s.ExecuteSkill();

					int count = instance.Configuration.Skills.Count - 1;

					_skillIndex = _skillIndex >= count ? 0 : ++_skillIndex;
				}
				await Task.Delay(instance.Configuration.SkillDelay);
			}

			if (Player.HasTarget)
			{
				Player.CancelTarget();
			}
		}

		private void AntiCounterHandler(string function, params object[] args)
		{
			if (function != "packetFromServer") return;
			try
			{
				Message message = NetworkUtils.CreateMessage((string)args[0]);
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
										Player.CancelAutoAttack();
										Player.CancelTarget();
										onPause = true;
										Console.WriteLine("Counter Attack: active");
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
							foreach (JObject aura in a)
							{
								JObject aura2 = (JObject)aura["aura"];
								if (aura2?["nam"]?.ToString() == "Counter Attack" && aura.GetValue("cmd")?.ToString().Contains("aura-") == true)
								{
									onPause = false;
									Console.WriteLine("Counter Attack: fades");
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"err: {e}");
			}
		}

		public override string ToString()
		{
			return $"Kill {Monster}";
		}
	}
}
