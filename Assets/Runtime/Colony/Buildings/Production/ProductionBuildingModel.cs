using System;
using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Colony.Orders;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingModel : BuildingModel
    {
        public bool IsActive { get; private set; }
        public int ProducedAmount { get; private set; }
        
        public long CompleteProductionTime;
        public long StartProductionTime;

        public InventoryModel Inventory { get; }

        public ProductionBuildingDescription Description { get; }

        private OrderModelCollection _orders;

        public ProductionBuildingModel(string id,
            Vector2 position,
            ProductionBuildingDescription description, int producedAmount) : base(id, position, description)
        {
            Description = description;
            ProducedAmount = producedAmount;

            IsActive = false;

            _orders = new OrderModelCollection(id);
            Inventory = new InventoryModel(1);
        }

        public void StartProduction(long currentTime)
        {
            if (!IsActive && CapacityLeft() > 0)
            {
                IsActive = true;
                StartProductionTime = currentTime;
                CompleteProductionTime = currentTime + Description.ProductionTime;
            }
        }

        public void StopProduction()
        {
            IsActive = false;
            CompleteProductionTime = 0;
            StartProductionTime = 0;
        }

        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize())
            {
                { "is_active", IsActive },
                { "produced_amount", ProducedAmount },
                { "complete_production_time", CompleteProductionTime },
                { "orders", _orders.Serialize() }
            };

            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            IsActive = data.GetBool("is_active");
            ProducedAmount = data.GetInt("produced_amount");
            CompleteProductionTime = data.GetLong("complete_production_time");
            
            _orders = new OrderModelCollection(Id);
            _orders.Deserialize(data.GetNode("orders"));
        }

        public bool ProduceOnceAndQueue()
        {
            if (CapacityLeft() > 0)
            {
                var canAdd = Math.Min(CapacityLeft(), Description.ProductionAmount);
                ProducedAmount += canAdd;

                _orders.Create();
                return true;
            }

            return false;
        }

        private int CapacityLeft()
        {
            return Description.MaxResource - ProducedAmount;
        }
    }
}