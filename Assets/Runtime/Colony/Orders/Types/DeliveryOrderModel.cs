using Runtime.Descriptions.Orders;

namespace Runtime.Colony.Orders.Types
{
    public class DeliveryOrderModel : OrderModel
    {
        public DeliveryOrderModel(ColonyOrderDescription description, int fromBuildingId) : base(description, fromBuildingId)
        {
            
        }
    }
}