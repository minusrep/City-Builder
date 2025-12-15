using System;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : IGameSystem
    {
        private readonly ProductionBuildingModel _model;

        private float _elapsed;

        public ProductionBuildingSystem(ProductionBuildingModel model)
        {
            _model = model;
        }

        public void Update(float deltaTime)
        {
            if (_model.IsActive)
            {
                var productionTime = _model.Description.ProductionTime;

                if (productionTime <= 0)
                {
                    _model.ProduceOnceAndQueue();
                    _model.StopProduction();
                    return;
                }

                while (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= _model.CompleteProductionTime)
                {
                    if (_model.ProduceOnceAndQueue())
                    {
                        _model.StartProductionTime = _model.CompleteProductionTime;
                        _model.CompleteProductionTime += productionTime;
                    }
                    else
                    {
                        _model.StopProduction();
                        break;
                    }
                }
            }
        }
    }
}