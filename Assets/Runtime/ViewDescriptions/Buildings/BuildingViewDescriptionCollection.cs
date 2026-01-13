using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescriptionCollection", menuName = "ViewDescription/Buildings/Collection")]
    public class BuildingViewDescriptionCollection : ScriptableObject
    {
        public IReadOnlyList<BuildingViewDescription> Descriptions => _descriptions;
        
        [SerializeField] private List<BuildingViewDescription> _descriptions;
        
        public BuildingViewDescription Get(string id)   
        {
            return _descriptions.Find(description => description.name == id);
        }
    }
}