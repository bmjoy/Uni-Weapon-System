
namespace WeaponSystem.Core.Weapon.Recoil
{
    public interface IRecoil
    {
        void Reset();
        void Generate();
        void Easing();
    }
}
