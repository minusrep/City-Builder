using System.Collections.Generic;
using Runtime.Descriptions.Achievements;
using Runtime.ModelCollections;

namespace Runtime.Colony.Achievements
{
    public class AchievementModelCollection : UniformModelCollection<AchievementModel>
    {
        private readonly AchievementDescriptionCollection _descriptions;
        
        //TODO: Отрефакторить
        public AchievementModelCollection(string descriptionKey, AchievementDescriptionCollection descriptions) : base(descriptionKey)
        {
            _descriptions = descriptions;
            
            foreach (var pair in _descriptions.Descriptions)
            {
                var achievementId = pair.Key;
                var description = pair.Value;
            
                var model = new AchievementModel(description);
                Add(achievementId, model);
            }
        }

        protected override AchievementModel CreateModel()
        {
            return null;
        }
        
        protected override AchievementModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var description = _descriptions.Descriptions[id];
            
            var model = new AchievementModel(description);
            
            model.Deserialize(data);
        
            return model;
        }
    }
}