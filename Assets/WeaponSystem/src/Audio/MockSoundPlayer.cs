using UnityEngine;


namespace Audio
{
    public class MockSoundPlayer : MonoBehaviour, ISoundPlayer
    {
        private bool _isPlaying;

        public void Play(string soundName)
        {
            _isPlaying = true;

#if DEBUG
            Debug.Log($"{soundName} play");
#endif
        }

        public void PlayAndStop(string soundName)
        {
            if (_isPlaying)
            {
                Stop();
            }
            else
            {
                Play(soundName);
            }
        }

        public void Stop()
        {
            _isPlaying = false;

#if DEBUG
            Debug.Log("Stop");
#endif
        }

        public float Pitch { get; set; }
        public float Volume { get; set; }
        public bool Mute { get; set; }
        public bool Loop { get; set; }
    }
}