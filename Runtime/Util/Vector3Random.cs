using UnityEngine;

namespace Meangpu.Util
{
    public static class Vector3Random
    {
        public static Vector3 GetOffsetRandom(Vector2 offset)
        {
            float x = Random.Range(offset.x, offset.y);
            float y = Random.Range(offset.x, offset.y);
            float z = Random.Range(offset.x, offset.y);
            return new Vector3(x, y, z);
        }
    }
}