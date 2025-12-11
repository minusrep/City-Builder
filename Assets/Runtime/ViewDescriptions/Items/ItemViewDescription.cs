using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "ItemViewDescription", menuName = "Resources/ViewDescription")]
    public class ItemViewDescription : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}