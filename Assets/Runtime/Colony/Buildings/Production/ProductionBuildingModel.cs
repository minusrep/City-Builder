using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Colony.Orders;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingModel : BuildingModel, IInventoryBuilding
    {
        public Action<float> OnProgressChanged;
        
        public ProductionBuildingDescription Description { get; }
        public InventoryModel Inventory { get; private set; }
        public bool IsActive { get; private set; }
        public ResourceDescription ResourceDescription { get; }
        private WorldDescription WorldDescription { get; }

        private float _progress;

        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnProgressChanged?.Invoke(_progress);
            }
        }

        public OrderModelCollection Orders { get; private set; }

        public ProductionBuildingModel(string id,
            Vector2 position,
            ProductionBuildingDescription description, WorldDescription worldDescription) : base(id, position,
            description)
        {
            WorldDescription = worldDescription;
            Description = description;

            IsActive = false;

            Orders = new OrderModelCollection(id);

            ResourceDescription = worldDescription.ResourceCollection.Descriptions[Description.ProductionResource];
            Inventory = new InventoryModel(1, Description.MaxResource, WorldDescription.ResourceCollection);
            Inventory.TryAddItem(ResourceDescription, 0);
        }

        public void StartProduction()
        {
            if (!HasResources())
            {
                StopProduction();
                return;
            }

            if (!IsActive && CapacityLeft())
            {
                IsActive = true;
            }
        }

        public void StopProduction()
        {
            IsActive = false;
        }

        public bool TryAddItem(ResourceDescription resourceDescription, int amount)
        {
            if (!Inventory.CanFit(resourceDescription, amount, out _))
            {
                return false;
            }
            
            Inventory.TryAddItem(resourceDescription, amount);

            if (Orders.Models.ContainsKey(resourceDescription.Id))
            {
                Orders.Models[resourceDescription.Id].Done(amount);
            }

            StartProduction();
            return true;
        }
        
        public bool TryRemoveItem(ResourceDescription resourceDescription, int amount)
        {
            if (!Inventory.CanExtract(resourceDescription, amount, out _))
            {
                return false;
            }
            
            Inventory.TryRemoveItem(resourceDescription, amount);
            
            if (Orders.Models.ContainsKey(resourceDescription.Id))
            {
                Orders.Models[resourceDescription.Id].Done(amount);
            }
            
            StartProduction();
            return true;
        }
        
        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize())
            {
                { "is_active", IsActive },
                { "progress", Progress },
                { "inventory", Inventory.Serialize() },
                { "orders", Orders.Serialize() },
            };

            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            IsActive = data.GetBool("is_active");
            Progress = data.GetFloat("progress");
            
            Inventory = new InventoryModel(1, Description.MaxResource, WorldDescription.ResourceCollection);
            Inventory.Deserialize(data.GetNode("inventory"));

            Orders = new OrderModelCollection(Id);
            Orders.Deserialize(data.GetNode("orders"));
        }

        public bool ProduceOnceAndQueue()
        {
            if (CapacityLeft())
            {
                foreach (var resource in Description.ResourcesForProduction)
                {
                    Inventory.TryRemoveItem(WorldDescription.ResourceCollection.Descriptions[resource.Key],
                        resource.Value);
                }
                
                Inventory.TryAddItem(ResourceDescription, Description.ProductionAmount);
                
                var order = new OrderModel(ResourceDescription.Id, Id)
                {
                    Type = "take_resource",
                    ResourceId = ResourceDescription.Id,
                    Amount = Description.ProductionAmount
                };
                Orders.Add(order.Id, order);
                
                return true;
            }

            return false;
        }

        public bool HasOrder()
        {
            return Orders.Models.Count > 0 && Orders.Models.Values.Any(o => o.FreeAmount > 0);
        }

        private bool CapacityLeft()
        {
            return Inventory.CanFit(ResourceDescription, Description.ProductionAmount, out _);
        }

        private bool HasResources()
        {
            return HasResources(Description.ResourcesForProduction) && HasResources(Description.ResourcesForWork) ;
        }
        
        private bool HasResources(Dictionary<string, int> resources)
        {
            bool hasEnough = true;
            
            foreach (var resource in resources)
            {
                if (!Inventory.CanExtract(WorldDescription.ResourceCollection.Descriptions[resource.Key], resource.Value, out _))
                {
                    hasEnough = false;
                    var order = new OrderModel(resource.Key, Id)
                    {
                        Type = "put_resource",
                        ResourceId = resource.Key,
                        Amount = resource.Value
                    };
                    if (!Orders.Models.ContainsKey(order.Id))
                    {
                        Orders.Add(order.Id, order);
                    }
                }
            }

            return hasEnough;
        }
    }
}