using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using UnityEngine;
using fastJSON;
using Runtime.Colony;
using Runtime.Colony.Buildings;
using Runtime.Services.SaveLoad;
using Runtime.ViewDescriptions.Buildings;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BuildingViewDescriptionCollection _viewDescriptionCollection;
        [SerializeField] private BuildingCollectionView _buildingCollectionView;

        private WorldDescription _worldDescription;

        private FactoryProvider _factoryProvider;

        private ResourceFactory _resourceFactory;
        private BuildingFactory _buildingFactory;

        private World _world;

        private BuildingCollectionPresenter _buildingCollectionPresenter;

        private void Start()
        {
            InitializeDescriptions();

            InitializeModelFactories(new CitizenNeedServiceMock());
            
            var saveLoadService = new SaveLoadService(new LocalSaveLoadStrategy(_worldDescription, _factoryProvider));
            _world = saveLoadService.Load();
            
            _buildingCollectionPresenter = new BuildingCollectionPresenter(_world.Buildings, _viewDescriptionCollection,
                _buildingCollectionView);
            _buildingCollectionPresenter.Enable();
        }

        private void InitializeModelFactories(ICitizenNeedService needService)
        {
            _resourceFactory = new ResourceFactory(_worldDescription.ResourceDescriptionCollection);
            _buildingFactory = new BuildingFactory(needService, _resourceFactory);

            _buildingFactory.RegisterAll();
            _factoryProvider = new FactoryProvider(_resourceFactory, _buildingFactory);
        }

        private void InitializeDescriptions()
        {
            var textAsset = Resources.Load<TextAsset>("descriptions");
            var descriptionData = (Dictionary<string, object>)JSON.Parse(textAsset.text);

            var descriptionFactory = new DescriptionFactory();
            descriptionFactory.Register<ProductionBuildingDescription>("production");
            descriptionFactory.Register<ServiceBuildingDescription>("service");
            descriptionFactory.Register<DecorBuildingDescription>("decor");
            descriptionFactory.Register<StorageBuildingDescription>("storage");

            _worldDescription = new WorldDescription(descriptionData);
        }


        internal class CitizenNeedServiceMock : ICitizenNeedService
        {
            public void RestoreNeed(int citizenId, string needId)
            {
            }
        }
    }
}