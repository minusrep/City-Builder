using System.Collections.Generic;

namespace Runtime.ModelCollections
{
    public interface ISerializeModel
    {
        Dictionary<string, object> Serialize();
    }
}