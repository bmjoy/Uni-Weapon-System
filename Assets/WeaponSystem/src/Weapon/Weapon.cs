using UnityEngine;
using WeaponSystem.Input;
using WeaponSystem.Movement;
using WeaponSystem.Runtime;
using WeaponSystem.Scripts.Runtime;
using WeaponSystem.Weapon.Action.AltAttackAction;
using WeaponSystem.Weapon.Action.AttackAction;
using WeaponSystem.Weapon.Magazine;

namespace WeaponSystem.Weapon
{
    [AddComponentMenu("WeaponSystem/Weapon")]
    public class Weapon : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IAttackAction _attackAction = new NoneAttackAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAltAttackAction _altAttackAction = new NoneAltAttackAction();
        
        [Space(20)] [SerializeReference, SubclassSelector]
        private IMagazine _magazine = new NoneMagazine();

        
        [Space(20)] [SerializeField] private Animator weaponAnimator;
        private static readonly int TakeOut = Animator.StringToHash("TakeOut");
        private static readonly int EndUp = Animator.StringToHash("EndUp");
        private IPlayerContext _context;
        
        private void Awake()
        {
            _altAttackAction?.Injection(transform, weaponAnimator, _magazine, _context);
            _attackAction?.Injection(transform, weaponAnimator, _magazine, _context);
        }

        private void Update()
        {
            var input = Locator<IWeaponInput>.Instance.Current;
            _context = Locator<IPlayerContext>.Instance.Current;
            if (input?.IsReload ?? false) CoroutineHandler.Instance.CoroutineStart(_magazine.Reload());
            _attackAction?.Action(input?.IsAttack ?? false);
            _altAttackAction?.Action(input?.IsAltAttack ?? false);
        }

        // 武器を取り出すアクション
        private void OnEnable()
        {
            weaponAnimator.NullCast()?.SetTrigger(TakeOut);
        }

        // 武器をしまうアクション
        private void OnDisable()
        {
            weaponAnimator.NullCast()?.SetTrigger(EndUp);
        }
    }
}