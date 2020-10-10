﻿using GameLogic;
using GameLogic.Architecture;
using GameLogic.Commands;
using GameLogic.Player;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LogicTests
{
    [TestClass]
    public class Architecture
    {
        [TestMethod]
        public void CorrectConstruction()
        {
            var core = new Core();
            var room = core.Ship.GetRoom(0);
            core.Bank.Change(ResourceType.Metal, 1000);

            Assert.AreEqual(BuildingType.Empty, room.Building.Type);
            Assert.AreEqual(0, room.Building.Modules.Count());

            Assert.IsTrue(
                new BuildingConstruct(
                    room,
                    core.Factory.ProduceBuilding(BuildingType.PowerPlant)
                )
                .Execute(core)
                .IsValid
            );

            room.Building.Constructible.Complete();

            Assert.AreEqual(BuildingType.PowerPlant, room.Building.Type);
            Assert.AreEqual(0, room.Building.Modules.Count());

            Assert.IsTrue(
                new ModuleConstruct(
                    room.Building,
                    core.Factory.ProduceModule(ModuleType.Generator),
                    2
                )
                .Execute(core)
                .IsValid
            );

            Assert.AreEqual(BuildingType.PowerPlant, room.Building.Type);
            Assert.AreEqual(ModuleType.Generator, room.Building.GetModule(2).Type);
            Assert.AreEqual(1, room.Building.Modules.Count());
        }
        [TestMethod]
        public void IncorrectConstruction()
        {
            var core = new Core();
            var room = core.Ship.GetRoom(0);
            core.Bank.Change(ResourceType.Metal, 1000);

            Assert.IsFalse(
                new BuildingConstruct(
                    room,
                    core.Factory.ProduceBuilding(BuildingType.Empty)
                )
                .Execute(core)
                .IsValid
            );

            Assert.IsFalse(
                new ModuleConstruct(
                    room.Building,
                    core.Factory.ProduceModule(ModuleType.Generator),
                    2
                )
                .Execute(core)
                .IsValid
            );

            new BuildingConstruct(
                room,
                core.Factory.ProduceBuilding(BuildingType.PowerPlant)
            )
            .Execute(core);

            room.Building.Constructible.Complete();

            Assert.IsFalse(
                new BuildingConstruct(
                    room,
                    core.Factory.ProduceBuilding(BuildingType.PowerPlant)
                )
                .Execute(core)
                .IsValid
            );

            Assert.IsFalse(
                new ModuleConstruct(
                    room.Building,
                    core.Factory.ProduceModule(ModuleType.Generator),
                    666
                )
                .Execute(core)
                .IsValid
            );
        }

        [TestMethod]
        public void CantConstructInWrongBuilding()
        {
            var core = new GameLogic.Core();
            var room = core.Ship.GetRoom(0);
            core.Bank.Change(ResourceType.Metal, 1000);

            new BuildingConstruct(
                room,
                core.Factory.ProduceBuilding(BuildingType.PowerPlant)
            )
            .Execute(core);

            room.Building.Constructible.Complete();

            Assert.IsFalse(
                new ModuleConstruct(
                    room.Building,
                    core.Factory.ProduceModule(ModuleType.Furnace),
                    2
                )
                .Execute(core)
                .IsValid
            );

            Assert.AreEqual(null, room.Building.GetModule(2));
        }

        [TestMethod]
        public void ModulesLimits()
        {
            var core = new GameLogic.Core();
            var roomRoboport = core.Ship.GetRoom(0);
            var roomPowerPlant = core.Ship.GetRoom(1);
            core.Bank.Change(ResourceType.Metal, 1000);


            Assert.IsTrue(
                new BuildingConstruct(
                    roomRoboport,
                    core.Factory.ProduceBuilding(BuildingType.Roboport)
                )
                .Execute(core)
                .IsValid
            );

            Assert.IsTrue(
                new BuildingConstruct(
                    roomPowerPlant,
                    core.Factory.ProduceBuilding(BuildingType.PowerPlant)
                )
                .Execute(core)
                .IsValid
            );

            roomRoboport.Building.Constructible.Complete();
            roomPowerPlant.Building.Constructible.Complete();

            Assert.IsFalse(
                new ModuleConstruct(
                    roomRoboport.Building,
                    core.Factory.ProduceModule(ModuleType.Miner),
                    3
                )
                .Execute(core)
                .IsValid
            );

            Assert.IsTrue(
                new ModuleConstruct(
                    roomPowerPlant.Building,
                    core.Factory.ProduceModule(ModuleType.Generator),
                    3
                )
                .Execute(core)
                .IsValid
            );
        }

        [TestMethod]
        public void CantConstructInUncompleteBuilding()
        {
            var core = new GameLogic.Core();
            var room = core.Ship.GetRoom(0);

            new BuildingConstruct(
                room,
                core.Factory.ProduceBuilding(BuildingType.PowerPlant)
            )
            .Execute(core);

            Assert.IsFalse(
                new ModuleConstruct(
                    room.Building,
                    core.Factory.ProduceModule(ModuleType.Generator),
                    2
                )
                .Execute(core)
                .IsValid
            );
        }

        [TestMethod]
        public void Constructible()
        {
            const int smelteryTime = 10;
            const int furnaceTime = 6;

            var core = new GameLogic.Core();
            var room = core.Ship.GetRoom(0);
            core.Bank.Change(ResourceType.Metal, 1000);

            // Smeltery

            new BuildingConstruct(
                room,
                core.Factory.ProduceBuilding(BuildingType.Smeltery)
            )
            .Execute(core);

            Assert.IsFalse(room.Building.Constructible.IsReady);

            new NextTurnCount(smelteryTime - 1).Execute(core);

            Assert.IsFalse(room.Building.Constructible.IsReady);

            new NextTurn().Execute(core);

            Assert.IsTrue(room.Building.Constructible.IsReady);

            // Furnace
            new ModuleConstruct(
                room.Building,
                core.Factory.ProduceModule(ModuleType.Furnace),
                2
            ).Execute(core);

            var module = room.Building.GetModule(2);

            Assert.IsFalse(module.Constructible.IsReady);

            new NextTurnCount(furnaceTime - 1).Execute(core);

            Assert.IsFalse(module.Constructible.IsReady);

            new NextTurn().Execute(core);

            Assert.IsTrue(module.Constructible.IsReady);
        }

        [TestMethod]
        public void CantBuiltCostly()
        {
            var core = new GameLogic.Core();
            var room = core.Ship.GetRoom(0);

            core.Bank.Change(ResourceType.Metal, 3);

            Assert.IsFalse(
                new BuildingConstruct(
                    room,
                    core.Factory.ProduceBuilding(BuildingType.Smeltery)
                )
                .Execute(core)
                .IsValid
            );
        }
    }
}
