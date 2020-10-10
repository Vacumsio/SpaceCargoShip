﻿using GameLogic.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Architecture
{
    public class Factory
    {
        private readonly Dictionary<BuildingType, BuildingConfig> buildings = new Dictionary<BuildingType, BuildingConfig>() {
        { BuildingType.Empty, new BuildingConfig() {
            Type = BuildingType.Empty
        }},
        { BuildingType.PowerPlant, new BuildingConfig() {
            Type = BuildingType.PowerPlant,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 20 }},
            ConstructionTime = 8,
            ModulesLimit = 5,
            AvailableModules = new[]{ ModuleType.Generator }
        }},
        { BuildingType.Smeltery, new BuildingConfig() {
            Type = BuildingType.Smeltery,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 20 }},
            ConstructionTime = 10,
            ModulesLimit = 4,
            AvailableModules = new[]{ ModuleType.Furnace }
        }},
        { BuildingType.Roboport, new BuildingConfig() {
            Type = BuildingType.Roboport,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 20 }},
            ConstructionTime = 12,
            ModulesLimit = 3,
            AvailableModules = new[]{
                ModuleType.Digger,
                ModuleType.Miner
            }
        }}
    };

        private readonly Dictionary<ModuleType, ModuleConfig> modules = new Dictionary<ModuleType, ModuleConfig>() {
        { ModuleType.Generator, new ModuleConfig() {
            Type = ModuleType.Generator,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 10 }},
            ConstructionTime = 5,
            CycleTime = 12,
            CycleInput = null,
			CycleOutput = new Dictionary<ResourceType, int>() {
                { ResourceType.Energy, 10 }
            }
        }},
        { ModuleType.Furnace, new ModuleConfig() {
            Type = ModuleType.Furnace,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 10 }},
            ConstructionTime = 6,
            CycleTime = 16,
            CycleInput = new Dictionary<ResourceType, int>() {
                { ResourceType.Energy, 6 },
                { ResourceType.Ore, 4 },
            },
            CycleOutput = new Dictionary<ResourceType, int>() {
                { ResourceType.Metal, 5 }
            }
        }},
        { ModuleType.Digger, new ModuleConfig() {
            Type = ModuleType.Digger,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 10 }},
            ConstructionTime = 7,
            CycleTime = 18,
            CycleInput = new Dictionary<ResourceType, int>() {
                { ResourceType.Energy, 2 }
            },
            CycleOutput = new Dictionary<ResourceType, int>() {
                { ResourceType.Ore, 7 }
            }
        }},
        { ModuleType.Miner, new ModuleConfig() {
            Type = ModuleType.Miner,
            ConstructionCost = new Dictionary<ResourceType, int>() {{ ResourceType.Metal, 40 }},
            ConstructionTime = 8,
            CycleTime = 32,
            CycleInput = new Dictionary<ResourceType, int>() {
                { ResourceType.Energy, 8 }
            },
            CycleOutput = new Dictionary<ResourceType, int>() {
                { ResourceType.Ore, 40 }
            }
        }}
    };

        public Building ProduceBuilding(BuildingType type)
        {
            if (!buildings.ContainsKey(type))
            {
                throw new ArgumentException("Unknown building type: " + type);
            }

            return new Building(buildings[type]);
        }
        public Module ProduceModule(ModuleType type)
        {
            if (!modules.ContainsKey(type))
            {
                throw new ArgumentException("Unknown module type: " + type);
            }

            return new Module(modules[type]);
        }
    }
}
