using System;

namespace WeaponSystem.Input
{
    [Serializable, AddTypeMenu("Gyro")]
    public class GyroCameraInput : ICameraInput
    {
        public float Vertical
        {
            get
            {
                return 0f;
            }
        }

        public float Horizontal
        {
            get
            {
                return 0f;
            }
        }
    }
}