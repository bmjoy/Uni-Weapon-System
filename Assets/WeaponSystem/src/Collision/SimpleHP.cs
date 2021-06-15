using UnityEngine;
using WeaponSystem.Collision;

public class SimpleHP : MonoBehaviour, IHitPoint
{
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    public void AddDamage(float damage)
    {
        if (damage >= currentHp) Death();
        currentHp -= damage;
    }

    public void AddRecovery(float hitPoint)
    {
        currentHp += Mathf.Clamp(hitPoint, 0f, maxHp);
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}