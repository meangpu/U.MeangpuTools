using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using TMPro;

namespace Meangpu.Util
{
    public class ButtonUIStatus : MonoBehaviour
    {
        [SerializeField] Button _btn;
        [SerializeField] Image _statusImg;
        [SerializeField] TMP_Text _statusText;
        [SerializeField] SOStatus _statusData;

        [Header("Start Status")]
        [SerializeField] bool _nowStatus = true;
        [Space]
        [Header("Set Option")]
        [SerializeField] bool _doSetButtonInteractable;
        [SerializeField] bool _doSetImage = true;
        [SerializeField] bool _doSetText;
        [SerializeField] bool _doSetAlphaOnly;

        private void Start() => SetButtonByStatus(_nowStatus);

        public void SetButtonByStatus(bool newStatus)
        {
            if (newStatus) DoEnableButton();
            else DoDisableButton();
        }

        [Button]
        public void ToggleButton()
        {
            _nowStatus = !_nowStatus;
            SetButtonByStatus(_nowStatus);
        }

        public void DoEnableButton()
        {
            _nowStatus = true;
            if (_doSetButtonInteractable) _btn.interactable = true;

            _statusImg.color = _doSetAlphaOnly ? new Color(_statusImg.color.r, _statusImg.color.g, _statusImg.color.b, _statusData.EnableColor.a) : _statusData.EnableColor;

            if (_doSetImage) _statusImg.sprite = _statusData.EnableImg;

            if (!_doSetText) return;
            _statusText?.SetText(_statusData.EnableText);
        }

        public void DoDisableButton()
        {
            _nowStatus = false;
            if (_doSetButtonInteractable) _btn.interactable = false;

            _statusImg.color = _doSetAlphaOnly ? new Color(_statusImg.color.r, _statusImg.color.g, _statusImg.color.b, _statusData.DisableColor.a) : _statusData.DisableColor;

            if (_doSetImage) _statusImg.sprite = _statusData.DisableImg;

            if (!_doSetText) return;
            _statusText?.SetText(_statusData.DisableText);
        }
    }
}
