using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    public float speed;
    public bool DoRotate = true;
    [SerializeField] Slider mainSlider;

    void Update()
    {
        if (DoRotate)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    public void doSwitchRotate()
    {
        if (DoRotate)
        {
            DoRotate = !DoRotate;
        }
        else
        {
            DoRotate = !DoRotate;
        }
    }

    public void setRotSpeed()
    {
        speed = mainSlider.value;
    }
}
