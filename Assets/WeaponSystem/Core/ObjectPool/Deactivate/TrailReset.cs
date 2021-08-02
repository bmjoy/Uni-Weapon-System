using UnityEngine;

namespace WeaponSystem.Core.ObjectPool.Deactivate
{
    public class TrailReset : MonoBehaviour
    {
        private TrailRenderer _renderer;

        private void Awake() => _renderer = GetComponent<TrailRenderer>();

        private void OnDisable() => _renderer.Clear();
    }
}