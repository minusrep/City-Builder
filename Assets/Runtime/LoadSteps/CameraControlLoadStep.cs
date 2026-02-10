using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.CameraControl;
using Runtime.Colony;
using Runtime.Common;
using Runtime.Descriptions;

namespace Runtime.LoadSteps
{
    public class CameraControlLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly CameraControlView _cameraControlView;
        private readonly WorldDescription _worldDescription;

        public CameraControlLoadStep(List<IPresenter> presenters, World world, CameraControlView cameraControlView,
            WorldDescription worldDescription)
        {
            _world = world;
            _cameraControlView = cameraControlView;
            _worldDescription = worldDescription;
            _presenters = presenters;
        }

        public Task Run()
        {
            var cameraControlPresenter = new CameraControlPresenter(_world.MainCameraControl, _cameraControlView,
                _worldDescription.CameraControlDescription, _world.GameSystems);
            cameraControlPresenter.Enable();
            _presenters.Add(cameraControlPresenter);
            return Task.CompletedTask;
        }
    }
}