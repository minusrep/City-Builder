using System.Collections.Generic;

namespace Runtime.Colony.ModelCollections
{
    public interface ISerializeModel
    {
        Dictionary<string, object> Serialize();
    }
}