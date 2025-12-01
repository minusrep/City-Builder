using Runtime.Colony.Buildings;
using Runtime.Colony.Citizens;
using Runtime.Colony.Citizens.StateMachine;
using Runtime.Colony.Orders;

namespace Runtime.Colony
{
    public class ColonyModel
    {
        public ColonyDescription Description;
        public ModelCollection<CitizenModelCollection> Citizens;
        /*public ModelCollection<BuildingModel> Buildings;
        public ModelCollection<DescriptionModel> Descriptions;*/
    }
}