using System;
using System.Collections.Generic;
using Runtime.Descriptions.Citizens;
using Runtime.StateMachine;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ICitizenModel
    {
        public event Action OnChangePointOfInterest;
        
        public int Id { get; set; }

        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                
                OnChangePointOfInterest?.Invoke();
            }
        }
        
        public CitizensDescription Description { get; }

        public Vector3 PointOfInterest { get; set; }
        
        public Dictionary<string, long> Timers { get; }
        
        public Dictionary<string, bool> Flags { get; }
        
        public Dictionary<string, float> Stats { get; }

        public StateMachineModel StateMachine { get; }        
        
        private string Name { get; set; }
        
        private Vector3 _position;

        public CitizenModel(int id, CitizensDescription description, string name, StateMachineModel stateMachine = null)
        {
            Id = id;
            Description = description;
            Name = name;
            Position = new Vector2(0, 0);
            PointOfInterest = new Vector2(0, 0);
            
            Flags = new Dictionary<string, bool>();
            Stats = new Dictionary<string, float>();
            Timers = new Dictionary<string, long>();
            
            StateMachine = stateMachine;
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