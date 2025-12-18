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
    public class ProductionBuildingModel : BuildingModel
    {
        public ProductionBuildingDescription Description { get; }
        public InventoryModel Inventory { get; private set; }
        public bool IsActive { get; private set; }
        private ResourceDescription ResourceDescription { get; }
        private WorldDescription WorldDescription { get; }

        public long StartProductionTime;

        private OrderModelCollection _orders;

        public ProductionBuildingModel(string id,
            Vector2 position,
            ProductionBuildingDescription description, WorldDescription worldDescription) : base(id, position,
            description)
        {
            WorldDescription = worldDescription;
            Description = description;

            IsActive = false;

            _orders = new OrderModelCollection(id);

            ResourceDescription = worldDescription.ResourceCollection.Descriptions[Description.ProductionResource];
            Inventory = new InventoryModel(1, Description.MaxResource, WorldDescription.ResourceCollection);
            Inventory.TryAddItem(ResourceDescription, 0);
        }

        public void StartProduction(long currentTime)
        {
            if (!IsActive && CapacityLeft())
            {
                IsActive = true;
                StartProductionTime = currentTime;
            }
        }

        public void StopProduction()
        {
            IsActive = false;
            StartProductionTime = 0;
        }

        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize())
            {
                { "is_active", IsActive },
                { "inventory", Inventory.Serialize() },
                { "orders", _orders.Serialize() }
            };

            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            IsActive = data.GetBool("is_active");
            Inventory = new InventoryModel(1, Description.MaxResource, WorldDescription.ResourceCollection);
            Inventory.Deserialize(data.GetNode("inventory"));

            _orders = new OrderModelCollection(Id);
            _orders.Deserialize(data.GetNode("orders"));
        }

        public bool ProduceOnceAndQueue()
        {
            if (CapacityLeft())
            {
                Inventory.TryAddItem(ResourceDescription, Description.ProductionAmount);

                _orders.Create();
                return true;
            }

            return false;
        }

        private bool CapacityLeft()
        {
            return Inventory.CanFit(ResourceDescription, Description.ProductionAmount, out _);
        }
    }
}