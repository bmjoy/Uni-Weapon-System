namespace WeaponSystem.Weapon.Muzzle
{
    
    public interface IAmmoHolder
    {
        int Remaining { get; set; }
        int GetAmmo(int amount);
    }
}