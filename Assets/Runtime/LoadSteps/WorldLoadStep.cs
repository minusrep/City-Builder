using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using fastJSON;
using Runtime.Colony;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Input;
using Runtime.SaveSystem;
using UnityEngine;

namespace Runtime.LoadSteps
{
    public class WorldLoadStep : IStep
    {
        private static string WorldDataPath => Path.Combine(Application.streamingAssetsPath, "world.json");

        private readonly World _world;
        private readonly WorldDescription _worldDescription;
        private readonly GameSystemCollection _gameSystems;
        private readonly PlayerControls _playerControls;
        private readonly string _saveName;

        public WorldLoadStep(World world, WorldDescription worldDescription, GameSystemCollection gameSystems,
            PlayerControls playerControls, string saveName = null)
        {
            _world = world;
            _worldDescription = worldDescription;
            _gameSystems = gameSystems;
            _playerControls = playerControls;
            _saveName = saveName;
        }

        public async Task Run()
        {
            _world.SetData(_worldDescription, _gameSystems, _playerControls);

            var loadPath = !string.IsNullOrEmpty(_saveName)
                ? SaveFileManager.GetSaveFilePath(_saveName)
                : WorldDataPath;

            if (File.Exists(loadPath))
            {
                var json = await File.ReadAllTextAsync(loadPath);

                var dictionary = JSON.ToObject<Dictionary<string, object>>(json);

                _world.Deserialize(dictionary);
            }
        }
    }
}