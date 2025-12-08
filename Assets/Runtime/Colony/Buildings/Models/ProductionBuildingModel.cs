using System;
using System.Collections.Generic;
using Runtime.Colony.Orders;
using Runtime.Descriptions.Buildings;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Colony.Buildings.Models
{
    public class ProductionBuildingModel : BuildingModel
    {
        public bool IsActive { get; private set; }
        public int ProducedAmount { get; private set; }

        private ProductionBuildingDescription Description { get; }
        
        private OrderModelCollection _orders;
        private long _completeProductionTime;

        public ProductionBuildingModel(string id,
            Vector2 position,
            ProductionBuildingDescription description, int producedAmount) : base(id, position, description)
        {
            Description = description;
            ProducedAmount = producedAmount;

            IsActive = false;

            _orders = new OrderModelCollection(id);
        }

        public void StartProduction(long currentTime)
        {
            if (!IsActive && CapacityLeft() > 0)
            {
                IsActive = true;
                _completeProductionTime = currentTime + Description.ProductionTime;
            }
        }

        public void StopProduction()
        {
            IsActive = false;
            _completeProductionTime = 0;
        }

        public void Update(long currentTime)
        {
            if (IsActive)
            {
                var productionTime = Description.ProductionTime;

                if (productionTime <= 0)
                {
                    ProduceOnceAndQueue();
                    StopProduction();
                    return;
                }

                while (currentTime >= _completeProductionTime)
                {
                    if (ProduceOnceAndQueue())
                    {
                        _completeProductionTime += productionTime;
                    }
                    else
                    {
                        StopProduction();
                        break;
                    }
                }
            }
        }

        public override Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>(base.Serialize())
            {
                { "is_active", IsActive },
                { "produced_amount", ProducedAmount },
                { "complete_production_time", _completeProductionTime },
                { "orders", _orders.Serialize() }
            };

            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            IsActive = data.GetBool("is_active");
            ProducedAmount = data.GetInt("produced_amount");
            _completeProductionTime = data.GetLong("complete_production_time");
            
            _orders = new OrderModelCollection(Id);
            _orders.Deserialize(data.GetNode("orders"));
        }

        private bool ProduceOnceAndQueue()
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