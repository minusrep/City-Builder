using Runtime.Colony.Buildings.Common;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    public abstract class BuildingViewDescription<TView> : BuildingViewDescriptionBase where TView : BuildingView
    {
        [SerializeField] private TView _prefab;

        public TView Prefab => _prefab;

        public override BuildingView PrefabBase => _prefab;

        public string Id => name;
    }
}