using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.Environment;

namespace Runtime.LoadSteps
{
    public class EnvironmentLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly EnvironmentView _environmentView;

        public EnvironmentLoadStep(List<IPresenter> presenters, World world, EnvironmentView environmentView)
        {
            _presenters = presenters;
            _world = world;
            _environmentView = environmentView;
        }

        public Task Run()
        {
            var environmentTimeSystem = _world.GameSystems.Get("environment") as EnvironmentSystem;
            
            var environmentPresenter = new EnvironmentPresenter(_world.Environment, _environmentView, environmentTimeSystem);
            
            environmentPresenter.Enable();
            
            _presenters.Add(environmentPresenter);
            
            return Task.CompletedTask;
        }
    }
}