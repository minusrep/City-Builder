using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.UI.HUD;

namespace Runtime.LoadSteps
{
    public class HUDLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        
        private readonly World _world;
        
        private readonly HUDView _hudView;

        public HUDLoadStep(List<IPresenter> presenters, World world, HUDView hudView)
        {
            _presenters = presenters;
            _world = world;
            _hudView = hudView;
        }

        public async Task Run()
        {
            var hudModel = new HUDModel();
            
            var hudPresenter = new HUDPresenter(_hudView, hudModel, _world);
            
            hudPresenter.Enable();
            
            _presenters.Add(hudPresenter);

            await Task.CompletedTask;
        }
    }
}