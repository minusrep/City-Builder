namespace Runtime.Colony.GameResources
{
    public class ResourceModel
    {
        public ResourceDescription Description;
        public int Amount { get; private set; }

        public void IncreaseAmount(int amount)
        {
            Amount += amount;
        }

        public void ReduceAmount(int amount)
        {
            Amount -= amount;
        }
    }
}