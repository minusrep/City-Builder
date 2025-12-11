using Runtime.Colony;

namespace Runtime.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    { 
        void Save(World world);
        
        World Load();
    }
}