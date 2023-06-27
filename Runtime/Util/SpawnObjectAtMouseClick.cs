using UnityEngine;

namespace Meangpu.Util
{
    public class SpawnObjectAtMouseClick : MonoBehaviour
    {
        // spawn object on 3D plane raycast
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _spawnPref;
        [SerializeField] Vector3 _offset;
        RaycastHit _hit;

        void Start()
        {
            if (_camera == null) _camera = Camera.main;
        }

        void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit))
            {
                Vector3 _planeHitPos = new(_hit.point.x, _hit.point.y + (_spawnPref.transform.position.y), _hit.point.z);
                Vector3 _targetSpawnPos = _planeHitPos + _offset;
                Instantiate(_spawnPref, _targetSpawnPos, Quaternion.identity);
            }
        }
    }
}