using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Stats
{
    [CreateAssetMenu(fileName = "StatViewDescription", menuName = "ViewDescription/Stats/Stat")]
    public class StatViewDescription : ScriptableObject
    {
        public VisualTreeAsset StatViewAsset;
        public StyleSheet StyleSheet;
        public Sprite Icon;
    }
}