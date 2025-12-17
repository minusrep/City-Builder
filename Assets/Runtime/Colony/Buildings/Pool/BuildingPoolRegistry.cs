using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common.ObjectPool;
using Runtime.Descriptions.Buildings;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Pool
{
    public class BuildingPoolRegistry
    {
        private readonly Dictionary<string, IBuildingViewPool> _pools = new();
        
        public void RegisterAll(
            BuildingViewDescriptionCollection viewDescriptions,
            BuildingsDescriptionCollection modelDescriptions,
            Transform root)
        {
            foreach (var viewDescription in viewDescriptions.Descriptions)
            {
                var viewId = viewDescription.name;

                var modelDescription = modelDescriptions.Descriptions.Values
                    .First(d => d.ViewDescriptionId == viewId);

                var prefab = viewDescription.GetViewDescription();
                switch (modelDescription)
                {
                    case ProductionBuildingDescription:
                        Register<ProductionBuildingView>(modelDescription.ViewDescriptionId, prefab, root);
                        break;

                    case ServiceBuildingDescription:
                        Register<ServiceBuildingView>(modelDescription.ViewDescriptionId, prefab, root);
                        break;

                    case StorageBuildingDescription:
                        Register<StorageBuildingView>(modelDescription.ViewDescriptionId, prefab, root);
                        break;

                    default:
                        Register<BuildingView>(modelDescription.ViewDescriptionId, prefab, root);
                        break;
                }
            }
        }

        public IBuildingViewPool Get(string viewId) => _pools[viewId];
        
        private void Register<TView>(string viewId, BuildingView prefab, Transform root) where TView : BuildingView
        {
            var buildingView = (TView)prefab;
            var pool = new ObjectPool<TView>(buildingView, 2, root);
            _pools[viewId] = new BuildingViewPool<TView>(pool);
        }
    }
}