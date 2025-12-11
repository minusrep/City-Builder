using UnityEngine;

namespace Runtime.ViewDescriptions.Inventory
{
    [CreateAssetMenu(fileName = "InventoryViewDescription", menuName = "Inventory/ViewDescription")]
    public class InventoryViewDescription : ScriptableObject
    {
        public GameObject Prefab;
        
        public ResourceViewDescriptionCollection ResourceViewDescriptions;
    }
}