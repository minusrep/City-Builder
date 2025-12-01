using System.Collections.Generic;

namespace Runtime.Colony.Orders
{
    public abstract class ColonyOrder
    {
        public int Id { get; set; }
        public int ToBuildingId { get; }
        public int FromBuildingId { get; }
        public readonly List<int> CitizensId = new();

        protected ColonyOrder(int toBuildingId, int fromBuildingId)
        {
            ToBuildingId = toBuildingId;
            FromBuildingId = fromBuildingId;
        }
    }
}