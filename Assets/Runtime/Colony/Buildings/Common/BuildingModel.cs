using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.ModelCollections;
using Runtime.Extensions;
using UnityEngine;
using System;
using Runtime.Colony.Citizens;

namespace Runtime.Colony.Buildings.Common
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        public event Action OnPositionChanged;
        
        public string Id { get; }

        public Vector2 Position
        {
            get => _position;
            private set
            {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }

        public BuildingDescription BaseDescription { get; }
        
        private Vector2 _position;

        protected BuildingModel(string id, Vector2 position, BuildingDescription baseDescription)
        {
            Id = id;    
            Position = position;
            BaseDescription = baseDescription;
        }

        public virtual void Enter(CitizenModel citizen)
        {
            
        }

        public virtual void Work(CitizenModel citizen)
        {
            
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