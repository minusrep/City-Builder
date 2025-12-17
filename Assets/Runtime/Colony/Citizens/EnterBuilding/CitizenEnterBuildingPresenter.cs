using System.Linq;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions.StateMachine.Actions;
using UnityEngine;

namespace Runtime.Colony.StateMachine
{
    public class CitizenEnterBuildingPresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly World _world;

        public CitizenEnterBuildingPresenter(CitizenModel model, World world)
        {
            _model = model;
            
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
                if (action is not EnterBuildingActionDescription enterBuilding)
                {
                    continue;
                }

                var buildings = _world.Buildings.Models.Values;

                var targetBuildings = buildings.Where(a => enterBuilding.Building == a.BaseDescription.Type).ToList();

                var minDistance = Vector3.Distance(targetBuildings[0].Position, _model.Position);

                BuildingModel resultBuilding = null;
                
                foreach (var building in targetBuildings)
                {
                    var distance = Vector3.Distance(_model.Position, building.Position);
                    
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        
                        resultBuilding = building;
                    }
                }

                resultBuilding?.Enter(_model);

                break;
            }
        }
    }
}