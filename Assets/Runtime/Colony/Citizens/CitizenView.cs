using Runtime.Colony.Citizens.Movement;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour
    {
        public MovementView MovementView => _movementView;
        
        [SerializeField] private MovementView _movementView;
    }
}