using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Input
{
    public class SimpleWeaponInputBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IWeaponInput input = new InputManagerKeyboardWeaponInput();

        private void Update() => Locator<IWeaponInput>.Instance.Bind(input);
    }
}
