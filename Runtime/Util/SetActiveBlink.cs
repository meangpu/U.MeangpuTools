using UnityEngine;

namespace Meangpu.Util
{
    public class SetActiveBlink : MonoBehaviour
    {
        [SerializeField] GameObject _target;
        [Header("Start state")]
        [SerializeField] bool _nowActiveState;
        [Header("Start setting")]
        [SerializeField] bool _blinkOnStart;

        bool _isBlinking;

        void OnDisable() => StopAllCoroutines();

        void ToggleObjState()
        {
            _nowActiveState = !_nowActiveState;
            _target?.SetActive(_nowActiveState);
        }
    }
}
