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
    public class StorageBuildingModel : BuildingModel
    {
        private const int MaxStackSize = 20;

        public InventoryModel Inventory { get; private set; }
        private WorldDescription WorldDescription { get; }

        private StorageBuildingDescription Description { get; }

        public StorageBuildingModel(string id,
            Vector2 position,
            StorageBuildingDescription description, WorldDescription worldDescription) : base(id, position, description)
        {
            WorldDescription = worldDescription;
            Description = description;

            Inventory = new InventoryModel(description.StoredResources.Count, MaxStackSize, worldDescription.ResourceCollection);
            
            foreach (var resourceDescriptionId in description.StoredResources)
            {
                var resourceDescription = worldDescription.ResourceCollection.Descriptions[resourceDescriptionId];
                Inventory.TryAddItem(resourceDescription, 0);    
            }
        }
        
        public bool TryAddResource(ResourceDescription resource, int amount)
        {
            return Inventory.TryAddItem(resource, amount);
        }
        
        public bool TryTakeResource(ResourceDescription resource, int amount)
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
            Inventory = new InventoryModel(1, MaxStackSize, WorldDescription.ResourceCollection);
            Inventory.Deserialize(data.GetNode("inventory"));
        }
    }
}