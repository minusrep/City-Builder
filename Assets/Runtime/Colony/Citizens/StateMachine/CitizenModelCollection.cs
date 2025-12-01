using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenModelCollection : ModelCollection<CitizenModel>
    {
        private readonly CitizenDescription _description;

        private int _id;

        public CitizenModelCollection(CitizenDescription description)
        {
            _description = description;
        }

        protected override CitizenModel CreateModel()
        {
            var index = Random.Range(0, _description.Names.Count);
            var name = _description.Names[index];
            
            var model = new CitizenModel(_id, _description, name);
            
            return model;
        }

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object>();

            foreach (var model in Models)
            {
                var modelData = model.Value.Serialize();
                data.Add(model.Key.ToString(), modelData);
            }
            
            return data;
        }

        public void Create(Vector2 position)
        {
            var index = Random.Range(0, _description.Names.Count);
            var name = _description.Names[index];
            
            var model = new CitizenModel(_id, _description, name);
            
            Models.Add(_id, model);
            
            _id++;
        }

        public void Remove(int modelId)
        {
            Models.Remove(modelId);
        }
    }
}