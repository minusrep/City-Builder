using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Citizens
{
    public class CitizenStatSystemDescription
    {
        private const string ChangeSpeedKey = "change_speed";
        private const string StatKey = "stat";

        public string Stat { get; }
        public float ChangeSpeed { get; }

        public CitizenStatSystemDescription(Dictionary<string, object> data)
        {
            ChangeSpeed = data.GetFloat(ChangeSpeedKey);
            
            Stat = data.GetString(StatKey);
        }
    }
}