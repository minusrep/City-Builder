using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Runtime.Colony.Buildings
{
    public sealed class BuildingModelCollection : ModelCollection<BuildingModel>
    {
        private readonly BuildingsDescriptionCollection _descriptions;
        private readonly BuildingFactory _factory;

        public BuildingModelCollection(BuildingsDescriptionCollection descriptions, BuildingFactory factory)
        {
            _descriptions = descriptions;
            _factory = factory;
        }

        protected override BuildingModel CreateModel()
        { 
            return _factory.Create("service", Index, Vector2.zero, _descriptions.Descriptions["shelter"]);
        }

        protected override BuildingModel CreateModelFromData(Dictionary<string, object> data)
        {
            var id = Convert.ToInt32(data["id"]);
            var position = (Vector2)data["position"];
            
            var description = _descriptions.Descriptions["description"];

            return _factory.Create(description.Type, id, position, description);
        }
    }
}