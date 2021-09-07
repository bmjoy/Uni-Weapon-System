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
    [AddComponentMenu("WeaponSystem/DualActionWeapon"), DisallowMultipleComponent,
     RequireComponent(typeof(IObjectPermission))]
    public class DualActionWeapon : MonoBehaviour, IWeapon
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

        private bool _isAim;
        private bool _isRigidity;

        private IDualActionWeaponInput _input;


        public IWeaponAction PrimaryAction => _primaryAction;
        public IWeaponAction SecondaryAction => _secondaryAction;

        public bool IsAim => _isAim;
        public IMagazine Magazine => _magazine;
        public IAmmoHolder AmmoHolder => _ammoHolder;

        private IPlayerState _state;


        private void Awake()
        {
            _state = Locator<IPlayerState>.Instance.Current;
            _primaryAction ??= new NoneAction();
            _secondaryAction ??= new NoneAction();
            _primaryAction?.Injection(transform, _magazine);
            _secondaryAction?.Injection(transform, _magazine);
            if (_magazine != null) _magazine.AmmoHolder = _ammoHolder;
            _input = GetComponent<IDualActionWeaponInput>();
        }


        private void Update()
        {
            if (_isRigidity) return;

            _state = Locator<IPlayerState>.Instance.Current;

            if ((_magazine?.IsReloading ?? false) == false && _input.IsReload) StartCoroutine(_magazine.Reload());

            _primaryAction?.Action(_input.IsAction, ref _isAim, _state);
            _primaryAction?.AltAction(_input.IsAction, _state);

            _secondaryAction?.Action(_input.IsSecondaryAction, ref _isAim, _state);
            _secondaryAction?.AltAction(_input.IsSecondaryAltAction, _state);
        }


        public void Holster() => StartCoroutine(HolsterRigidity());


        private void OnEnable()
        {
            StartCoroutine(DrawRigidity());
            onDraw.Invoke();
        }


        private IEnumerator DrawRigidity()
        {
            onDraw.Invoke();
            _isRigidity = true;
            _primaryAction.OnDraw(ref _isAim);
            _secondaryAction.OnDraw(ref _isAim);
            yield return new WaitForSeconds(drawRigidityTime);

            _isRigidity = false;
        }


        private IEnumerator HolsterRigidity()
        {
            onHolster.Invoke();
            _isRigidity = true;
            _primaryAction.OnHolster(ref _isAim);
            _secondaryAction.OnHolster(ref _isAim);
            
            yield return new WaitForSeconds(holsterRigidityTime);

            _isRigidity = false;
            onHolster.Invoke();

            gameObject.SetActive(false);
        }
    }
}