using Runtime.Descriptions.Items;

namespace Runtime.Colony.GameResources
{
    public sealed class ResourceFactory
    {
        private readonly ResourceDescriptionCollection _collection;

        public ResourceFactory(ResourceDescriptionCollection collection)
        {
            _collection = collection;
        }

        public ResourceModel Create(string id)
        {
            return new ResourceModel(_collection.Descriptions[id]);
        }
    }
}