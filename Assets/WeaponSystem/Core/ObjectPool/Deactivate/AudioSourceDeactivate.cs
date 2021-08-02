﻿using UnityEngine;

namespace WeaponSystem.Core.ObjectPool.Deactivate
{
    public class AudioSourceDeactivate : MonoBehaviour
    {
        private AudioSource _src;

        private void Awake() => _src = GetComponent<AudioSource>();

        private void Update()
        {
            if (_src.isPlaying) return;
            gameObject.SetActive(false);
        }
    }
}