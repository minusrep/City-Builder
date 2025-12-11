using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private InventoryView _view;
        private readonly InventoryModel _model;
        private readonly InventoryViewDescription _viewDescription;
        private readonly Transform _transform;
        private readonly Dictionary<CellModel, CellView> _cells = new();

        public InventoryPresenter(InventoryModel model, InventoryViewDescription viewDescription, Transform transform)
        {
            _model = model;
            _viewDescription =  viewDescription;
            _transform = transform;
        }

        public void Enable()
        {
            var prefab = Object.Instantiate(_viewDescription.Prefab, _transform);
            _view = prefab.GetComponent<InventoryView>();
            
            CreateCells();
            
            _model.OnItemChanged += UpdateCell;
        }

        private void CreateCells()
        {
            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset);
                
                _view.Root.Add(cellView.Root);
                _cells.Add(pair.Value, cellView);

                UpdateCell(pair.Value);
            }
        }
        
        private void UpdateCell(CellModel model)
        {
            _cells.TryGetValue(model, out var cellView);
            
            cellView.Amount.text = model.Amount.ToString();

            if (model.Amount == 0)
            {
                cellView.Image.style.backgroundImage = null;
                cellView.Amount.style.display = DisplayStyle.None;
            }
            else
            {
                cellView.Amount.style.display = DisplayStyle.Flex;
                
                //TODO: Жесткая связь с ResourceViewDescription
                var itemViewDescription = _viewDescription.ResourceViewDescriptions.Get(model.Item.Type);
                cellView.Image.style.backgroundImage = itemViewDescription.Image.texture;
            }
        }

        private void DestroyCells()
        {
            foreach (var cell in _cells)
            {
                cell.Value.Root.RemoveFromHierarchy();
            }

            _cells.Clear();
        }

        public void Disable()
        {
            _model.OnItemChanged -= UpdateCell;
            
            DestroyCells();
            
            //TODO: Должно ли вью разрушаться?
            Object.Destroy(_view.gameObject);
        }
    }
}