using System.Linq;
using Runtime.Common;
using Runtime.Descriptions.StateMachine.Actions;
using UnityEngine;

namespace Runtime.Colony.Citizens.SetBuildingPointOfInterest
{
    public class CitizenBuildingSetPointOfInterestPresenter : IPresenter
    {
        private readonly CitizenModel _model;

        private readonly World _world;

        public CitizenBuildingSetPointOfInterestPresenter(CitizenModel model, World world)
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
                if (action is not SetPointOfInterestBuildingActionDescription setBuildingPointOfInterest)
                {
                    continue;
                }

                var buildings = _world.Buildings.Models.Values;

                var targetBuildings = buildings.Where(a => setBuildingPointOfInterest.Type == a.BaseDescription.Type).ToList();

                var buildingPosition = targetBuildings[0].Position;
                
                var minDistance = Vector3.Distance(buildingPosition, _model.Position);
                
                foreach (var building in targetBuildings)
                {
                    var distance = Vector3.Distance(_model.Position, building.Position);
                    
                    if (distance < minDistance)
                    {
                        buildingPosition = building.Position;
                        
                        minDistance = distance;
                    }
                }
                
                _model.SetPointOfInterest(setBuildingPointOfInterest.BuildingPointOfInterest, buildingPosition);

                break;
            }
        }
    }
}