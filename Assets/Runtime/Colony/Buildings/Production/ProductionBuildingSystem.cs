using System;
using Runtime.Colony.Buildings.Common;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingSystem : IGameSystem
    {
        public string Id { get; }

        private readonly ProductionBuildingModel _model;
        private readonly BuildingView _view;

        public ProductionBuildingSystem(string id, ProductionBuildingModel model, BuildingView view)
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
                    _model.Produce();
                    _model.StartProductionTime += _model.Description.ProductionTime;

                    if (!_model.CapacityLeft())
                    {
                        _model.StopProduction();
                    }
                }
            }
            else
            {
                UpdateProgressBar(1f);
            }
        }

        private void UpdateProgressBar(float progress)
        {
            _view.ProgressBar.value = Math.Clamp(progress, 0f, 1f) * 100f;
        }
    }
}