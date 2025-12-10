using System.Collections.Generic;
using Runtime.Colony.Inventory.Cell;

namespace Runtime.Colony.Inventory
{
    public class InventoryPresenter
    {
        private readonly InventoryModel _model;
        private readonly InventoryView _view;
        
        private List<CellView> _cells = new();

        public InventoryPresenter(InventoryModel model, InventoryView view)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            CreateCells();
        }

        private void CreateCells()
        {
            foreach (var pair in _model.Models)
            {
                var cellView = new CellView(_view.CellAsset);

                cellView.Amount.text = pair.Value.Amount.ToString();
                
                _view.WorldRoot.Add(cellView.Root);
                
                _cells.Add(cellView);
            }
        }

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
        }
    }
}