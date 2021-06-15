using System.Collections;

namespace WeaponSystem.Weapon.Magazine
{
    public interface IMagazine
    {
        public int Current { get; }
        public bool UseAmmo(int useAmount);
        public bool IsReloading { get; }
        public IEnumerator Reload();
    }
}