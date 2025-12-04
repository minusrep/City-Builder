using System.Collections.Generic;
using Runtime.Colony.Citizens.StateMachine.States;
using UnityEngine;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateMachinePresenter
    {
        private const string WalkToStateKey = "WalkToState";
        private const string WaitToStateKey = "WaitState";

        private readonly CitizenStateMachineModel _model;

        private readonly CitizenStateMachineView _view;

        private readonly Dictionary<string, CitizenStatePresenter> _statePresenters;
        
        private readonly TempCitizenModel _citizenModel;

        public CitizenStateMachinePresenter(CitizenStateMachineModel model, CitizenStateMachineView view, TempCitizenModel citizenModel)
        {
            _model = model;
            
            _view = view;
            
            _citizenModel =  citizenModel;

            _statePresenters = new Dictionary<string, CitizenStatePresenter>()
            {
                {WalkToStateKey, new CitizenWalkToStatePresenter()},
                {WaitToStateKey, new CitizenWaitStatePresenter()}
            };
        }

        public void Enable()
        {
            _view.OnUpdate += Update;
        }
        
        public void Disable()
        {
            _view.OnUpdate -= Update;
        }

        private void Update()
        {
            UpdateStateMachine();

            _statePresenters[_model.CurrentStateDescription.Name]?.Update(_view);
        }

        private void UpdateStateMachine()
        {
            foreach (var transition in _model.CurrentStateDescription.Transitions)
            {
                var canTransit = true;
                
                foreach (var condition in transition.Conditions)
                {
                    Debug.Log(transition.ToState + ": " + condition.Check(_citizenModel));
                    
                    if (condition.Check(_citizenModel))
                    {
                        continue;
                    }
                    
                    canTransit = false;
                    
                    break;
                }

                if (canTransit)
                {
                    _model.Enter(transition.ToState);
                }
            }
        }
    }
}