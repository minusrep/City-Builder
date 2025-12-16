using System;
using System.Collections.Generic;
using Runtime.Colony.Buildings.Decor;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Buildings.Storage;
using Runtime.Colony.Citizens;
using Runtime.Colony.GameResources;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public sealed class BuildingModelFactory
    {
        private readonly Dictionary<string, Func<string, Vector2, BuildingDescription, BuildingModel>> _constructors
            = new();

        private readonly ICitizenNeedService _needService;
        private readonly IResourceFactory _resourceFactory;

        public BuildingModelFactory(ICitizenNeedService needService, IResourceFactory resourceFactory)
        {
            _needService = needService;
            _resourceFactory = resourceFactory;
        }

        public void RegisterAll()
        {
            Register("production",
                (id, position, description) => new ProductionBuildingModel(id, position,
                    (ProductionBuildingDescription)description));

            Register("service",
                (id, position, description) =>
                    new ServiceBuildingModel(id, position, (ServiceBuildingDescription)description, _needService));

            Register("storage", (id, position, description) =>
            {
                var storageDescription = (StorageBuildingDescription)description;
                
                return new StorageBuildingModel(id, position, storageDescription, _resourceFactory);
            });

            Register("decor",
                (id, position, description) =>
                    new DecorBuildingModel(id, position, (DecorBuildingDescription)description));
        }

        private void Register<T>(string type,
            Func<string, Vector2, BuildingDescription, T> ctor)
            where T : BuildingModel
        {
            _constructors[type] = ctor;
        }

        public BuildingModel Create(string type, string id, Vector2 pos, BuildingDescription desc)
        {
            return _constructors[type](id, pos, desc);
        }
    }
}