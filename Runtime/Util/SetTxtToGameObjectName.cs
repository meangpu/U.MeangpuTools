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
            if (IsNoNeedToChange()) return;
            ChangeTextToName();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        [Button]
        public void ChangeTextToName()
        {
            if (IsNoNeedToChange()) return;
            _text.text = _targetObject.name;
            gameObject.name = _targetObject.name;
        }

        bool IsNoNeedToChange()
        {
            if (_targetObject == null || _text == null) return true;
            if (_text.text == _targetObject.name && gameObject.name == _targetObject.name) return true;
            return false;
        }
    }
}