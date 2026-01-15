using System.Collections.Generic;
using Runtime.CameraControl;
using Runtime.Colony.Achievements.Collection;
using Runtime.Colony.Buildings.Collection;
using Runtime.Colony.Buildings.Construction;
using Runtime.Colony.Buildings.Construction.WorldGrid;
using Runtime.Colony.Citizens.Collection;
using Runtime.Colony.Orders;
using Runtime.Descriptions;
using Runtime.Extensions;
using Runtime.GameSystems;
using Runtime.Input;
using Runtime.ModelCollections;
using Runtime.Selection;
using Runtime.UI.InGameMenu;
using UnityEngine;

namespace Runtime.Colony
{
    public class World : ISerializeModel, IDeserializeModel
    {
        private const string CitizensKey = "citizens";
        private const string BuildingsKey = "buildings";
        private const string OrderManagerKey = "order_manager";
        private const string AchievementsKey = "achievements";

        public Camera MainCamera { get; private set; }
        public CitizenModelCollection Citizens { get; private set; }
        public BuildingModelCollection Buildings { get; private set; }
        public BuildingConstructionModel BuildingConstructionModel { get; private set; }
        public SelectionModel SelectionModel { get; private set; }
        public AchievementModelCollection Achievements { get; private set; }
        public WorldGridModel Grid { get; private set; }
        public PlayerControls PlayerControls { get; private set; }
        public WorldDescription WorldDescription { get; private set; }
        public GameSystemCollection GameSystems { get; private set; }
        public OrderManager OrderManager { get; private set; }
        public CameraControlModel MainCameraControl { get; private set; }
        public InGameMenuModel InGameMenuModel { get; private set; }

        public void SetData(WorldDescription worldDescription,
            GameSystemCollection gameSystems, PlayerControls playerControls)
        {
            MainCamera = Camera.main;
            PlayerControls = playerControls;
            WorldDescription = worldDescription;
            GameSystems = gameSystems;
            
            Citizens = new CitizenModelCollection(worldDescription);
            
            Buildings = new BuildingModelCollection(worldDescription.BuildingCollection, this);
            BuildingConstructionModel = new BuildingConstructionModel(PlayerControls);
            SelectionModel = new SelectionModel();
            
            Achievements = new AchievementModelCollection(worldDescription.AchievementsCollection);
            Grid = new WorldGridModel(worldDescription.WorldGridDescription);

            OrderManager = new OrderManager();
            MainCameraControl = new CameraControlModel(PlayerControls);
            InGameMenuModel = new InGameMenuModel(PlayerControls);
        }

        public Dictionary<string, object> Serialize()
        {
            var dictionary = new Dictionary<string, object>
            {
                [CitizensKey] = Citizens.Serialize(),
                [BuildingsKey] = Buildings.Serialize(),
                [OrderManagerKey] = OrderManager.Serialize(),
                [AchievementsKey] = Achievements.Serialize()
            };

            return dictionary;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Buildings.Deserialize(data.GetNode(BuildingsKey));
            Citizens.Deserialize(data.GetNode(CitizensKey));
            Achievements.Deserialize(data.GetNode(AchievementsKey));
            OrderManager.Deserialize(data.GetNode(OrderManagerKey));
            
            Grid.RebuildFromBuildings(Buildings.Models.Values);
        }
    }
}