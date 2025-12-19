using Runtime.ViewDescriptions.Items;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Inventory
{
    [CreateAssetMenu(fileName = "InventoryViewDescription", menuName = "ViewDescription/Inventory/Inventory")]
    public class InventoryViewDescription : ScriptableObject
    {
        public VisualTreeAsset CellViewAsset;
        public ItemViewDescriptionCollection ItemViewDescriptions;
    }
}