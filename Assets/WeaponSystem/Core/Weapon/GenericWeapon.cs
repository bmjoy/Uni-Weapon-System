using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Collision;
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
        private IMagazine _magazine = new UnlimitedMagazine();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAmmoHolder _ammoHolder = new UnlimitedAmmoHolder();

        [SerializeField] private float drawRigidityTime = .5f;
        [SerializeField] private float holsterRigidityTime = .5f;

        public UnityEvent onDraw;
        public UnityEvent onHolster;

        private bool _isRigidity;

        public bool IsPrimaryAction { get; set; }
        public bool IsPrimaryAltAction { get; set; }
        public bool IsSecondaryAction { get; set; }
        public bool IsSecondaryAltAction { get; set; }
        public bool IsReload { get; set; }

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
            _primaryAction?.Injection(transform, _magazine);
            _secondaryAction?.Injection(transform, _magazine);
        }

        private void Update()
        {
            if (_isRigidity) return;
            _context = Locator<IPlayerContext>.Instance.Current;

            if (_magazine.IsReloading == false && IsReload) StartCoroutine(_magazine.Reload());
            _magazine.AmmoHolder = _ammoHolder;

            _primaryAction?.Action(IsPrimaryAction, _context);
            _primaryAction?.AltAction(IsPrimaryAltAction, _context);

            _secondaryAction?.Action(IsSecondaryAction, _context);
            _secondaryAction?.AltAction(IsSecondaryAltAction, _context);
        }

        public void Holster() => StartCoroutine(HolsterRigidity());

        private void OnEnable()
        {
            StartCoroutine(DrawRigidity());
            onDraw.Invoke();
        }

        private void OnDisable() => onHolster.Invoke();

        private IEnumerator DrawRigidity()
        {
            onDraw.Invoke();
            _isRigidity = true;
            yield return new WaitForSeconds(drawRigidityTime);
            _isRigidity = false;
        }

        private IEnumerator HolsterRigidity()
        {
            onHolster.Invoke();
            _isRigidity = true;
            yield return new WaitForSeconds(holsterRigidityTime);
            _isRigidity = false;
            gameObject.SetActive(false);
        }
    }
}