using UnityEngine;

namespace WeaponSystem.Collision
{
    public class ObjectPermission : MonoBehaviour, IObjectPermission
    {
        [SerializeField] private bool selfInteract;
        [SerializeField] private bool selfDamage;
        [SerializeField] private bool selfOwned;

        [SerializeField] private bool teamInteract;
        [SerializeField] private bool teamDamage;
        [SerializeField] private bool teamOwned;

        [SerializeField] private bool enemyInteract;
        [SerializeField] private bool enemyDamage;
        [SerializeField] private bool enemyOwned;

        public bool SelfInteract => selfInteract;
        public bool SelfDamage => selfDamage;
        public bool SelfOwned => selfOwned;

        public bool TeamInteract => teamInteract;
        public bool TeamDamage => teamDamage;
        public bool TeamOwned => teamOwned;

        public bool EnemyInteract => enemyInteract;
        public bool EnemyDamage => enemyDamage;
        public bool EnemyOwned => enemyOwned;
    }
}