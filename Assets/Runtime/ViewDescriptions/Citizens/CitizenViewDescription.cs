using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.ViewDescriptions.Citizens
{
    [CreateAssetMenu(fileName = "Citizen", menuName = "City Builder/View Descriptions/Citizens/Citizen")]
    public class CitizenViewDescription : ScriptableObject
    {
        public string Id => name;
        
        public AssetReferenceT<GameObject>  Prefab;
    }
}