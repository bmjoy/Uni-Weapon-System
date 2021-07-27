using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Input
{
    public class SimpleMovementInputBinder : MonoBehaviour
    {
        [SubclassSelector, SerializeReference] private IMovementInput _input;
        private void Update() => Locator<IMovementInput>.Instance.Bind(_input);
    }
}