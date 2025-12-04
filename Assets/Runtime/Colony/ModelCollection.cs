using System;
using System.Collections.Generic;

namespace Runtime.Colony
{
    public abstract class ModelCollection<T> : ISerializeModel, IDeserializeModel where T : ISerializeModel, IDeserializeModel
    {
        public Dictionary<int, T> Models { get; private set; } = new();
        public event Action<T> OnCreateModel;
        public event Action<T> OnDeleteModel;
        public int Index { get; protected set; }

        public void Create()
        {
            var newModel = CreateModel();

            Models.Add(Index, newModel);

            Index++;
            
            OnCreateModel?.Invoke(newModel);
        }
        
        protected abstract T CreateModel();
        protected abstract T CreateModelFromData(Dictionary<string, object> data);

        public void Delete(int modelId)
        {
            var model = Models[modelId];

            Models.Remove(modelId);

            OnDeleteModel?.Invoke(model);
        }

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object> { { "index", Index } };

            var models = new Dictionary<string, object>();

            foreach (var model in Models)
            {
                var modelData = model.Value.Serialize();
                models.Add(model.Key.ToString(), modelData);
            }

            data.Add("models", models);

            return data;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var models = new Dictionary<int, T>();
            
            foreach (var model in (Dictionary<string, object>)data["models"])
            {
                var modelData = (Dictionary<string, object>)model.Value;
            
                var modelInstance = CreateModelFromData(modelData);
                
                models.Add(Convert.ToInt32(model.Key), modelInstance);
            }
            
            Index = Convert.ToInt32(data["index"]);

            Models = models;
        }

        public T Find(int id)
        {
            var model = Models[id];

            return model;
        }
    }
}