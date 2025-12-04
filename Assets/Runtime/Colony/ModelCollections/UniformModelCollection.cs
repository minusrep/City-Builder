namespace Runtime.Colony.ModelCollections
{
    public abstract class UniformModelCollection<T> : ModelCollectionBase<T>
        where T : ISerializeModel, IDeserializeModel
    {
        public void Create()
        {
            var model = CreateModel();
            Models.Add(Index++, model);
            InvokeOnCreateModel(model);
        }

        protected abstract T CreateModel();
    }
}