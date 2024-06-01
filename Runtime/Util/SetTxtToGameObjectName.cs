using UnityEngine;
using TMPro;
using VInspector;

namespace Meangpu.Util
{
    [RequireComponent(typeof(TMP_Text))]
    public class SetTxtToGameObjectName : MonoBehaviour
    {
        [SerializeField] GameObject _targetObject;
        TMP_Text _text;

#if UNITY_EDITOR
        private void OnValidate()
        {
            ChangeTextToName();
            if (IsNoNeedToChange()) return;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        [Button]
        public void ChangeTextToName()
        {
            Init();
            if (IsNoNeedToChange()) return;
            _text.text = _targetObject.name;
            gameObject.name = _targetObject.name;
        }

        void Init()
        {
            if (_text == null) _text = GetComponent<TMP_Text>();
        }

        bool IsNoNeedToChange()
        {
            if (_targetObject == null || _text == null) return true;
            if (_text.text == _targetObject.name) return true;
            return false;
        }
    }
}