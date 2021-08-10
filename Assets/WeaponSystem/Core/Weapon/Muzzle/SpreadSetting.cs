using UnityEngine;
using WeaponSystem.Core.Movement;

namespace WeaponSystem.Core.Weapon.Muzzle
{
    [CreateAssetMenu(menuName = "WeaponSystem/New Spread Setting")]
    public class SpreadSetting : SpreadSettingBase
    {
        [SerializeField] private Spread[] spreads;

        public override Spread this[PlayerMovementAction action]
        {
            get
            {
                foreach (var spread in spreads)
                {
                    if (spread.Action == action) return spread;
                }

                return Spread.Default;
            }
        }
    }
}