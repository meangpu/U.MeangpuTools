using VInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Meangpu.Util
{
    public class EventBoolToggle : MonoBehaviour
    {
        [SerializeField] UnityEvent _OnEvent;
        [SerializeField] UnityEvent _OffEvent;
        [SerializeField] bool _nowBool;

        [Button]
        public void ToggleBtn()
        {
            _nowBool = !_nowBool;
            if (_nowBool) _OnEvent?.Invoke();
            else _OffEvent?.Invoke();
        }
    }
}