using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] Vector3 scrollZoomSpeed = new Vector3(0, 0, 5);
    [SerializeField] float clampZMin = -20;
    [SerializeField] float clampZMax = 0;

    Vector3 _previousPosRotate;

    Vector3 _lastMousePosPan;
    [SerializeField] float _panSpeed = 0.1f;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        SetCameraPosition();
    }

    void Update()
    {
        PanMouse();
        RotateAroundObj();
        ZoomInOut();

        offset.z = Mathf.Clamp(offset.z, clampZMin, clampZMax);  // clamp z value
        SetCameraPosition();

    }

    private void ZoomInOut()
    {
        if (Input.mouseScrollDelta.y > 0)  // mouse up
        {
            offset += scrollZoomSpeed;
        }
        if (Input.mouseScrollDelta.y < 0)  // mouse down
        {
            offset -= scrollZoomSpeed;
        }
    }

    private void RotateAroundObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousPosRotate = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = _previousPosRotate - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = new Vector3();
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            _previousPosRotate = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void PanMouse()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _lastMousePosPan = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 _delta = Input.mousePosition - _lastMousePosPan;
            offset += new Vector3(-_delta.x * _panSpeed, -_delta.y * _panSpeed, 0);
            _lastMousePosPan = Input.mousePosition;
        }
    }

    private void SetCameraPosition()
    {
        cam.transform.position = new Vector3();
        cam.transform.Translate(offset);
    }
}
