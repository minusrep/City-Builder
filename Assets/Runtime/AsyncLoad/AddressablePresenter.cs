using System.Collections.Generic;
using Runtime.Common;

namespace Runtime.AsyncLoad
{
    public class AddressablePresenter : IPresenter
    {
        private readonly AddressableModel _addressableModel;
        private readonly Dictionary<ILoadModel, ILoadPresenter> _loadPresenters = new();

        public AddressablePresenter(AddressableModel addressableModel)
        {
            _addressableModel = addressableModel;
        }

        public void Enable()
        {
            _addressableModel.LoadModels.OnAdded += OnModelAdded;
            _addressableModel.LoadModels.OnRemoved += OnModelRemoved;
        }

        public void Disable()
        {
            _addressableModel.UnloadAll();
            _addressableModel.LoadModels.OnAdded -= OnModelAdded;
            _addressableModel.LoadModels.OnRemoved -= OnModelRemoved;
        }

        private void OnModelAdded(ILoadModel loadModel)
        {
            var presenter = loadModel.CreatePresenter();

            _loadPresenters.Add(loadModel, presenter);
            presenter.Enable();
        }

        private void OnModelRemoved(ILoadModel loadModel)
        {
            if (_loadPresenters.TryGetValue(loadModel, out var presenter))
            {
                presenter.Disable();
                _loadPresenters.Remove(loadModel);
            }
        }
    }
}
