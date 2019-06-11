﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    [Serializable]
    public class Start: ActionSquare
    {
        public Start(int squareId, string name, int r, int g, int b) : base(squareId, name, r, g, b)
        {
            
        }

        public override void Action(Player player)
        {
            player.Cash += 200;
        }
    }
}
