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

        public Vector2 WorldPosition
        {
            get => _worldPosition;
            private set
            {
                _worldPosition = value;
                OnPositionChanged?.Invoke();
            }
        }
        
        public Vector2Int GridPosition { get; private set; }
        public BuildingDescription BaseDescription { get; }
        
        private Vector2 _worldPosition;
        
        protected BuildingModel(string id, Vector2 worldPosition, BuildingDescription baseDescription)
        {
            Id = id;    
            WorldPosition = worldPosition;
            BaseDescription = baseDescription;
        }
        
        public void SetGridPosition(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
        }
        
        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "description", BaseDescription.Id },
                { "grid_position", GridPosition.ToList() }
            };
        }

        public abstract void Deserialize(Dictionary<string, object> data);
    }
}