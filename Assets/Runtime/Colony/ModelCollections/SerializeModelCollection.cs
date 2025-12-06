using System.Collections.Generic;
using Runtime.Utilities;
using System;

namespace Runtime.Colony.ModelCollections
{
    public abstract class SerializeModelCollection<T> : ModelCollectionBase<string, T> where T : ISerializeModel 
    {
        protected int Index { get; private set; }

        protected string DescriptionKey;

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object>();

            var models = new Dictionary<string, object>();
            
            foreach (var model in Models)
            {
                models.Add(model.Key, model.Value.Serialize());
            }

            data.Set("models", models);
            return data;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var models = data.GetNode("models");
            foreach (var pair in models)
            {
                var modelData = (Dictionary<string, object>)pair.Value;
                var model = CreateModelFromData(pair.Key, modelData);
                Models.Add(pair.Key, model);
                Index++;
            }
        }

        protected abstract override T CreateModelFromData(string id, Dictionary<string, object> data);
        
        protected int GetCurrentId(string key)
        {
            return Convert.ToInt32(key.Split('_')[1]);
        }

        protected string GetCurrentKey()
        {
            return DescriptionKey + "_" + Index++;
        }
    }
}