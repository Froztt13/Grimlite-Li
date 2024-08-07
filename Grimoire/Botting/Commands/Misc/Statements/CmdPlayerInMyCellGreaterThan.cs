using Grimoire.Game;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerInMyCellGreaterThan : StatementCommand, IBotCommand
    {
        public CmdPlayerInMyCellGreaterThan()
        {
            Tag = "Player";
            Text = "Player count is greater than [cell]";
        }

        public Task Execute(IBotEngine instance)
        {
            int count = 0;
            List<string> playerInMap = World.PlayersInMap;
            foreach (string pl in playerInMap)
            {
                string reqs = Flash.Call<string>("CheckCellPlayer", new string[] {
                    pl,
                    Player.Cell
                });
                if (bool.Parse(reqs))
                {
                    count++;
                }
            }

            if (count <= int.Parse((instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player count greater than [cell]: " + Value1;
        }
    }
}