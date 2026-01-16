using Runtime.Descriptions.Buildings;

namespace Runtime.Colony.Achievements.Events.Types
{
    public class BuildingUpdateEvent : GameEvent
    {
        public override string Type => "building_level_updated";
        public BuildingDescription Building { get; }
        public int Level { get; }
        public string BuildingInstanceId { get; }

        public BuildingUpdateEvent(BuildingDescription building, int level, string buildingInstanceId)
        {
            Building = building;
            Level = level;
            BuildingInstanceId = buildingInstanceId;
        }
    }
}