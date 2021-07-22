namespace WeaponSystem.Weapon.Muzzle
{
    
    public interface IAmmoHolder
    {
        bool IsEmpty { get; }
        int Remaining { get; set; }
        int GetAmmo(int amount);
    }
}