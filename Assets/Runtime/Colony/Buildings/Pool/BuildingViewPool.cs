using Runtime.Colony.Buildings.Common;
using Runtime.Common.ObjectPool;

namespace Runtime.Colony.Buildings.Pool
{
    public class BuildingViewPool<TView> : IBuildingViewPool where TView : BuildingView
    {
        private readonly ObjectPool<TView> _viewPool;

        public BuildingViewPool(ObjectPool<TView> viewPool)
        {
            _viewPool = viewPool;
        }

        public BuildingView Get()
        {
            var view = _viewPool.Get();
            view.Initialize();
            return view;
        }

        public void Release(BuildingView view)
        {
            _viewPool.Release((TView)view);
        }
    }
}