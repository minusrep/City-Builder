using Runtime.Colony.ModelCollections;
using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public sealed class BuildingModelCollection : DescribedModelCollection<BuildingModel>
    {
        private readonly BuildingsDescriptionCollection _descriptions;
        private readonly BuildingFactory _factory;

        public BuildingModelCollection(BuildingsDescriptionCollection descriptions, BuildingFactory factory)
        {
            _descriptions = descriptions;
            _factory = factory;
        }

        protected override BuildingModel CreateModel(string descriptionKey)
        {
            var description = _descriptions.Descriptions[descriptionKey];
            return _factory.Create(description.Type, Index, Vector2.zero, description);
        }

        protected override BuildingModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var position = data.GetVector2("position");

            var descriptionId = data.GetString("description");
            
            var description = _descriptions.Descriptions[descriptionId];

            DescriptionKey = id;

            var building = _factory.Create(description.Type, GetCurrentId(id), position, description);
            
            building.Deserialize(data);
            
            return building;
        }
    }
}