using Runtime.Colony.Buildings.Common;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    public abstract class BuildingViewDescriptionBase : ScriptableObject
    {
        public abstract BuildingView GetViewDescription();
        public abstract string Id { get; }
    }
}