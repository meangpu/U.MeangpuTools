using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu
{
    [RequireComponent(typeof(Slider))]
    public class SliderSetTextValue : MonoBehaviour
    {
        Slider _slider;
        [SerializeField] TMP_Text _valueDisplay;
        [SerializeField] string _numberFormat = "F2";
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener((v) => _valueDisplay.SetText(v.ToString(_numberFormat)));
            _valueDisplay.SetText(_slider.value.ToString(_numberFormat));
        }
    }
}
