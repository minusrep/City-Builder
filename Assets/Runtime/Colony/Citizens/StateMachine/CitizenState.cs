using System;
using Runtime.Descriptions.Citizens;

namespace Runtime.Colony.Citizens.StateMachine
{
    [Serializable]
    public class CitizenState
    {
        public CitizenStateDescription Description;

        public CitizenState(CitizenStateDescription description)
        {
            Description = description;
        }
    }
}