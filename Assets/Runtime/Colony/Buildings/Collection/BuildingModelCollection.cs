using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using Runtime.ModelCollections;
using UnityEngine;

namespace Runtime.Colony.Buildings.Collection
{
    public sealed class BuildingModelCollection : DescribedModelCollection<BuildingModel>
    {
        private readonly BuildingsDescriptionCollection _descriptions;
        private readonly BuildingModelFactory _modelFactory;

        public BuildingModelCollection(BuildingsDescriptionCollection descriptions, BuildingModelFactory modelFactory)
        {
            _descriptions = descriptions;
            _modelFactory = modelFactory;
        }

        protected override BuildingModel CreateModel(string descriptionKey)
        {
            DescriptionKey = descriptionKey;
            var description = _descriptions.Descriptions[descriptionKey];
            return _modelFactory.Create(description.Type, GetCurrentKey(), Vector2.zero, description);
        }

        protected override BuildingModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var position = data.GetVector2("position");

            var descriptionId = data.GetString("description");
            DescriptionKey = descriptionId;
            
            var description = _descriptions.Descriptions[descriptionId];

            var building = _modelFactory.Create(description.Type, GetCurrentKey(), position, description);
            
            building.Deserialize(data);
            
            return building;
        }
    }
}