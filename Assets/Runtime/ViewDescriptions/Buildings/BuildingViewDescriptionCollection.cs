using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescriptionCollection", menuName = "ViewDescription/Buildings/Collection")]
    public class BuildingViewDescriptionCollection : ScriptableObject
    {
        public IReadOnlyList<BuildingViewDescriptionBase> Descriptions => _descriptions;
        
        [SerializeField] private List<BuildingViewDescriptionBase> _descriptions;
        
        public BuildingViewDescriptionBase Get(string id)   
        {
            return _descriptions.Find(description => description.name == id);
        }
    }
}