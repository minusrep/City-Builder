using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using Runtime.Colony.Citizens;
using UnityEngine;
using System;

namespace Runtime.Colony.Buildings
{
    public sealed class BuildingFactory
    {
        private readonly Dictionary<string, Func<int, Vector2, BuildingDescription, BuildingModel>> _constructors
            = new();

        private readonly ICitizenNeedService _needService;
        private readonly IResourceFactory _resourceFactory;

        public BuildingFactory(ICitizenNeedService needService, IResourceFactory resourceFactory)
        {
            _needService = needService;
            _resourceFactory = resourceFactory;
        }

        public void RegisterAll()
        {
            Register("production",
                (id, position, description) => new ProductionBuildingModel(id, position,
                    (ProductionBuildingDescription)description, 0));

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
            Func<int, Vector2, BuildingDescription, T> ctor)
            where T : BuildingModel
        {
            _constructors[type] = ctor;
        }

        public BuildingModel Create(string type, int id, Vector2 pos, BuildingDescription desc)
        {
            return _constructors[type](id, pos, desc);
        }
    }
}