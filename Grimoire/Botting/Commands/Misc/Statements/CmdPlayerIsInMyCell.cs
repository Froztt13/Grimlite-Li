﻿using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;
using System.Text.RegularExpressions;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsInMyCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsInMyCell()
        {
            Tag = "Player";
            Text = "Player is in my cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string reqs;
            if ( instance.IsVar(Value1) )
            {
                reqs = Flash.Call<string>("GetCellPlayers", new string[] { Configuration.Tempvariable[instance.GetVar(Value1)] });
            }
            else
            {
                reqs = Flash.Call<string>("GetCellPlayers", new string[] { Value1 });
            }

            bool isExists = bool.Parse(reqs);

            if (!isExists)
            {
                instance.Index++;
            }

            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is in my cell: " + Value1;
        }
    }
}