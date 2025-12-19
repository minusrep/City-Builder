using Runtime.Colony.Buildings.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingView : BuildingView
    {
        public UIDocument Document => _uiDocument;
        public ProgressBar ProgressBar => _progressBar;

        [SerializeField] private UIDocument _uiDocument;
        private ProgressBar _progressBar;

        public override void Initialize()
        {
            base.Initialize();
            var root = _uiDocument.rootVisualElement;
            _progressBar = root.Q<ProgressBar>("production-progress");
        }
    }
}