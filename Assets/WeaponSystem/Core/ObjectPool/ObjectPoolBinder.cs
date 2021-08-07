using UnityEngine;
using WeaponSystem.Core.Runtime;

namespace WeaponSystem.Core.ObjectPool
{
    public class ObjectPoolBinder : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] private IObjectPoolFactory _factory = new DefaultObjectPoolFactory();

        private void Awake() => Locator<IObjectPoolFactory>.Instance.Bind(_factory);
    }
}