using System.Collections.Generic;
using Runtime.ViewDescriptions.Items;
using UnityEngine;

namespace Runtime.ViewDescriptions.Resource
{
    [CreateAssetMenu(fileName = "ItemViewDescriptionCollection", menuName = "Resources/ViewDescriptionCollection")]
    public class ItemViewDescriptionCollection : ScriptableObject
    {
        [SerializeField] private List<ItemViewDescription> _descriptions;

        public ItemViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Name == id);
        }
    }
}