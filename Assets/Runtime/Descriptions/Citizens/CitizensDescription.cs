using System.Collections.Generic;
using Runtime.Utilities;

namespace Runtime.Descriptions.Citizens
{
    public class CitizensDescription
    {
        public List<string> Names { get; }

        public float StartMoveSpeed { get; }

        public CitizensDescription(Dictionary<string, object> data)
        {
            Names = data.GetList<string>("names");
            StartMoveSpeed = data.GetFloat("start_move_speed");
        }
    }
}