using System;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Mathf;

namespace WeaponSystem.Core.Input.OldInput
{
    [Serializable, AddTypeMenu("GamePad")]
    public class StickCameraInput : ICameraInput
    {
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] private AnimationCurve inputCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private bool isInvert;
        [SerializeField] private string verticalAxisName = "Camera Vertical";
        [SerializeField] private string horizontalAxisName = "Camera Horizontal";

        public float Vertical
        {
            get
            {
                var vertical = GetAxisRaw(verticalAxisName);
                vertical *= inputCurve.Evaluate(Abs(vertical));
                vertical *= sensitivity * Rad2Deg * Time.deltaTime;
                return vertical * (isInvert ? -1f : 1f);
            }
        }

        public float Horizontal
        {
            get
            {
                var horizontal = GetAxisRaw(horizontalAxisName);
                horizontal *= inputCurve.Evaluate(Abs(horizontal));
                horizontal *= sensitivity * Rad2Deg * Time.deltaTime;
                return horizontal;
            }
        }
    }
}