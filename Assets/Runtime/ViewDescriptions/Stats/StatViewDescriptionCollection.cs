using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "City Builder/View Descriptions/Stats/Stat Collection")]
    public class StatViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<StatViewDescription> _descriptions;

        public IReadOnlyList<StatViewDescription> Descriptions => _descriptions;

        public StatViewDescription this[string key] => _descriptions.Find(d => d.name == key);
    }
}