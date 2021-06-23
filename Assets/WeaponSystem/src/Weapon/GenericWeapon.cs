using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.AttackAction;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon
{
    [AddComponentMenu("WeaponSystem/GenericWeapon"), DisallowMultipleComponent]
    public class GenericWeapon : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IAttackAction _attackAction = new NoneAttackAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAltAttackAction _altAttackAction = new NoneAltAttackAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IMagazine _magazine = new NoneMagazine();

        [Space(20)] [SerializeField] private Animator weaponAnimator;
        private IPlayerContext _context;

        private void Awake()
        {
            _context = Locator<IPlayerContext>.Instance.Current;
            _altAttackAction?.Injection(transform, weaponAnimator, _magazine);
            _attackAction?.Injection(transform, weaponAnimator, _magazine);
        }

        private void Update()
        {
            var input = Locator<IWeaponInput>.Instance.Current;
            _context = Locator<IPlayerContext>.Instance.Current;
            if (input?.IsReload ?? false) CoroutineHandler.Instance.CoroutineStart(_magazine.Reload());

            _attackAction?.Action(input?.IsAttack ?? false, _context);
            _altAttackAction?.Action(input?.IsAltAttack ?? false, _context);
        }
    }
}