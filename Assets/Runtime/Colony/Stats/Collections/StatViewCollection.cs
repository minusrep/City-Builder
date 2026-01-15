using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Runtime.Colony.Stats.Collections
{
    public class StatViewCollection 
    {
        public VisualElement Root { get; private set; }

        public StatViewCollection(VisualElement root)
        {
            Root = root;
        }
    }
}