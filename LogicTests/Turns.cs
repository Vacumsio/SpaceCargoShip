using GameLogic;
using GameLogic.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class Turns
    {
        [TestMethod]
        public void NextTurnsCommand()
        {
            var core = new Core();

            Assert.AreEqual(0, core.Turns.CurrentTurn);

            Assert.IsTrue(
                new NextTurnCount(4)
                    .Execute(core)
                    .IsValid
            );

            Assert.AreEqual(4, core.Turns.CurrentTurn);
        }
    }


}
