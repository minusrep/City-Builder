using System.Collections.Generic;
using Runtime.Colony.Achievements.Events;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers.Types
{
    public class BuildingMaxLevelCountTriggerModel : TriggerModel
    {
        private readonly HashSet<string> _maxLevelBuildingIds = new();

        public BuildingMaxLevelCountTriggerModel(TriggerDescription description, AchievementModel model) : base(description, model)
        {
        }

        public override void Subscribe()
        {
            MessageBroker.Instance.Subscribe("building_level_updated", OnEventReceived);
        }

        public override void Unsubscribe()
        {
            MessageBroker.Instance.Unsubscribe("building_level_updated", OnEventReceived);
        }

        protected override void OnEventReceived(GameEvent gameEvent)
        {
            if (!Model.IsActive)
            {
                return;
            }

            if (gameEvent is not BuildingUpdateEvent buildingUpdateEvent)
            {
                return;
            }
            
            if (Description.Value != buildingUpdateEvent.Building.Type)
            {
                return;
            }
            
            if (buildingUpdateEvent.Level < 3)
            {
                return;
            }
            
            if (_maxLevelBuildingIds.Add(buildingUpdateEvent.BuildingInstanceId))
            {
                Model.SetProgress(_maxLevelBuildingIds.Count);
            }
        }
    }
}