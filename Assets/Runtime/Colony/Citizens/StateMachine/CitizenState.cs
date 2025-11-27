using System;

namespace Runtime.Colony.Citizens
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