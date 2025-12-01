using Runtime.Descriptions.Buildings;
using Runtime.Colony.GameResources;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public class StorageBuildingModel : BuildingModel
    {
        private readonly StorageBuildingDescription _description;
        private readonly Dictionary<string, ResourceModel> _resources;

        public StorageBuildingModel(int id, Vector2 position, StorageBuildingDescription description, Dictionary<string, ResourceModel> resources) : base(id, position)
        {
            _description = description;
            _resources = resources;
        }
        
        public bool TryAddResource(string resourceKey, ResourceModel resourceModel)
        {
            if (_resources.TryGetValue(resourceKey, out var stored))
            {
                if (stored.Amount + resourceModel.Amount <= _description.MaxResourceAmount)
                {
                    stored.IncreaseAmount(resourceModel.Amount);
                    return true;
                }
            }

            return false;
        }
        
        public bool TryTakeResource(string resourceKey, int amount)
        {
            if (_resources.TryGetValue(resourceKey, out var resource))
            {
                if (resource.Amount >= amount)
                {
                    resource.ReduceAmount(amount);
                    return true;
                }
            }

            return false;
        }
    }
}