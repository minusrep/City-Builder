using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.AsyncLoad
{
    public class LoadPresenter<T> : ILoadPresenter
    {
        private readonly LoadModel<T> _loadModel;
        private AsyncOperationHandle<T> _handle;

        public LoadPresenter(LoadModel<T> loadModel)
        {
            _loadModel = loadModel;
        }

        public void Enable()
        {
            _handle = Addressables.LoadAssetAsync<T>(_loadModel.Key);
            _handle.Completed += OnCompleted;
        }

        public void Disable()
        {
            _loadModel.DisposeLoad();
            _handle.Completed -= OnCompleted;
            Addressables.Release(_handle);
        }

        private void OnCompleted(AsyncOperationHandle<T> handle)
        {
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Failed to load {_loadModel.Key}: {handle.OperationException}");
            }
            _loadModel.Result = handle.Result;
            _loadModel.CompleteLoad();
        }
    }
}
