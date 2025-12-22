using System.Collections.Generic;
using Runtime.Descriptions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Citizens.Collection
{
    public class CitizenModelCollection : UniformModelCollection<CitizenModel>
    {
        private readonly WorldDescription _description;

        public CitizenModelCollection(WorldDescription description) : base("citizen")
        {
            _description = description;
        }

        protected override CitizenModel CreateModel()
        {
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