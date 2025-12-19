using System.Threading.Tasks;
using Runtime.AsyncLoad;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;

namespace Runtime.Services.SaveLoadSteps
{
    public class ViewDescriptionsLoadStep : IStep
    {
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly AddressableModel _addressableModel;

        public ViewDescriptionsLoadStep(WorldViewDescriptions worldViewDescriptions, AddressableModel addressableModel)
        {
            _worldViewDescriptions = worldViewDescriptions;
            _addressableModel = addressableModel;
        }

        public async Task Run()
        {
            var buildingViewLoad = _addressableModel.Load<BuildingViewDescriptionCollection>("BuildingViewDescriptionCollection");
            var inventoryViewLoad = _addressableModel.Load<InventoryViewDescription>("InventoryViewDescription");

            await buildingViewLoad.LoadAwaiter;
            await inventoryViewLoad.LoadAwaiter;

            _worldViewDescriptions.BuildingViewDescriptions = buildingViewLoad.Result;
            _worldViewDescriptions.InventoryViewDescription = inventoryViewLoad.Result;
        }
    }
}