using System.Threading.Tasks;
using Runtime.AsyncLoad;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;

namespace Runtime.Services.SaveLoadSteps
{
    public class ViewDescriptionsLoadStep : IStep
    {
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions;
        private readonly AddressableModel _addressableModel;

        public ViewDescriptionsLoadStep(ViewDescriptions.ViewDescriptions viewDescriptions, AddressableModel addressableModel)
        {
            _viewDescriptions = viewDescriptions;
            _addressableModel = addressableModel;
        }

        public async Task Run()
        {
            var buildingViewLoad = _addressableModel.Load<BuildingViewDescriptionCollection>("BuildingViewDescriptionCollection");
            var inventoryViewLoad = _addressableModel.Load<InventoryViewDescription>("InventoryViewDescription");

            await buildingViewLoad.LoadAwaiter;
            await inventoryViewLoad.LoadAwaiter;

            _viewDescriptions.BuildingViewDescriptions = buildingViewLoad.Result;
            _viewDescriptions.InventoryViewDescription = inventoryViewLoad.Result;
        }
    }
}