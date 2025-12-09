using Runtime.Colony.Buildings;
using Runtime.Colony.Buildings.Models;
using Runtime.Colony.Citizens;

namespace Runtime.Colony
{
    public class World
    {
        public CitizenModelCollection Citizens { get; private set; }

        public BuildingModelCollection Buildings { get; private set; }

        public World(CitizenModelCollection citizens, BuildingModelCollection buildings)
        {
            Citizens = citizens;
            
            Buildings = buildings;
        }
    }
}