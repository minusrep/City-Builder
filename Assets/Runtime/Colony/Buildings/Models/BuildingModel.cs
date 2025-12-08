using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using Runtime.ModelCollections;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Colony.Buildings.Models
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        public string Id { get; }
        public Vector2 Position { get; set; }
        public BuildingDescription BaseDescription { get; }

        protected BuildingModel(string id, Vector2 position, BuildingDescription baseDescription)
        {
            Id = id;    
            Position = position;
            BaseDescription = baseDescription;
        }

        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "description", BaseDescription.Id },
                { "position", Position.ToList() }
            };
        }

        public abstract void Deserialize(Dictionary<string, object> data);
    }
}