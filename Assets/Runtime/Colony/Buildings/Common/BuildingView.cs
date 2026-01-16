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
        public SelectionOutline Outline => _outline;

        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private SelectionOutline _outline;


        public void OnEnable()
        {
            _outline.SetOutline(SelectionOutlineType.None);
            
            Transform = transform;
            GameObject =  gameObject;
            
            if (_uiDocument)
            {
                var root = _uiDocument.rootVisualElement;
                ProgressBar = root.Q<ProgressBar>("production-progress");
            }
        }
    }
}