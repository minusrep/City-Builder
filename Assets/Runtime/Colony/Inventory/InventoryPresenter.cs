using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private InventoryView _view;
        private readonly InventoryModel _model;
        private readonly UIDocument _uiDocument;
        private readonly InventoryViewDescription _viewDescriptions;
        private readonly List<CellPresenter> _cellPresenters = new();

        public InventoryPresenter(InventoryModel model, UIDocument uiDocument, WorldViewDescriptions viewDescriptions)
        {
            _model = model;
            _uiDocument = uiDocument;
            _viewDescriptions = viewDescriptions.InventoryViewDescription;
        }

        public void Enable()
        {
            _view = new InventoryView(_uiDocument, _viewDescriptions.CellViewAsset);

            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset);
                _view.Root.Add(cellView.Root);

                var cellPresenter = new CellPresenter(pair.Value, cellView, _viewDescriptions);
                cellPresenter.Enable();
                
                _cellPresenters.Add(cellPresenter);
            }
        }

        public void Disable()
        {
            foreach (var presenter in _cellPresenters)
            {
                presenter.Disable();
            }
            
            _cellPresenters.Clear();
        }
    }
}