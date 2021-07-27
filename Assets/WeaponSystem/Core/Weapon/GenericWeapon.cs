using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
using WeaponSystem.Core.Input;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Weapon.Action;
using WeaponSystem.Core.Weapon.AmmoHolder;
using WeaponSystem.Core.Weapon.Magazine;

namespace WeaponSystem.Core.Weapon
{
    [AddComponentMenu("WeaponSystem/GenericWeapon"), DisallowMultipleComponent,
     RequireComponent(typeof(IObjectPermission))]
    public class GenericWeapon : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IWeaponAction _primaryAction = new NoneAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IWeaponAction _secondaryAction = new NoneAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IMagazine _magazine = new BoxMagazine();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAmmoHolder _ammoHolder = new UnlimitedAmmoHolder();

        public UnityEvent onDraw;
        public UnityEvent onHolster;

        public IWeaponAction PrimaryAction => _primaryAction;
        public IWeaponAction SecondaryAction => _secondaryAction;
        public IMagazine Magazine => _magazine;
        public IAmmoHolder AmmoHolder => _ammoHolder;

        private IPlayerContext _context;


        private void Awake()
        {
            _context = Locator<IPlayerContext>.Instance.Current;
            _primaryAction ??= new NoneAction();
            _secondaryAction ??= new NoneAction();
            _secondaryAction?.Injection(transform, _magazine);
            _primaryAction?.Injection(transform, _magazine);
        }

        private void Update()
        {
            var input = Locator<IWeaponInput>.Instance.Current;
            _context = Locator<IPlayerContext>.Instance.Current;
            if (_magazine.IsReloading == false && (input?.IsReload ?? false)) StartCoroutine(_magazine.Reload());
            _magazine.AmmoHolder = _ammoHolder;

            _primaryAction?.Action(input?.IsPrimaryAction ?? false, _context);
            _primaryAction?.AltAction(input?.IsPrimaryAltAltAction ?? false, _context);

            _secondaryAction?.Action(input?.IsSecondaryAction ?? false, _context);
            _secondaryAction?.AltAction(input?.IsSecondaryAltAction ?? false, _context);
        }

        private void OnEnable() => onDraw.Invoke();
        private void OnDisable() => onHolster.Invoke();
    }
}