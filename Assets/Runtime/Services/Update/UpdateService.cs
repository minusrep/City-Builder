using System;
using UnityEngine;

namespace Runtime.Services.Update
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