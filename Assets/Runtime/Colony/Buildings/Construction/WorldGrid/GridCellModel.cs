using Runtime.Colony.Buildings.Common;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
{
    public class GridCellModel
    {
        public bool IsFree => OccupiedBy == null;
        
        public BuildingModel OccupiedBy { get; set; }

        public void Occupy(BuildingModel building)
        {
            OccupiedBy = building;
        }
        
        public void Clear()
        {
            OccupiedBy = null;
        }
    }
}