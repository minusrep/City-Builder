namespace Runtime.Colony.ModelCollections
{
    public abstract class DescribedModelCollection<T> : ModelCollectionBase<T>
        where T : ISerializeModel, IDeserializeModel
    {
        public void Create(string descriptionKey)
        {
            var model = CreateModel(descriptionKey);
            Models.Add(Index++, model);
            InvokeOnCreateModel(model);
        }

        protected abstract T CreateModel(string descriptionKey);
    }
}