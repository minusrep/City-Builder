using Runtime.ViewDescriptions.Inventory;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory.Cell
{
    public class CellPresenter
    {
        private readonly CellModel _model;
        private readonly CellView _view;
        private readonly InventoryViewDescription _viewDescription;

        public CellPresenter(CellModel model, CellView view, InventoryViewDescription viewDescription)
        {
            _model = model;
            _view = view;
            _viewDescription = viewDescription;
        }

        public void Enable()
        {
            _model.OnChanged += UpdateView;

            UpdateView();
        }

        public void Disable()
        {
            _model.OnChanged -= UpdateView;
        }

        private void UpdateView()
        {
            _view.Amount.text = _model.Amount.ToString();
            
            _view.Amount.style.display = DisplayStyle.Flex;
            
            if (_model.Resource == null)
                return;
            
            var itemViewDescription = _viewDescription.ItemViewDescriptions.Get(_model.Resource.ViewId);
            
            _view.Image.style.backgroundImage = itemViewDescription.Image.texture;
        }
    }
}