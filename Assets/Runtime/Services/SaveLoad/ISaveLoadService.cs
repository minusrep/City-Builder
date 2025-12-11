namespace Runtime.Colony
{
    public interface ISaveLoadService : IService
    { 
        void Save(World world);
        
        World Load();
    }
}