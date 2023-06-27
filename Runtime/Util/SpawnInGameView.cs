using UnityEngine;

namespace Meangpu.Util
{
    public class SpawnInGameView : MonoBehaviour
    {
        [SerializeField] float minX = 0.05f;
        [SerializeField] float maxX = 0.95f;

        [SerializeField] float minY = 0.05f;
        [SerializeField] float maxY = 0.95f;
        float zPos = 10;

        Vector3 GetSpawnPos()
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 pos = new(x, y, zPos);
            return Camera.main.ViewportToWorldPoint(pos);
        }
    }
}