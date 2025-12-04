using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using Runtime.Descriptions.Buildings;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public abstract class BuildingModel : ISerializeModel
    {
        protected int Id { get; }
        private Vector2 Position { get; set; }
        public BuildingDescription Description { get; }

        protected BuildingModel(int id, Vector2 position, BuildingDescription description)
        {
            Id = id;    
            Position = position;
            Description = description;
        }

        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "description", Description.Id },
                { "position", Position.ToList() }
            };
        }
    }
}