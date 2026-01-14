using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.ViewDescriptions.Citizens
{
    [CreateAssetMenu(fileName = "CitizenViewDescription", menuName = "ViewDescription/CitizenViewDescription")]
    public class CitizenViewDescription : ScriptableObject
    {
        public AssetReferenceT<GameObject>  Prefab;
    }
}