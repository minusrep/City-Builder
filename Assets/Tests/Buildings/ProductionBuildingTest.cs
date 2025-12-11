/*
using Runtime.WorldDescription.Buildings;
using System.Collections.Generic;
using Runtime.Colony.Buildings;
using Runtime.Colony.Orders;
using NUnit.Framework;
using UnityEngine;
using System;

namespace Tests.Buildings
{
    [TestFixture]
    [TestOf(typeof(ProductionBuildingModel))]
    public class ProductionBuildingTest
    {
        private ProductionBuildingDescription _description;
        private OrderManager _orders;

        [SetUp]
        public void Setup()
        {
            _description = new ProductionBuildingDescription("0",new Dictionary<string, object>
            {
                { "type" , "production" },
                { "production_amount", 1 },
                { "production_time", 10 },
                { "max_resource", 3 },
                { "production_resource", "wood" }
            });
            
            _orders = new OrderManager();
        }
        
        private ProductionBuildingModel CreateModel(int producedAmount = 0)
        {
            return new ProductionBuildingModel(
                id: 1,
                position: new Vector2(0, 0),
                description: _description,
                orders: _orders,
                producedAmount: producedAmount);
        }
        
        [Test]
        public void StartProduction_WhenEnoughCapacity_Starts()
        {
            var model = CreateModel();

            model.StartProduction(DateTime.UtcNow.Ticks);
            
            var isActive = model.IsActive;
            Assert.IsTrue(isActive);
        }
        
        [Test]
        public void StartProduction_WhenNoCapacity_DoesNotStart()
        {
            var model = CreateModel(_description.MaxResource);

            model.StartProduction(DateTime.UtcNow.Ticks);

            var isActive = model.IsActive;
            Assert.IsFalse(isActive);
        }
        
        [Test]
        public void Update_WhenTimeReached_ProducesResourceAndCreatesOrder()
        {
            var model = CreateModel();

            const long start = 1000;
            model.StartProduction(start);

            model.Update(start + 10);

            var produced = model.ProducedAmount;
            Assert.AreEqual(1, produced);

            Assert.AreEqual(1, _orders.AvailableOrders.Count);
        }
        
        [Test]
        public void Update_WhenMultipleTicks_ProducesMultiple()
        {
            var model = CreateModel();

            const long start = 1000;
            model.StartProduction(start);

            model.Update(start + 30);

            var produced = model.ProducedAmount;
            Assert.AreEqual(3, produced);
            Assert.AreEqual(3, _orders.AvailableOrders.Count);
        }
        
        [Test]
        public void Update_WhenCapacityReached_StopsProduction()
        {
            var model = CreateModel();

            model.StartProduction(0);

            model.Update(40);

            Assert.AreEqual(3, model.ProducedAmount);
            Assert.AreEqual(3, _orders.AvailableOrders.Count);

            var active = model.IsActive;
            Assert.IsFalse(active);
        }
        
        [Test]
        public void Update_WhenProductionTimeZero_ProduceOnceAndStop()
        {
            _description = new ProductionBuildingDescription("0", new Dictionary<string, object>
            {
                { "type" , "production" },
                { "production_amount", 1 },
                { "production_time", 0 },
                { "max_resource", 3 },
                { "production_resource", "wood" }
            });

            var model = CreateModel();

            model.StartProduction(0);
            model.Update(0);

            Assert.AreEqual(1, model.ProducedAmount);
            Assert.AreEqual(1, _orders.AvailableOrders.Count);

            var active = model.IsActive;
            Assert.IsFalse(active);
        }
        
        [Test]
        public void StopProduction_Stops()
        {
            var model = CreateModel();

            model.StartProduction(0);
            model.StopProduction();

            var active = model.IsActive;
            Assert.IsFalse(active);
        }
    }
}
*/
