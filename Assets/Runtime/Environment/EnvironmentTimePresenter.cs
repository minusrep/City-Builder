using Runtime.Common;
using UnityEngine;

namespace Runtime.Environment
{
    public class EnvironmentTimePresenter : IPresenter
    {
        private readonly EnvironmentTimeModel _model;
        private readonly EnvironmentView _view;
        
        public EnvironmentTimePresenter(EnvironmentTimeModel model, EnvironmentView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.OnTick += HandleTick;
        }

        public void Disable()
        {
            _model.OnTick -= HandleTick;
        }

        private void HandleTick(float currentTime)
        {
            var cycleLength = _model.Description.CycleLength;
            
            var currentIntensity = _view.DayLightIntensity * Mathf.Sin(Mathf.PI * currentTime/cycleLength);
            
            _view.DirectionalLight.intensity = currentIntensity;

            var currentX = (currentTime / cycleLength) * _view.MaxRotationX;
            
            _view.DirectionalLightTransform.rotation = Quaternion.Euler(currentX, 0f, 0f);
        }
    }
}