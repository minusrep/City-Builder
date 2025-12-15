using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Colony.Items;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Storage
{
    public class StorageBuildingModel : BuildingModel
    {
        public InventoryModel Inventory { get; }
        
        private StorageBuildingDescription Description { get; }
        private Dictionary<string, ItemModel> _resources = new();
        private IItemFactory ItemFactory { get; }

        public StorageBuildingModel(string id,
            Vector2 position,
            StorageBuildingDescription description,
            IItemFactory itemFactory) : base(id, position, description)
        {
            ItemFactory = itemFactory;
            Description = description;

            foreach (var resourceId in Description.StoredResources)
            {
                _resources.Add(resourceId, itemFactory.Create(resourceId));
            }

            Inventory = new InventoryModel(description.StoredResources.Count);
        }
        
        public bool TryAddResource(string resourceKey, ItemModel itemModel)
        {
            if (_resources.TryGetValue(resourceKey, out var stored))
            {
                if (stored.Amount + itemModel.Amount <= Description.MaxResourceAmount)
                {
                    stored.IncreaseAmount(itemModel.Amount);
                    return true;
                }
            }

            return false;
        }
        
        public bool TryTakeResource(string resourceKey, int amount)
        {
            if (_resources.TryGetValue(resourceKey, out var resource))
            {
                if (resource.Amount >= amount)
                {
                    resource.ReduceAmount(amount);
                    return true;
                }
            }

            return false;
        }

        public int GetAmount(string resourceKey)
        {
            return _resources[resourceKey].Amount;
        }

        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize());
            var serializedResources = new Dictionary<string, object>();
            foreach (var kvp in _resources)
            {
                serializedResources[kvp.Key] = kvp.Value.Serialize();
            }

            dictionary.Set("resources", serializedResources);
            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            _resources = new Dictionary<string, ItemModel>();
            var node = data.GetNode("resources");
            
            foreach (var resource in node)
            {
                var resourceModel = ItemFactory.Create(resource.Key);
                resourceModel.Deserialize(node.GetNode(resource.Key));
                _resources.Add(resource.Key, resourceModel);
            }
        }
    }
}