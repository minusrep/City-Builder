using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory.Cell
{
    public class CellPresenter
    {
        private readonly CellModel _model;
        private readonly CellView _view;

        public CellPresenter(CellModel model, CellView view)
        {
            _model = model;
            _view = view;
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
            _view.Amount.text = _model.Amount > 0 ? _model.Amount.ToString() : "";
            
            _view.Amount.style.display = DisplayStyle.Flex;

            if (_model.Resource == null || _model.Amount <= 0)
            {
                _view.Image.style.backgroundImage = null;
                return;
            }

            var itemViewDescription = _view.Description.Get(_model.Resource.ViewId);
            
            _view.Image.style.backgroundImage = itemViewDescription.Image.texture;
        }
    }
}