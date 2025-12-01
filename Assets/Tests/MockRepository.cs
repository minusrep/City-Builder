using System.Collections.Generic;
using Runtime.Colony.Citizens;
using Runtime.Colony.GameResources;

namespace Tests
{
    public static class MockRepository
    {
        public static Dictionary<string, object> CreateTestCitizenDescription()
        {
            return new Dictionary<string, object>
            {
                { "names", new List<object> { "John", "Jane", "Bob" } },
                { "startMoveSpeed", 5f },
                { "thresholdNeeds", new Dictionary<string, object> 
                    { 
                        { "hunger", 100f }, 
                        { "energy", 100f } 
                    } 
                }
            };
        }

        public static Dictionary<string, ResourceModel> CreateTestCitizenNeeds()
        {
            return new Dictionary<string, ResourceModel>
            {
                {"hunger", new ResourceModel() },
                {"energy", new ResourceModel() },
            };
        }

        public static Dictionary<string, CitizenStateDescription> CreateTestCitizenStateDescription()
        {
            var idleToEatTransition = new TransitionDescription()
            {
                ToState = "eat",
                ConditionDescriptions = new ConditionDescription[]
                {
                    new ConditionDescription()
                    {
                        Parameter = "hunger",
                        Operator = "<",
                        Value = 30
                    }
                }
            };
    
            var eatToIdleTransition = new TransitionDescription()
            {
                ToState = "idle",
                ConditionDescriptions = new ConditionDescription[]
                {
                    new ConditionDescription()
                    {
                        Parameter = "hunger",
                        Operator = ">",
                        Value = 80
                    }
                }
            };

            return new Dictionary<string, CitizenStateDescription>
            {
                { "idle", new CitizenStateDescription(new[] { idleToEatTransition }) },
                { "eat", new CitizenStateDescription(new[] { eatToIdleTransition }) }
            };
        }
    }
}