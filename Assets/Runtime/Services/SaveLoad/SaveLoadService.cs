using Runtime.Colony;

namespace Runtime.Services.SaveLoad
{
    public class SaveLoadService :  ISaveLoadService
    {
        private SaveLoadStrategy _saveLoadStrategy;
        
        public SaveLoadService(SaveLoadStrategy saveLoadStrategy)
        {
            _saveLoadStrategy = saveLoadStrategy;
        }

        public void Save(World world)
        {
            _saveLoadStrategy.Save(world);
        }

        public World Load()
        {
            return _saveLoadStrategy.Load();
        }

        public void Switch(SaveLoadStrategy strategy)
        {
            _saveLoadStrategy = strategy;
        }
    }
}