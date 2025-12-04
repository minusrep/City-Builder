using System.Collections.Generic;
using Runtime.Colony.Citizens;
using Runtime.Colony.Citizens.StateMachine.Temp;
using Runtime.Colony.GameResources;
using Runtime.Descriptions.Citizens;

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
    }
}