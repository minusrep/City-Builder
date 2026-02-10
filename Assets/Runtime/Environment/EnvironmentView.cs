using UnityEngine;

namespace Runtime.Environment
{
    public class EnvironmentView : MonoBehaviour
    {
        public Light DirectionalLight => _directionalLight;
        public Transform DirectionalLightTransform => _directionalLightTransform;
        
        public float DayLightIntensity => _dayLightIntensity;
        
        public float MaxRotationX => _maxRotationX;

        [SerializeField] private Light _directionalLight;
        
        [SerializeField] private Transform _directionalLightTransform;

        [SerializeField] private float _dayLightIntensity;

        [SerializeField] private float _maxRotationX;
    }
}