using System.Collections.Generic;
using System.IO;
using fastJSON;
using NUnit.Framework;
using Runtime.Colony.Orders;
using UnityEngine;

namespace Tests.Buildings
{
    [TestFixture]
    public class OrderTest
    {
        private OrderModelCollection _collection;
        
        [SetUp]
        public void Setup()
        {
            _collection = new OrderModelCollection(5);
        }

        [Test]
        public void Serialize()
        {
            _collection.Create();
            _collection.Create();
            _collection.Create();

            var data= _collection.Serialize();
            
            var path = Path.Combine(Application.persistentDataPath, "save_order.json");
            
            File.WriteAllText(path, JSON.ToJSON(data));
            
            Assert.AreEqual(3, _collection.Models.Count);
        }

        [Test]
        public void Deserialize()
        {
            var path = Path.Combine(Application.persistentDataPath, "save_order.json");
            
            var jsonString = File.ReadAllText(path);
            var data = JSON.ToObject<Dictionary<string, object>>(jsonString);
            
            _collection.Deserialize(data);
            
            Assert.AreEqual(3, _collection.Models.Count);
        }
    }
}