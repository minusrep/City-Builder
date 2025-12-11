using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions
{
    [CreateAssetMenu(fileName = "ResourceViewDescriptionCollection", menuName = "Resources/ViewDescriptionCollection")]
    public class ResourceViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<ResourceViewDescription> _descriptions;

        public ResourceViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Name == id);
        }
    }
}