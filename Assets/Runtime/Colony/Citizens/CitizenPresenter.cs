using Runtime.Colony.Citizens.EnterBuilding;
using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.Citizens.SetBuildingPointOfInterest;
using Runtime.Colony.Citizens.SetPointOfIntrest;
using Runtime.Colony.Citizens.Timer;
using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Services.Update;

namespace Runtime.Colony.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private CitizenMovementPresenter _citizenMovementPresenter;
        
        private CitizenTimerPresenter _citizenTimerPresenter;
        
        private CitizenBuildingSetPointOfInterestPresenter _citizenBuildingSetPointOfInterestPresenter;
        
        private CitizenSetPointOfInterestPresenter _citizenSetPointOfInterestPresenter;
        
        private CitizenEnterBuildingPresenter _citizenEnterBuildingPresenter;

        private readonly CitizenModel _model;
        
        private readonly World _world;
        
        private readonly CitizenView _view;
        
        private readonly UpdateService _updateService;
        
        public CitizenPresenter(CitizenView view,  CitizenModel model, World world, UpdateService updateService)
        {
            _view =  view;
            
            _world =  world;
            
            _model = model;
            
            _updateService = updateService;
        }
        
        public void Enable()
        {
            _citizenMovementPresenter = new CitizenMovementPresenter(_model, _view.MovementView, _updateService);

            _citizenBuildingSetPointOfInterestPresenter = new CitizenBuildingSetPointOfInterestPresenter(_model, _world);

            _citizenSetPointOfInterestPresenter = new CitizenSetPointOfInterestPresenter(_model, _world);
            
            _citizenTimerPresenter = new CitizenTimerPresenter(_model);
            
            _citizenEnterBuildingPresenter = new CitizenEnterBuildingPresenter(_model, _world);
            
            _citizenMovementPresenter.Enable();
            
            _citizenBuildingSetPointOfInterestPresenter.Enable();
            
            _citizenSetPointOfInterestPresenter.Enable();
            
            _citizenTimerPresenter.Enable();
            
            _citizenEnterBuildingPresenter.Enable();
        }

        public void Disable()
        {
            _citizenMovementPresenter.Disable();

            _citizenBuildingSetPointOfInterestPresenter.Disable();
            
            _citizenSetPointOfInterestPresenter.Disable();
            
            _citizenTimerPresenter.Disable();
            
            _citizenEnterBuildingPresenter.Disable();
            
            _citizenMovementPresenter = null;
            
            _citizenBuildingSetPointOfInterestPresenter = null;

            _citizenSetPointOfInterestPresenter = null;
            
            _citizenTimerPresenter = null;
            
            _citizenEnterBuildingPresenter = null;
        }
    }
}