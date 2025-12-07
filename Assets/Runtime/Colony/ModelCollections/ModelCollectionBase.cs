using System.Collections.Generic;
using System;

namespace Runtime.Colony.ModelCollections
{
    public abstract class ModelCollectionBase<TKey, TValue> : IModelCollection<TValue>
    {
        public event Action<TValue> OnAdded;
        public event Action<TValue> OnRemoved;

        public Dictionary<TKey, TValue> Models { get; } = new();
        
        public void Remove(TKey id)
        {
            var model = Models[id];
            Models.Remove(id);
            OnRemoved?.Invoke(model);
        }

        public TValue Get(TKey id)
        {
            return Models[id];
        }
        
        protected void Add(TKey key, TValue model)
        {
            Models.Add(key, model);
            OnAdded?.Invoke(model);
        }
    }
}