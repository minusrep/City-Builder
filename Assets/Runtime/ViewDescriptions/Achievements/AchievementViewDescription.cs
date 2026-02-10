using UnityEngine;

namespace Runtime.ViewDescriptions.Achievements
{
    [CreateAssetMenu(fileName = "Achievement", menuName = "City Builder/View Descriptions/Achievements/Achievement")]
    public class AchievementViewDescription : ScriptableObject
    {
        public string Id => name;
        public Sprite Icon;
        public string Title;
        public string Description;
    }
}