namespace Runtime.Colony.ModelCollections
{
    public abstract class UniformModelCollection<T> : SerializeModelCollection<T> where T : ISerializeModel
    {
        protected UniformModelCollection(string descriptionKey)
        {
            DescriptionKey = descriptionKey;
        }

        public void Create()
        {
            var model = CreateModel();
            Models.Add(GetCurrentKey(), model);
            InvokeOnCreateModel(model);
        }

        protected abstract T CreateModel();
    }
}