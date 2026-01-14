using System.Collections.Generic;
using Runtime.Colony.Achievements.Events.Types;
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
            return _modelFactory.Create(description.Type, GetCurrentKey(), Vector2Int.zero, description);
        }

        public override void Add(string key, BuildingModel model)
        {
            base.Add(key, model);
            
            MessageBroker.Instance.Publish(new BuildingChangeEvent(model.BaseDescription, 1));
        }

        protected override BuildingModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var position = data.GetVector2Int("position");

            var descriptionId = data.GetString("description");
            DescriptionKey = descriptionId;
            
            var description = _descriptions.Descriptions[descriptionId];

            var building = _modelFactory.Create(description.Type, GetCurrentKey(), position, description);
            
            MessageBroker.Instance.Publish(new BuildingChangeEvent(description, 1));
            
            building.Deserialize(data);
            
            return building;
        }
    }
}