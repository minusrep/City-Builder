namespace Runtime.Services
{
    public interface IServiceLocator
    {
        void Register<T>(T service) where T : IService;
        
        T Get<T>() where T : IService;
    }
}