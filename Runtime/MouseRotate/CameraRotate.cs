using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    public float Speed;
    public bool DoRotate = true;
    [SerializeField] Slider _mainSlider;

    void Update()
    {
        if (DoRotate) transform.Rotate(0, Speed * Time.deltaTime, 0);
    }

    public void ToggleRotate()
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
        Speed = _mainSlider.value;
    }

}
