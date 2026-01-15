using Runtime.Colony.Citizens.Animations;
using Runtime.Colony.Citizens.Debugging;
using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.Inventory;
using Runtime.Colony.Stats.Collections;
using Runtime.Common;
using Runtime.Selection;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private CitizenMovementPresenter _citizenMovementPresenter;

        private CitizenAnimatorPresenter _citizenAnimatorPresenter;
        
        private CitizenDebugPresenter _citizenDebugPresenter;

        private CitizenHUDPresenter _citizenHUDPresenter;

        private InventoryPresenter _inventoryPresenter;
        
        private readonly CitizenModel _model;
        
        private readonly World _world;
        
        private readonly WorldViewDescriptions _viewDescriptions;
        
        private readonly CitizenView _view;
                
        public CitizenPresenter(CitizenView view,  CitizenModel model, World world, WorldViewDescriptions viewDescriptions)
        {
            _view =  view;
            
            _world =  world;
            
            _viewDescriptions = viewDescriptions;
            
            _model = model;

            _model.StateMachine.OnChange += OnChangeState;

            _model.OnVisibilityChanged += OnVisibilityChanged;
        }

        public void Enable()
        {
            _view.Id = _model.Id.ToString();

            _view.Outline.SetOutline(SelectionOutlineType.None);

            _citizenHUDPresenter = new CitizenHUDPresenter(_view.CitizenHUDView, _model);
            
            _citizenMovementPresenter = new CitizenMovementPresenter(_model, _view.CitizenMovementView);
            
            _citizenAnimatorPresenter = new CitizenAnimatorPresenter(_view.CitizenAnimatorView, _model);
            
            _citizenDebugPresenter = new CitizenDebugPresenter(_view.CitizenDebugView, _model);

            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                _view.CitizenHUDView.UIDocument, _viewDescriptions);
            
            _citizenHUDPresenter.Enable();
            
            _citizenMovementPresenter.Enable();
            
            _citizenAnimatorPresenter.Enable();
            
            _citizenDebugPresenter.Enable();
            
            _inventoryPresenter.Enable();
            
            ExecuteActions();
        }

        public void Disable()
        {
            _citizenHUDPresenter.Disable();
            
            _inventoryPresenter.Disable();
            
            _citizenMovementPresenter.Disable();

            _citizenAnimatorPresenter.Disable();
            
            _citizenDebugPresenter.Disable();
            
            _citizenMovementPresenter = null;
            
            _citizenAnimatorPresenter = null;
        }

        private void OnChangeState()
        {
            ExecuteActions();
        }

        private void ExecuteActions()
        {
            foreach (var action in _model.StateMachine.CurrentState.Actions)
            {
                action.Execute(_world, _model);
            }
        }
        
        private void OnVisibilityChanged(bool visibility)
        {
            _view.gameObject.SetActive(visibility);
            
            if (visibility)
            {
                _inventoryPresenter.Enable();
                
                _citizenHUDPresenter.Enable();
            }
            else
            {
                _inventoryPresenter.Disable();
                
                _citizenHUDPresenter.Disable();
            }
        }
    }
}