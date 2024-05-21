using System.Collections;
using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public abstract class BaseLoadingRandom : MonoBehaviour
    {
        [SerializeField] float _timeBetweenChange = 0;

        private void OnEnable()
        {
            if (_timeBetweenChange > 0)
            {
                StartCoroutine(DoRandomCountdownCall());
            }
            else
            {
                SetToNewRandom();
            }
        }

        private void OnDisable() => StopAllCoroutines();

        IEnumerator DoRandomCountdownCall()
        {
            SetToNewRandom();
            yield return new WaitForSecondsRealtime(_timeBetweenChange);
            StartCoroutine(DoRandomCountdownCall());
        }

        [Button] protected abstract void SetToNewRandom();
    }
}