﻿using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsInCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsInCell()
        {
            Tag = "Player";
            Text = "Player is in cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string Val1;
            string Val2;

            if ( instance.IsVar(Value1) )
            {
                Val1 = Configuration.Tempvariable[instance.GetVar(Value1)];
            } 
            else
            {
                Val1 = Value1;
            }

            if (instance.IsVar(Value2))
            {
                Val2 = Configuration.Tempvariable[instance.GetVar(Value2)];
            }
            else
            {
                Val2 = Value2;
            }

            string reqs = Flash.Call<string>("CheckCellPlayer", new string[] {
                Val1,
                Val2
            });
            bool isExists = bool.Parse(reqs);

            if (!isExists)
            {
                instance.Index++;
            }

            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is in cell: " + Value1;
        }
    }
}