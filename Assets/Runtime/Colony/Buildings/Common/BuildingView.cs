using Runtime.Colony.Buildings.Selection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingView : MonoBehaviour, ISelectableUnit
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public ProgressBar ProgressBar { get; private set; }
        public UIDocument Document => _uiDocument;
        public BuildingPreview Preview => _preview;

        public string Id { get; set; }

        public SelectionOutline Outline => _outline;

        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private BuildingPreview _preview;

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