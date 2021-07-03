using System;
using UnityEngine;

namespace WeaponSystem.Effect
{
    public class ShurikenDisableController : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private void Awake() => _particleSystem = GetComponent<ParticleSystem>();

        private void Update()
        {
            if (_particleSystem.isPlaying == false) gameObject.SetActive(false);
        }
    }
}