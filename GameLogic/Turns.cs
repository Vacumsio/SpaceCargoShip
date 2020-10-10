using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Turns
    {
        public int CurrentTurn { get; private set; }

        internal void NextTurn()
        {
            CurrentTurn++;
        }
    }
}
