using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Inventory;
using Runtime.Colony.Stats;
using Runtime.Colony.Stats.Collections;
using Runtime.Common;

namespace Runtime.Selection.SelectedPanel
{
    public class SelectedCitizenPanelPresenter : IPresenter
    {
        private const string CitizenPrefixId = "citizen_";
        
        private readonly SelectedPanelView _view;
        private readonly SelectionModel _model;
        private readonly World _world;

        private StatPresenterCollection _statPresenterCollection;
        private InventoryPresenter _inventoryPresenter;
        
        public SelectedCitizenPanelPresenter(SelectedPanelView view, SelectionModel model, World world)
        {
            _view = view;
            _model = model;
            _world = world;
        }
        
        public void Enable()
        {
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            TryDrawCitizenModel();
        }

        private void TryDrawCitizenModel()
        {
            if (!_world.Citizens.TryGet($"{CitizenPrefixId}{_model.SelectedId}", out var selectedCitizen))
            {
                return;
            }
            
            SelectionPanelUtility.SetupPanel(_view.Root);
            
            _statPresenterCollection?.Disable();
            
            _inventoryPresenter?.Disable();

            _view.Root.Clear();
            
            var statViewCollection = new StatViewCollection(_view.Root);
            
            _statPresenterCollection =
                new StatPresenterCollection(statViewCollection, selectedCitizen.Stats, _view.StatViewDescriptions);
            
            var inventoryRoot = _view.InventoryViewDescription.InventoryAsset.CloneTree();
            
            var inventoryView = new InventoryView(inventoryRoot, _view.InventoryViewDescription);
            
            _inventoryPresenter = new InventoryPresenter(inventoryView, selectedCitizen.Inventory);
            
            _view.Root.Add(SelectionPanelUtility.CreateTitle(selectedCitizen.Name));

            _statPresenterCollection.Enable();
            
            _inventoryPresenter.Enable();
            
            _view.Root.Add(inventoryRoot);
        }
    }
}