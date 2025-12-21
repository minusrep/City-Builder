using System;
using System.Collections.Generic;
using Runtime.Colony.StateMachine;
using Runtime.Colony.Stats.Collections;
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
        
        private const string PointsOfInterestKey = "points_of_interest";
        
        private const string FlagsKey = "flags";
        
        private const string StatsKey = "stats";
        
        private const string TimerKey = "timers";
        
        private const string StateMachineKey = "state_machine";
        
        private const string BuildingIdKey = "building_id";

        private const string HasBuildingFlagKey = "has_building";

        public event Action<string> OnStartMove;

        public event Action<string> OnInvokeAnimation;
        
        public int Id { get; set; }

        public Vector3 Position { get; set; }

        public CitizensDescription Description { get; }

        public Dictionary<string, long> Timers { get; private set; }

        public Dictionary<string, Vector3> PointsOfInterest { get; set; }
        
        public Dictionary<string, bool> Flags { get; private set; }

        public StatModelCollection Stats { get; private set; }

        public StateMachineModel StateMachine { get; }

        private string Name { get; set; }
        
        public CitizenModel(int id, CitizensDescription description)
        {
            Id = id;
            Description = description;
            Position = new Vector2(0, 0);
            PointsOfInterest = new Dictionary<string, Vector3>();
            Flags = new Dictionary<string, bool>();
            Stats = new StatModelCollection(description.Stats);
            Timers = new Dictionary<string, long>();

            StateMachine = new StateMachineModel(description.States);
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { IdKey, Id},
                { NameKey, Name },
                { PositionKey, Position.ToList() },
                { PointsOfInterestKey, PointsOfInterest },
                { FlagsKey, Flags },
                { StatsKey, Stats.Serialize() },
                { TimerKey, Timers },
                { StateMachineKey, StateMachine.Serialize()},
            };
        }


        public void Deserialize(Dictionary<string, object> data)
        {
            Name = data.GetString(NameKey);
            Position = data.GetVector3(PositionKey);
            PointsOfInterest = data.GetDictionary<string, Vector3>(PointsOfInterestKey);
            Flags = data.GetDictionary<string, bool>(FlagsKey);
            Stats.Deserialize(data.GetNode(StatsKey));
            Timers = data.GetDictionary<string, long>(TimerKey);
            StateMachine.Deserialize(data.GetNode(StateMachineKey));
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