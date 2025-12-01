using System.Collections.Generic;

namespace Runtime.Colony
{
    public interface ISerializeModel
    {
        Dictionary<string, object> Serialize();
    }
}