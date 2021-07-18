using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Input
{
    public class SimpleMovementInputBinder : MonoBehaviour
    {
        [SubclassSelector, SerializeReference] private IMovementInput _input;
        private void Update() => Locator<IMovementInput>.Instance.Bind(_input);
    }
}