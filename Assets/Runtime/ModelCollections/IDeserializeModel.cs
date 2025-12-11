using System.Collections.Generic;

namespace Runtime.ModelCollections
{
    public interface IDeserializeModel
    {
        void Deserialize(Dictionary<string, object> data);
    }
}