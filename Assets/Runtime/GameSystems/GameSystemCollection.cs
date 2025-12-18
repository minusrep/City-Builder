using System.Collections.Generic;

namespace Runtime.GameSystems
{
    public class GameSystemCollection 
    {
        private readonly Dictionary<string, IGameSystem> _systems = new();

        public IGameSystem Get(string Id)
        {
            return _systems[Id];
        }
        
        public void Add(IGameSystem system)
        {
            _systems.Add(system.Id, system);
        }

        public void Remove(IGameSystem system)
        {
            _systems.Remove(system.Id);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in _systems.Values)
            {
                system.Update(deltaTime);
            }
        }
    }
}