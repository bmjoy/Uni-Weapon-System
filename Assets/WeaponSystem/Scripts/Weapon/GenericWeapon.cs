using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Scripts.Weapon.Magazine;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.AttackAction;
using WeaponSystem.Weapon.Magazine;
using WeaponSystem.Weapon.Muzzle;

namespace WeaponSystem.Weapon
{
    [AddComponentMenu("WeaponSystem/GenericWeapon"), DisallowMultipleComponent]
    public class GenericWeapon : MonoBehaviour
    {
        [SerializeField] private Animator weaponAnimator;
        [SerializeReference, SubclassSelector] private IAttackAction _attackAction = new NoneAttackAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAltAttackAction _altAttackAction = new NoneAltAttackAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IMagazine _magazine = new BoxMagazine();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAmmoHolder _ammoHolder = new UnlimitedAmmoHolder();


        private IPlayerContext _context;

        private void Awake()
        {
            _context = Locator<IPlayerContext>.Instance.Current;
            _altAttackAction?.Injection(transform, weaponAnimator, _magazine);
            _attackAction?.Injection(transform, weaponAnimator, _magazine);
            _magazine?.Injection(weaponAnimator);
        }

        private void Update()
        {
            var input = Locator<IWeaponInput>.Instance.Current;
            _context = Locator<IPlayerContext>.Instance.Current;
            if (_magazine.IsReloading == false && (input?.IsReload ?? false)) StartCoroutine(_magazine.Reload());
            _magazine.AmmoHolder = _ammoHolder;
            _attackAction?.Action(input?.IsAttack ?? false, _context);
            _altAttackAction?.Action(input?.IsAltAttack ?? false, _context);
        }
    }
}