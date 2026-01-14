using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Load
{
    [CreateAssetMenu(fileName = "LoadViewDescription", menuName = "ViewDescription/Load/LoadViewDescription")]
    public class LoadViewDescription : ScriptableObject
    {
        public VisualTreeAsset LoadAsset;
        public VisualTreeAsset SaveAsset;
    }
}