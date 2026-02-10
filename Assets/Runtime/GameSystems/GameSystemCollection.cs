using System.Collections.Generic;

namespace Runtime.GameSystems
{
    public class GameSystemCollection
    {
        private readonly Dictionary<string, IGameSystem> _systems = new();
        private readonly List<IGameSystem> _pendingAdd = new();
        private readonly List<IGameSystem> _pendingRemove = new();
        
        private bool _isUpdating;

        public IGameSystem Get(string id)
        {
            return _systems[id];
        }

        public void Add(IGameSystem system)
        {
            if (_isUpdating)
            {
                _pendingAdd.Add(system);
                return;
            }

            _systems.Add(system.Id, system);
        }

        public void Remove(IGameSystem system)
        {
            if (_isUpdating)
            {
                _pendingRemove.Add(system);
                return;
            }

            _systems.Remove(system.Id);
        }

        public void Update(float deltaTime)
        {
            _isUpdating = true;
            
            foreach (var system in _systems.Values)
            {
                system.Update(deltaTime);
            }
            
            _isUpdating = false;

            if (_pendingRemove.Count > 0)
            {
                foreach (var system in _pendingRemove)
                {
                    _systems.Remove(system.Id);
                }

                _pendingRemove.Clear();
            }

            if (_pendingAdd.Count > 0)
            {
                foreach (var system in _pendingAdd)
                {
                    _systems.Add(system.Id, system);
                }

                _pendingAdd.Clear();
            }
        }
        
        public void Clear()
        {
            _systems.Clear();
            _pendingAdd.Clear();
            _pendingRemove.Clear();
        }
    }
}