using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

namespace Meangpu.Util
{
    public class ButtonUIStatus : MonoBehaviour
    {
        [SerializeField] Button _btn;
        [SerializeField] Image _statusImg;
        [SerializeField] SOStatus _statusData;
        [SerializeField] bool nowStatus = true;

        private void Start()
        {
            SetButtonByStatus();
        }

        private void SetButtonByStatus()
        {
            if (nowStatus) DoEnableButton();
            else DoDisableButton();
        }

        [Button]
        public void ToggleButton()
        {
            nowStatus = !nowStatus;
            SetButtonByStatus();
        }

        public void DoEnableButton()
        {
            nowStatus = true;
            _btn.interactable = true;
            _statusImg.color = _statusData.EnableColor;
            _statusImg.sprite = _statusData.EnableImg;
        }

        public void DoDisableButton()
        {
            nowStatus = false;
            _btn.interactable = false;
            _statusImg.color = _statusData.DisableColor;
            _statusImg.sprite = _statusData.DisableImg;
        }
    }
}