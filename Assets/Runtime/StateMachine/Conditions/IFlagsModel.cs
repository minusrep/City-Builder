using System.Collections.Generic;

namespace Runtime.Core
{
    public interface IFlagsModel 
    {
        Dictionary<string, bool> Flags { get; }
    }
}