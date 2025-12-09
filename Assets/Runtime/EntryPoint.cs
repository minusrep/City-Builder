using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Buildings;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using UnityEngine;
using System.IO;
using fastJSON;
using Runtime.Colony;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        private Descriptions.Descriptions _descriptions;

        private ResourceFactory _resourceFactory;
        private BuildingFactory _buildingFactory;

        private World _world;
        
        private void Start()
        {
            InitializeDescriptions();

            InitializeModelFactories(new CitizenNeedServiceMock());
            
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            _world = new World(InitializeCitizens(), InitializeBuildings());
        }
        
        private CitizenModelCollection InitializeCitizens()
        {
            var citizens = new CitizenModelCollection(_descriptions.CitizensDescription);

            var path = Path.Combine(Application.persistentDataPath, "citizens_data.json");
            if (File.Exists(path))
            {
                var data = (Dictionary<string, object>)JSON.Parse(File.ReadAllText(path));

                citizens.Deserialize(data);
            }

            return citizens;
        }

        private BuildingModelCollection InitializeBuildings()
        {
            var buildings = new BuildingModelCollection(_descriptions.BuildingDescriptionCollection, _buildingFactory);

            var path = Path.Combine(Application.persistentDataPath, "buildings_data.json");
            if (File.Exists(path))
            {
                var data = (Dictionary<string, object>)JSON.Parse(File.ReadAllText(path));
                
                buildings.Deserialize(data);
            }

            return buildings;
        }

        private void InitializeModelFactories(ICitizenNeedService needService)
        {
            _resourceFactory = new ResourceFactory(_descriptions.ResourceDescriptionCollection);
            _buildingFactory = new BuildingFactory(needService, _resourceFactory);
            _buildingFactory.RegisterAll();
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

            _descriptions = new Descriptions.Descriptions(descriptionData);
        }
    }

    internal class CitizenNeedServiceMock : ICitizenNeedService
    {
        public void RestoreNeed(int citizenId, string needId)
        {
        }
    }
}