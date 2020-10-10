using GameLogic.Player;
using System.Collections.Generic;

namespace GameLogic.Architecture
{
    public class ModuleConfig
    {
        public ModuleType Type;
        public int ConstructionTime;
        public Dictionary<ResourceType, int> ConstructionCost;

        public int CycleTime; // сколько времени модуль будет перетравливать сырье
        public Dictionary<ResourceType, int> CycleInput; // сколько сырья
        public Dictionary<ResourceType, int> CycleOutput; // какой выход готовой продукции
    }
}
