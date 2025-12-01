using System.Collections.Generic;

namespace Runtime.Colony
{
    public interface IDeserializeModel
    {
        void Deserialize(Dictionary<string, object> data);
    }
}