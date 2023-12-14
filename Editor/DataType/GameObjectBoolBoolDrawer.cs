using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(GameObjectWithBool))]
    public class GameObjectBoolBoolDrawer : BaseDataWithBoolDrawer
    {
        public override void UpdateSetting()
        {
            _setting = new("gameObjectValue");
        }
    }
}