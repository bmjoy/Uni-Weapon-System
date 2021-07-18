using UnityEngine;
using WeaponSystem.Runtime;

namespace WeaponSystem.Input
{
    public class SimpleCameraInputBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private ICameraInput _input = new UnityMouseCameraInput();

        private void Update() => Locator<ICameraInput>.Instance.Bind(_input);
    }
}