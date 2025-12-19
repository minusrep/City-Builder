using Runtime.Colony;
using Runtime.Descriptions;

namespace Runtime.Services.SaveLoad
{
    public abstract class SaveLoadStrategy
    {
        protected readonly FactoryProvider FactoryProvider;

        protected readonly WorldDescription WorldDescription;

        protected SaveLoadStrategy(WorldDescription worldDescription, FactoryProvider factoryProvider)
        {
            WorldDescription = worldDescription;

            FactoryProvider = factoryProvider;
        }

        public abstract void Save(World world);

        public abstract World Load();
    }
}