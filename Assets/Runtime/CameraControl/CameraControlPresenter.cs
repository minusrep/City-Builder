using Runtime.Common;
using Runtime.Descriptions.CameraControl;
using Runtime.GameSystems;

namespace Runtime.CameraControl
{
    public class CameraControlPresenter : IPresenter
    {
        private readonly CameraControlView _cameraControlView;
        private readonly CameraControlModel _cameraControlModel;
        private readonly CameraControlDescription _cameraControlDescription;
        private readonly CameraControlSystem _cameraControlSystem;
        private readonly GameSystemCollection _gameSystemCollection;

        public CameraControlPresenter(CameraControlModel model, CameraControlView view, CameraControlDescription cameraControlDescription, GameSystemCollection gameSystemCollection)
        {
            _cameraControlModel = model;
            _cameraControlView = view;
            _cameraControlDescription = cameraControlDescription;
            _gameSystemCollection = gameSystemCollection;

            _cameraControlSystem = new CameraControlSystem(_cameraControlModel, _cameraControlView, _cameraControlDescription);
        }

        public void Enable()
        {
            _cameraControlModel.playerControls.Camera.Enable();
            _gameSystemCollection.Add(_cameraControlSystem);
        }

        public void Disable()
        {
            _cameraControlModel.playerControls.Camera.Disable();
            _gameSystemCollection.Remove(_cameraControlSystem);
        }
    }
}
