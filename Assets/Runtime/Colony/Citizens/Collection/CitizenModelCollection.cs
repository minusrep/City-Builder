using System.Collections.Generic;
using Runtime.Descriptions.Citizens;
using Runtime.ModelCollections;
using Random = UnityEngine.Random;

namespace Runtime.Colony.Citizens.Collection
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
            
            var model = new CitizenModel(Index, _description);
            
            return model;
        }
        
        protected override CitizenModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var citizenModel =  new CitizenModel(Index, _description);
            
            citizenModel.Deserialize(data);

            return citizenModel;
            
        }
    }
}