using UnityEngine;
using UnityEngine.InputSystem;

namespace Meangpu.Setting
{
    public class SettingManager : BaseMeSingleton<SettingManager>
    {
        [SerializeField] GameObject _settingPanel;
        [SerializeField] InputActionReference _openMenu;
        bool _nowIsSettingOpen;

        private void Start()
        {
            CloseSettingPanel();
        }

        void OnEnable()
        {
            _openMenu.action.performed += ToggleSettingPanel;
            _openMenu.action.Enable();
        }

        void OnDisable()
        {
            _openMenu.action.performed -= ToggleSettingPanel;
            _openMenu.action.Disable();
        }

        public void OpenSettingPanel()
        {
            _nowIsSettingOpen = true;
            _settingPanel.gameObject.SetActive(true);
            TimeScaleSetter.Instance.StopTimeImmediately();
            ActionOnSetting.OnPauseBySetting?.Invoke();
        }

        public void CloseSettingPanel()
        {
            _nowIsSettingOpen = false;
            _settingPanel.gameObject.SetActive(false);
            TimeScaleSetter.Instance.ResetTimeScale();
            ActionOnSetting.OnUnPauseBySetting?.Invoke();
        }

        public void DoResetAllSetting() => ActionOnSetting.OnResetSetting?.Invoke();

        private void ToggleSettingPanel(InputAction.CallbackContext context) => ToggleSettingPanel();

        public void ToggleSettingPanel()
        {
            _nowIsSettingOpen = !_nowIsSettingOpen;
            if (_nowIsSettingOpen) OpenSettingPanel();
            else CloseSettingPanel();
        }
    }
}
