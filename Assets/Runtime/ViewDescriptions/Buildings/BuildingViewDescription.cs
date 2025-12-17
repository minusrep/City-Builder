using Runtime.Colony.Buildings.Common;

namespace Runtime.ViewDescriptions.Buildings
{
    public abstract class BuildingViewDescription<TView> : BuildingViewDescriptionBase where TView : BuildingView
    {
        public TView Prefab;

        public override BuildingView GetViewDescription()
        {
            return Prefab;
        }

        public override string Id => name;
    }
}