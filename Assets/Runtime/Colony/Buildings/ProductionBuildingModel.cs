using Runtime.Descriptions.Buildings;
using Runtime.Colony.Orders;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Runtime.Colony.Buildings
{
    public class ProductionBuildingModel : BuildingModel
    {
        public bool IsActive { get; private set; }
        public int ProducedAmount { get; private set; }
        private ColonyOrdersManager Orders { get; }
        
        private readonly ProductionBuildingDescription _description;

        private long _completeProductionTime;

        public ProductionBuildingModel(int id,
            Vector2 position,
            ProductionBuildingDescription description,
            ColonyOrdersManager orders, int producedAmount) : base(id, position, description)
        {
            _description = description;
            Orders = orders;
            ProducedAmount = producedAmount;

            IsActive = false;
        }
        
        public void StartProduction(long currentTime)
        {
            if (!IsActive && CapacityLeft() > 0)
            {
                IsActive = true;
                _completeProductionTime = currentTime + _description.ProductionTime;
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
                var productionTime = _description.ProductionTime;

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
                { "complete_production_time", _completeProductionTime }
            };
            return dictionary;
        }

        private bool ProduceOnceAndQueue()
        {
            if (CapacityLeft() > 0)
            {
                var canAdd = Math.Min(CapacityLeft(), _description.ProductionAmount);
                ProducedAmount += canAdd;

                CreateDeliveryOrder();
                return true;
            }

            return false;
        }

        private void CreateDeliveryOrder()
        {
            var order = new DeliveryOrder(0, Id, _description.ProductionResource);
            Orders.AddOrder(order);
        }
        
        private int CapacityLeft()
        {
            return _description.MaxResource - ProducedAmount;
        }
    }
}