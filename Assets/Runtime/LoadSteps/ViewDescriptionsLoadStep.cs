using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Runtime.AsyncLoad;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Achievements;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Citizens;
using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;
using Runtime.ViewDescriptions.UI.Load;
using Object = UnityEngine.Object;

namespace Runtime.LoadSteps
{
    public class ViewDescriptionsLoadStep : IStep
    {
        private readonly AddressableModel _addressableModel;
        private readonly Dictionary<string, Action<Object>> _loadMap;

        public ViewDescriptionsLoadStep(WorldViewDescriptions worldViewDescriptions, AddressableModel addressableModel)
        {
            _addressableModel = addressableModel;

            _loadMap = new Dictionary<string, Action<Object>>
            {
                {
                    "BuildingViewDescriptionCollection",
                    obj => worldViewDescriptions.BuildingViewDescriptions = obj as BuildingViewDescriptionCollection
                },
                {
                    "CitizenViewDescription",
                    obj => worldViewDescriptions.CitizenViewDescription = obj as CitizenViewDescription
                },
                {
                    "InventoryViewDescription",
                    obj => worldViewDescriptions.InventoryViewDescription = obj as InventoryViewDescription
                },
                {
                    "StatViewDescriptionCollection",
                    obj => worldViewDescriptions.StatViewDescriptions = obj as StatViewDescriptionCollection
                },
                {
                    "AchievementViewDescriptionCollection",
                    obj => worldViewDescriptions.AchievementsViewDescription =
                        obj as AchievementViewDescriptionCollection
                },
                {
                    "MenuViewDescription",
                    obj => worldViewDescriptions.MenuViewDescription = obj as MenuViewDescription
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