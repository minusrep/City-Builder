using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "City Builder/View Descriptions/Inventory/Item")]
    public class ItemViewDescription : ScriptableObject
    {
        public string Id => name;
        public Sprite Image;
    }
}