using Runtime.Descriptions.Items;

namespace Runtime.Colony.Items
{
    public sealed class ItemFactory : IItemFactory
    {
        private readonly ItemDescriptionCollection _collection;

        public ItemFactory(ItemDescriptionCollection collection)
        {
            _collection = collection;
        }

        public ItemModel Create(string id)
        {
            return new ItemModel(_collection.Descriptions[id]);
        }
    }
}