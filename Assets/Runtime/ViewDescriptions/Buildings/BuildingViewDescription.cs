using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.ViewDescriptions.Buildings
{
    public class BuildingViewDescription : ScriptableObject
    {
        public string Id => name;
        
        public AssetReferenceT<GameObject> Prefab;
        
        public Vector2Int VisualSizeInCells = Vector2Int.one;
    }
}