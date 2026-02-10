using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.ModelCollections
{
    public abstract class SerializeModelCollection<T> : ModelCollectionBase<string, T> where T : ISerializeModel
    {
        protected int Index { get; private set; }

        protected string DescriptionKey { get; set; }

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
                Add(pair.Key, model);
            }
        }

        protected abstract T CreateModelFromData(string id, Dictionary<string, object> data);

        public override void Add(string key, T model)
        {
            base.Add(key, model);
            Index++;
        }
        
        protected string GetCurrentKey()
        {
            if (string.IsNullOrEmpty(DescriptionKey))
            {
                return Index.ToString();
            }
            
            return DescriptionKey + "_" + Index;
        }
    }
}