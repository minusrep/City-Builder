using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Runtime.AsyncLoad;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Achievements;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;
using Object = UnityEngine.Object;

namespace Runtime.LoadSteps
{
    public class ViewDescriptionsLoadStep : IStep
    {
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly AddressableModel _addressableModel;
        private readonly Dictionary<string, Action<Object>> _loadMap;

        public ViewDescriptionsLoadStep(WorldViewDescriptions worldViewDescriptions, AddressableModel addressableModel)
        {
            _worldViewDescriptions = worldViewDescriptions;
            _addressableModel = addressableModel;

            _loadMap = new Dictionary<string, Action<Object>>
            {
                {
                    "BuildingViewDescriptionCollection",
                    obj => _worldViewDescriptions.BuildingViewDescriptions = obj as BuildingViewDescriptionCollection
                },
                {
                    "InventoryViewDescription",
                    obj => _worldViewDescriptions.InventoryViewDescription = obj as InventoryViewDescription
                },
                {
                    "StatViewDescriptionCollection",
                    obj => _worldViewDescriptions.StatViewDescriptions = obj as StatViewDescriptionCollection
                },
                {
                    "AchievementViewDescriptionCollection",
                    obj => _worldViewDescriptions.AchievementsViewDescription =
                        obj as AchievementViewDescriptionCollection
                }
            };
        }

        public async Task Run()
        {
            var tasks = _loadMap.Select(async kvp =>
            {
                var model = _addressableModel.Load<Object>(kvp.Key);

                await model.LoadAwaiter;

                kvp.Value(model.Result);
            }).ToArray();

            await Task.WhenAll(tasks);
        }
    }
}