using System;
using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Environment
{
    public class EnvironmentTimeModel : ISerializeModel, IDeserializeModel
    {
        private const string CurrentTimeKey = "current_time";

        public event Action<float> OnTick;

        public EnvironmentTimeDescription Description { get; }
        
        private float CurrentTime { get; set; }

        public EnvironmentTimeModel(EnvironmentTimeDescription description)
        {
            Description = description;
        }

        public void Tick(float deltaTime)
        {
            CurrentTime %=  Description.CycleLength;
            
            CurrentTime += deltaTime * Description.TimeScale;
            
            OnTick?.Invoke(CurrentTime);
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                {CurrentTimeKey, CurrentTime}
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            CurrentTime = data.GetFloat(CurrentTimeKey);
        }
    }
}