using System.Collections.Generic;
using fastJSON;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Citizens;
using Runtime.Colony.Items;
using Runtime.Descriptions;
using UnityEngine;
using Runtime.Services.SaveLoad;
using Runtime.ViewDescriptions;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BuildingCollectionView _buildingCollectionView;

        private WorldDescription _worldDescription;
        private ViewDescriptions.ViewDescriptions _viewDescriptions;

        private FactoryProvider _factoryProvider;

        private ItemFactory _itemFactory;
        private BuildingModelFactory _buildingModelFactory;

        private World _world;

        private BuildingCollectionPresenter _buildingCollectionPresenter;

        private void Start()
        {
            InitializeDescriptions();

            InitializeModelFactories(new CitizenNeedServiceMock());

            var saveLoadService = new SaveLoadService(new LocalSaveLoadStrategy(_worldDescription, _factoryProvider));
            _world = saveLoadService.Load();

            _buildingCollectionPresenter = new BuildingCollectionPresenter(_world.Buildings,
                _viewDescriptions,
                _buildingCollectionView);
            _buildingCollectionPresenter.Enable();
        }

        private void InitializeModelFactories(ICitizenNeedService needService)
        {
            _itemFactory = new ItemFactory(_worldDescription.ItemCollection);
            _buildingModelFactory = new BuildingModelFactory(needService, _itemFactory);

            _buildingModelFactory.RegisterAll();
            _factoryProvider = new FactoryProvider(_itemFactory, _buildingModelFactory);
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
                { "resources", resourcesDescriptions }
            };

            _worldDescription = new WorldDescription(data);

            _viewDescriptions = ViewDescriptionsLoader.Load();
        }


        private class CitizenNeedServiceMock : ICitizenNeedService
        {
            public void RestoreNeed(int citizenId, string needId)
            {
            }
        }
    }
}