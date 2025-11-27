using System;

namespace Runtime.Colony.Citizens
{
    [Serializable]
    public class CitizenStateDescription
    {
        public TransitionDescription[] TransitionDescriptions;

        public CitizenStateDescription(TransitionDescription[] transitionDescriptions)
        {
            TransitionDescriptions = transitionDescriptions;
        }
    }
}