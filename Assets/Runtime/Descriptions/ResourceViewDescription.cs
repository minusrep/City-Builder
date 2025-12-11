using UnityEngine;

namespace Runtime.Descriptions
{
    [CreateAssetMenu(fileName = "ResourceViewDescription", menuName = "Resources/ViewDescription")]
    public class ResourceViewDescription : ScriptableObject
    {
        public string Name;
        public Sprite Image;
    }
}