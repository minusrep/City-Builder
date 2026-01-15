using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryView _view;
        private readonly InventoryModel _model;
        private readonly List<CellPresenter> _cellPresenters = new();

        public InventoryPresenter(InventoryView view, InventoryModel model)
        {
            _view = view;
            
            _model = model;
        }

        public void Enable()
        {
            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset, _view.ViewDescription.ItemViewDescriptions);
                _view.Root.Add(cellView.Root);

                var cellPresenter = new CellPresenter(pair.Value, cellView);
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