using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Runtime.ViewDescriptions.Citizens
{
    [CreateAssetMenu(fileName = "CitizenViewDescription", menuName = "ViewDescription/CitizenViewDescription")]
    public class CitizenViewDescription : ScriptableObject
    {
        public string Id => name;
        
        public AssetReferenceT<GameObject>  Prefab;
    }
}