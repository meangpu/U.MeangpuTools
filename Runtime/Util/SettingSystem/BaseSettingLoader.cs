using UnityEngine;

namespace Meangpu.Setting
{
    public abstract class BaseSettingLoader : MonoBehaviour
    {
        protected abstract string _settingWord { get; }
        protected abstract int _defaultIndex { get; }
        protected int _currentIndex;

        protected void SaveSettingValue() => PlayerPrefs.SetInt(_settingWord, _currentIndex);
        protected void LoadSavedSettingValue()
        {
            if (PlayerPrefs.HasKey(_settingWord)) ChangeSettingByIndex(PlayerPrefs.GetInt(_settingWord));
            else ResetSetting();
        }

        protected abstract void UpdateSettingUIVisual();
        public abstract void ChangeSettingByIndex(int newIndex);

        public virtual void ResetSetting()
        {
            ChangeSettingByIndex(_defaultIndex);
            UpdateSettingUIVisual();
        }

        protected virtual void OnEnable()
        {
            InitLoadSetting();
            ActionOnSetting.OnResetSetting += ResetSetting;
        }

        void OnDisable()
        {
            ActionOnSetting.OnResetSetting -= ResetSetting;
        }

        protected virtual void InitLoadSetting()
        {
            LoadSavedSettingValue();
            UpdateSettingUIVisual();
        }
    }
}
