using System.Threading.Tasks;
using Runtime.CameraControl;
using Runtime.Colony;
using Runtime.Descriptions;

namespace Runtime.LoadSteps
{
    public class CameraControlLoadStep : IStep
    {
        private readonly World _world;
        private readonly CameraControlView _cameraControlView;
        private readonly WorldDescription _worldDescription;

        public CameraControlLoadStep(World world, CameraControlView cameraControlView, WorldDescription worldDescription)
        {
            _world = world;
            _cameraControlView = cameraControlView;
            _worldDescription = worldDescription;
        }

        public Task Run()
        {
            var cameraControlPresenter = new CameraControlPresenter(_world.MainCameraControl, _cameraControlView,
                _worldDescription.CameraControlDescription, _world.GameSystems);
            cameraControlPresenter.Enable();
            
            return Task.CompletedTask;
        }
    }
}