namespace GameLogic.Architecture
{
    public class Module
    {
        public readonly ModuleType Type;
        public readonly ModuleConfig Config;

        // В конструкторе принимаем конфиг, а не индекс
        public Module(ModuleConfig config)
        {
            Type = config.Type;
            Config = config;

        }

        public Module(ModuleType type)
        {
            Type = type;
        }
    }
}
