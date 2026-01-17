using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Environment
{
    public class EnvironmentModel : ISerializeModel, IDeserializeModel
    {
        private const string TimeKey = "time";

        private EnvironmentDescription Description { get; }

        public EnvironmentTimeModel Time { get; }

        public EnvironmentModel(EnvironmentDescription description)
        {
            Description = description;
            
            Time = new EnvironmentTimeModel(Description.Time);
        }

        public void Tick(float deltaTime)
        {
            Time.Tick(deltaTime);
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                {TimeKey, Time.Serialize()}  
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Time.Deserialize(data.GetNode(TimeKey));
        }
    }
}