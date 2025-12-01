using System.Collections.Generic;
using Runtime.Colony.Citizens.StateMachine;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ISerializeModel
    {
        public int Id { get; }
        public string Name { get; }
        public CitizenDescription Description { get; }
        public Vector2 Position { get; set; }
        
        public CitizenModel(int id, CitizenDescription description, string name)
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