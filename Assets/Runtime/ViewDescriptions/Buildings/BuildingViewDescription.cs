using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "City Builder/View Descriptions/Buildings/Building")]
    public class BuildingViewDescription : ScriptableObject
    {
        public string Id => name;
        
        public AssetReferenceT<GameObject> Prefab;
        
        public Vector2Int VisualSizeInCells = Vector2Int.one;

        public Sprite Icon;
        public string Title;
    }
}