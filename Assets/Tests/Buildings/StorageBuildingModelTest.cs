using Runtime.Descriptions.GameResources;
using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Buildings;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Buildings
{
    [TestFixture]
    [TestOf(typeof(StorageBuildingModel))]
    public class StorageBuildingModelTest
    {
        private StorageBuildingDescription _description;
        private Dictionary<string, ResourceModel> _resources;
        
        [SetUp]
        public void SetUp()
        {
            _description = new StorageBuildingDescription
            {
                MaxResourceAmount = 100
            };

            _resources = new Dictionary<string, ResourceModel>
            {
                {
                    "wood",
                    new ResourceModel(new ResourceDescription
                    {
                        Type = "wood"
                    })
                },
                {
                    "iron",
                    new ResourceModel(new ResourceDescription
                    {
                        Type = "iron"
                    })
                }
            };
        }

        private StorageBuildingModel CreateModel()
        {
            return new StorageBuildingModel(
                id: 1,
                position: Vector2.zero,
                description: _description,
                _resources);
        }


        [Test]
        public void TryAddResource_WhenEnoughCapacity_AddResource()
        {
            var model = CreateModel();

            var resource = new ResourceModel(new ResourceDescription { Type = "wood" });
            resource.IncreaseAmount(30);

            var result = model.TryAddResource("wood", resource);
            
            Assert.IsTrue(result);
            Assert.AreEqual(30, _resources["wood"].Amount);
        }

        [Test]
        public void TryAddResource_WhenExceedsCapacity_ReturnsFalse()
        {
            var model = CreateModel();
            
            _resources["wood"].IncreaseAmount(90);
            
            var wood = new ResourceModel(new ResourceDescription { Type = "wood" });
            wood.IncreaseAmount(20);

            var result = model.TryAddResource("wood", wood);

            Assert.IsFalse(result);
            Assert.AreEqual(90, _resources["wood"].Amount);
        }
        
        [Test]
        public void TryAddResource_WhenResourceNotRegistered_ReturnsFalse()
        {
            var model = CreateModel();

            var stone = new ResourceModel(new ResourceDescription { Type = "stone" });
            stone.IncreaseAmount(10);

            var result = model.TryAddResource("stone", stone);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void TryTakeResource_WhenEnoughAmount_TakesResource()
        {
            var model = CreateModel();

            _resources["iron"].IncreaseAmount(50);

            var result = model.TryTakeResource("iron", 30);

            Assert.IsTrue(result);
            Assert.AreEqual(20, _resources["iron"].Amount);
        }

        [Test]
        public void TryTakeResource_WhenNotEnoughAmount_ReturnsFalse()
        {
            var model = CreateModel();

            _resources["iron"].IncreaseAmount(10);

            var result = model.TryTakeResource("iron", 30);

            Assert.IsFalse(result);
            Assert.AreEqual(10, _resources["iron"].Amount);
        }

        [Test]
        public void TryTakeResource_WhenResourceNotRegistered_ReturnsFalse()
        {
            var model = CreateModel();

            var result = model.TryTakeResource("stone", 10);

            Assert.IsFalse(result);
        }
    }
}