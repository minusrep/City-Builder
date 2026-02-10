using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Citizens
{
    [CreateAssetMenu(fileName = "CitizenCollection", menuName = "City Builder/View Descriptions/Citizens/Citizen Collection")]
    public class CitizenViewDescriptionCollection : ScriptableObject
    {
        public IReadOnlyList<CitizenViewDescription> Descriptions => _descriptions;

        [SerializeField] private List<CitizenViewDescription> _descriptions;

        public CitizenViewDescription Get(string id)
        {
            return _descriptions.Find(a => a.Id == id);
        }
    }
}