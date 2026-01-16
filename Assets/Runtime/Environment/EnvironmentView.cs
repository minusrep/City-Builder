using UnityEngine;

namespace Runtime.Environment
{
    public class EnvironmentView : MonoBehaviour
    {
        public Light DirectionalLight => _directionalLight;

        public float NightLightIntensity => _nightLightIntensity;
        
        public float DayLightIntensity => _dayLightIntensity;
        
        [SerializeField] private Light _directionalLight;

        [SerializeField] private float _nightLightIntensity;

        [SerializeField] private float _dayLightIntensity;
    }
}