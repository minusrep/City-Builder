using System;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : IGameSystem
    {
        private readonly ProductionBuildingModel _model;
        private readonly ProductionBuildingView _view;

        public ProductionBuildingSystem(ProductionBuildingModel model, ProductionBuildingView view)
        {
            _model = model;
            _view = view;
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
                
                UpdateProgressBar(currentTime);
            }
        }

        private void UpdateProgressBar(long currentTime)
        {
            var progress = (float)(currentTime - _model.StartProductionTime) / _model.Description.ProductionTime;
            _view.ProgressBar.value = Math.Clamp(progress, 0f, 1f) * 100f;
        }
    }
}