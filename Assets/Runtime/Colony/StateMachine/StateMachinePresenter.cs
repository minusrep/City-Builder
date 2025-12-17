using System;
using System.Linq;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions.StateMachine.Actions;
using UnityEngine;

namespace Runtime.Colony.StateMachine
{
    public class StateMachinePresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly World _world;

        public StateMachinePresenter(CitizenModel model, World world)
        {
            _model =  model;

            _world = world;
        }
        
        public void Enable()
        {
            _model.StateMachine.OnChange += OnChangeState;
        }

        public void Disable()
        {
            _model.StateMachine.OnChange -= OnChangeState;
        }

        private void OnChangeState()
        {
            foreach (var action in _model.StateMachine.CurrentState.Actions)
            {
                switch (action)
                {
                    case TimerActionDescription timerAction:
                        
                        _model.Timers[timerAction.Timer] = DateTimeOffset.UtcNow.AddSeconds(timerAction.Duration).ToUnixTimeSeconds();

                        break;
                    
                    case SetPointOfInterestActionDescription setPointOfInterestAction:

                        Vector3 point;
                        
                        if (setPointOfInterestAction.PointOfInterest == "current_building")
                        {
                            var buildingModel = _world.Buildings.Get(_model.BuildingId);
                            
                            point = new Vector3(buildingModel.Position.x, 0f, buildingModel.Position.y);
                        }
                        else
                        {
                            point = _world.WorldDescription.PointOfInterestCollection.Get(setPointOfInterestAction.PointOfInterest);
                        }
                        
                        _model.SetPointOfInterest(point);
                        
                        break;
                }
            }
        }
    }
}