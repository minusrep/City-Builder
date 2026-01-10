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

        public Vector2Int GridPosition { get; set; }
        
        public Vector2 WorldPosition
        {
            get => _worldPosition;
            set
            {
                _worldPosition = value;
                OnPositionChanged?.Invoke();
            }
        }
        
        
        public bool CanUpgrade => Level < BaseDescription.MaxLevel;
        
        public int Level { get; private set; }

        public BuildingDescription BaseDescription { get; }
        
        private Vector2 _worldPosition;
        
        protected BuildingModel(string id, Vector2Int gridPosition, BuildingDescription baseDescription)
        {
            Id = id;    
            GridPosition = gridPosition;
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
                { PositionKey, WorldPosition.ToList()},
                { LevelKey, Level}
            };
        }

        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Level = data.GetInt(LevelKey);
        }
    }
}