using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private InventoryModel _model;
        private InventoryView _view;
        private Transform _target;
        
        public InventoryPresenter(InventoryView view, int size,Transform target)
        {
            _model = new InventoryModel(size);
            _view = view;
            _target = target;
        }

        public void Enable()
        {
            var screenPos = Camera.main.WorldToScreenPoint(_target.position);
            
            foreach (var cell in _model.Cells)
            {
                var cellView = new CellView(_view.CellAsset);
                
                cellView.Root.style.position = Position.Absolute;
                cellView.Root.style.left = screenPos.x;
                cellView.Root.style.top = Screen.height - screenPos.y;
                
                _view.MenuRoot.Add(cellView.Root);
                
                cellView.Amount.text = cell.Amount.ToString();
            }
        }

        public void Disable()
        {
            
        }
    }
}