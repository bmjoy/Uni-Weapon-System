using WeaponSystem.Scripts.Movement;

namespace WeaponSystem.Weapon.Action.AltAttackAction
{
    public interface IAltAttackAction : IWeaponAction
    {
        void Action(bool isAction, IPlayerContext context);
    }
}