using System;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : IGameSystem
    {
        public event Action<long> OnUpdate;
        
        private readonly ProductionBuildingModel _model;

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

                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                while (currentTime >= _model.CompleteProductionTime)
                {
                    if (_model.ProduceOnceAndQueue())
                    {
                        _model.StartProductionTime = currentTime;
                        _model.CompleteProductionTime += productionTime;
                    }
                    else
                    {
                        _model.StopProduction();
                        break;
                    }
                }

                OnUpdate?.Invoke(currentTime);
            }
        }
    }
}