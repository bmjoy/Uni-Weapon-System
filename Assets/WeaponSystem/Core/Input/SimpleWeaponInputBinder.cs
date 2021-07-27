using UnityEngine;
using WeaponSystem.Core.Input.OldInput;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Input
{
    public class SimpleWeaponInputBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IWeaponInput input = new UnityKeyboardWeaponInput();

        private void Update() => Locator<IWeaponInput>.Instance.Bind(input);
    }
}
