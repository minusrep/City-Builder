using UnityEngine;

namespace Runtime.Colony.Citizens.Animations
{
    public class CitizenAnimatorView : MonoBehaviour
    {
        public Animator Animator => _animator;
        
        [SerializeField] private Animator _animator;
    }
}