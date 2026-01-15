using System.Collections.Generic;
using Runtime.Descriptions.Achievements;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Achievements.Collection
{
    public class AchievementModelCollection : IDeserializeModel, ISerializeModel
    {
        public Dictionary<string, AchievementModel> Models { get; } = new();

        private readonly AchievementDescriptionCollection _descriptions;

        public AchievementModelCollection(AchievementDescriptionCollection descriptions)
        {
            _descriptions = descriptions;

            Create();
        }

        private void Create()
        {
            foreach (var (id, description) in _descriptions.Descriptions)
            {
                var model = new AchievementModel(description);
                Models.Add(id, model);
            }
        }

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object>();
            var models = new Dictionary<string, object>();

            foreach (var (id, model) in Models)
            {
                models.Add(id, model.Serialize());
            }

            data.Set("models", models);
            return data;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var models = data.GetNode("models");

            foreach (var (id, value) in models)
            {
                var achievementData = (Dictionary<string, object>)value;

                if (Models.TryGetValue(id, out var model))
                {
                    model.Deserialize(achievementData);
                }
            }
        }

        public void Clear()
        {
            Models.Clear();
            Create();
        }
    }
}