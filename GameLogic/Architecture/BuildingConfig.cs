using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Architecture
{
    public class BuildingConfig
    {
        public BuildingType Type;
        // Теперь никаких констант
        public int ModulesLimit;
        // Каждое строение может иметь только определенные модули
        public ModuleType[] AvailableModules;
    }
}
