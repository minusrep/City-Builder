using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace Runtime.Colony.Buildings.Descriptions
{
    [Serializable]
    public sealed class BuildingDescriptionCollection
    {
        [JsonProperty("buildings")]
        public Dictionary<string, BuildingDescription> Descriptions;
    }
}