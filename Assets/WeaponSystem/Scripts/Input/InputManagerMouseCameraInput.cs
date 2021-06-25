using System;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Mathf;

namespace WeaponSystem.Input
{
    [Serializable]
    public class InputManagerMouseCameraInput : ICameraInput
    {
        [SerializeField, Range(0.01f, 100f)] private float sensitivity = 10f;
        [SerializeField] private bool isInvert;
        [SerializeField] private string verticalAxisName = "Mouse Y";
        [SerializeField] private string horizontalAxisName = "Mouse X";


        public float Vertical => GetAxisRaw(verticalAxisName) * sensitivity * PI * Deg2Rad * (isInvert ? -1f : 1f);

        public float Horizontal => GetAxisRaw(horizontalAxisName) * sensitivity * PI * Deg2Rad;
    }
}