using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;
using Runtime.Descriptions;
using UnityEngine;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private InventoryView _view;
        private readonly InventoryModel _model;
        private readonly InventoryViewDescription _viewDescription;
        private readonly Transform _transform;
        private readonly List<CellView> _cells = new();

        public InventoryPresenter(InventoryModel model, InventoryViewDescription viewDescription, Transform transform)
        {
            _model = model;
            _viewDescription =  viewDescription;
            _transform = transform;
        }

        public void Enable()
        {
            var view = Object.Instantiate(_viewDescription.Prefab, _transform);
            _view = view.GetComponent<InventoryView>();
            
            CreateCells();
        }

        private void CreateCells()
        {
            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset);

                cellView.Amount.text = pair.Value.Amount.ToString();

                //TODO: Жесткая связь с ResourceViewDescription
                var itemViewDescription = _viewDescription.ResourceViewDescriptions.Get(pair.Value.Item.Type);
                
                cellView.Image.style.backgroundImage = itemViewDescription.Image.texture;
                    
                _view.Root.Add(cellView.Root);
                
                _cells.Add(cellView);
            }
        }

        // TODO: Дописать обновление ячеек
        private void UpdateCells()
        {
            
        }

        private void DestroyCells()
        {
            foreach (var cell in _cells)
            {
                cell.Root.RemoveFromHierarchy();
            }
        }

        public void Disable()
        {
            DestroyCells();
            
            //TODO: Должно ли вью разрушаться?
            Object.Destroy(_view.gameObject);
        }
    }
}