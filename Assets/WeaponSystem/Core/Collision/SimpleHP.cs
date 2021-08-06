using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem.Core.Collision
{
    public class SimpleHP : MonoBehaviour, IHasHitPoint
    {
        [SerializeField] private float maxHp;
        [ReadOnly, SerializeField] private float currentHp;

        public UnityEvent<float, float> onTakeDamage;
        public UnityEvent onDie;

        private void OnEnable() => currentHp = maxHp;

        public void AddDamage(float damage)
        {
            if (damage >= currentHp)
            {
                Death();
                currentHp = 0f;
                return;
            }

            currentHp -= damage;
            onTakeDamage.Invoke(damage, currentHp);
        }

        public void AddRecovery(float hitPoint) => currentHp += Mathf.Clamp(hitPoint, 0f, maxHp);

        public void Death() => onDie.Invoke();
    }
}