using System;

namespace Runtime.Descriptions.Citizens
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