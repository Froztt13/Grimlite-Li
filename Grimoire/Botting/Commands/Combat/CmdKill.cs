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

        public async Task Execute(IBotEngine instance) => await Execute(instance, CancellationToken.None);
        public async Task Execute(IBotEngine instance, CancellationToken token)
        {
            BotData.BotState = BotData.State.Combat;

            onPause = false;

            if (instance.Configuration.SkipAttack)
            {
                if (Player.HasTarget) Player.CancelTarget();
                return;
            }

            string Monster = instance.IsVar(this.Monster) ? Configuration.Tempvariable[instance.GetVar(this.Monster)] : this.Monster;

            //waiting monster to respawn for 3s
            await instance.WaitUntil(() => World.IsMonsterAvailable(Monster), timeout: 3);

            if (instance.Configuration.WaitForAllSkills)
                await Task.Delay(Player.AllSkillsAvailable);

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

            _token = token;
            if (instance.Configuration.Skills.Count > 0)
                await UseSkillsSet(instance);
            await instance.WaitUntil(() => _token.IsCancellationRequested || !Player.HasTarget && !onPause, timeout: 40);
            if (Player.HasTarget)
                Player.CancelTarget();//timeout increased to 40 for Autoattack/empty skills users

            if (AntiCounter)
            {
                OptionsManager.DisableAnimations = disableAnims;
                Flash.FlashCall2 -= AntiCounterHandler;
            }
        }

        private static CancellationToken _token;

        private static int Index = 0;

        private static string _lastBotSkill;
        private async Task UseSkillsSet(IBotEngine instance)
        {
            int ClassIndex = 0;
            bool flag = BotData.SkillSet != null && BotData.SkillSet.ContainsKey("[" + BotData.BotSkill + "]");
            if (flag)
                ClassIndex = BotData.SkillSet["[" + BotData.BotSkill + "]"] + 1;

            //skill order here won't get reset unless preset changed
            int Count = instance.Configuration.Skills.Count - 1;
            if (Index > Count)
            {
                Index = ClassIndex;
                //LogForm.Instance.AppendDebug($"Index is higher than CurrentSkillcount, Sets index as {Index}");
            }
            if (_lastBotSkill != BotData.BotSkill)
            {// Reset index only when skillset changed
                if (ClassIndex != 0)
                {
                    Index = ClassIndex;
                }
                _lastBotSkill = BotData.BotSkill;
                //LogForm.Instance.AppendDebug($"Skillset Used  : {BotData.BotSkill}");
            }


            while (!_token.IsCancellationRequested && !onPause && Player.IsLoggedIn && Player.HasTarget && Player.GetTargetHealth() > 0)
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

                //Moved to ensure not causing crazy atk spam
                await Task.Delay(instance.Configuration.SkillDelay);

                Skill s = instance.Configuration.Skills[Index];
                if (s.Type == Skill.SkillType.Label)
                {
                    //Reset back when meet with another SkillLabel
                    Index = ClassIndex;
                    continue;
                }

                if (!Player.IsAlive)
                    return;

                if (!s.hasaura() && !s.waitCd)
                {
                    Index = (Index < Count) ? Index + 1 : ClassIndex;
                    continue;
                }

                if (instance.Configuration.WaitForSkill || s.waitCd)
                {
                    BotManager.Instance.OnSkillIndexChanged(Index);
                    await Task.Delay(Player.SkillAvailable(s.Index));
                }

                s.ExecuteSkill();

                if (s.waitCd && !s.hasaura())
                    await Task.Delay(200);

                //Reset back when reaching end of skill list index
                Index = (Index < Count) ? Index + 1 : ClassIndex;
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
