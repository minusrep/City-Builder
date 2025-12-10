using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescriptionCollection", menuName = "Buildings/ViewDescriptionCollection")]
    public sealed class BuildingViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<BuildingViewDescription> descriptions;
        
        public BuildingViewDescription Get(string id)
        {
            return descriptions.Find(description => description.name == id);
        }
    }
}