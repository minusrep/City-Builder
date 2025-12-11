using UnityEngine;

namespace Runtime.ViewDescriptions
{
    [CreateAssetMenu(fileName = "ResourceViewDescription", menuName = "Resources/ViewDescription")]
    public class ResourceViewDescription : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}