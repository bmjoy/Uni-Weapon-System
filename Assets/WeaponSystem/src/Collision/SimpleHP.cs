using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem.Collision
{
    public class SimpleHP : MonoBehaviour, IHasHitPoint, IObjectGroup
    {
        [SerializeField] private int selfId;
        [SerializeField] private int teamId;

        [SerializeField] private float maxHp;
        [ReadOnly, SerializeField] private float currentHp;
        public UnityEvent onDie;

        public int SelfId => selfId;
        public int GroupId => teamId;
        
        private void OnEnable() => currentHp = maxHp;

        public void AddDamage(float damage)
        {
            if (damage >= currentHp)
            {
                Death();
            }

            currentHp -= damage;
        }

        public void AddRecovery(float hitPoint)
        {
            currentHp += Mathf.Clamp(hitPoint, 0f, maxHp);
        }

        public void Death() => onDie.Invoke();
    }
}