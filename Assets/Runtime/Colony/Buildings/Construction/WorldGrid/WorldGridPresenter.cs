using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
{
    public class WorldGridPresenter : IPresenter
    {
        private readonly WorldGridModel _model;
        private readonly WorldGridView _view;

        public WorldGridPresenter(WorldGridModel model, WorldGridView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.Transform.position = new Vector3(_model.Description.Origin.x, 1f, _model.Description.Origin.z);
            _model.OnActiveChanged += HandleActiveChanged;
            _view.GameObject.SetActive(false);
        }

        private void HandleActiveChanged(bool value)
        {
            _view.GameObject.SetActive(value);
        }

        public void Disable()
        {
            _model.OnActiveChanged -= HandleActiveChanged;
        }
    }
}