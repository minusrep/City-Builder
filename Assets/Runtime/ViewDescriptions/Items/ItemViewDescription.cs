using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "ItemViewDescription", menuName = "ViewDescription/Inventory/Item")]
    public class ItemViewDescription : ScriptableObject
    {
        public string Id => name;
        public Sprite Image;
    }
}