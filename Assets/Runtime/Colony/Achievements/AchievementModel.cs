using System.Collections.Generic;
using Runtime.Descriptions.Achievements;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Achievements
{
    public class AchievementModel : ISerializeModel, IDeserializeModel
    {
        public bool IsActive { get; private set; }
        public bool IsCompleted { get; private set;}
        public int Progress { get; private set;}
        public AchievementDescription Description { get; }

        public AchievementModel(AchievementDescription description)
        {
            Description = description;
        }
        
        public void SetProgress(int value)
        {
            if (!IsActive)
            {
                return;
            }

            Progress = value;

            if (Progress >= Description.TargetValue)
            {
                Complete();
            }
        }
        
        private void Complete()
        {
            IsCompleted = true;
            IsActive = false;
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { "is_active", IsActive },
                { "is_completed", IsCompleted },
                { "progress", Progress }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            IsActive = data.GetBool("is_active");
            IsCompleted = data.GetBool("is_completed");
            Progress = data.GetInt("progress");
        }
    }
}