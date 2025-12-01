using System;
using Runtime.Colony.Buildings.Descriptions;
using Runtime.Factories;

namespace Runtime
{
    [Serializable]
    public sealed class Descriptions
    {
        public BuildingDescriptionCollection BuildingDescriptionCollection { get; }
        
        public Descriptions(DescriptionFactory factory)    
        {
            
        }
    }
}