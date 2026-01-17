using System;
using UnityEngine;

namespace Runtime.Colony.Citizens.Debugging
{
    public class CitizenDebugView : MonoBehaviour
    {
        public event Action OnGui;

        [SerializeField] private bool _active;

        private void OnGUI()
        {
            if (_active) OnGui?.Invoke();
        }
    }
}