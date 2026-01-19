using Runtime.Colony;
using Runtime.GameSystems;
using UnityEngine.UIElements;

namespace Runtime.UI.DayNightIndicator
{
    public class DayNightIndicatorSystem : IGameSystem
    {
        public string Id => "day_night_indicator";

        private readonly DayNightIndicatorView _view;
        private readonly World _world;

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
            var angle = normalizedTime * 360f;
            _view.IndicatorInner.style.rotate = new StyleRotate(new Angle(angle, AngleUnit.Degree));
        }
    }
}