using UnityEngine;

namespace Meangpu.Setting
{
    public class SettingManagerInvoker : MonoBehaviour
    {
        public void CloseSettingPanel() => SettingManager.Instance.CloseSettingPanel();
        public void OpenSettingPanel() => SettingManager.Instance.OpenSettingPanel();
    }
}
