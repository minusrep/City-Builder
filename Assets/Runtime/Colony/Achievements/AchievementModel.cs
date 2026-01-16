using System;
using System.Collections.Generic;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Colony.Achievements.Triggers;
using Runtime.Descriptions.Achievements;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Achievements
{
    public class AchievementModel : ISerializeModel, IDeserializeModel
    {
        public bool IsActive { get; set; }
        public bool IsCompleted { get; private set; }
        public int Progress { get; private set; }
        public AchievementDescription Description { get; }
        public List<TriggerModel> Triggers { get; } = new();
        public event Action OnCompleted;

        public AchievementModel(AchievementDescription description)
        {
            Description = description;

            foreach (var triggerDescription in Description.Triggers)
            {
                var trigger = TriggerFactory.Create(triggerDescription, this);
                Triggers.Add(trigger);
            }
        }

        public void AddProgress(int value)
        {
            Progress += value;

            if (Progress >= Description.Target)
            {
                Complete();
            }
        }

        public void SetProgress(int value)
        {
            Progress = value;   
            
            if (Progress >= Description.Target)
            {
                Complete();
            }
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

        private void Complete()
        {
            IsCompleted = true;
            IsActive = false;

            MessageBroker.Instance.Publish(new AchievementCompleteEvent(Description));
            OnCompleted?.Invoke();
        }
    }
}