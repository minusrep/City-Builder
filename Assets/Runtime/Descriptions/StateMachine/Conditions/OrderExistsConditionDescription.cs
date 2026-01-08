using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;
using System.Linq;
using Runtime.Colony.Buildings.Production;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class OrderExistsConditionDescription : ConditionDescription
    {
        public OrderExistsConditionDescription(Dictionary<string, object> data) : base(data)
        {
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            var productionBuildings = world.Buildings.Models.Values
                .Where(b => b is ProductionBuildingModel)
                .Cast<ProductionBuildingModel>();
            
            return productionBuildings.Any(
                productionBuilding => productionBuilding.Orders.Models.Count > 0 && 
                                      productionBuilding.Orders.Models.Values.Any(o => o.FreeAmount > 0));
        }
    }
}