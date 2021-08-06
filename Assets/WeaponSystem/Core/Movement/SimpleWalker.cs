using UnityEngine;

namespace WeaponSystem.Core.Movement
{
    public class SimpleWalker : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;

        [Header("Rotation & Direction")] [SerializeField]
        private Vector3 referenceUp = Vector3.up;

        [SerializeField] private bool clampDirection;
        [SerializeField] private Transform referenceRotate;

        [Header("Speed")] [SerializeField] private float speed = 5f;

        [Header("Height")] [SerializeField] private float standHeight = 1.7f;
        [SerializeField] private float crouchHeight = 1f;
        [SerializeField] private float crouchingSpeed = .1f;
        [SerializeField] private bool isCrouch;

        [Header("Ground Check & Gravity")] [SerializeField]
        private LayerMask groundCollisionMask = Physics.AllLayers;

        [SerializeField] private float groundOffset = .1f;
        [SerializeField] private Vector3 gravity = Physics.gravity;
        [SerializeField] private bool useGravity = true;
        [SerializeField] private float jumpHeight = 3f;

        public Vector3 CurrentGravity { get; private set; }

        
        // Input property
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                var normal = ReferenceUp * (clampDirection ? 1f : 0f);
                var vector = referenceRotate.rotation * value.normalized;
                _direction = Vector3.ProjectOnPlane(vector, normal).normalized;
            }
        }

        public float Speed
        {
            get => speed;
            set => speed = Mathf.Abs(value);
        }

        private Vector3 _direction;

        public bool IsCrouch
        {
            get => isCrouch;
            set => isCrouch = value;
        }

        public bool IsJump { get; set; }

        // property
        public Vector3 ReferenceUp
        {
            get => referenceUp;
            set => referenceUp = value.normalized;
        }

        public float StandHeight
        {
            get => standHeight;
            set => standHeight = Mathf.Abs(value);
        }

        public float CrouchHeight
        {
            get => crouchHeight;
            set => crouchHeight = Mathf.Abs(value);
        }

        private Vector3 Center => transform.TransformPoint(controller.center);

        public Vector3 CheckPosition =>
            Center + Vector3.down * (controller.height / 2f + (groundOffset - controller.radius));

        public bool IsGrounded =>
            Physics.CheckSphere(CheckPosition, controller.radius, groundCollisionMask, QueryTriggerInteraction.Ignore);

        private void FixedUpdate()
        {
            var height = IsCrouch ? CrouchHeight : StandHeight;
            controller.height = Mathf.Lerp(controller.height, height, Time.deltaTime / crouchingSpeed);
            Move();
            FallOrJump();
        }

        private void OnDrawGizmos()
        {
            if (controller == null)
            {
                if (TryGetComponent(out controller) == false) return;
            }

            Gizmos.DrawWireSphere(CheckPosition, controller.radius);
        }

        private void Move() => controller.Move(Direction * (Speed * Time.deltaTime));

        private void FallOrJump()
        {
            if (IsGrounded || useGravity == false)
            {
                if (IsJump) CurrentGravity = -gravity * (jumpHeight * 2f);
                else CurrentGravity = Vector3.zero;
            }
 
            CurrentGravity += gravity;
            controller.Move(CurrentGravity * (Time.deltaTime * Time.deltaTime));
        }
    }
}