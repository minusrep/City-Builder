using Runtime.Descriptions.Items;

namespace Runtime.Colony.GameResources
{
    public sealed class ResourceFactory : IResourceFactory
    {
        private readonly ItemDescriptionCollection _collection;

        public ResourceFactory(ItemDescriptionCollection collection)
        {
            _collection = collection;
        }

        public ResourceModel Create(string id)
        {
            return new ResourceModel(_collection.Descriptions[id]);
        }
    }
}