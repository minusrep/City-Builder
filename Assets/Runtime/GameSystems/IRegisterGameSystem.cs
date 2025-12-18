namespace Runtime.GameSystems
{
    public interface IRegisterGameSystem<T> : IGameSystem
    {
        void Register(T item);
        
        void Unregister(T item);
    }
}