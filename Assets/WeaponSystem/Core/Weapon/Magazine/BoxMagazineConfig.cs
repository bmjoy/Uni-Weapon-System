using UnityEngine;

namespace WeaponSystem.Core.Weapon.Magazine
{
    [CreateAssetMenu(menuName = "WeaponSystem")]
    public class BoxMagazineConfig : ScriptableObject
    {
        [SerializeField] private float reloadTime = .5f;
        [SerializeField] private float tacticalReloadTime = .3f;
        [SerializeField] private uint maxAmount = 30;
    }
}