using System;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : IGameSystem
    {
        public string Id { get; }
        
        private readonly ProductionBuildingModel _model;
        private readonly ProductionBuildingView _view;

        public ProductionBuildingSystem(string id, ProductionBuildingModel model, ProductionBuildingView view)
        {
            _model = model;
            _view = view;
            Id = id;
        }

        public void Update(float deltaTime)
        {
            if (_model.IsActive)
            {
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                
                var progress = (float)(currentTime - _model.StartProductionTime) / _model.Description.ProductionTime;
                
                UpdateProgressBar(progress);

                if (progress >= 1f)
                {
                    if (_model.ProduceOnceAndQueue())
                    {
                        _model.StartProductionTime += _model.Description.ProductionTime;
                    }
                    else
                    {
                        _model.StopProduction();
                    }
                }
            }
        }

        private void UpdateProgressBar(float progress)
        {
            _view.ProgressBar.value = Math.Clamp(progress, 0f, 1f) * 100f;
        }
    }
}