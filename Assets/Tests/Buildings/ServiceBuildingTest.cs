using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using NUnit.Framework;
using Runtime.Colony.Buildings.Models;
using UnityEngine;

namespace Tests.Buildings
{
    [TestFixture]
    [TestOf(typeof(ServiceBuildingModel))]
    public sealed class ServiceBuildingTest
    {
        private ServiceBuildingDescription _description;
        
        [SetUp]
        public void Setup()
        {
            _description = new ServiceBuildingDescription("0", new Dictionary<string, object>
            {
                { "type" , "service" },
                { "max_citizen_amount", 2 },
                { "max_queue", 2 },
                { "max_time_service", 10 },
                { "service_resource", "food" }
            });
        }

        private ServiceBuildingModel CreateModel()
        {
            return new ServiceBuildingModel(
                id: "warehouse_0",
                position: Vector2.zero,
                description: _description,
                needService: new CitizenNeedServiceMock());
        }
        
        [Test]
        public void TryEnter_WhenServiceAvailable_AddsToService()
        {
            var model = CreateModel();

            var result = model.TryEnter(1);

            Assert.IsTrue(result);

            var inService = model.InService;
            Assert.AreEqual(1, inService.Count);
            Assert.AreEqual(_description.ServiceTime, inService[1]);
        }
        
        [Test]
        public void TryEnter_WhenServiceFull_AddsToQueue()
        {
            var model = CreateModel();

            model.TryEnter(1);
            model.TryEnter(2);

            var result = model.TryEnter(3);

            Assert.IsTrue(result);

            var queue = model.WaitingQueue;
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(3, queue.Peek());
        }
        
        [Test]
        public void TryEnter_WhenBothFull_ReturnsFalse()
        {
            var model = CreateModel();

            model.TryEnter(1);
            model.TryEnter(2);

            model.TryEnter(3);
            model.TryEnter(4);

            var result = model.TryEnter(5);
            Assert.IsFalse(result);
            
            var inService = model.InService;
            Assert.AreEqual(2, inService.Count);
            
            var queue = model.WaitingQueue;
            Assert.AreEqual(2, queue.Count);
            Assert.AreEqual(3, queue.Peek());
        }
        
        [Test]
        public void Update_WhenTimeElapsed_RestoresNeedAndRemovesFromService()
        {
            var model = CreateModel();

            model.TryEnter(1);

            model.Update(10);

            Assert.AreEqual(1, ((CitizenNeedServiceMock)model.NeedService).Calls.Count);
            Assert.AreEqual((1, "food"), ((CitizenNeedServiceMock)model.NeedService).Calls[0]);

            var inService = model.InService;
            Assert.AreEqual(0, inService.Count);
        }
        
        [Test]
        public void Update_WhenQueueExists_MovesNextCitizenFromQueue()
        {
            var model = CreateModel();

            model.TryEnter(1);
            model.TryEnter(2);
            model.TryEnter(3);

            model.Update(10);

            var inService = model.InService;
    
            Assert.IsTrue(inService.ContainsKey(3));
            Assert.AreEqual(_description.ServiceTime, inService[3]);
        }
        
        [Test]
        public void Update_WhenMultipleCitizensFinished_AllAreProcessed()
        {
            var model = CreateModel();

            model.TryEnter(1);
            model.TryEnter(2);

            model.Update(10);
            var mock = (CitizenNeedServiceMock)model.NeedService;
            Assert.AreEqual(2, mock.Calls.Count);
            Assert.AreEqual((1, "food"), mock.Calls[0]);
            Assert.AreEqual((2, "food"), mock.Calls[1]);

            var inService = model.InService;
            Assert.AreEqual(0, inService.Count);
        }
    }
}