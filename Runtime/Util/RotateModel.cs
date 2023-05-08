using UnityEngine;
public class RotateModel : MonoBehaviour
{
    [SerializeField] Vector3 _rotSpeed = new Vector3(0, 1, 0);
    Vector3 _nowRotSpeed;

    void RotateStop() => _nowRotSpeed = Vector3.zero;
    void RotateStart() => _nowRotSpeed = _rotSpeed;

    private void Start() => _nowRotSpeed = _rotSpeed;

    void Update() => transform.Rotate(_nowRotSpeed * Time.deltaTime);

}

