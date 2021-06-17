using System;

namespace WeaponSystem.Collision
{
    [Serializable]
    public struct DamagePermission
    {
        public bool self;
        public bool team;
        public bool enemy;
    }
}