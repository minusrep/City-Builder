using System.Collections.Generic;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.StateMachine.Descriptions
{
    public class PointOfInterestDescriptionCollection
    {
        private readonly Dictionary<string, Vector2> _pointOfInterestDescriptions;

        public PointOfInterestDescriptionCollection(Dictionary<string, object> data)
        {
            _pointOfInterestDescriptions = new Dictionary<string, Vector2>();

            foreach (var key in data.Keys)
            {
                _pointOfInterestDescriptions[key] = data.GetVector3(key);
            }
        }
        
        public Vector2 Get(string name)
        {
            return _pointOfInterestDescriptions[name];
        }
    }
}