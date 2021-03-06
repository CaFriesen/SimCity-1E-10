﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    [Serializable]
    public class GoToJail: ActionSquare
    {
        public override string Info
        {
            get
            {
                return "You get sent to jail!";
            }
        }
        public GoToJail(int squareId, string name, int r, int g, int b) : base(squareId, name, r, g, b)
        {
        }

        public override void Action(Player player)
        {
            player.Jailed = true;
        }
    }
}
