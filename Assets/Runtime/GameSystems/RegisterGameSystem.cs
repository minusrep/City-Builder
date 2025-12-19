using System.Collections.Generic;

namespace Runtime.GameSystems
{
    public abstract class RegisterGameSystem<T> : IRegisterGameSystem<T>
    {
        public string Id { get; set; }

        private readonly List<T> _items =  new();

        private readonly List<T> _itemsToRemove = new();

        public RegisterGameSystem(string id)
        {
            Id = id;
        }
        
        public void Update(float deltaTime)
        {
            foreach (var item in _items)
            {
                Update(item, deltaTime);
            }

            foreach (var item in _itemsToRemove)
            {
                _items.Remove(item);
            }
            
            _itemsToRemove.Clear();
        }

        public void Register(T item)
        {
            _items.Add(item);
        }

        public void Unregister(T item)
        {
            if (_items.Contains(item))
            {
                _itemsToRemove.Add(item);
            }
        }

        protected abstract void Update(T item, float deltaTime);
    }
}