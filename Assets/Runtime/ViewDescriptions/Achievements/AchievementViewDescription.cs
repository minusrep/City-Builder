using UnityEngine;

namespace Runtime.ViewDescriptions.Achievements
{
    [CreateAssetMenu(fileName = "AchievementViewDescription", menuName = "ViewDescription/Achievements/AchievementViewDescription")]
    public class AchievementViewDescription : ScriptableObject
    {
        public string Id => name;
        public Sprite Icon;
        public string Title;
        public string Description;
    }
}