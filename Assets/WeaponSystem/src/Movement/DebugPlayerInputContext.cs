using UnityEngine;
using WeaponSystem;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;

public class DebugPlayerInputContext : MonoBehaviour, IPlayerContext
{
    [SerializeField] private PlayerMovementState state;
    public PlayerMovementState State => state;
    public bool isAiming;

    public bool IsAiming
    {
        get => isAiming;
        set => isAiming = value;
    }

    private void Start() => Locator<IPlayerContext>.Instance.Bind(this);
}