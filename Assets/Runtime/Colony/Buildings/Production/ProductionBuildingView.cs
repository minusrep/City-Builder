using Runtime.Colony.Buildings.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingView : BuildingView
    {
        [SerializeField] private UIDocument _document;

        public VisualTreeAsset ProgressBarAsset;
        public VisualElement Root { get; private set; }
    }
}