using System.Collections.Generic;
using Runtime.Descriptions.StateMachine;
using Runtime.Descriptions.Stats;
using Runtime.Extensions;

namespace Runtime.Descriptions.Citizens
{
    public class CitizensDescription
    {
        public List<string> Names { get; }

        public float StartMoveSpeed { get; }
        
        public StateDescriptionCollection States { get; }
        
        public StatDescriptionCollection Stats { get; }
        
        public CitizenStatSystemDescriptionCollection Systems { get; }

        public CitizensDescription(Dictionary<string, object> data)
        {
            Names = data.GetList<string>("names");
            StartMoveSpeed = data.GetFloat("start_move_speed");
            States = new StateDescriptionCollection(data.GetNode("states"));
            Stats = new StatDescriptionCollection(data.GetNode("stats"));
            Systems = new CitizenStatSystemDescriptionCollection(data.GetNode("systems"));
        }
    }
}