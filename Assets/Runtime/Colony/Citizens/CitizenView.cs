using Runtime.Colony.Buildings.Selection;
using Runtime.Colony.Citizens.Animations;
using Runtime.Colony.Citizens.Debugging;
using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.Stats.Collections;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour, ISelectableUnit
    {
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        public CitizenAnimatorView CitizenAnimatorView => _citizenAnimatorView;
        public CitizenDebugView CitizenDebugView => _citizenDebugView;
        
        public string Id { get; set; }

        public SelectionOutline Outline => _outline;
        
        public StatViewCollection StatViewCollection => _statViewCollection;

        [SerializeField] private CitizenMovementView _citizenMovementView;

        [SerializeField] private CitizenAnimatorView _citizenAnimatorView;
        
        [SerializeField] private CitizenDebugView _citizenDebugView;
        
        [SerializeField] private StatViewCollection _statViewCollection;

        [SerializeField] private SelectionOutline _outline;

    }
}