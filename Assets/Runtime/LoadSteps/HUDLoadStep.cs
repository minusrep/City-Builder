using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.Input;
using Runtime.UI.HUD;

namespace Runtime.LoadSteps
{
    public class HUDLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        
        private readonly World _world;

        private readonly PlayerControls _playerControls;
        
        private readonly HUDView _hudView;

        public HUDLoadStep(List<IPresenter> presenters, World world, PlayerControls playerControls, HUDView hudView)
        {
            _presenters = presenters;
            _world = world;
            _playerControls = playerControls;
            _hudView = hudView;
        }

        public async Task Run()
        {
            var hudModel = new HUDModel();
            
            var hudPresenter = new HUDPresenter(_hudView, hudModel, _world,  _playerControls);
            
            hudPresenter.Enable();
            
            _presenters.Add(hudPresenter);

            await Task.CompletedTask;
        }
    }
}