using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Stats
{
    [CreateAssetMenu(fileName = "StatViewDescription", menuName = "ViewDescription/Stats/Collection")]
    public class StatViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] List<StatViewDescription> _descriptions;

        public IReadOnlyList<StatViewDescription> Descriptions => _descriptions;

        public StatViewDescription this[string key] => _descriptions.Find(d => d.name == key);
    }
}