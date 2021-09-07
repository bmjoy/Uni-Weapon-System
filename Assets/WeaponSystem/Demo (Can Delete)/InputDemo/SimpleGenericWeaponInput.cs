using UnityEngine;
using WeaponSystem.Core.Weapon;
using WeaponSystem.Input;


namespace WeaponSystem.InputDemo
{
    public class SimpleGenericWeaponInput : MonoBehaviour, IDualActionWeaponInput
    {
        [SerializeField] private InputKeys primaryActionKeys = new InputKeys(KeyCode.Mouse0);
        [SerializeField] private InputKeys primaryAltActionKeys = new InputKeys(KeyCode.B);
        [SerializeField] private InputKeys secondaryActionKeys = new InputKeys(KeyCode.Mouse1);
        [SerializeField] private InputKeys secondaryAltActionKeys = new InputKeys(KeyCode.LeftShift);
        [SerializeField] private InputKeys reloadKeys = new InputKeys(KeyCode.R);

        public bool IsAction => primaryActionKeys.IsAnyKeyPressed;
        public bool IsAltAction => primaryAltActionKeys.IsAnyKeyDown;
        public bool IsReload => reloadKeys.IsAnyKeyDown;
        public bool IsSecondaryAction => secondaryActionKeys.IsAnyKeyPressed;
        public bool IsSecondaryAltAction => secondaryAltActionKeys.IsAnyKeyDown;
    }
}