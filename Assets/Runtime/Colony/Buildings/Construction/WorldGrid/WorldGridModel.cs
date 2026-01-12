using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
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
                    Cells[x, y] = new GridCellModel();
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

            building.GridPosition = position;
            var worldPosition = GridToWorld(position);
            building.WorldPosition =  new Vector2(worldPosition.x, worldPosition.z);
        }
        
        public Vector3 GridToWorld(Vector2Int gridPosition)
        {
            return Description.Origin +
                   new Vector3(gridPosition.x, Description.Origin.y, gridPosition.y) * WorldGridDescription.CellSize;
        }

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            var local = worldPosition - Description.Origin;
            var x = Mathf.FloorToInt(local.x / WorldGridDescription.CellSize);
            var y = Mathf.FloorToInt(local.z / WorldGridDescription.CellSize);
            return new Vector2Int(x, y);
        }
        
        public void RebuildFromBuildings(IEnumerable<BuildingModel> buildings)
        {
            Clear();

            foreach (var building in buildings)
            {
                PlaceBuilding(building, building.GridPosition);
            }
        }
        
        private void Clear()
        {
            for (var x = 0; x < Description.Width; x++)
            {
                for (var y = 0; y < Description.Height; y++)
                {
                    Cells[x, y].Clear();
                }
            }
        }
    }
}