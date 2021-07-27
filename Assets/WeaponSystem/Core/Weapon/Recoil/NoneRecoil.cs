using System;

namespace WeaponSystem.Core.Weapon.Recoil
{
    [Serializable, AddTypeMenu("None")]
    public class NoneRecoil : IRecoil
    {
        void IRecoil.Reset() { }

        void IRecoil.Generate() { }

        public void Easing() { }
    }
}