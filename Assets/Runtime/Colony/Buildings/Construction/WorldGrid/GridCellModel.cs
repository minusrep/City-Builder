using Runtime.Colony.Buildings.Common;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
{
    public class GridCellModel
    {
        public Vector2Int Position { get; }
        public BuildingModel OccupiedBy { get; private set; }

        public bool IsFree => OccupiedBy == null;
        
        public GridCellModel(int x, int y)
        {
            Position = new Vector2Int(x, y);
        }

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