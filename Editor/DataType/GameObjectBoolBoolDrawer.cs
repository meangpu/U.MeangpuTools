using UnityEditor;

namespace Meangpu.Datatype
{
    [CustomPropertyDrawer(typeof(GameObjectWithBool))]
    public class GameObjectBoolBoolDrawer : BaseDatatypeDrawer
    {
        public override void UpdateSetting() => _setting = new("gameObjectValue");
    }
}