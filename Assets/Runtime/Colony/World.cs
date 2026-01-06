using System.Collections.Generic;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Citizens.Collection;
using Runtime.Colony.Construction;
using Runtime.Descriptions;
using Runtime.Extensions;
using Runtime.GameSystems;
using Runtime.Input;
using Runtime.ModelCollections;
using UnityEngine;

namespace Runtime.Colony
{
    public class World : ISerializeModel, IDeserializeModel
    {
        private const string CitizensKey = "citizens";

        private const string BuildingsKey = "buildings";
        
        public Camera MainCamera { get; private set; }

        public CitizenModelCollection Citizens { get; private set; }

        public BuildingModelCollection Buildings { get; private set; }
        
        public WorldGridModel Grid { get; private set; }
        
        public PlayerControls PlayerControls { get; private set; }

        public WorldDescription WorldDescription { get; private set; }
        
        public GameSystemCollection GameSystems { get; private set; }
        
        public void SetData(WorldDescription worldDescription, FactoryProvider factoryProvider, GameSystemCollection gameSystems)
        {
            MainCamera = Camera.main;
            
            WorldDescription = worldDescription;
            GameSystems = gameSystems;

            Citizens = new CitizenModelCollection(worldDescription);
            Buildings = new BuildingModelCollection(worldDescription.BuildingCollection, factoryProvider.BuildingModelFactory);
            Grid = new WorldGridModel(worldDescription.WorldGridDescription);
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();
        }

        public Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>
            {
                [CitizensKey] = Citizens.Serialize(),
                [BuildingsKey] = Buildings.Serialize()
            };

            return dictionary;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Buildings.Deserialize(data.GetNode(BuildingsKey));
            
            Citizens.Deserialize(data.GetNode(CitizensKey));
        }
    }
}