using System;
using Runtime.Colony.Buildings;
using Runtime.Colony.Buildings.Descriptions;
using Runtime.Factories;

namespace Runtime
{
    [Serializable]
    public sealed class Descriptions
    {
        public BuildingsDescriptionCollection BuildingDescriptionCollection { get; }
        
        public Descriptions(DescriptionFactory factory)    
        {
            
        }
    }
}