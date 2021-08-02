using System.Collections;
using UnityEngine;

namespace WeaponSystem.Core.ObjectPool.Deactivate
{
    public class LifeTimeDeactivate : MonoBehaviour
    {
        [SerializeField] private float duration;
        private WaitForSeconds _seconds;

        private void OnEnable() => StartCoroutine(Deactive());

        IEnumerator Deactive()
        {
            _seconds ??= new WaitForSeconds(duration);
            yield return _seconds;
            gameObject.SetActive(false);
        }
    }
}