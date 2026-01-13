using System;
using System.Collections.Generic;
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
        private World World { get; }
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
        
        public long ProductionTime
        {
            get => Description.ProductionTimeByLevel[Level];
        }

        public ResourceRequests Orders { get; private set; }

        public ProductionBuildingModel(string id,
            Vector2Int gridPosition,
            ProductionBuildingDescription description, World world) : base(id, gridPosition,
            description)
        {
            World = world;
            WorldDescription = world.WorldDescription;
            Description = description;

            IsActive = false;

            Orders = new ResourceRequests();

            ResourceDescription = WorldDescription.ResourceCollection.Descriptions[Description.ProductionResource];
            Inventory = new InventoryModel(Description.MaxResource, WorldDescription.ResourceCollection);
            for (var i = 0; i < Description.ResourcesForProduction.Count + Description.ResourcesForWork.Count + 1; i++)
            {
                Inventory.Create();
            }
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
            CloseOrder(resourceDescription, amount);
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
            CloseOrder(resourceDescription, amount);
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
            base.Deserialize(data);
            
            IsActive = data.GetBool("is_active");
            Progress = data.GetFloat("progress");
            
            Inventory = new InventoryModel(Description.MaxResource, WorldDescription.ResourceCollection);
            Inventory.Deserialize(data.GetNode("inventory"));

            Orders = new ResourceRequests();
            Orders.Deserialize(data.GetNode("orders"));
        }

        public bool Produce()
        {
            if (CapacityLeft())
            {
                foreach (var resource in Description.ResourcesForProduction)
                {
                    Inventory.TryRemoveItem(WorldDescription.ResourceCollection.Descriptions[resource.Key],
                        resource.Value);
                }
                
                Inventory.TryAddItem(ResourceDescription, Description.ProductionAmount);
                
                var order = new OrderModel($"{Id}_{ResourceDescription.Id}", Id)
                {
                    Type = "take_resource",
                    ResourceId = ResourceDescription.Id,
                    Amount = Description.ProductionAmount
                };
                Orders.Add(order.ResourceId, order.Amount);
                World.OrderManager.AddOrder(order);
                return true;
            }

            return false;
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
            var hasEnough = true;
            
            foreach (var resource in resources)
            {
                if (!Inventory.CanExtract(WorldDescription.ResourceCollection.Descriptions[resource.Key], resource.Value, out _))
                {
                    hasEnough = false;
                    var order = new OrderModel($"{Id}_{resource.Key}", Id)
                    {
                        Type = "put_resource",
                        ResourceId = resource.Key,
                        Amount = resource.Value
                    };
                    if (!Orders.Contains(order.ResourceId))
                    {
                        Orders.Add(order.ResourceId, order.Amount);
                        World.OrderManager.AddOrder(order);
                    }
                }
            }

            return hasEnough;
        }

        private void CloseOrder(ResourceDescription resource, int amount)
        {
            if (Orders.Contains(resource.Id))
            {
                Orders.Remove(resource.Id, amount);
            }
        }
    }
}