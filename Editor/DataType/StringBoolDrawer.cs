using UnityEngine;
using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(StringWithBool))]
    public class StringBoolDrawer : BaseDatatypeDrawer
    {
        public override void UpdateSetting() => _setting = new("stringValue");
    }
}