using UnityEngine;
using WeaponSystem.Core.Input.OldInput;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.Input
{
    public class SimpleCameraInputBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICameraInput _input = new UnityMouseCameraInput();

        private void Update() => Locator<ICameraInput>.Instance.Bind(_input);
    }
}