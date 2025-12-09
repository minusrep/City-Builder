using System.Collections.Generic;
using Runtime.Descriptions.Citizens;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ICitizenModel
    {
        public int Id { get; set; }
        public CitizensDescription Description { get; }

        public Vector2 PointOfInterest { get; private set; }

        public Vector2 Position { get; set; }

        public Dictionary<string, ulong> Timers { get; }
        
        public Dictionary<string, bool> Flags { get; }
        
        public Dictionary<string, float> Stats { get; }

        private string Name { get; set; }

        public CitizenModel(int id, CitizensDescription description, string name)
        {
            Id = id;
            Description = description;
            Name = name;
            Position = new Vector2(0, 0);
            PointOfInterest = new Vector2(0, 0);
            
            Flags = new Dictionary<string, bool>();
            Stats = new Dictionary<string, float>();
            Timers = new Dictionary<string, ulong>();
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