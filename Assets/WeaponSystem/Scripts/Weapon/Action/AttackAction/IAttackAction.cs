using WeaponSystem.Scripts.Movement;

namespace WeaponSystem.Weapon.Action.AttackAction
{
    public interface IAttackAction : IWeaponAction
    {
        void Action(bool isAction, IPlayerContext context);
    }
}