using System.Collections.Generic;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.GameResources
{
    public class ResourceModel : ISerializeModel, IDeserializeModel
    {
        public ResourceDescription Description { get; }
        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public ResourceModel(ResourceDescription description)
        {
            Description = description;
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "amount", Amount },
                { "description", Description.Type }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Amount = data.GetInt("amount");
        }
    }
}