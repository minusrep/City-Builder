using System.Collections.Generic;
using Runtime.Colony.Citizens.Systems;
using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;
using UnityEngine;

namespace Runtime.Colony.Citizens.Collection
{
    public class CitizenPresenterCollection : IPresenter
    {
        private readonly Dictionary<int, CitizenPresenter> _presenters = new();

        private readonly CitizenModelCollection _model;

        private readonly CitizenViewCollection _view;
        
        private readonly World _world;

        private readonly WorldViewDescriptions _viewDescriptions;
        
        private readonly Dictionary<string, ObjectPool<CitizenView>> _pools = new ();

        public CitizenPresenterCollection(CitizenViewCollection view, CitizenModelCollection model, 
            World world,  WorldViewDescriptions viewDescriptions)
        {
            _view = view;
            
            _model = model;

            _world = world;
            
            _viewDescriptions = viewDescriptions;
        }

        public async void Enable()
        {
            foreach (var viewDescription in _viewDescriptions.CitizenViewDescriptionCollection.Descriptions)
            {
                var prefab = await viewDescription.Prefab.LoadAssetAsync().Task;
                var citizenView = prefab.GetComponent<CitizenView>();
                var viewPool = new ObjectPool<CitizenView>(citizenView, 2, _view.Transform);
                _pools[viewDescription.Id] = viewPool;

                viewDescription.Prefab.ReleaseAsset();
            }
            
            _model.OnAdded += OnAdded;

            _model.OnRemoved += OnRemoved;
            
            foreach (var model in _model.Models.Values)
            {
                CreateCitizenPresenter(model);
            }
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
            var stressSystem = _world.GameSystems.Get("stress") as CitizenStatSystem;
            var stateMachineSystem = _world.GameSystems.Get("state_machine") as StateMachineSystem;
            
            
            hungrySystem?.Unregister(citizenModel);
            fatigueSystem?.Unregister(citizenModel);
            stressSystem?.Unregister(citizenModel);
            stateMachineSystem?.Unregister(citizenModel);
            
            citizenPresenter.Disable();
            
            _presenters.Remove(citizenModel.Id);
        }

        private void CreateCitizenPresenter(CitizenModel citizenModel)
        {
            var citizenView = _pools[citizenModel.ViewDescription].Get();

            var citizenPresenter = new CitizenPresenter(citizenView, citizenModel, _world, _viewDescriptions);
            
            _presenters[citizenModel.Id] = citizenPresenter;

            var stateMachineSystem = _world.GameSystems.Get("state_machine") as StateMachineSystem;
            var hungrySystem = _world.GameSystems.Get("hungry") as CitizenStatSystem;
            var fatigueSystem = _world.GameSystems.Get("fatigue") as CitizenStatSystem;
            var stressSystem = _world.GameSystems.Get("stress") as CitizenStatSystem;
            
            stateMachineSystem?.Register(citizenModel);
            
            hungrySystem?.Register(citizenModel);
            
            fatigueSystem?.Register(citizenModel);

            stressSystem?.Register(citizenModel);

            citizenPresenter.Enable();
        }
    }
}