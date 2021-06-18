using UnityEngine;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;

namespace WeaponSystem.src.Camera
{
    public class CameraRotator : MonoBehaviour, ICameraRotate
    {
        private Transform _transform;

        public float Vertical
        {
            get => _transform.rotation.eulerAngles.x;
            set
            {
                var angle = Mathf.Clamp(value, -90f, 90f);
                _transform.rotation *= Quaternion.AngleAxis(angle, Vector3.left);
            }
        }

        public float Horizontal
        {
            get => _transform.rotation.eulerAngles.y;
            set
            {
                var angle = Mathf.Clamp(value, -90f, 90f);
                _transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            }
        }

        private void Awake() => _transform = transform;
        private void OnEnable() => Locator<ICameraRotate>.Instance.Bind(this);
        private void OnDisable() => Locator<ICameraRotate>.Instance.Unbind(this);
    }
}