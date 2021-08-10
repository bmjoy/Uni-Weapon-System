using UnityEngine;
using WeaponSystem.Core.Weapon;
using WeaponSystem.Input;

namespace WeaponSystem.InputDemo
{
    public class SimpleGenericWeaponInput : MonoBehaviour
    {
        [SerializeField] private InputKeys primaryActionKeys = new InputKeys(KeyCode.Mouse0);
        [SerializeField] private InputKeys primaryAltActionKeys = new InputKeys(KeyCode.B);
        [SerializeField] private InputKeys secondaryActionKeys = new InputKeys(KeyCode.Mouse1);
        [SerializeField] private InputKeys secondaryAltActionKeys = new InputKeys(KeyCode.LeftShift);
        [SerializeField] private InputKeys reloadKeys = new InputKeys(KeyCode.R);
        
        private DualActionWeapon _dualActionWeapon;
        
        private void Awake() => _dualActionWeapon = GetComponent<DualActionWeapon>();

        private void Update()
        {
            _dualActionWeapon.IsPrimaryAction = primaryActionKeys.IsAnyKeyPressed;
            _dualActionWeapon.IsPrimaryAltAction = primaryAltActionKeys.IsAnyKeyDown;
            _dualActionWeapon.IsSecondaryAction = secondaryActionKeys.IsAnyKeyPressed;
            _dualActionWeapon.IsSecondaryAltAction = secondaryAltActionKeys.IsAnyKeyDown;
            _dualActionWeapon.IsReload = reloadKeys.IsAnyKeyDown;
        }
    }
}