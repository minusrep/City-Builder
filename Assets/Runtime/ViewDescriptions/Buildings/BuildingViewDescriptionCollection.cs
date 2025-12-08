using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescriptionCollection", menuName = "Buildings/ViewDescriptionCollection")]
    public sealed class BuildingViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<BuildingViewDescription> _descriptions;

        private Dictionary<string, BuildingViewDescription> _dictionaryDescriptions;

        public void Initialize()
        {
            _dictionaryDescriptions = new Dictionary<string, BuildingViewDescription>();
            foreach (var description in _descriptions)
            {
                _dictionaryDescriptions.Add(description.BuildingDescriptionId, description);
            }
        }

        public BuildingViewDescription GetById(string id)
        {
            return _dictionaryDescriptions[id];
        }
    }
}