using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Core
{
    public class MovementView : MonoBehaviour
    {
        public Transform Transform => _transform;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        [SerializeField] private Transform _transform;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
    }
}