using System;
using Runtime.Colony.Citizens.Movement;
using UnityEngine;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour
    {
        public event Action OnGui;
        
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        
        [SerializeField] private CitizenMovementView _citizenMovementView;

        private void OnGUI()
        {
            OnGui?.Invoke();
        }
    }
}