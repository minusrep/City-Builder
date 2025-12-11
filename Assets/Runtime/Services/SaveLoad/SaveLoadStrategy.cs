using Runtime.Colony;
using Runtime.Descriptions;

namespace Runtime.Services.SaveLoad
{
    public abstract class SaveLoadStrategy
    {
        protected readonly FactoryProvider _factoryProvider;
        
        protected readonly WorldDescription _worldDescription;

        protected SaveLoadStrategy(WorldDescription worldDescription, FactoryProvider factoryProvider)
        {
            _worldDescription = worldDescription;
            
            _factoryProvider = factoryProvider;
        }

        public abstract void Save(World world);

        public abstract World Load();
    }
}