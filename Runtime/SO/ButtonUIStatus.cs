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
        [SerializeField] Image _statusImgColorTarget;
        [SerializeField] TMP_Text _statusText;
        [Expandable]
        [SerializeField] SOStatus _statusData;

        [Header("Start Status")]
        [SerializeField] bool _nowStatus = true;
        [SerializeField] bool _doSetVisualOnStart = true;
        [Space]
        [Header("Set Option")]
        [SerializeField] bool _doSetButtonInteractable;
        [SerializeField] bool _doSetImage = true;
        [SerializeField] bool _doSetText;
        [SerializeField] bool _doSetAlphaOnly;

        private void Start()
        {
            if (_doSetVisualOnStart) SetButtonByStatus(_nowStatus);
            if (_statusImgColorTarget == null) _statusImgColorTarget = _statusImg;
        }

        public void SetButtonByStatus(bool isEnable)
        {
            if (_statusImgColorTarget == null) _statusImgColorTarget = _statusImg;

            if (isEnable) DoEnableButton();
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
            if (_doSetButtonInteractable && _btn) _btn.interactable = true;

            if (_statusImgColorTarget && _statusImg)
            {
                _statusImgColorTarget.color = _doSetAlphaOnly ? new Color(_statusImg.color.r, _statusImg.color.g, _statusImg.color.b, _statusData.EnableColor.a) : _statusData.EnableColor;

                if (_doSetImage) _statusImg.sprite = _statusData.EnableImg;
            }

            if (!_doSetText) return;
            _statusText?.SetText(_statusData.EnableText);
        }

        public void DoDisableButton()
        {
            _nowStatus = false;
            if (_doSetButtonInteractable && _btn) _btn.interactable = false;

            if (_statusImgColorTarget && _statusImg)
            {
                _statusImgColorTarget.color = _doSetAlphaOnly ? new Color(_statusImg.color.r, _statusImg.color.g, _statusImg.color.b, _statusData.EnableColor.a) : _statusData.DisableColor;

                if (_doSetImage) _statusImg.sprite = _statusData.DisableImg;
            }

            if (!_doSetText) return;
            _statusText?.SetText(_statusData.DisableText);
        }
    }
}
