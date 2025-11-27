using System.Collections.Generic;

namespace Runtime.Colony.Orders
{
    public class ColonyOrder
    {
        public int Id;
        public int ToBuildingId;
        public int FromBuildingId;
        public List<int> CitizensId;
    }
}