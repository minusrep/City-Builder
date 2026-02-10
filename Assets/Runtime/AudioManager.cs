using Runtime.Colony.Achievements.Events;
using UnityEngine;

namespace Runtime
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _achievementAudioClip;

        private void OnEnable()
        {
            MessageBroker.Instance.Subscribe("achievement_complete", PlayAchievementComplete);
        }

        private void OnDisable()
        {
            MessageBroker.Instance.Unsubscribe("achievement_complete", PlayAchievementComplete);
        }

        private void PlayAchievementComplete(GameEvent gameEvent)
        {
            var go = new GameObject("OneShotAudio");
            var audioSource = go.AddComponent<AudioSource>();

            audioSource.clip = _achievementAudioClip;
            audioSource.Play();

            Destroy(go, _achievementAudioClip.length);
        }
    }
}