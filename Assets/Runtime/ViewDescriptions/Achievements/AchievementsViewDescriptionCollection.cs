using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.Achievements
{
    [CreateAssetMenu(fileName = "AchievementViewDescriptionCollection",
        menuName = "ViewDescription/Achievements/AchievementViewDescriptionCollection")]
    public class AchievementViewDescriptionCollection : ScriptableObject
    {
        public VisualTreeAsset AchievementAsset { get; }
        public int Duration { get; }

        [SerializeField] private List<AchievementViewDescription> _descriptions;

        public AchievementViewDescription Get(string id)
        {
            return _descriptions.Find(descriptions => descriptions.Id == id);
        }
    }
}