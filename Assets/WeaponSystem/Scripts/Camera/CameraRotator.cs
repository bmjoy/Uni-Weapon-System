using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Runtime;

namespace WeaponSystem.Camera
{
    public class CameraRotator : MonoBehaviour, ICameraRotate
    {
        [SerializeField] private RotateAxis verticalAxis;
        [SerializeField] private RotateAxis verticalOffsetAxis;
        [SerializeField] private RotateAxis horizontalAxis;
        [SerializeField] private RotateAxis horizontalOffsetAxis;
        [SerializeField] private Transform playerBody;
        [SerializeField] private Transform head;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Locator<ICameraRotate>.Instance.Bind(this);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Locator<ICameraRotate>.Instance.Unbind(this);
        }

        private void Update()
        {
            verticalAxis.Current += Locator<ICameraInput>.Instance.Current?.Vertical ?? 0f;
            horizontalAxis.Current += Locator<ICameraInput>.Instance.Current?.Horizontal ?? 0f;
        }

        private void LateUpdate()
        {
            var horizontalRotate = Quaternion.AngleAxis(Horizontal + HorizontalOffset, Vector3.up);

            var verticalRotate = Quaternion.AngleAxis(Vertical + VerticalOffset, Vector3.left);

            if (playerBody == false || playerBody == head)
            {
                head.transform.rotation = horizontalRotate * verticalRotate;
            }
            else
            {
                head.transform.localRotation = verticalRotate;
                playerBody.rotation = horizontalRotate;
            }
        }

        public float Vertical
        {
            get => verticalAxis.Current;
            set => verticalAxis.Current = value;
        }

        public float VerticalOffset
        {
            get => verticalOffsetAxis.Current;
            set => verticalOffsetAxis.Current = value;
        }

        public float Horizontal
        {
            get => horizontalAxis.Current;
            set => horizontalAxis.Current = value;
        }

        public float HorizontalOffset
        {
            get => horizontalOffsetAxis.Current;
            set => horizontalOffsetAxis.Current = value;
        }
    }
}