using Runtime.Descriptions.GameResources;
using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using NUnit.Framework;
using Runtime.Colony.Buildings.Models;
using UnityEngine;

namespace Tests.Buildings
{
    [TestFixture]
    [TestOf(typeof(StorageBuildingModel))]
    public class StorageBuildingModelTest
    {
        private StorageBuildingDescription _description;
        
        [SetUp]
        public void SetUp()
        {
            _description = new StorageBuildingDescription("0", new Dictionary<string, object>
            {
                { "type", "storage" },
                { "stored_resources", new List<object> { "wood" } },
                { "max_resource_amount", 100 },
            });
        }

        private StorageBuildingModel CreateModel()
        {
            return new StorageBuildingModel(
                id: "warehouse_0",
                position: Vector2.zero,
                description: _description, new ResourceFactory(new ResourceDescriptionCollection(new Dictionary<string, object>
                {
                    { "wood", new Dictionary<string, object>
                    {
                        { "type", "wood" },
                        { "reduction_time", 0},
                        { "reduction_amount", 0}
                    }},
                    { "iron", new Dictionary<string, object>
                    {
                        { "type", "iron" },
                        { "reduction_time", 0},
                        { "reduction_amount", 0}
                    }}
                })));
        }

        [Test]
        public void TryAddResource_WhenEnoughCapacity_AddResource()
        {
            var model = CreateModel();

            var resource = new ResourceModel(new ResourceDescription(new Dictionary<string, object>
            {
                { "type", "wood" },
                { "reduction_time", 0},
                { "reduction_amount", 0}
            }));
            resource.IncreaseAmount(30);

            var result = model.TryAddResource("wood", resource);
            
            Assert.IsTrue(result);
            Assert.AreEqual(30, model.GetAmount("wood"));
        }

        [Test]
        public void TryAddResource_WhenExceedsCapacity_ReturnsFalse()
        {
            var model = CreateModel();
            
            var resource = new ResourceModel(new ResourceDescription(new Dictionary<string, object>
            {
                { "type", "wood" },
                { "reduction_time", 0},
                { "reduction_amount", 0}
            }));
            resource.IncreaseAmount(110);

            var result = model.TryAddResource("wood", resource);

            Assert.IsFalse(result);
            Assert.AreEqual(0, model.GetAmount("wood"));
        }
        
        [Test]
        public void TryAddResource_WhenResourceNotRegistered_ReturnsFalse()
        {
            var model = CreateModel();

            var resource = new ResourceModel(new ResourceDescription(new Dictionary<string, object>
            {
                { "type", "wood" },
                { "reduction_time", 0},
                { "reduction_amount", 0}
            }));
            resource.IncreaseAmount(10);

            var result = model.TryAddResource("stone", resource);

            Assert.IsFalse(result);
        }
        
        [Test]
        public void TryTakeResource_WhenEnoughAmount_TakesResource()
        {
            var model = CreateModel();

            var resource = new ResourceModel(new ResourceDescription(new Dictionary<string, object>
            {
                { "type", "wood" },
                { "reduction_time", 0},
                { "reduction_amount", 0}
            }));
            resource.IncreaseAmount(50);
            model.TryAddResource("wood", resource);

            var result = model.TryTakeResource("wood", 30);

            Assert.IsTrue(result);
            Assert.AreEqual(20, model.GetAmount("wood"));
        }

        [Test]
        public void TryTakeResource_WhenNotEnoughAmount_ReturnsFalse()
        {
            var model = CreateModel();
            
            var resource = new ResourceModel(new ResourceDescription(new Dictionary<string, object>
                {
                    { "type", "wood" },
                    { "reduction_time", 0},
                    { "reduction_amount", 0}
                }));
            resource.IncreaseAmount(10);
            model.TryAddResource("wood", resource);

            var result = model.TryTakeResource("wood", 30);

            Assert.IsFalse(result);
            Assert.AreEqual(10, model.GetAmount("wood"));
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