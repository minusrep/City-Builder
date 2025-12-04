using System.Collections.Generic;

namespace Runtime.Colony.ModelCollections
{
    public interface IDeserializeModel
    {
        void Deserialize(Dictionary<string, object> data);
    }
}