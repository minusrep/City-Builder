namespace Runtime.Colony.Orders
{
    public sealed class DeliveryOrder : ColonyOrder
    {
        public string ResourceKey { get; }
        
        public DeliveryOrder(int toBuildingId, int fromBuildingId, string resourceKey) : base(toBuildingId, fromBuildingId)
        {
            ResourceKey = resourceKey;
        }
    }
}