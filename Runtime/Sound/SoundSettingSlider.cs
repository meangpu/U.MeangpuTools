using UnityEngine;
using UnityEngine.Audio;

public class SoundSettingSlider : MonoBehaviour
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
