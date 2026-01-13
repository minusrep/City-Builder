using System.Collections.Generic;
using System.Linq;
using Runtime.Extensions;

namespace Runtime.Colony.Buildings.Production
{
    public class ResourceRequests
    {
        private readonly Dictionary<string, int> _resourceRequests = new Dictionary<string, int>();

        public void Add(string key, int amount)
        {
            if (!_resourceRequests.TryAdd(key, amount))
            {
                _resourceRequests[key] += amount;
            }
        }

        public void Remove(string key, int amount)
        {
            _resourceRequests[key] -= amount;
            if  (_resourceRequests[key] <= 0)
            {
                _resourceRequests.Remove(key);
            }
        }

        public bool Contains(string key)
        {
            return _resourceRequests.ContainsKey(key);
        }
        
        public Dictionary<string, object> Serialize()
        {
            return _resourceRequests.ToDictionary<KeyValuePair<string, int>, string, object>(r => r.Key, r => r.Value);
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            _resourceRequests.Clear();
            foreach (var id in data.Keys)
            {
                _resourceRequests.Add(id, data.GetInt(id));
            }
        }
    }
}