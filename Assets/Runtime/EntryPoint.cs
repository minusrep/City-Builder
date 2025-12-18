using System.Collections.Generic;
using System.Threading.Tasks;
using fastJSON;
using Runtime.AsyncLoad;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Citizens;
using Runtime.Colony.Citizens.Systems;
using Runtime.Colony.GameResources;
using Runtime.Colony.StateMachine;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BuildingCollectionView _buildingCollectionView;

        [SerializeField] private CitizenView _citizenView;
        
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
        
        private CitizenStatSystem _citizenFeedStatSystem;
        
        private CitizenStatSystem _citizenStarvationSystem;

        private StateMachineSystem _stateMachineSystem;
        
        private CitizenPresenter _citizenPresenter;
        
        private CitizenModel _citizenModel;

        private async void Start()
        {
            _gameSystemCollection = new GameSystemCollection();
            
            InitializeAddressable();

            InitializeDescriptions();

            await InitializeViewDescriptionsAsync();

            InitializeModelFactories();
            
            _world = new World(_worldDescription, _factoryProvider, _gameSystemCollection);

            _buildingCollectionPresenter = new BuildingCollectionPresenter(_world,
                _buildingCollectionView, _worldDescription.BuildingCollection, _viewDescriptions, _gameSystemCollection);
            _buildingCollectionPresenter.Enable();

            _citizenModel = new CitizenModel(0, _world.WorldDescription.Citizens);

            _citizenModel.Stats["satiety"] = 50;

            _citizenPresenter = new CitizenPresenter(_citizenView, _citizenModel, _world);
            
            _stateMachineSystem = new StateMachineSystem(_citizenModel.StateMachine, _world, _citizenModel);
            
            _gameSystemCollection.Add(_stateMachineSystem);
            
            _world.Citizens.Add("0", _citizenModel);
            
            foreach (var citizen in _world.Citizens.Models.Values)
            {
                _world.CitizenStarvationStatSystem.Register(citizen);
            }
            
            _citizenPresenter.Enable();
        }

        private void Update()
        {
            _gameSystemCollection.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            _buildingCollectionPresenter.Disable();
        }

        private void InitializeModelFactories()
        {
            _resourceFactory = new ResourceFactory(_worldDescription.ResourceCollection);
            _buildingModelFactory = new BuildingModelFactory(_worldDescription);

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

            var data = new Dictionary<string, object>
            {
                { "buildings", buildingDescriptions },
                { "citizens", citizensDescriptions },
                { "resources", resourcesDescriptions },
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
    }
}