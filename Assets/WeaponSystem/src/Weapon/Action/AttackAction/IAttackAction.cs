namespace WeaponSystem.Weapon.Action.AttackAction
{
    public interface IAttackAction : IWeaponAction
    {
        void Action(bool isAction);
    }
}