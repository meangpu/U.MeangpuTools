using UnityEngine;

public class SpawnInGameView : MonoBehaviour
{
    void Start()
    {
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);
    }
}