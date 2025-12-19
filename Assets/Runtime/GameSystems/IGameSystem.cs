namespace Runtime.GameSystems
{
    public interface IGameSystem
    {
        void Update(float deltaTime);
        string Id { get; }
    }
}