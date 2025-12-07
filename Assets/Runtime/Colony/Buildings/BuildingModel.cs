using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.ModelCollections;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        protected int Id { get; }
        private Vector2 Position { get; set; }
        public BuildingDescription BaseDescription { get; }

        protected BuildingModel(int id, Vector2 position, BuildingDescription baseDescription)
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