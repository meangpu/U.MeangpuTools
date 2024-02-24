using UnityEngine;
using TMPro;

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
            _text = GetComponent<TMP_Text>();
            if (_targetObject == null || _text == null) return;
            if (_text.text == _targetObject.name && gameObject.name == _targetObject.name) return;
            _text.text = _targetObject.name;
            gameObject.name = _targetObject.name;

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}