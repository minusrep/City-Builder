using Runtime.Colony;
using Runtime.GameSystems;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.UI.DayNightIndicator
{
    public class DayNightIndicatorSystem : IGameSystem
    {
        private const float RotationOffset = 90f;
        public string Id => "day_night_indicator";

        private readonly DayNightIndicatorView _view;
        private readonly World _world;
        
        private float _currentAngle;

        public DayNightIndicatorSystem(DayNightIndicatorView view, World world)
        {
            _view = view;
            _world = world;
        }

        public void Update(float deltaTime)
        {
            var currentTime = _world.Environment.Time.CurrentTime;
            var cycleLength = _world.Environment.Description.Time.CycleLength;
            var normalizedTime = currentTime / cycleLength;
            var targetAngle = normalizedTime * 360f + RotationOffset;

            _currentAngle = Mathf.LerpAngle(_currentAngle, targetAngle, deltaTime);
            _view.IndicatorInner.style.rotate = new StyleRotate(new Angle(_currentAngle, AngleUnit.Degree));
        }
    }
}