using Runtime.Descriptions.Buildings;

namespace Runtime.Colony.Achievements.Events.Types
{
    public class BuildingUpdateEvent : GameEvent
    {
        public override string Type => "building_level_updated";
        public BuildingDescription Building { get; }
        public int Level { get; }

        public BuildingUpdateEvent(BuildingDescription building, int level)
        {
            Building = building;
            Level = level;
        }
    }
}