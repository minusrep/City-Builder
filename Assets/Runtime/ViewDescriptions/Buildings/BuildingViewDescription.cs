using Runtime.Colony.Buildings.Common;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    public class BuildingViewDescription : ScriptableObject
    {
        public string Id => name;
        
        public BuildingView Prefab;
    }
}