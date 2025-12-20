using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Colony.Citizens.Debugging
{
    public class CitizenDebugView : MonoBehaviour
    {
        public event Action OnGui;

        [SerializeField] private bool _active = false;

        private void OnGUI()
        {
            if (_active) OnGui?.Invoke();
        }
    }
}