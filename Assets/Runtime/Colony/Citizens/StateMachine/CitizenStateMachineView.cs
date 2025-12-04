using System;
using UnityEngine;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateMachineView : MonoBehaviour
    {
        public event Action OnUpdate;

        [SerializeField] public CharacterController CharacterController;
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}