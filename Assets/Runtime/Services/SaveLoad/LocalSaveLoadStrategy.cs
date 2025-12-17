using System.Collections.Generic;
using System.IO;
using fastJSON;
using Runtime.Colony;
using Runtime.Descriptions;
using UnityEngine;

namespace Runtime.Services.SaveLoad
{
    public class LocalSaveLoadStrategy : SaveLoadStrategy
    {
        private static string WorldDataPath => Path.Combine(Application.persistentDataPath, "world.json");
        
        public LocalSaveLoadStrategy(WorldDescription worldDescription, FactoryProvider factoryProvider) : base(worldDescription, factoryProvider)
        {
            
        }

        public override void Save(World world)
        {
            var json = JSON.ToNiceJSON(world.Serialize(), new JSONParameters()
            {
                UseExtensions = false
            });
            
            File.WriteAllText(WorldDataPath, json);
            
            Debug.Log("Saved World: " + WorldDataPath);
        }

        public override World Load()
        {
            var world = new World();
            world.SetData(_worldDescription, _factoryProvider);
            
            if (File.Exists(WorldDataPath))
            {
                var json = File.ReadAllText(WorldDataPath);
                
                var dictionary = JSON.ToObject<Dictionary<string, object>>(json);
                
                world.Deserialize(dictionary);
            }

            return world;
        }
    }
}