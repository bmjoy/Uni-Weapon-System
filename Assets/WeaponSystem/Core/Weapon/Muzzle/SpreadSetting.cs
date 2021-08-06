using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    [CreateAssetMenu(menuName = "WeaponSystem/New Spread Setting")]
    public class SpreadSetting : SpreadSettingBase
    {
        [SerializeField] private Spread[] spreads;

        public override Spread this[PlayerMovementState state]
        {
            get
            {
                foreach (var spread in spreads)
                {
                    if (spread.State == state) return spread;
                }

                return Spread.Default;
            }
        }
    }
}