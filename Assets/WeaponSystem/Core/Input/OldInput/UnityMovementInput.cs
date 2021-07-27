using System;
using UnityEngine;

namespace WeaponSystem.Core.Input.OldInput
{
    [Serializable]
    public class UnityMovementInput : IMovementInput
    {
        [SerializeField] private KeyCode[] rightKeys = {KeyCode.D};
        [SerializeField] private KeyCode[] leftKeys = {KeyCode.A};
        [SerializeField] private KeyCode[] forwardKeys = {KeyCode.W};
        [SerializeField] private KeyCode[] backwardKeys = {KeyCode.S};
        [SerializeField] private KeyCode[] sprintKeys = {KeyCode.LeftShift};
        [SerializeField] private KeyCode[] jumpKeys = {KeyCode.Space};
        [SerializeField] private KeyCode[] crouchKeys = {KeyCode.LeftControl};

        public float Vertical
        {
            get
            {
                if (forwardKeys.IsAnyKeyPressed()) return 1f;
                if (backwardKeys.IsAnyKeyPressed()) return -1f;

                return 0f;
            }
        }

        public float Horizontal
        {
            get
            {
                if (rightKeys.IsAnyKeyPressed()) return 1f;
                if (leftKeys.IsAnyKeyPressed()) return -1f;

                return 0f;
            }
        }

        public bool IsSprint => sprintKeys.IsAnyKeyPressed();

        public bool IsJump => jumpKeys.IsAnyKeyPressed();
        public bool IsCrouch => crouchKeys.IsAnyKeyPressed();
    }
}