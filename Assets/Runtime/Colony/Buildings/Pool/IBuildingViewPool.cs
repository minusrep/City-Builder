using Runtime.Colony.Buildings.Common;

namespace Runtime.Colony.Buildings.Pool
{
    public interface IBuildingViewPool
    {
        BuildingView Get();
        void Release(BuildingView view);
    }
}