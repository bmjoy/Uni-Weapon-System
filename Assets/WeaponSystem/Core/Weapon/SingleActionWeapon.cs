using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem.Core.Movement;
using WeaponSystem.Core.Runtime;
using WeaponSystem.Core.Weapon.Action;
using WeaponSystem.Core.Weapon.AmmoHolder;
using WeaponSystem.Core.Weapon.Magazine;


namespace WeaponSystem.Core.Weapon
{
    public class SingleActionWeapon : MonoBehaviour, IWeapon
    {
        [SerializeReference, SubclassSelector] private IWeaponAction _action = new NoneAction();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IMagazine _magazine = new UnlimitedMagazine();

        [Space(20)] [SerializeReference, SubclassSelector]
        private IAmmoHolder _ammoHolder = new UnlimitedAmmoHolder();

        [SerializeField] private float drawRigidityTime = .5f;
        [SerializeField] private float holsterRigidityTime = .5f;

        public UnityEvent onDraw;
        public UnityEvent onHolster;

        private ISingleActionWeaponInput _input;

        private bool _isAim;
        private bool _isRigidity;


        public bool IsAim => _isAim;

        public IWeaponAction Action => _action;
        public IMagazine Magazine => _magazine;
        public IAmmoHolder AmmoHolder => _ammoHolder;

        private IPlayerState _state;


        private void Awake()
        {
            _state = Locator<IPlayerState>.Instance.Current;
            _action ??= new NoneAction();
            _action?.Injection(transform, _magazine);
            _input = GetComponent<ISingleActionWeaponInput>();
        }


        private void Update()
        {
            if (_isRigidity) return;

            _state = Locator<IPlayerState>.Instance.Current;

            if (_magazine.IsReloading == false && _input.IsReload) StartCoroutine(_magazine.Reload());
            _magazine.AmmoHolder = _ammoHolder;

            _action?.Action(_input.IsAction, ref _isAim, _state);
            _action?.AltAction(_input.IsAltAction, _state);
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

            _action.OnDraw(ref _isAim);

            yield return new WaitForSeconds(drawRigidityTime);

            _isRigidity = false;
        }


        private IEnumerator HolsterRigidity()
        {
            onHolster.Invoke();
            _isRigidity = true;
            
            _action.OnHolster(ref _isAim);

            yield return new WaitForSeconds(holsterRigidityTime);

            _isRigidity = false;
            gameObject.SetActive(false);
        }
    }
}