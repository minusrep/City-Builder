using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using Runtime.Colony.Orders;
using UnityEngine;
using System;

namespace Runtime.Colony.Buildings
{
    public class ProductionBuildingModel : BuildingModel
    {
        public bool IsActive { get; private set; }

        public int ProducedAmount { get; private set; }

        private ProductionBuildingDescription Description { get; }
        private ColonyOrdersPool OrderPool { get; }
        private ResourceModel Resource { get; }
        
        private long _completeProductionTime;

        public ProductionBuildingModel(int id,
            Vector2 position,
            ProductionBuildingDescription description,
            ColonyOrdersPool orderPool,
            ResourceModel resource, int producedAmount) : base(id, position)
        {
            Description = description;
            OrderPool = orderPool;
            Resource = resource;
            ProducedAmount = producedAmount;

            IsActive = false;
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
        
        private bool ProduceOnceAndQueue()
        {
            if (CapacityLeft() > 0)
            {
                var canAdd = Math.Min(CapacityLeft(), Description.ProductionAmount);
                ProducedAmount += canAdd;

                CreateDeliveryOrder();
                return true;
            }

            return false;
        }

        private void CreateDeliveryOrder()
        {
            var order = new DeliveryOrder(0, Id, Description.ProductionResource);
            OrderPool.AddOrder(order);
        }
        
        private int CapacityLeft()
        {
            return Description.MaxResource - ProducedAmount;
        }
    }
}