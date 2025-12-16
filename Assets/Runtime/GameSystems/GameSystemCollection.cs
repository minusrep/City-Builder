using System.Collections.Generic;

namespace Runtime.GameSystems
{
    public class GameSystemCollection
    {
        private readonly List<IGameSystem> _systems = new();

        public void Add(IGameSystem system)
        {
            _systems.Add(system);
        }

        public void Remove(IGameSystem system)
        {
            _systems.Remove(system);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _systems)
            {
                system.Update(deltaTime);
            }
        }
    }
}