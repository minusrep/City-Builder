using Runtime.ViewDescriptions.Items;
using Runtime.ViewDescriptions.Resource;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.ViewDescriptions.Inventory
{
    [CreateAssetMenu(fileName = "InventoryViewDescription", menuName = "Inventory/ViewDescription")]
    public class InventoryViewDescription : ScriptableObject
    {
        public GameObject Prefab;
        
        public ItemViewDescriptionCollection itemViewDescriptions;
    }
}