using System.Collections.Generic;
using Runtime.Colony.Citizens.StateMachine;
using Runtime.Descriptions.Citizens;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ISerializeModel
    {
        public int Id { get; }
        public string Name { get; }
        public CitizensDescription Description { get; }
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
    }
}