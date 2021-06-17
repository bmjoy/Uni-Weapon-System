using UnityEngine;
using WeaponSystem.Collision;

namespace WeaponSystem
{
    public class TestGroup : MonoBehaviour
    {
        public DamagePermission d1;
        public DamageCollision self;
        public DamageCollision target;

        
        private void Update()
        {
            if (self.PlayerId == target.PlayerId && d1.self)
            {
                Debug.Log($"self damage");
                return;
            }

            if (self.TeamId == target.TeamId && d1.team)
            {
                Debug.Log("team damage");
                return;
            }

            if (self.TeamId != target.TeamId && d1.enemy)
            {
                Debug.Log("enemy damage");
                return;
            }
        }
    }
}