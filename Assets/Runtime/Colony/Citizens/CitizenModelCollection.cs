using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Colony.Citizens
{
    public class CitizenModelCollection : UniformModelCollection<CitizenModel>
    {
        private readonly CitizensDescription _description;

        public CitizenModelCollection(CitizensDescription description)
        {
            _description = description;
        }

        protected override CitizenModel CreateModel()
        {
            var index = Random.Range(0, _description.Names.Count);
            var name = _description.Names[index];
            
            var model = new CitizenModel(Index, _description, name);
            
            return model;
        }

        protected override CitizenModel CreateModelFromData(Dictionary<string, object> data)
        {
            var name = (string)data["name"];
            var position = (Vector2)data["position"];
    
            return new CitizenModel(Index++, _description, name)
            {
                Position = position
            };
        }
    }
}