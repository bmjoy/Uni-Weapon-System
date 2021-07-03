using UnityEngine;

namespace Audio
{
    public class MockSoundPlayer : MonoBehaviour, ISoundPlayer
    {
        public void Play() { }
        public void PlayAndStop() { }
        public void Stop() { }

        public float Pitch { get; set; }
        public float Volume { get; set; }
        public bool Mute { get; set; }
        public bool Loop { get; set; }
    }
}