using System.Collections.Generic;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Citizens;
using Runtime.Descriptions;
using Runtime.Extensions;
using Runtime.GameSystems;
using Runtime.ModelCollections;

namespace Runtime.Colony
{
    public class World : ISerializeModel, IDeserializeModel
    {
        private const string CitizensKey = "citizens";

        private const string BuildingsKey = "buildings";

        public CitizenModelCollection Citizens { get; private set; }

        public BuildingModelCollection Buildings { get; private set; }

        public WorldDescription WorldDescription { get; private set; }
        
        public GameSystemCollection GameSystems { get; private set; }
        
        public PointOfInterestDescriptionCollection PointsOfInterest { get; private set; }
        
        public void SetData(WorldDescription worldDescription, FactoryProvider factoryProvider, GameSystemCollection gameSystems)
        {
            WorldDescription = worldDescription;

            Citizens = new CitizenModelCollection(worldDescription.Citizens);

            Buildings = new BuildingModelCollection(worldDescription.BuildingCollection, factoryProvider.BuildingModelFactory);
            
            GameSystems = gameSystems;
        }

        public Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>();

            dictionary[CitizensKey] = Citizens.Serialize();
            
            dictionary[BuildingsKey] = Buildings.Serialize();
            
            return dictionary;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Buildings.Deserialize(data.GetNode(BuildingsKey));
            
            Citizens.Deserialize(data.GetNode(CitizensKey));
        }
    }
}