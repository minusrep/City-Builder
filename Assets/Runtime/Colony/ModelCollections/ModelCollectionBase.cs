using System.Collections.Generic;
using System;
using Runtime.Utilities;

namespace Runtime.Colony.ModelCollections
{
    public abstract class ModelCollectionBase<T> : IModelCollection<T> where T : ISerializeModel
    {
        public event Action<T> OnCreateModel;
        public event Action<T> OnDeleteModel;

        protected Dictionary<int, T> Models { get; private set; } = new();
        public int Index { get; protected set; }
        
        public void DeleteModel(int id)
        {
            var model = Models[id];
            Models.Remove(id);
            OnDeleteModel?.Invoke(model);
        }

        public T FindModel(int id)
        {
            return Models[id];
        }
        
        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object>
            {
                { "index", Index }
            };

            var models = new Dictionary<string, object>();
            
            foreach (var model in Models)
            {
                models.Add(model.Key.ToString(), model.Value.Serialize());
            }

            data.Set("models", models);
            return data;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Index = data.GetInt("index");
            Models = new Dictionary<int, T>();

            var models = data.GetDict("models");
            foreach (var pair in models)
            {
                var modelData = (Dictionary<string, object>)pair.Value;
                var model = CreateModelFromData(Convert.ToInt32(pair.Key), modelData);
                Models.Add(Convert.ToInt32(pair.Key), model);
            }
        }

        protected abstract T CreateModelFromData(int id, Dictionary<string, object> data);
        
        protected void InvokeOnCreateModel(T model)
        {
            OnCreateModel?.Invoke(model);
        }
    }
}