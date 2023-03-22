using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
class VolSlider
{
    public Slider _slider;
    public TMP_Text _text;
    public string _mixerGroupName;

    public void UpdateSliderTextVolumeValue(float newVal, AudioMixer mixer)
    {
        _slider.value = newVal;
        UpdateTextAndVolume(newVal, mixer);
    }

    public void UpdateTextAndVolume(float newVal, AudioMixer mixer)
    {
        _text.SetText((newVal * 10).ToString("0"));
        mixer.SetFloat(_mixerGroupName, Mathf.Log10(newVal) * 20);
    }
}
