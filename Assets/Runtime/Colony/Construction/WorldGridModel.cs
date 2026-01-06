using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class WorldGridModel
    {
        public WorldGridDescription Description { get; }
        
        private GridCellModel[,] Cells { get; }

        public WorldGridModel(WorldGridDescription description)
        {
            Description = description;
            
            Cells = new GridCellModel[Description.Width, Description.Height];

            for (var x = 0; x < Description.Width; x++)
            {
                for (var y = 0; y < Description.Height; y++)
                {
                    Cells[x, y] = new GridCellModel(x, y);
                }
            }
        }
        
        public bool CanPlaceBuilding(BuildingDescription description, Vector2Int position)
        {
            foreach (var cellOffset in description.Cells)
            {
                var x = position.x + cellOffset.x;
                var y = position.y + cellOffset.y;
                
                if (x < 0 || x >= Description.Width || y < 0 || y >= Description.Height)
                {
                    return false;
                }

                if (!Cells[x, y].IsFree)
                {
                    return false;
                }
            }
            return true;
        }
        
        public void PlaceBuilding(BuildingModel building, Vector2Int position)
        {
            foreach (var cellOffset in building.BaseDescription.Cells)
            {
                var x = position.x + cellOffset.x;
                var y = position.y + cellOffset.y;
                Cells[x, y].Occupy(building);
            }
            building.WorldPosition = GridToWorld(position, building.BaseDescription);
        }
        
        public Vector3 GridToWorld(Vector2Int gridPosition, BuildingDescription description)
        {
            var sizeOffset = new Vector3(
                (description.Size.x - 1) * 0.5f,
                (description.Size.y - 1) * 0.5f);

            return Description.Origin +
                   new Vector3(gridPosition.x, 0, gridPosition.y) * Description.CellSize +
                   sizeOffset * Description.CellSize;
        }
    }
}