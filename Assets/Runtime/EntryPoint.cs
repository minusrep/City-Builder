using System.Collections.Generic;
using System.Threading.Tasks;
using fastJSON;
using Runtime.AsyncLoad;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Citizens;
using Runtime.Colony.GameResources;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.Services.SaveLoad;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BuildingCollectionView _buildingCollectionView;

        private WorldDescription _worldDescription;
        private ViewDescriptions.ViewDescriptions _viewDescriptions;

        private FactoryProvider _factoryProvider;

        private ResourceFactory _resourceFactory;
        private BuildingModelFactory _buildingModelFactory;

        private World _world;

        private BuildingCollectionPresenter _buildingCollectionPresenter;
        
        private GameSystemCollection _gameSystemCollection;
        
        private AddressableModel _addressableModel;
        private AddressablePresenter _addressablePresenter;
        
        private async void Start()
        {
            _gameSystemCollection = new GameSystemCollection();
            
            InitializeAddressable();

            InitializeDescriptions();

            await InitializeViewDescriptionsAsync();

            InitializeModelFactories(new CitizenNeedServiceMock());

            var saveLoadService = new SaveLoadService(new LocalSaveLoadStrategy(_worldDescription, _factoryProvider));
            _world = saveLoadService.Load();

            _buildingCollectionPresenter = new BuildingCollectionPresenter(_world.Buildings,
                _buildingCollectionView, _worldDescription.BuildingCollection, _viewDescriptions, _gameSystemCollection);
            _buildingCollectionPresenter.Enable();
        }

        private void Update()
        {
            _gameSystemCollection.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            _buildingCollectionPresenter.Disable();
        }

        private void InitializeModelFactories(ICitizenNeedService needService)
        {
            _resourceFactory = new ResourceFactory(_worldDescription.ItemCollection);
            _buildingModelFactory = new BuildingModelFactory(needService, _resourceFactory);

            _buildingModelFactory.RegisterAll();
            _factoryProvider = new FactoryProvider(_resourceFactory, _buildingModelFactory);
        }

        private void InitializeDescriptions()
        {
            var buildingDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/buildings_description").text);
            var citizensDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/citizens_description").text);
            var resourcesDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/items_description").text);

            var pointsOfInterestDescriptions =
                JSON.ToObject<Dictionary<string, object>>(
                    Resources.Load<TextAsset>("Descriptions/points_of_interest_description").text);
            
            var data = new Dictionary<string, object>
            {
                { "buildings", buildingDescriptions },
                { "citizens", citizensDescriptions },
                { "resources", resourcesDescriptions },
                { "points_of_interest", pointsOfInterestDescriptions}
            };

            _worldDescription = new WorldDescription(data);
        }

        private async Task InitializeViewDescriptionsAsync()
        {
            var buildingViewLoad = _addressableModel.Load<BuildingViewDescriptionCollection>("BuildingViewDescriptionCollection");
            var inventoryViewLoad = _addressableModel.Load<InventoryViewDescription>("InventoryViewDescription");

            await buildingViewLoad.LoadAwaiter;
            await inventoryViewLoad.LoadAwaiter;
            
            _viewDescriptions = new ViewDescriptions.ViewDescriptions(buildingViewLoad.Result, inventoryViewLoad.Result);
        }

        private void InitializeAddressable()
        {
            _addressableModel = new AddressableModel();
            _addressablePresenter = new AddressablePresenter(_addressableModel);
            
            _addressablePresenter.Enable();
        }

        private class CitizenNeedServiceMock : ICitizenNeedService
        {
            public void RestoreNeed(int citizenId, string needId)
            {
            }
        }
    }
}