using System.Collections.Generic;
using Runtime.Selection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingView : MonoBehaviour, ISelectableUnit
    {
        public string Id { get; set; }
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public ProgressBar ProgressBar { get; private set; }
        public UIDocument Document => _uiDocument;
        public IReadOnlyList<Renderer> Renderers => _renderers;
        public BuildingFootprintView Footprint => _footprint;
        public SelectionOutline Outline => _outline;

        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private SelectionOutline _outline;
        [SerializeField] private BuildingFootprintView _footprint;

        private BuildingViewState _state;

        public void OnEnable()
        {
            _outline.SetOutline(SelectionOutlineType.None);

            Transform = transform;
            GameObject = gameObject;

            if (_uiDocument)
            {
                var root = _uiDocument.rootVisualElement;
                ProgressBar = root.Q<ProgressBar>("production-progress");
            }
        }

        public void SetState(BuildingViewState state)
        {
            if (_state == state)
                return;

            _state = state;

            switch (state)
            {
                case BuildingViewState.Placed:
                    ApplyPlacedState();
                    break;

                case BuildingViewState.Preview:
                    ApplyPreviewState();
                    break;

                case BuildingViewState.Construction:
                    ApplyConstructionState();
                    break;
                
                default:
                    ApplyPlacedState();
                    break;
            }
        }

        private void ApplyPlacedState()
        {
            if (_uiDocument)
                _uiDocument.enabled = true;

            _footprint.GameObject.SetActive(false);

            _outline.SetOutline(SelectionOutlineType.None);
        }

        private void ApplyPreviewState()
        {
            if (_uiDocument)
                _uiDocument.enabled = false;

            _footprint.GameObject.SetActive(true);
            _footprint.Renderer.material.color = Color.green;

            _outline.SetOutline(SelectionOutlineType.None);
        }

        private void ApplyConstructionState()
        {
            if (_uiDocument)
                _uiDocument.enabled = false;

            _footprint.GameObject.SetActive(true);

            _outline.SetOutline(SelectionOutlineType.Selected);
        }
    }
}