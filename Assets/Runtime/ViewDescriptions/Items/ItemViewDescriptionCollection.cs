using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ViewDescriptions.Items
{
    [CreateAssetMenu(fileName = "ItemViewDescriptionCollection", menuName = "ViewDescription/Inventory/ItemsCollection")]
    public class ItemViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<ItemViewDescription> _descriptions;

        public ItemViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Id == id);
        }
    }
}