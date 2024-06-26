using AxShockwaveFlashObjects;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grimoire.Botting
{
    public static class BotUtilities
    {
        public static ManualResetEvent BankLoadEvent;

        public static AxShockwaveFlash flash;

        public static async Task WaitUntil(this IBotEngine instance, Func<bool> condition, Func<bool> prerequisite = null, int timeout = 15)
        {
            int iterations = 0;
            while ((prerequisite ?? (() => instance.IsRunning && Player.IsLoggedIn && Player.IsAlive))() && !condition() && (iterations < timeout || timeout == -1))
            {
                await Task.Delay(1000);
                iterations++;
            }
        }

        public static bool RequiresDelay(this IBotCommand cmd)
        {
            if (cmd is StatementCommand || cmd is CmdIndex || cmd is CmdLabel || cmd is CmdGotoLabel || cmd is CmdBlank || cmd is CmdSkillSet)
                return false;
            return true;
        }

        public static void LoadAllQuests(this IBotEngine instance)
        {
            List<int> list = new List<int>();
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command is CmdAcceptQuest cmdAcceptQuest)
                {
                    list.Add(cmdAcceptQuest.Quest.Id);
                }
                else if (command is CmdCompleteQuest cmdCompleteQuest)
                {
                    list.Add(cmdCompleteQuest.Quest.Id);
                }
                else if (command is CmdAddQuestList cmdAddQuestList)
                {
                    list.Add(cmdAddQuestList.Id);
                }
            }
            list.AddRange(instance.Configuration.Quests.Select((Quest q) => q.Id));
            if (list.Count > 0)
            {
                Player.Quests.Get(list);
            }
        }


        public static async void StopCommands(this IBotEngine instance)
        {
            foreach (IBotCommand command in instance.Configuration.Commands)
            {
                if (command is CmdAddQuestList cmdAddQuestList)
                {
                    var remove = new CmdRemoveQuestList
					{
                        Id = cmdAddQuestList.Id,
                        ItemId = cmdAddQuestList.ItemId,
                        SafeRelogin = cmdAddQuestList.SafeRelogin,
					};
					await remove.Execute(instance);
                }
            }
        }

        public static void LoadBankItems(this IBotEngine instance)
        {
			if (instance.Configuration.Commands.Any((IBotCommand c) =>
				c is CmdBankSwap || 
                c is CmdBankTransfer || 
                c is CmdInBank || 
                c is CmdNotInBank || 
                c is CmdInBankOrInvent || 
                c is CmdNotInBankAndInvent || 
                c is CmdBankList))
			{
				//Player.Bank.LoadItems();
                Player.Bank.GetBank();
            }
        }

        static BotUtilities()
        {
            BankLoadEvent = new ManualResetEvent(initialState: false);
        }
    }
}