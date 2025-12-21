using System.Collections.Generic;
using Runtime.Colony.Citizens.Systems;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Citizens.Collection
{
    public class CitizenPresenterCollection : IPresenter
    {
        private readonly Dictionary<int, CitizenPresenter> _presenters = new Dictionary<int, CitizenPresenter>();

        private readonly CitizenModelCollection _model;

        private readonly CitizenViewCollection _view;
        
        private readonly World _world;
        
        private readonly WorldViewDescriptions _viewDescriptions;

        public CitizenPresenterCollection(CitizenViewCollection view, CitizenModelCollection model, 
            World world, WorldViewDescriptions viewDescriptions)
        {
            _view = view;
            
            _model = model;

            _world = world;
            
            _viewDescriptions = viewDescriptions;
        }

        public void Enable()
        {
            foreach (var model in _model.Models.Values)
            {
                CreateCitizenPresenter(model);
            }
            
            _model.OnAdded += OnAdded;

            _model.OnRemoved += OnRemoved;
        }

        public void Disable()
        {
            _model.OnAdded -= OnAdded;

            _model.OnRemoved -= OnRemoved;
        }

        private void OnAdded(CitizenModel citizenModel)
        {
            CreateCitizenPresenter(citizenModel);
        }

        private void OnRemoved(CitizenModel citizenModel)
        {
            var citizenPresenter = _presenters[citizenModel.Id];
            
            var hungrySystem = _world.GameSystems.Get("hungry") as CitizenStatSystem;
            var fatigueSystem = _world.GameSystems.Get("fatigue") as CitizenStatSystem;
            
            hungrySystem.Unregister(citizenModel);
            fatigueSystem.Unregister(citizenModel);
            
            citizenPresenter.Disable();
            
            _presenters.Remove(citizenModel.Id);
        }

        private void CreateCitizenPresenter(CitizenModel citizenModel)
        {
            var citizenView = _view.InstantiateCitizenView();

            var citizenPresenter = new CitizenPresenter(citizenView, citizenModel, _world, _viewDescriptions);
            
            _presenters[citizenModel.Id] = citizenPresenter;

            var hungrySystem = _world.GameSystems.Get("hungry") as CitizenStatSystem;
            var fatigueSystem = _world.GameSystems.Get("fatigue") as CitizenStatSystem;
            
            hungrySystem.Register(citizenModel);
            
            fatigueSystem.Register(citizenModel);
            
            citizenPresenter.Enable();
        }
    }
}