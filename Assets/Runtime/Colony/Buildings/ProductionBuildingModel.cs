using System;
using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using Runtime.Colony.Orders;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public class ProductionBuildingModel : BuildingModel
    {
        public bool IsActive => _isActive;
        
        private ProductionBuildingDescription Description { get; }
        private ColonyOrdersPool OrderPool { get; }
        private ResourceModel Resource { get; }
        
        private int _producedAmount;
        private long _completeProductionTime;
        private bool _isActive;

        public ProductionBuildingModel(int id,
            Vector2 position,
            ProductionBuildingDescription description,
            ColonyOrdersPool orderPool,
            ResourceModel resource) : base(id, position)
        {
            Description = description;
            OrderPool = orderPool;
            Resource = resource;

            _isActive = false;
        }
        
        public void StartProduction(long currentTime)
        {
            if (!_isActive && CapacityLeft() > 0)
            {
                _isActive = true;
                _completeProductionTime = currentTime + Description.ProductionTime;
            }
        }

        public void StopProduction()
        {
            _isActive = false;
            _completeProductionTime = 0;
        }
        
        public void Update(long currentTime)
        {
            if (_isActive)
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
                _producedAmount += canAdd;

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
            return Description.MaxResource - _producedAmount;
        }
    }
}