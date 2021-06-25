using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem.Collision
{
    public class SimpleHP : MonoBehaviour, IHasHitPoint, IObjectGroup
    {
        private Guid selfId;
        [SerializeField] private int teamId;

        [SerializeField] private float maxHp;
        [ReadOnly, SerializeField] private float currentHp;
        public UnityEvent onDie;

        public Guid SelfId => selfId;
        public int GroupId => teamId;

        private void Awake() => selfId = Guid.NewGuid();

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