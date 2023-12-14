using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(IntWithBool))]
    public class IntBoolDrawer : BaseDatatypeDrawer
    {
        public override void UpdateSetting() => _setting = new("intValue");
    }
}