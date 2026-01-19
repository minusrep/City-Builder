using System;
using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using Runtime.Extensions;
using Runtime.ModelCollections;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public abstract class BuildingModel : ISerializeModel, IDeserializeModel
    {
        private const string LevelKey = "level";
        private const string PositionKey = "position";
        private const string DescriptionKey = "description";

        public event Action OnPositionChanged;
        public event Action<bool> OnConstructionModeChanged;

        public string Id { get; }

        public Vector2Int GridPosition { get; set; }

        public Vector3 WorldPosition
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

        private Vector3 _worldPosition;

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

        public void SetConstructionMode(bool enabled)
        {
            OnConstructionModeChanged?.Invoke(enabled);
        }

        public virtual Vector3 GetInteractionPoint()
        {
            return WorldPosition + BaseDescription.InteractionPoints[0];
        }

        public virtual Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { DescriptionKey, BaseDescription.Id },
                { PositionKey, GridPosition.ToList() },
                { LevelKey, Level }
            };
        }

        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Level = data.GetInt(LevelKey);
        }
    }
}