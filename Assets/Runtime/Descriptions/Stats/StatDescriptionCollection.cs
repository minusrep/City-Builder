using System.Collections;
using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Stats
{
    public class StatDescriptionCollection : IEnumerable<StatDescription>
    {
        private readonly Dictionary<string, StatDescription> _descriptions;
        
        public StatDescriptionCollection(Dictionary<string, object> data)
        {
            _descriptions = new Dictionary<string, StatDescription>();

            foreach (var description in data)
            {
                _descriptions.Add(description.Key, new StatDescription(description.Key, data.GetNode(description.Key)));
            }
        }
        
        public StatDescription this[string key] => _descriptions[key];
        
        public IEnumerator<StatDescription> GetEnumerator()
        {
            return _descriptions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}