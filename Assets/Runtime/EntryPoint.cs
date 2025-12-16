using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using UnityEngine;
using fastJSON;
using Runtime.Colony;
using Runtime.Colony.Buildings;
using Runtime.Colony.Buildings.Collection;
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
            _resourceFactory = new ResourceFactory(_worldDescription.ResourceCollection);
            _buildingFactory = new BuildingFactory(needService, _resourceFactory);

            _buildingFactory.RegisterAll();
            _factoryProvider = new FactoryProvider(_resourceFactory, _buildingFactory);
        }

        private void InitializeDescriptions()
        {
            var buildingsDescriptionsTextAsset = Resources.Load<TextAsset>("Descriptions/buildings_description");
            var citizensDescriptionTextAsset = Resources.Load<TextAsset>("Descriptions/citizens_description");
            var resourcesDescriptionTextAsset = Resources.Load<TextAsset>("Descriptions/resources_description");
            var pointsOfInterestDescriptionTextAsset = Resources.Load<TextAsset>("Descriptions/points_of_interest_description");
            var statesDescriptionTextAsset = Resources.Load<TextAsset>("Descriptions/states_description");

            var allDictionary = new Dictionary<string, object>();
            
            allDictionary.Add("buildings", JSON.ToObject<Dictionary<string, object>>(buildingsDescriptionsTextAsset.text));
            allDictionary.Add("citizens", JSON.ToObject<Dictionary<string, object>>(citizensDescriptionTextAsset.text));
            allDictionary.Add("resources", JSON.ToObject<Dictionary<string, object>>(resourcesDescriptionTextAsset.text));
            allDictionary.Add("points_of_interest", JSON.ToObject<Dictionary<string, object>>(pointsOfInterestDescriptionTextAsset.text));
            allDictionary.Add("states", JSON.ToObject<Dictionary<string, object>>(statesDescriptionTextAsset.text));
            
            var descriptionFactory = new DescriptionFactory();
            descriptionFactory.Register<ProductionBuildingDescription>("production");
            descriptionFactory.Register<ServiceBuildingDescription>("service");
            descriptionFactory.Register<DecorBuildingDescription>("decor");
            descriptionFactory.Register<StorageBuildingDescription>("storage");

            _worldDescription = new WorldDescription(allDictionary);
        }


        internal class CitizenNeedServiceMock : ICitizenNeedService
        {
            public void RestoreNeed(int citizenId, string needId)
            {
            }
        }
    }
}