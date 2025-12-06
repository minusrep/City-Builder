using System;
using UnityEngine;

namespace Runtime.StateMachine
{
    public class UpdateService : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}