using Runtime.Common;
using Runtime.Descriptions.CameraControl;
using Runtime.GameSystems;

namespace Runtime.CameraControl
{
    public class CameraControlPresenter : IPresenter
    {
        private readonly CameraControlModel _cameraControlModel;
        private readonly CameraControlSystem _cameraControlSystem;
        private readonly GameSystemCollection _gameSystemCollection;

        public CameraControlPresenter(CameraControlModel model, CameraControlView view, CameraControlDescription cameraControlDescription, GameSystemCollection gameSystemCollection)
        {
            _cameraControlModel = model;
            _gameSystemCollection = gameSystemCollection;

            _cameraControlSystem = new CameraControlSystem(_cameraControlModel, view, cameraControlDescription);
        }

        public void Enable()
        {
            _cameraControlModel.PlayerControls.Player.Enable();
            _gameSystemCollection.Add(_cameraControlSystem);
        }

        public void Disable()
        {
            _cameraControlModel.PlayerControls.Player.Disable();
            _gameSystemCollection.Remove(_cameraControlSystem);
        }
    }
}
