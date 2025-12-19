using Runtime.Colony.Citizens.Movement;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenView : MonoBehaviour
    {
        public CitizenMovementView CitizenMovementView => _citizenMovementView;
        
        [SerializeField] private CitizenMovementView _citizenMovementView;
    }
}