using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "ItemViewDescription", menuName = "ViewDescription/Inventory/Item")]
    public class ItemViewDescription : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}