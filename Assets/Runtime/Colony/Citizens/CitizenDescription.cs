using System;
using System.Collections.Generic;

namespace Runtime.Colony.Citizens
{
    public class CitizenDescription
    {
        public List<string> Names { get; private set; }
        public Dictionary<string, float> ThresholdNeeds { get; private set; }
        public float StartMoveSpeed { get; }

        public CitizenDescription(Dictionary<string, object> description)
        {
            var list = new List<string>();
            var needs = new Dictionary<string, float>();

            foreach (var pair in (List<object>)description["names"])
            {
                list.Add(pair.ToString());
            }

            foreach (var pair in (Dictionary<string, object>)description["thresholdNeeds"])
            {
                needs.Add(pair.Key, (float)pair.Value);
            }
            
            Names = list;
            
            StartMoveSpeed = Convert.ToSingle(description["startMoveSpeed"]);
            ThresholdNeeds = needs;
        }
    }
}