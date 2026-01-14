using System.IO;
using System.Threading.Tasks;
using fastJSON;
using Runtime.Colony;
using Runtime.SaveSystem;
using UnityEngine;

namespace Runtime.LoadSteps
{
    public class WorldSaveStep : IStep
    {
        private static string WorldDataPath => Path.Combine(Application.streamingAssetsPath, "world.json");

        private readonly World _world;
        private readonly string _saveName;

        public WorldSaveStep(World world, string saveName = null)
        {
            _world = world;
            _saveName = saveName;
        }

        public async Task Run()
        {
            var json = JSON.ToJSON(_world.Serialize(), new JSONParameters { UseExtensions = false });

            if (!string.IsNullOrEmpty(_saveName))
            {
                var savePath = SaveFileManager.GetSaveFilePath(_saveName);
                await File.WriteAllTextAsync(savePath, json);
            }
            else
            {
                await File.WriteAllTextAsync(WorldDataPath, json);
            }
        }
    }
}