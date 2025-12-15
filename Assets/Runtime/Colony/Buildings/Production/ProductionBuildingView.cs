using Runtime.Colony.Buildings.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingView : BuildingView
    {
        [SerializeField] private UIDocument _uiDocument;

        public ProgressBar ProgressBar;

        public override void Initialize()
        {
            base.Initialize();
            var root = _uiDocument.rootVisualElement;
            ProgressBar = root.Q<ProgressBar>("production-progress");
        }
    }
}