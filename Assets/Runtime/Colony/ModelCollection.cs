using System;
using System.Collections.Generic;
using Runtime.Colony.Citizens.StateMachine;

namespace Runtime.Colony
{
    public abstract class ModelCollection<T> : ISerializeModel where T : ISerializeModel
    {
        public Dictionary<int, T> Models { get; } = new();
        public event Action<T> OnCreateModel;
        public event Action<T> OnDeleteModel;

        private int _index;

        public void Create()
        {
            var newModel = CreateModel();

            Models.Add(_index, newModel);

            _index++;

            OnCreateModel?.Invoke(newModel);
        }

        protected abstract T CreateModel();

        public void Delete(int modelId)
        {
            var model = Models[modelId];

            Models.Remove(modelId);

            OnDeleteModel?.Invoke(model);
        }

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object> { { "index", _index } };

            var models = new Dictionary<string, object>();

            foreach (var model in Models)
            {
                var modelData = model.Value.Serialize();
                models.Add(model.Key.ToString(), modelData);
            }

            data.Add("models", models);

            return data;
        }

        public T Find(int id)
        {
            var model = Models[id];

            return model;
        }
    }
}