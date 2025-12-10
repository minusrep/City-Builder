using System.Collections.Generic;
using NUnit.Framework;
using Runtime.Colony.GameResources;
using Runtime.Colony.Inventory;
using Runtime.Descriptions.GameResources;

namespace Tests
{
    [TestFixture]
    public class InventoryTest
    {
        [SetUp]
        public void Setup()
        {

        }
        
        [Test]
        public void CreateEmptyCells_SeveralCells()
        {
            var inventory = new InventoryModel(8);

            Assert.AreEqual(8, inventory.Cells.Count);
        }
        
        [Test]
        public void TryAddItem_SeveralItems()
        {
            var inventory = new InventoryModel(3);

            Assert.IsTrue(inventory.CanFit(MockData.ResourceDescriptions["wood"], 15, out var woodFreeSpace));
            Assert.AreEqual(2, woodFreeSpace.Count);
            inventory.TryAddItem(MockData.ResourceDescriptions["wood"], 15);
            
            Assert.IsTrue(inventory.CanFit(MockData.ResourceDescriptions["iron"], 6, out var ironFreeSpace));
            Assert.AreEqual(1, ironFreeSpace.Count);
            inventory.TryAddItem(MockData.ResourceDescriptions["iron"], 6);
            
            Assert.IsTrue(inventory.CanFit(MockData.ResourceDescriptions["wood"], 5, out var woodFreeSpace1));
            Assert.AreEqual(1, woodFreeSpace1.Count);
            inventory.TryAddItem(MockData.ResourceDescriptions["wood"], 5);
            
            Assert.AreEqual(10, inventory.Cells[0].Amount);
            
            if (inventory.Cells[0].Item is ResourceModel res)
            {
                Assert.AreEqual("wood", res.Description.Type);
            }
            
            Assert.AreEqual(10, inventory.Cells[1].Amount);
            
            if (inventory.Cells[1].Item is ResourceModel res1)
            {
                Assert.AreEqual("wood", res1.Description.Type);
            }
            
            Assert.AreEqual(6, inventory.Cells[2].Amount);
            
            if (inventory.Cells[2].Item is ResourceModel res2)
            {
                Assert.AreEqual("iron", res2.Description.Type);
            }
        }

        [Test]
        public void TryRemoveItem_OneItem()
        {
            var inventory = new InventoryModel(3);
            
            Assert.AreEqual(3, inventory.Cells.Count);
            
            inventory.TryAddItem(MockData.ResourceDescriptions["wood"], 10);
            
            Assert.AreEqual(10, inventory.Cells[0].Amount);
            
            if (inventory.Cells[0].Item is ResourceModel wood)
            {
                Assert.AreEqual("wood", wood.Description.Type);
            }

            inventory.TryRemoveItem(MockData.ResourceDescriptions["wood"], 6);
            
            Assert.AreEqual(4, inventory.Cells[0].Amount);
        }
        
        [Test]
        public void TryRemoveItem_SeveralItems()
        {
            var inventory = new InventoryModel(3);
            
            Assert.AreEqual(3, inventory.Cells.Count);
            
            inventory.TryAddItem(MockData.ResourceDescriptions["wood"], 18);
            
            Assert.AreEqual(10, inventory.Cells[0].Amount);
            Assert.AreEqual(8, inventory.Cells[1].Amount);
            
            if (inventory.Cells[0].Item is ResourceModel wood1)
            {
                Assert.AreEqual("wood", wood1.Description.Type);
            }
            
            if (inventory.Cells[1].Item is ResourceModel wood2)
            {
                Assert.AreEqual("wood", wood2.Description.Type);
            }

            inventory.TryRemoveItem(MockData.ResourceDescriptions["wood"], 12);
            
            Assert.AreEqual(6, inventory.Cells[0].Amount);
        }
    }

    public static class MockData
    {
        public static Dictionary<string, ResourceModel> ResourceDescriptions = new()
        {
            {
                "wood",
                new ResourceModel(new ResourceDescription(new Dictionary<string, object>
                {
                    { "type", "wood" },
                    { "reduction_time", 0},
                    { "reduction_amount", 0}
                }
                ))
                {
                    MaxAmount = 10
                }
            },
            {
                "iron",
                new ResourceModel(new ResourceDescription(new Dictionary<string, object>
                {
                    { "type", "iron" },
                    { "reduction_time", 0},
                    { "reduction_amount", 0}
                }))
                {
                    MaxAmount = 10
                }
            }
        };
    }
}