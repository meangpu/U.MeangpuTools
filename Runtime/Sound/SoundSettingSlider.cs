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

public class SettingSlider : MonoBehaviour
{

    [Header("VolumeMixer")]
    [SerializeField] AudioMixer _mixer;

    [Header("Volume")]
    [SerializeField] VolSlider _master;
    [SerializeField] VolSlider _fx;
    [SerializeField] VolSlider _bg;

    [Header("Setting")]
    [SerializeField] bool _isLoadFromSave;

    private void Start()
    {
        SetSliderVolume(_master);
        SetSliderVolume(_fx);
        SetSliderVolume(_bg);

        if (_isLoadFromSave) LoadSoundSetting();
    }

    void SetSliderVolume(VolSlider sliderGroup)
    {
        sliderGroup._slider.onValueChanged.AddListener((v) =>
        {
            UpdateVolumeValue(sliderGroup, v);
        });
    }

    void UpdateVolumeValue(VolSlider sliderGroup, float value)
    {
        sliderGroup.UpdateTextAndVolume(value, _mixer);
        PlayerPrefs.SetFloat(sliderGroup._mixerGroupName, value);
    }

    float GetCreateSoundSetting(string settingName, float defaultVal = 0.5f)
    {
        if (PlayerPrefs.HasKey(settingName)) return PlayerPrefs.GetFloat(settingName);
        else return defaultVal;
    }

    public void LoadSoundSetting()
    {
        float saved_Master;
        float saved_FX;
        float saved_BG;

        saved_Master = GetCreateSoundSetting(_master._mixerGroupName);
        saved_FX = GetCreateSoundSetting(_fx._mixerGroupName);
        saved_BG = GetCreateSoundSetting(_bg._mixerGroupName);

        _master.UpdateSliderTextVolumeValue(saved_Master, _mixer);
        _fx.UpdateSliderTextVolumeValue(saved_FX, _mixer);
        _bg.UpdateSliderTextVolumeValue(saved_BG, _mixer);

    }

}
