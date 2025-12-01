using System;
using Runtime.Descriptions.GameResources;

namespace Runtime.Colony.GameResources
{
    public class ResourceModel
    {
        public ResourceDescription Description;

        public int Amount { get; private set; }
        
        public ResourceModel(ResourceDescription description)
        {
            Description = description;
        }

        public void IncreaseAmount(int amount)
        {
            if (amount > 0)
            {
                Amount += amount;
            }
        }

        public void ReduceAmount(int amount)
        {
            if (amount > 0)
            {
                var taken = Math.Min(Amount, amount);
                Amount -= taken;
            }
        }
    }
}