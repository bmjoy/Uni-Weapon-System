using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public class CameraRotateHeightSetter : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private new Collider collider;
        private Transform _transform;

        private void Awake() => _transform = transform;

        private void Update()
        {
            switch (collider)
            {
                case BoxCollider b:
                    _transform.localPosition = new Vector3(0f, b.size.y / 2f) + offset;
                    return;

                case CapsuleCollider c:
                    _transform.localPosition = new Vector3(0f, c.height / 2f + c.radius) + offset;
                    return;

                case CharacterController c:
                    _transform.localPosition = new Vector3(0f, c.height / 2f + - c.radius) + offset;
                    return;

                default: return;
            }
        }
    }
}