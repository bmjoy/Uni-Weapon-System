using System.Collections;
using UnityEngine;

namespace WeaponSystem.Core.Runtime
{
    public class CoroutineHandler : SingletonMonoBehavior<CoroutineHandler>
    {
        public Coroutine CoroutineStart(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}