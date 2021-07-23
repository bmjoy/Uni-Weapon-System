using UnityEngine;
using WeaponSystem.Scripts.Movement;

namespace WeaponSystem.Weapon.Muzzle
{
    [CreateAssetMenu]
    public class SpreadSetting : ScriptableObject
    {
        [SerializeField] private Spread[] spreads;

        public Spread this[PlayerMovementState state]
        {
            get
            {
                foreach (var spread in spreads)
                {
                    if (spread.state == state) return spread;
                }

                return Spread.Default;
            }
        }
    }
}