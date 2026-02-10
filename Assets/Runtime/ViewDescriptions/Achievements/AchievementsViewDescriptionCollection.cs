using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Achievements
{
    [CreateAssetMenu(fileName = "AchievementCollection",
        menuName = "City Builder/View Descriptions/Achievements/Achievement Collection")]
    public class AchievementViewDescriptionCollection : ScriptableObject
    {
        public VisualTreeAsset AchievementAsset;
        public int Duration;

        [SerializeField] private List<AchievementViewDescription> _descriptions;

        public AchievementViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Id == id);
        }
    }
}