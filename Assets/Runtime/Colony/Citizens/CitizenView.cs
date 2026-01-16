using Runtime.Colony.Citizens.Animations;
using Runtime.Colony.Citizens.Debugging;
using Runtime.Colony.Citizens.HUD;
using Runtime.Colony.Citizens.Movement;
using Runtime.Selection;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour, ISelectableUnit
    {
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        public CitizenAnimatorView CitizenAnimatorView => _citizenAnimatorView;
        public CitizenDebugView CitizenDebugView => _citizenDebugView;
        
        public CitizenHUDView CitizenHUDView => _citizenHUDView;
        
        public string Id { get; set; }

        public SelectionOutline Outline => _outline;

        public Transform RenderPoint => _rendererPoint;
        
        [SerializeField] private CitizenMovementView _citizenMovementView;

        [SerializeField] private CitizenAnimatorView _citizenAnimatorView;
        
        [SerializeField] private CitizenDebugView _citizenDebugView;
        
        [SerializeField] private CitizenHUDView _citizenHUDView;
        
        [SerializeField] private SelectionOutline _outline;

        [SerializeField] private Transform _rendererPoint;
    }
}