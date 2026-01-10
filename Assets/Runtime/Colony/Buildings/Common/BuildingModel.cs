using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.ModelCollections;
using Runtime.Extensions;
using UnityEngine;
using System;

namespace Runtime.Colony.Buildings.Common
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        private const string LevelKey = "level";
        private const string PositionKey = "position";
        private const string DescriptionKey = "description";
        
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
        
        public bool CanUpgrade => Level < BaseDescription.MaxLevel;
        
        public int Level { get; private set; }

        public BuildingDescription BaseDescription { get; }
        
        private Vector2 _position;
        
        protected BuildingModel(string id, Vector2 position, BuildingDescription baseDescription)
        {
            Id = id;    
            Position = position;
            BaseDescription = baseDescription;
        }

        public void Upgrade()
        {
            if (!CanUpgrade)
            {
                return;
            }

            Level++;
        }
        
        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { DescriptionKey, BaseDescription.Id },
                { PositionKey, Position.ToList()},
                { LevelKey, Level}
            };
        }

        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Level = data.GetInt(LevelKey);
        }
    }
}