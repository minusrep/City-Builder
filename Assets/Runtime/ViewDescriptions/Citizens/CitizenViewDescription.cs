using UnityEngine;

namespace Runtime.ViewDescriptions.Citizens
{
    [CreateAssetMenu(fileName = "CitizenViewDescription", menuName = "ViewDescription/CitizenViewDescription")]
    public class CitizenViewDescription : ScriptableObject
    {
        public GameObject Prefab;
    }
}