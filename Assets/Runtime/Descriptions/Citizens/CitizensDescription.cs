using System.Collections.Generic;
using Runtime.Descriptions.StateMachine;
using Runtime.Descriptions.Stats;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.Citizens
{
    public class CitizensDescription
    {
        private const string NamesKey = "names";
        private const string ViewDescriptionKey = "view_descriptions";
        private const string StartMoveSpeedKey =  "start_move_speed";
        private const string StatesKey = "states";
        private const string StatsKey = "stats";
        private const string SystemsKey = "systems";
        
        public StateDescriptionCollection States { get; }
        public StatDescriptionCollection Stats { get; }
        public CitizenStatSystemDescriptionCollection Systems { get; }
        
        private List<string> Names { get; }
        private List<string> ViewDescriptions { get; }

        public CitizensDescription(Dictionary<string, object> data)
        {
            Names = data.GetList<string>(NamesKey);
            ViewDescriptions = data.GetList<string>(ViewDescriptionKey);
            data.GetFloat(StartMoveSpeedKey);
            States = new StateDescriptionCollection(data.GetNode(StatesKey));
            Stats = new StatDescriptionCollection(data.GetNode(StatsKey));
            Systems = new CitizenStatSystemDescriptionCollection(data.GetNode(SystemsKey));
        }

        public string GetRandomName()
        {
            return Names[Random.Range(0, Names.Count)];
        }

        public string GetRandomViewDescription()
        {
            return ViewDescriptions[Random.Range(0, ViewDescriptions.Count)];
        }
    }
}