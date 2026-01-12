using Runtime.Descriptions.Items;

namespace Runtime.Colony.Achievements.Events
{
    public class ResourceChangeEvent : GameEvent
    {
        public override string Type => "resource_amount_changed";
        public ResourceDescription Resource { get; }
        public int Amount { get; }

        public ResourceChangeEvent(ResourceDescription resource, int amount)
        {
            Resource = resource;
            Amount = amount;
        }
    }
}