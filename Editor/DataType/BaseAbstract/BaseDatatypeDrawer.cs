using UnityEngine;
using UnityEditor;

namespace Meangpu.Datatype
{
    public abstract class BaseDatatypeDrawer : PropertyDrawer
    {
        protected BoolDrawerSetting _setting = new("floatValue");

        /*
        In child Do something like:
        public override void UpdateSetting() => _setting = new("gameObjectValue");
        */

        public abstract void UpdateSetting();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            DrawProperty(position, property);
            EditorGUI.EndProperty();
        }

        public virtual void DrawProperty(Rect position, SerializedProperty property)
        {
            UpdateSetting();
            SerializedProperty A_var = property.FindPropertyRelative(_setting.A_var_name);
            SerializedProperty B_var = property.FindPropertyRelative(_setting.B_var_name);

            int widthRatio = _setting.objectColumnSpace - _setting.boolTickSpace;
            float widthSize = position.width / _setting.objectColumnSpace;

            Rect pos1 = new(position.x, position.y, (widthSize * widthRatio) - _setting.offsetSize, position.height);
            Rect pos2 = new(position.x + (widthSize * widthRatio), position.y, widthSize - _setting.offsetSize, position.height);

            EditorGUI.PropertyField(pos1, A_var, GUIContent.none);
            EditorGUI.PropertyField(pos2, B_var, GUIContent.none);
        }
    }
}