using System;
using System.Collections.Generic;
using Runtime.Descriptions.Stats;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Stats
{
    public class StatModel : ISerializeModel, IDeserializeModel
    {
        public event Action<float> ValueChanged;
        
        public StatDescription Stat { get; }
        
        public float Value { get; private set; }

        public StatModel(StatDescription stat)
        {
            Stat = stat;
            Value = Stat.Max;
        }
        
        public void ChangeValue(float delta)
        {
            Value +=  delta;
            Value = Math.Clamp(Value, Stat.Min, Stat.Max);
            ValueChanged?.Invoke(Value);
        }
        
        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { "stat", Stat.Id },
                { "value", Value }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Value = data.GetFloat("value");
        }
    }
}