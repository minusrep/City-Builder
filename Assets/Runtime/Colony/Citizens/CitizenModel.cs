using System;
using System.Collections.Generic;
using Runtime.Colony.Inventory;
using Runtime.Colony.Citizens.Collection;
using Runtime.Colony.StateMachine;
using Runtime.Colony.Stats.Collections;
using Runtime.Descriptions;
using Runtime.Descriptions.Citizens;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ICitizenModel
    {
        private const string IdKey = "id";
        
        private const string NameKey = "name";
        
        private const string PositionKey = "position";
        
        private const string SpawnedFromBuildingID = "spawned_from_building_id";
        
        private const string PointsOfInterestKey = "points_of_interest";
        
        private const string FlagsKey = "flags";
        
        private const string StatsKey = "stats";
        
        private const string TimerKey = "timers";
        
        private const string StateMachineKey = "state_machine";
        
        private const string InventoryKey = "inventory";

        public event Action<string> OnStartMove;

        public event Action<string> OnInvokeAnimation;
        
        public int Id { get; set; }

        public Vector3 Position { get; set; }

        public CitizensDescription Description { get; }

        public Dictionary<string, long> Timers { get; private set; }

        public PointOfInterestCollection PointsOfInterest { get; set; }
        
        public Dictionary<string, bool> Flags { get; private set; }

        public StatModelCollection Stats { get; private set; }

        public StateMachineModel StateMachine { get; }
        
        public string SpawnedFromBuildingId { get; set; }
        
        public InventoryModel Inventory { get; private set; }
        
        private WorldDescription _description;

        private string Name { get; set; }
        
        public CitizenModel(int id, WorldDescription description)
        {
            _description = description;
            
            Id = id;
            Description = description.Citizens;
            Position = new Vector2(0, 0);
            PointsOfInterest = new PointOfInterestCollection();
            Flags = new Dictionary<string, bool>();
            Stats = new StatModelCollection(description.Citizens.Stats);
            Timers = new Dictionary<string, long>();
            Inventory = new InventoryModel(1, 1, description.ResourceCollection);
            Inventory.Create();

            StateMachine = new StateMachineModel(description.Citizens.States);
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { IdKey, Id},
                { NameKey, Name },
                { SpawnedFromBuildingID, SpawnedFromBuildingId },
                { PositionKey, Position.ToList() },
                { PointsOfInterestKey, PointsOfInterest.Serialize() },
                { FlagsKey, Flags },
                { StatsKey, Stats.Serialize() },
                { TimerKey, Timers },
                { StateMachineKey, StateMachine.Serialize()},
                { InventoryKey, Inventory.Serialize() }
            };
        }


        public void Deserialize(Dictionary<string, object> data)
        {
            Name = data.GetString(NameKey);
            Position = data.GetVector3(PositionKey);
            SpawnedFromBuildingId = data.GetString(SpawnedFromBuildingID);
            PointsOfInterest.Deserialize(data.GetNode(PointsOfInterestKey));
            Flags = data.GetDictionary<string, bool>(FlagsKey);
            Stats.Deserialize(data.GetNode(StatsKey));
            Timers = data.GetDictionary<string, long>(TimerKey);
            StateMachine.Deserialize(data.GetNode(StateMachineKey));
            
            Inventory = new InventoryModel(1, 1, _description.ResourceCollection);
            Inventory.Deserialize(data.GetNode(InventoryKey));
        }

        public void SetPointOfInterest(string key, Vector3 point)
        {
            PointsOfInterest[key] = point;                
        }

        public void StartMove(string pointOfInterest)
        {
            OnStartMove?.Invoke(pointOfInterest);
        }

        public void InvokeAnimation(string animation)
        {
            OnInvokeAnimation?.Invoke(animation);
        }
    }
}