using Runtime.Colony.Inventory;
using Runtime.ViewDescriptions.Items;
using UnityEngine;

namespace Runtime.ViewDescriptions.Inventory
{
    [CreateAssetMenu(fileName = "InventoryViewDescription", menuName = "ViewDescription/Inventory/Inventory")]
    public class InventoryViewDescription : ScriptableObject
    {
        public InventoryView Prefab;
        
        public ItemViewDescriptionCollection ItemViewDescriptions;
    }
}