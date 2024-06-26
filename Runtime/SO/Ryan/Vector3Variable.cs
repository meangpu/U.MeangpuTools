
using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu]
    public class Vector3Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Vector3 Value;
        public void SetValue(Vector3 value) => Value = value;
        public void SetValue(Vector3Variable value) => Value = value.Value;
        public void ApplyChange(Vector3 amount) => Value += amount;
        public void ApplyChange(Vector3Variable amount) => Value += amount.Value;
        public void SetValueToXZ(Vector2 value) => Value = new(value.x, 0, value.y);
    }
}

