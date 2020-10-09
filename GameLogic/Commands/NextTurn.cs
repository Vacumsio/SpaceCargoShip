using System;

namespace GameLogic.Commands
{
    public class NextTurn : Command
    {
        protected override bool Run()
        {
            // Именно тут будет вся логика хода

            Core.Turns.NextTurn();
            return true;
        }

        internal void Execute(object сore)
        {
            throw new NotImplementedException();
        }
    }
}
