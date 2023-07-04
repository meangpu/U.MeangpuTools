using UnityEngine;

namespace Meangpu.Util
{
    public class TransformSetTargetObjectHeight : MonoBehaviour
    {
        [SerializeField] Transform _targetTransform;
        // horizontal mean it vertical bar but it slider left right that set object height
        [SerializeField] bool _sliderIsHorizontal;
        Vector3 _startPos;

        void Awake() => _startPos = transform.position;

        float GetOffsetY() => -(_startPos.y - transform.position.y);
        float GetOffsetX() => -(_startPos.x - transform.position.x);

        void Update() => UpdatePinPosToSetTargetObjectHeight();

        void UpdatePinPosToSetTargetObjectHeight()
        {
            if (_sliderIsHorizontal)
            {
                _targetTransform.position = new Vector3(_targetTransform.position.x, GetOffsetX(), _targetTransform.position.z);
            }
            else
            {
                _targetTransform.position = new Vector3(_targetTransform.position.x, GetOffsetY(), _targetTransform.position.z);
            }
        }
    }
}
