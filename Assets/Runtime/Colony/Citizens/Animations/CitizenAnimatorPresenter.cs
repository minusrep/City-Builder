using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Citizens.Animations
{
    public class CitizenAnimatorPresenter : IPresenter
    {
        private readonly CitizenAnimatorView _view;
        
        private readonly CitizenModel _model;

        public CitizenAnimatorPresenter(CitizenAnimatorView view, CitizenModel model)
        {
            _view = view;
            
            _model = model;
        }

        public void Enable()
        {
            _model.OnInvokeAnimation += OnInvokeAnimation;
        }

        public void Disable()
        {
            _model.OnInvokeAnimation -= OnInvokeAnimation;
        }

        private void OnInvokeAnimation(string animation)
        {
            Debug.Log(animation);
            _view.Animator.Play(animation);
        }
    }
}