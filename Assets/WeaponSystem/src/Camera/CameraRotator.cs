using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Runtime;

namespace WeaponSystem.Camera
{
    public class CameraRotator : MonoBehaviour, ICameraRotate
    {
        [SerializeField] private RotateAxis verticalAxis;
        [SerializeField] private RotateAxis horizontalAxis;
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
            if (playerBody == false || playerBody == head)
            {
                head.transform.rotation = horizontalAxis[Vector3.up] * verticalAxis[Vector3.left];
            }
            else
            {
                head.transform.localRotation = verticalAxis[Vector3.left];
                playerBody.rotation = horizontalAxis[Vector3.up];
            }
        }

        public float Vertical
        {
            get => verticalAxis.Current;
            set => verticalAxis.Current = value;
        }

        public float Horizontal
        {
            get => horizontalAxis.Current;
            set => horizontalAxis.Current = value;
        }
    }
}