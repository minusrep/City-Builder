namespace Runtime.Colony.Items
{
    public interface IItemFactory
    {
        ItemModel Create(string id);
    }
}