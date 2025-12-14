namespace Runtime.AsyncLoad
{
    public class LoadModel<T> : ILoadModel
    {
        public CustomAwaiter LoadAwaiter { get; } = new CustomAwaiter();

        public T Result { get; set; }

        public string Key { get; }

        public LoadModel(string key)
        {
            Key = key;
        }

        public void CompleteLoad()
        {
            LoadAwaiter.Complete();
        }

        public void DisposeLoad()
        {
            LoadAwaiter.Dispose();
        }

        public ILoadPresenter CreatePresenter()
        {
            return new LoadPresenter<T>(this);
        }
    }
}
