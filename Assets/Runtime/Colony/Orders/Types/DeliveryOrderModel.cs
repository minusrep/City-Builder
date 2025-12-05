namespace Runtime.Colony.Orders.Types
{
    public class DeliveryOrderModel : OrderModel
    {
        public override string Type { get; } = OrderConstants.OrderTypes.Delivery;

        public DeliveryOrderModel(int fromBuildingId) : base(fromBuildingId)
        {
        }
    }
}