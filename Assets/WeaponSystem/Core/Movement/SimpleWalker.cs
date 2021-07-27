using UnityEngine;
using WeaponSystem.Core.Input;
using WeaponSystem.Core.Runtime;
using static UnityEngine.Physics;
using static UnityEngine.QueryTriggerInteraction;


namespace WeaponSystem.Core.Movement
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerContext))]
    public class SimpleWalker : MonoBehaviour
    {
        // RequireComponent
        [Header("Require Component")] [SerializeField]
        private CharacterController controller;

        [SerializeField] private PlayerContext context;


        // rotation && direction
        [Header("Rotation & Direction")] [SerializeField]
        private Vector3 referenceUp = -gravity.normalized;

        [SerializeField] private Transform rotateReference;
        [SerializeField] private Transform[] characterBodies;

        // height
        [Header("Height")] [SerializeField] private float standHeight = 1.6f;
        [SerializeField] private float crouchHeight = .7f;
        [SerializeField] private float crouchDuration = .1f;

        // speed
        [Header("Speed")] [SerializeField] private float walkSpeed = 7f;
        [SerializeField] private float sprintSpeed = 10f;
        [SerializeField] private float crouchSpeed = 2.5f;
        [SerializeField] private float aimSpeed = 5f;
        [SerializeField] private float jumpPower;

        // ground check
        [Header("Ground Check")] [SerializeField]
        private LayerMask layerMask = AllLayers;

        [SerializeField] private float groundOffset = .1f;

        public Vector3 ReferenceUp
        {
            get => referenceUp;
            set => referenceUp = value.normalized;
        }


        public bool Grounded => CheckSphere(FootPosition, controller.radius, layerMask, Ignore);

        private Vector3 FootPosition =>
            new Vector3(0f, -controller.height / 2f + controller.radius - groundOffset) + transform.position;


        private float CurrentSpeed
        {
            get
            {
                var input = Locator<IMovementInput>.Instance.Current;
                if (input?.IsCrouch ?? false) return crouchSpeed;
                if (context != null && context.IsAiming) return aimSpeed;
                if (input?.IsSprint ?? false) return sprintSpeed;
                return walkSpeed;
            }
        }


        private void FixedUpdate()
        {
            var input = Locator<IMovementInput>.Instance.Current;
            var grounded = Grounded;


            // movement
            var horizontal = input?.Horizontal ?? 0f;
            var vertical = input?.Vertical ?? 0f;
            ContextUpdate(input, grounded);
            Move(new Vector3(horizontal, 0f, vertical), CurrentSpeed);
            JumpOrFall(true, false);
            // crouch or standing

            var height = input?.IsCrouch ?? false ? crouchHeight : standHeight;
            controller.height = Mathf.Lerp(controller.height, height, Time.deltaTime / crouchDuration);
        }

        void ContextUpdate(IMovementInput input, bool grounded)
        {
            context.IsGrounded = grounded;
            context.IsCrouch = input?.IsCrouch ?? false;
        }

        private void Update()
        {
            var characterForward = Vector3.ProjectOnPlane(rotateReference.forward, ReferenceUp);
            foreach (var characterBody in characterBodies)
            {
                characterBody.forward = characterForward;
            }
        }

        void Move(Vector3 direction, float speed)
        {
            direction = Vector3.ProjectOnPlane(rotateReference.rotation * direction.normalized, -ReferenceUp);
            direction.Normalize();
            controller.Move(direction * (speed * Time.deltaTime));
        }

        void JumpOrFall(bool isJump, bool isGrounded)
        {
            controller.Move(gravity * Time.deltaTime);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(FootPosition, controller.radius);
        }
    }
}