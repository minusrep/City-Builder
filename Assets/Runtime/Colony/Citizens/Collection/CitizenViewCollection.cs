using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenViewCollection : MonoBehaviour
    {
        public Transform Transform => _transform;

        public CitizenView  CitizenViewPrefab => _citizenViewPrefab;
        
        [SerializeField] private Transform _transform;

        [SerializeField] private CitizenView _citizenViewPrefab;

        public CitizenView InstantiateCitizenView()
        {
            return Instantiate(_citizenViewPrefab, _transform);
        }
    }
}