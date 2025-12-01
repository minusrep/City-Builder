using System.Collections.Generic;
using Runtime.Descriptions.Citizens;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ISerializeModel, IDeserializeModel
    {
        public int Id { get; set; }
        public CitizensDescription Description { get; }
        public string Name { get; private set; }
        public Vector2 Position { get; set; }

        public CitizenModel(int id, CitizensDescription description, string name)
        {
            Id = id;
            Description = description;
            Name = name;
        }

        public Dictionary<string, object> Serialize() =>
            new()
            {
                { "position", new[] { Position.x, Position.y } },
                { "name", Name }
            };

        public void Deserialize(Dictionary<string, object> data)
        {
            Name = (string)data["name"];
            Position = (Vector2)data["position"];
        }
    }
}