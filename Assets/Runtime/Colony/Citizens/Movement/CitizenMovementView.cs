using System;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Colony.Citizens.Movement
{
    public class CitizenMovementView : MonoBehaviour
    {
        public event Action OnUpdate;
        
        public Transform Transform => _transform;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        [SerializeField] private Transform _transform;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private void Update()
        {
            // TODO: REMOVE THIS
            
            OnUpdate?.Invoke();
        }
    }
}