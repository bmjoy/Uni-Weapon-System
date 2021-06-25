using System;
using WeaponSystem.Camera;
using WeaponSystem.Runtime;

namespace WeaponSystem.Weapon.Recoil
{
    [Serializable, AddTypeMenu("None")]
    public class NoneRecoil : IRecoil
    {
        void IRecoil.Reset()
        {
            Locator<ICameraRotate>.Instance.Current.HorizontalOffset = 0f;
            Locator<ICameraRotate>.Instance.Current.VerticalOffset = 0f;
        }

        void IRecoil.Generate() { }

        public void Easing() { }
    }
}