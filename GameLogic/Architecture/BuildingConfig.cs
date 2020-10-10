using GameLogic.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Architecture
{
    public class BuildingConfig
    {
        public BuildingType Type;
        public int ModulesLimit;
        public int ConstructionTime;
        // Каждое строение может иметь только определенные модули
        public ModuleType[] AvailableModules;
        public Dictionary<ResourceType, int> ConstructionCost;
    }
}
