using Runtime.Descriptions.Items;

namespace Runtime.Colony.Achievements.Events
{
    public class ResourceChange : GameEvent
    {
        public override string Type => "resource_amount_changed";
        public ResourceDescription Resource { get; }
        public int Amount { get; }

        public ResourceChange(ResourceDescription resource, int amount)
        {
            Resource = resource;
            Amount = amount;
        }
    }
}