using UnityEngine;

namespace WeaponSystem.Core.Camera
{
    public abstract class CameraFixedStarBase : MonoBehaviour, ICameraFixedStar
    {
        public abstract float Vertical { get; set; }
        public abstract float VerticalOffset { get; set; }
        public abstract float Horizontal { get; set; }
        public abstract float HorizontalOffset { get; set; }
    }
}