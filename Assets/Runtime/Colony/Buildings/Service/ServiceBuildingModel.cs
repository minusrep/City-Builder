using System;
using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Service
    {
        public class ServiceBuildingModel : BuildingModel
        {
            public event Action<int> OnCitizenAmountChanged;

            public int CurrentCitizenAmount
            {
                get => _currentCitizenAmount;
                set
                {
                    _currentCitizenAmount = value;
                    OnCitizenAmountChanged?.Invoke(_currentCitizenAmount);
                }
            }

            public ServiceBuildingDescription Description { get; }
            
            private int _currentCitizenAmount;

            public ServiceBuildingModel(string id,
                Vector2Int gridPosition,
                ServiceBuildingDescription description) : base(id,
                gridPosition,
                description)
            {
                Description = description;
            }
        }
    }