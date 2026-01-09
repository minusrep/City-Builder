using Runtime.Descriptions.Buildings;

namespace Runtime.Colony.Achievements.Events
{
    public class BuildingChange : GameEvent
    {
        public override string Type => "buildings_count_changed";
        public BuildingDescription Building { get; }
        public int Amount { get; }

        public BuildingChange(BuildingDescription building, int amount)
        {
            Building = building;
            Amount = amount;
        }
    }
}