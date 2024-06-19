using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Meangpu.Util
{
    public class InputDoAction : MonoBehaviour
    {
        [SerializeField] InputActionReference _input;
        [Header("event")]
        [SerializeField] UnityEvent _onPerform;
        [SerializeField] UnityEvent _onKeyPress;
        [SerializeField] UnityEvent _onRelease;

        void OnEnable()
        {
            _input.action.performed += OnPerform;
            _input.action.started += OnPress;
            _input.action.canceled += OnRelease;
            _input.action.Enable();
        }

        void OnDisable()
        {
            _input.action.performed -= OnPerform;
            _input.action.started -= OnPress;
            _input.action.canceled -= OnRelease;
            _input.action.Disable();
        }

        private void OnPerform(InputAction.CallbackContext context) => _onPerform?.Invoke();
        private void OnPress(InputAction.CallbackContext context) => _onKeyPress?.Invoke();
        private void OnRelease(InputAction.CallbackContext context) => _onRelease?.Invoke();
    }
}