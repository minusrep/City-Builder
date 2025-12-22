using System.Collections;
using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;
using UnityEngine;

namespace Runtime.Colony.Citizens.Collection
{
    public class PointOfInterestCollection : ISerializeModel, IDeserializeModel, IEnumerable<KeyValuePair<string, Vector3>>
    {
        private readonly Dictionary<string, Vector3> _points = new Dictionary<string, Vector3>();
        
        public Vector3 this[string key] { get => _points[key]; set => _points[key] = value; }
        
        public int Count => _points.Count;

        public Dictionary<string, object> Serialize()
        {
            return _points.ToJson();
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            _points.Clear();
            foreach (var pair in data)
            {
                var vectorData = data.GetNode(pair.Key);
                
                var x = vectorData.GetFloat("x");
                var y = vectorData.GetFloat("y");
                var z = vectorData.GetFloat("z");
                
                _points.Add(pair.Key, new Vector3(x, y, z));
            }
        }

        public IEnumerator<KeyValuePair<string, Vector3>> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}