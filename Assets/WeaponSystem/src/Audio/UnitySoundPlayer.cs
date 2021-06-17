using System;
using System.Linq;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class UnitySoundPlayer : MonoBehaviour, ISoundPlayer
    {
        [Serializable]
        public class UnityAudioKeyValuePair : GenericKeyValuePair<string, AudioClip> { }
        
        [SerializeField] private UnityAudioKeyValuePair[] audioClips;

        private AudioSource _audioSrc;
        private void Awake() => _audioSrc = GetComponent<AudioSource>();

        public void Play(string soundName)
        {
            var clip = audioClips.FirstOrDefault(s => s.Key == soundName);
            if (clip == null) return;
            // Stop();
            _audioSrc.PlayOneShot(clip.Value);
        }

        public void PlayAndStop(string soundName)
        {
            if (_audioSrc.isPlaying)
            {
                Stop();
            }
            else
            {
                Play(soundName);
            }
        }

        public void Stop() => _audioSrc.Stop();

        public float Pitch
        {
            get => _audioSrc.pitch;
            set => _audioSrc.pitch = value;
        }

        public float Volume
        {
            get => _audioSrc.pitch;
            set => _audioSrc.volume = Mathf.Clamp01(value);
        }

        public bool Mute
        {
            get => _audioSrc.mute;
            set => _audioSrc.mute = value;
        }

        public bool Loop
        {
            get => _audioSrc.loop;
            set => _audioSrc.loop = value;
        }
    }
}