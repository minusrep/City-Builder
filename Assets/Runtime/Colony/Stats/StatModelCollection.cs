using System.Collections.Generic;
using Runtime.Descriptions.Stats;
using Runtime.Extensions;

namespace Runtime.Colony.Stats
{
    public class StatModelCollection
    {
        private readonly Dictionary<string, StatModel> _stats;
        
        public StatModelCollection(StatDescriptionCollection statDescriptions)
        {
            _stats = new Dictionary<string, StatModel>();

            foreach (var statDescription in statDescriptions)
            {
                _stats.Add(statDescription.Id, new StatModel(statDescription));
            }
        }

        public StatModel Get(string id)
        {
           return  _stats[id];
        }

        public Dictionary<string, object> Serialize()
        {
            var statsData = new Dictionary<string, object>();

            foreach (var stat in _stats)
            {
                statsData.Add(stat.Key, stat.Value.Serialize());
            }
            
            return statsData;
        }

        public void Deserialize(Dictionary<string, object> statsData)
        {
            foreach (var data in statsData)
            {
                _stats[data.Key].Deserialize(statsData.GetNode(data.Key));
            }
        }
    }
}