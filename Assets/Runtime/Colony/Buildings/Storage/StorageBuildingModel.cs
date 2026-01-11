using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Storage
{
    public class StorageBuildingModel : BuildingModel, IInventoryBuilding
    {
        public InventoryModel Inventory { get; private set; }
        private WorldDescription WorldDescription { get; }

        private StorageBuildingDescription Description { get; }

        public StorageBuildingModel(string id,
            Vector2Int gridPosition,
            StorageBuildingDescription description, WorldDescription worldDescription) : base(id, gridPosition, description)
        {
            WorldDescription = worldDescription;
            Description = description;

            Inventory = new InventoryModel(description.StoredResources.Count, description.MaxResourceAmount, worldDescription.ResourceCollection);
            
            foreach (var resourceDescriptionId in description.StoredResources)
            {
                var resourceDescription = worldDescription.ResourceCollection.Descriptions[resourceDescriptionId];
                Inventory.Create();
                Inventory.TryAddItem(resourceDescription, 0);    
            }
        }
        
        public bool TryAddItem(ResourceDescription resource, int amount)
        {
            return Inventory.TryAddItem(resource, amount);
        }
        
        public bool TryRemoveItem(ResourceDescription resource, int amount)
        {
            return Inventory.TryRemoveItem(resource, amount);
        }

        public int GetAmount(ResourceDescription resource)
        {
            foreach (var cell in Inventory.Models.Values)
            {
                if (cell.Resource != null && cell.Resource == resource)
                {
                    return cell.Amount;
                }
            }
            return 0;
        }

        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize())
            {
                { "inventory", Inventory.Serialize() }
            };
            
            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            Inventory = new InventoryModel(1, Description.MaxResourceAmount, WorldDescription.ResourceCollection);
            Inventory.Deserialize(data.GetNode("inventory"));
        }
    }
}