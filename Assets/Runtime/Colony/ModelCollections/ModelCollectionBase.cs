using System.Collections.Generic;
using System;

namespace Runtime.Colony.ModelCollections
{
    public abstract class ModelCollectionBase<TKey, TValue> : IModelCollection<TValue>
    {
        public event Action<TValue> OnAdded;
        public event Action<TValue> OnRemoved;

        protected Dictionary<TKey, TValue> Models { get; } = new();
        
        public void DeleteModel(TKey id)
        {
            var model = Models[id];
            Models.Remove(id);
            OnRemoved?.Invoke(model);
        }

        public TValue FindModel(TKey id)
        {
            return Models[id];
        }

        protected abstract TValue CreateModelFromData(TKey id, Dictionary<string, object> data);
        
        protected void InvokeOnCreateModel(TValue model)
        {
            OnAdded?.Invoke(model);
        }
    }
}