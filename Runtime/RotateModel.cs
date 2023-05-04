using UnityEngine;
public class RotateModel : MonoBehaviour
{
    [SerializeField] float _rotSpeed;
    float _nowRotSpeed;

    void RotateStop() => _nowRotSpeed = 0;
    void RotateStart() => _nowRotSpeed = _rotSpeed;

    private void Start() => _nowRotSpeed = _rotSpeed;

    void Update() => transform.Rotate(0, _rotSpeed, 0);
}

