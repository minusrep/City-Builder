using Runtime.ModelCollections;

namespace Runtime.AsyncLoad
{
    public class AddressableModel
    {
        private readonly LoadModelCollection _loadModels = new LoadModelCollection();

        public IModelCollection<ILoadModel> LoadModels => _loadModels;

        public LoadModel<T> Load<T>(string key)
        {
            var model = new LoadModel<T>(key);

            _loadModels.Add(key, model);           

            return model;
        }
        
        public void Unload<T>(LoadModel<T> loadModel)
        {
            _loadModels.Remove(loadModel.Key);            
        }

        public void UnloadAll()
        {
            _loadModels.Clear();
        }
    }
}


