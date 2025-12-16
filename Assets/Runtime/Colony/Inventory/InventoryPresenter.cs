using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private InventoryView _view;
        private readonly InventoryModel _model;
        private readonly InventoryViewDescription _viewDescription;
        private readonly Transform _transform;
        private readonly List<CellPresenter> _cellPresenters = new();

        public InventoryPresenter(InventoryModel model, InventoryViewDescription viewDescription, Transform transform)
        {
            _model = model;
            _viewDescription = viewDescription;
            _transform = transform;
        }

        public void Enable()
        {
            _view = Object.Instantiate(_viewDescription.Prefab, _transform);

            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset);
                _view.Root.Add(cellView.Root);

                var cellPresenter = new CellPresenter(pair.Value, cellView, _viewDescription);
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

            Object.Destroy(_view.gameObject);
        }
    }
}