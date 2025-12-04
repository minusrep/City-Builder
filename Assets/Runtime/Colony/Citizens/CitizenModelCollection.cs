using System;
using System.Collections.Generic;
using Runtime.Descriptions.Citizens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenModelCollection : ModelCollection<CitizenModel>
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
            var posArray = (List<object>)data["position"];
            var position = new Vector2(
                Convert.ToSingle(posArray[0]), 
                Convert.ToSingle(posArray[1])
            );
    
            return new CitizenModel(Index++, _description, name)
            {
                Position = position
            };
        }
    }
}