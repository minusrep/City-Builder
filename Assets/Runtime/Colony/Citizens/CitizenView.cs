using Runtime.Colony.Citizens.Animations;
using Runtime.Colony.Citizens.Debugging;
using Runtime.Colony.Citizens.Movement;
using Runtime.Common;
using Runtime.Selection;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Runtime.Colony.Citizens
{
    public class CitizenHUDPresenter : IPresenter
    {
        private const string CitizenNameStyle = "citizen-name";
        
        private readonly CitizenHUDView _view;
        
        private readonly CitizenModel _model;

        public CitizenHUDPresenter(CitizenHUDView view, CitizenModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void Enable()
        {
            var nameTextElement = new TextElement()
            {
                text = _model.Name,
            };

            nameTextElement.AddToClassList(CitizenNameStyle);
            
            _view.Root.Add(nameTextElement);
        }

        public void Disable()
        {
            _view.Root.Clear();
        }
    }

    public class CitizenView : MonoBehaviour, ISelectableUnit
    {
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        public CitizenAnimatorView CitizenAnimatorView => _citizenAnimatorView;
        public CitizenDebugView CitizenDebugView => _citizenDebugView;
        
        public CitizenHUDView CitizenHUDView => _citizenHUDView;
        
        public string Id { get; set; }

        public SelectionOutline Outline => _outline;
        
        [SerializeField] private CitizenMovementView _citizenMovementView;

        [SerializeField] private CitizenAnimatorView _citizenAnimatorView;
        
        [SerializeField] private CitizenDebugView _citizenDebugView;
        
        [SerializeField] private CitizenHUDView _citizenHUDView;
        
        [SerializeField] private SelectionOutline _outline;

    }
}