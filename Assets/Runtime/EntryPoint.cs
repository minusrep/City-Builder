using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Buildings;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using UnityEngine;
using System.IO;
using fastJSON;

namespace Runtime
{
    public sealed class EntryPoint : MonoBehaviour
    {
        private Descriptions.Descriptions _descriptions;

        private ResourceFactory _resourceFactory;
        private BuildingFactory _buildingFactory;

        private BuildingModelCollection _buildings;
        private CitizenModelCollection _citizens;

        private void Start()
        {
            InitializeDescriptions();

            InitializeModelFactories(new CitizenNeedServiceMock());

            InitializeBuildings();

            InitializeCitizens();
        }

        private void InitializeCitizens()
        {
            _citizens = new CitizenModelCollection(_descriptions.CitizensDescription);

            var path = Path.Combine(Application.persistentDataPath, "citizens_data.json");
            if (File.Exists(path))
            {
                var data = (Dictionary<string, object>)JSON.Parse(File.ReadAllText(path));

                _citizens.Deserialize(data);
            }
        }

        private void InitializeBuildings()
        {
            _buildings = new BuildingModelCollection(_descriptions.BuildingDescriptionCollection, _buildingFactory);

            var path = Path.Combine(Application.persistentDataPath, "buildings_data.json");
            if (File.Exists(path))
            {
                var data = (Dictionary<string, object>)JSON.Parse(File.ReadAllText(path));
                
                _buildings.Deserialize(data);
            }
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