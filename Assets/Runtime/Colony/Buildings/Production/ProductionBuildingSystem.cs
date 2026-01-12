using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : RegisterGameSystem<ProductionBuildingModel>
    {
        public ProductionBuildingSystem(string id) : base(id)
        {
        }

        protected override void Update(ProductionBuildingModel item, float deltaTime)
        {
            if (item.IsActive)
            {
                item.Progress += deltaTime / item.ProductionTime * 1000f;
                
                if (item.Progress >= 1f)
                {
                    if (item.Produce())
                    {
                        item.Progress = 0;
                        item.StartProduction();
                    }
                    else
                    {
                        item.StopProduction();
                    }
                }
            }
        }
    }
}