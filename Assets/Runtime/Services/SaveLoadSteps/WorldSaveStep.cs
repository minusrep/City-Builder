using System.IO;
using System.Threading.Tasks;
using fastJSON;
using Runtime.Colony;
using UnityEngine;

namespace Runtime.Services.SaveLoadSteps
{
    public class WorldSaveStep : IStep
    {
        private static string WorldDataPath => Path.Combine(Application.streamingAssetsPath, "world.json");
        
        private readonly World _world;

        public WorldSaveStep(World world)
        {
            _world = world;
        }
        
        public async Task Run()
        {
            var json = JSON.ToJSON(_world.Serialize(), new JSONParameters() { UseExtensions = false });
            await File.WriteAllTextAsync(WorldDataPath, json);
        }
    }
}