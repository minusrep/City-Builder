using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using fastJSON;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.GameResources;
using Runtime.Descriptions;
using Runtime.GameSystems;
using UnityEngine;

namespace Runtime.Services.SaveLoadSteps
{
    public class WorldLoadStep : IStep
    {
        private static string WorldDataPath => Path.Combine(Application.streamingAssetsPath, "world.json");
        
        private readonly World _world;
        private readonly WorldDescription _worldDescription;
        private readonly GameSystemCollection _gameSystems;

        public WorldLoadStep(World world, WorldDescription worldDescription, GameSystemCollection gameSystems)
        {
            _world = world;
            _worldDescription = worldDescription;
            _gameSystems = gameSystems;
        }
        
        public async Task Run()
        {
            var resourceFactory = new ResourceFactory(_worldDescription.ResourceCollection);
            var buildingModelFactory = new BuildingModelFactory(_worldDescription);

            buildingModelFactory.RegisterAll();
            var factoryProvider = new FactoryProvider(resourceFactory, buildingModelFactory);
            
            _world.SetData(_worldDescription, factoryProvider, _gameSystems);
            
            if (File.Exists(WorldDataPath))
            {
                 var json = await File.ReadAllTextAsync(WorldDataPath);
                
                var dictionary = JSON.ToObject<Dictionary<string, object>>(json);
                
                _world.Deserialize(dictionary);
            }
        }
    }
}