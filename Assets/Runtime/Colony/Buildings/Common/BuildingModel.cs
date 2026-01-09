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
        
        public BuildingDescription BaseDescription { get; }
        
        private Vector2 _worldPosition;
        
        protected BuildingModel(string id, Vector2Int gridPosition, BuildingDescription baseDescription)
        {
            Id = id;    
            GridPosition = gridPosition;
            BaseDescription = baseDescription;
        }
        
        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "description", BaseDescription.Id },
                { "position", GridPosition.ToList() }
            };
        }

        public abstract void Deserialize(Dictionary<string, object> data);
    }
}