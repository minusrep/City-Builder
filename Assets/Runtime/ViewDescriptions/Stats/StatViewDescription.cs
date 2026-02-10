using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "City Builder/View Descriptions/Stats/Stat")]
    public class StatViewDescription : ScriptableObject
    {
        public VisualTreeAsset StatViewAsset;
        public StyleSheet StyleSheet;
        public Sprite Icon;
    }
}