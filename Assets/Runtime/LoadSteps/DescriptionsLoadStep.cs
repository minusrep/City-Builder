using System.Collections.Generic;
using System.Threading.Tasks;
using fastJSON;
using Runtime.AsyncLoad;
using Runtime.Descriptions;
using UnityEngine;

namespace Runtime.LoadSteps
{
    public class DescriptionsLoadStep : IStep
    {
        private readonly WorldDescription _worldDescription;
        private readonly AddressableModel _addressableModel;
        
        private readonly Dictionary<string, string> _keys = new()
        {
            { "buildings", "buildings_description" },
            { "citizens", "citizens_description" },
            { "resources", "items_description" },
            { "achievements", "achievements_description" },
            { "points_of_interest", "points_of_interest_description" },
            { "camera_control", "camera_control" },
            { "world_grid", "world_grid_description" }
        };

        public DescriptionsLoadStep(WorldDescription worldDescription, AddressableModel addressableModel)
        {
            _worldDescription = worldDescription;
            _addressableModel = addressableModel;
        }

        public async Task Run()
        {
            var data = new Dictionary<string, object>();
            
            foreach (var kvp in _keys)
            {
                var loadModel = _addressableModel.Load<TextAsset>(kvp.Value);

                await loadModel.LoadAwaiter;

                var parsed = JSON.ToObject<Dictionary<string, object>>(loadModel.Result.text);
                data[kvp.Key] = parsed;
            }
            
            _worldDescription.SetData(data);
        }
    }
}