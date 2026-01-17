using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "ItemCollection", menuName = "City Builder/View Descriptions/Inventory/Items Collection")]
    public class ItemViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<ItemViewDescription> _descriptions;

        public ItemViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Id == id);
        }
    }
}