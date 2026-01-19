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
using Runtime.ViewDescriptions.UI;
using Runtime.ViewDescriptions.UI.Menu;

namespace Runtime.LoadSteps
{
    public class ViewDescriptionsLoadStep : IStep
    {
        private readonly AddressableModel _addressableModel;
        private readonly Dictionary<string, Action<Object>> _loadMap;

        public ViewDescriptionsLoadStep(WorldViewDescriptions worldViewDescriptions, AddressableModel addressableModel)
        {
            var worldViewDescriptions1 = worldViewDescriptions;
            _addressableModel = addressableModel;

            _loadMap = new Dictionary<string, Action<Object>>
            {
                {
                    "BuildingViewDescriptionCollection",
                    obj => worldViewDescriptions1.BuildingViewDescriptions = obj as BuildingViewDescriptionCollection
                },
                {
                    "CitizenViewDescriptionCollection",
                    obj => worldViewDescriptions1.CitizenViewDescriptionCollection = obj as CitizenViewDescriptionCollection
                },
                {
                    "InventoryViewDescription",
                    obj => worldViewDescriptions1.InventoryViewDescription = obj as InventoryViewDescription
                },
                {
                    "StatViewDescriptionCollection",
                    obj => worldViewDescriptions1.StatViewDescriptions = obj as StatViewDescriptionCollection
                },
                {
                    "AchievementViewDescriptionCollection",
                    obj => worldViewDescriptions1.AchievementsViewDescription =
                        obj as AchievementViewDescriptionCollection
                },
                {
                    "MenuViewDescription",
                    obj => worldViewDescriptions.MenuViewDescription = obj as MenuViewDescription
                },
                {
                    "HudViewDescription",
                    obj => worldViewDescriptions.HudViewDescription = obj as HudViewDescription
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