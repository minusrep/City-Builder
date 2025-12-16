using System;
using System.Collections.Generic;
using Runtime.Colony.StateMachine;
using Runtime.Descriptions.Citizens;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenModel : ICitizenModel
    {
        private const string NameKey = "name";
        
        private const string PositionKey = "position";
        
        private const string PointOfInterestKey = "point_of_interest";
        
        private const string FlagsKey = "flags";
        
        private const string StatsKey = "stats";
        
        private const string TimerKey = "timers";
        
        private const string StateMachineKey = "state_machine";
        
        private const string BuildingIdKey = "building_id";

        private const string HasBuildingFlagKey = "has_building";

        public event Action OnChangePointOfInterest;
        
        public int Id { get; set; }

        public Vector3 Position { get; set; }
        
        public CitizensDescription Description { get; }

        public Vector3 PointOfInterest { get; set; }
        
        public Dictionary<string, long> Timers { get; private set; }

        public Dictionary<string, bool> Flags { get; private set; }

        public Dictionary<string, float> Stats { get; private set; }

        public StateMachineModel StateMachine { get; }

        public string BuildingId { get; private set; }

        private string Name { get; set; }
        
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

            StateMachine = new StateMachineModel(description.States);
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { NameKey, Name },
                { PositionKey, Position.ToList() },
                { PointOfInterestKey, PointOfInterest.ToList() },
                { FlagsKey, Flags },
                { StatsKey, Stats },
                { TimerKey, Timers },
                { StateMachineKey, StateMachine.Serialize()},
            };
        }


        public void Deserialize(Dictionary<string, object> data)
        {
            Name = data.GetString(NameKey);
            Position = data.GetVector3(PositionKey);
            PointOfInterest = data.GetVector3(PointOfInterestKey);
            Flags = data.GetDictionary<string, bool>(FlagsKey);
            Stats = data.GetDictionary<string, float>(StatsKey);
            Timers = data.GetDictionary<string, long>(TimerKey);
            StateMachine.Deserialize(data.GetNode(StateMachineKey));
        }

        public void SetPointOfInterest(Vector3 point)
        {
            PointOfInterest = point;
            
            OnChangePointOfInterest?.Invoke();
        }

        public void SetBuildingId(string buildingId)
        {
            BuildingId = buildingId;

            Flags[HasBuildingFlagKey] = BuildingId == string.Empty;
        }
    }
}