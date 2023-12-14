using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(FloatWithBool))]
    public class FloatBoolDrawer : BaseDataWithBoolDrawer
    {
        public override void UpdateSetting()
        {
            _setting = new("floatValue");
        }
    }
}