using Runtime.Colony.ModelCollections;
using Runtime.Descriptions.Citizens;
using System.Collections.Generic;
using Runtime.Utilities;
using Random = UnityEngine.Random;

namespace Runtime.Colony.Citizens
{
    public class CitizenModelCollection : UniformModelCollection<CitizenModel>
    {
        private readonly CitizensDescription _description;

        public CitizenModelCollection(CitizensDescription description) : base("citizen")
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
        
        protected override CitizenModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var name = data.GetString("name");
            var position = data.GetVector2("position");
    
            return new CitizenModel(GetCurrentId(id), _description, name)
            {
                Position = position
            };
        }
    }
}