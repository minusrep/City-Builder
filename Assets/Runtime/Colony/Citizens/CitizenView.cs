using System;
using Runtime.Colony.Citizens.Animations;
using Runtime.Colony.Citizens.Debugging;
using Runtime.Colony.Citizens.Movement;
using UnityEngine;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour
    {
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        public CitizenAnimatorView CitizenAnimatorView => _citizenAnimatorView;
        
        public CitizenDebugView CitizenDebugView => _citizenDebugView;

        [SerializeField] private CitizenMovementView _citizenMovementView;

        [SerializeField] private CitizenAnimatorView _citizenAnimatorView;
        
        [SerializeField] private CitizenDebugView _citizenDebugView;
    }
}