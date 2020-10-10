namespace GameLogic.Architecture
{
    public class Module
    {
        public readonly ModuleType Type;
        public readonly ModuleConfig Config;
        public readonly Progression Constructible;
        public readonly Progression Cycle;

        // В конструкторе принимаем конфиг, а не индекс
        public Module(ModuleConfig config)
        {
            Type = config.Type;
            Config = config;
            Constructible = new Progression(config.ConstructionTime);
            Cycle = new Progression(config.CycleTime);
        }

        public Module(ModuleType type)
        {
            Type = type;
        }
    }
}
