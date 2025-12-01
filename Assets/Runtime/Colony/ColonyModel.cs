using Runtime.Colony.Orders;
using Runtime.Descriptions.Colony;

namespace Runtime.Colony
{
    public class ColonyModel
    {
        public ColonyDescription Description;
        public ColonyPopulationModel Population;
        public ColonyBuildingsModel BuildingsModel;
        public ColonyOrdersPool OrdersPool;
    }
}