using System;
using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using UnityEngine;

namespace Runtime.StateMachine.Descriptions
{
    public class PointOfInterestDescriptionCollection : IDeserializeModel
    {
        private Dictionary<string, Vector2> _pointOfInterestDescriptions;

        public Vector2 Get(string name)
        {
            return _pointOfInterestDescriptions[name];
        }
        
        public void Deserialize(Dictionary<string, object> data)
        {
            _pointOfInterestDescriptions = new Dictionary<string, Vector2>();

            foreach (var pair in data)
            {
                if (pair.Value is not List<object> position) throw new Exception();

                var x = Convert.ToSingle(position[0]);
                
                var y = Convert.ToSingle(position[1]);
                
                _pointOfInterestDescriptions[pair.Key] = new Vector2(x, y);
            }
        }
    }
}