using Runtime.Movement;
using UnityEngine;

namespace Runtime.Descriptions.Citizens
{
    public class CitizenView : MonoBehaviour
    {
        public MovementView MovementView => _movementView;
        
        [SerializeField] private MovementView _movementView;
    }
}