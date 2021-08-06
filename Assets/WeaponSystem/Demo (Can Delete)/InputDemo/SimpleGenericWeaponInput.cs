using UnityEngine;
using WeaponSystem.Core.Weapon;
using WeaponSystem.Input;

namespace WeaponSystem.InputDemo
{
    public class SimpleGenericWeaponInput : MonoBehaviour
    {
        [SerializeField] private InputKeys primaryActionKeys = new InputKeys(KeyCode.Mouse0);
        [SerializeField] private InputKeys primaryAltActionKeys = new InputKeys(KeyCode.LeftShift);
        [SerializeField] private InputKeys secondaryActionKeys = new InputKeys(KeyCode.Mouse1);
        [SerializeField] private InputKeys secondaryAltActionKeys = new InputKeys(KeyCode.B);
        [SerializeField] private InputKeys reloadKeys = new InputKeys(KeyCode.R);
        
        private GenericWeapon _genericWeapon;
        
        private void Awake() => _genericWeapon = GetComponent<GenericWeapon>();

        private void Update()
        {
            _genericWeapon.IsPrimaryAction = primaryActionKeys.IsAnyKeyPressed;
            _genericWeapon.IsPrimaryAltAction = primaryAltActionKeys.IsAnyKeyPressed;
            _genericWeapon.IsSecondaryAction = secondaryActionKeys.IsAnyKeyPressed;
            _genericWeapon.IsPrimaryAltAction = secondaryAltActionKeys.IsAnyKeyPressed;
            _genericWeapon.IsReload = reloadKeys.IsAnyKeyDown;
        }
    }
}