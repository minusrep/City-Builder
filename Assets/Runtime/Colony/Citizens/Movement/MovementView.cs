using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Colony.Citizens.Movement
{
    public class MovementView : MonoBehaviour
    {
        public Transform Transform => _transform;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        [SerializeField] private Transform _transform;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
    }
}