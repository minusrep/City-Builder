using Runtime.ViewDescriptions.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "City Builder/View Descriptions/Inventory/Inventory")]
    public class InventoryViewDescription : ScriptableObject
    {
        public VisualTreeAsset CellViewAsset;
        public VisualTreeAsset InventoryAsset;
        public ItemViewDescriptionCollection ItemViewDescriptions;
    }
}