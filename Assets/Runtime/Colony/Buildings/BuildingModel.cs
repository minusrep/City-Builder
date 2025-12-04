using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        protected int Id { get; }
        private Vector2 Position { get; set; }
        private BuildingDescription Description { get; }

        protected BuildingModel(int id, Vector2 position, BuildingDescription description)
        {
            Id = id;    
            Position = position;
            Description = description;
        }

        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { "description", Description.Type },
                { "position", new[] { Position.x, Position.y } }
            };
        }

        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Position = (Vector2)data["position"];
        }
    }
}