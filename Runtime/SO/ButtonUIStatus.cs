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
        [SerializeField] bool _nowStatus = true;
        [SerializeField] bool _setButtonInteractable = false;

        private void Start()
        {
            SetButtonByStatus();
        }

        private void SetButtonByStatus()
        {
            if (_nowStatus) DoEnableButton();
            else DoDisableButton();
        }

        [Button]
        public void ToggleButton()
        {
            _nowStatus = !_nowStatus;
            SetButtonByStatus();
        }

        public void DoEnableButton()
        {
            _nowStatus = true;
            if (_setButtonInteractable) _btn.interactable = true;
            _statusImg.color = _statusData.EnableColor;
            _statusImg.sprite = _statusData.EnableImg;
        }

        public void DoDisableButton()
        {
            _nowStatus = false;
            if (_setButtonInteractable) _btn.interactable = false;
            _statusImg.color = _statusData.DisableColor;
            _statusImg.sprite = _statusData.DisableImg;
        }
    }
}