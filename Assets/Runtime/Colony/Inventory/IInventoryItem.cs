namespace Runtime.Colony.Inventory
{
    public interface IInventoryItem
    {
        string Type { get; }
        int Amount { get; } //TODO: Должно ли быть здесь?
        int MaxAmount { get; } //TODO: Должно ли быть здесь?
    }
}