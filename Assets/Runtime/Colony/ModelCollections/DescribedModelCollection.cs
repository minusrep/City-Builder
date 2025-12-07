namespace Runtime.Colony.ModelCollections
{
    public abstract class DescribedModelCollection<T> : SerializeModelCollection<T>
        where T : ISerializeModel
    {
        public void Create(string descriptionKey)
        {
            DescriptionKey = descriptionKey;
            var model = CreateModel(descriptionKey);
            Add(GetCurrentKey(), model);
        }
        
        protected abstract T CreateModel(string descriptionKey);
    }
}