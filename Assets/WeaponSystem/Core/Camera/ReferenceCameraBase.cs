using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public abstract class ReferenceCameraBase : MonoBehaviour
    {
        public static float FieldOfView
        {
            get => _fieldOfView;
            set => _fieldOfView = Mathf.Clamp(Mathf.Abs(value), 0.01f, 170f);
        }

        private static float _fieldOfView = 60f;

        public abstract float FovMultiple { get; set; }

        public abstract UnityEngine.Camera Camera { get; }

        public abstract Transform Center { get; }
    }
}