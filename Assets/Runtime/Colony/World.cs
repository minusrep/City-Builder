using Runtime.Colony.Buildings;
using Runtime.Colony.Citizens;

namespace Runtime.Colony
{
    public class World
    {
        public CitizenModelCollection Citizens { get; private set; }
        
        public BuildingModelCollection Buildings { get; private set; }
    }
}