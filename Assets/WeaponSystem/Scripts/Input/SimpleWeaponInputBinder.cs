using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Input
{
    public class SimpleWeaponInputBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IWeaponInput input = new UnityKeyboardWeaponInput();

        private void Update() => Locator<IWeaponInput>.Instance.Bind(input);
    }
}
