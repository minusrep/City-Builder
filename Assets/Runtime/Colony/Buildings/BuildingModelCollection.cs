using Runtime.Colony.ModelCollections;
using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Utilities;

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
            var desc = _descriptions.Descriptions[descriptionKey];
            return _factory.Create(desc.Type, Index, Vector2.zero, desc);
        }

        protected override BuildingModel CreateModelFromData(int id, Dictionary<string, object> data)
        {
            var position = data.GetVector2("position");

            var descriptionId = data.GetString("description");
            
            var description = _descriptions.Descriptions[descriptionId];

            return _factory.Create(description.Type, id, position, description);
        }
    }
}