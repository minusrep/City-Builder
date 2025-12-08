using Runtime.Colony.Buildings.Views;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescription", menuName = "Buildings/ViewDescription")]
    public class BuildingViewDescription : ScriptableObject
    {
        public string BuildingDescriptionId;
        public BuildingView Prefab;
    }
}