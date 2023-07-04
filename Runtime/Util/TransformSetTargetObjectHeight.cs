using UnityEngine;

namespace Meangpu.Util
{
    public class TransformSetTargetObjectHeight : MonoBehaviour
    {
        [SerializeField] Transform _targetTransform;
        // horizontal mean it vertical bar but it slider left right that set object height
        [SerializeField] bool _sliderIsHorizontal;
        [SerializeField] float _moveMultiplier = 1;
        Vector3 _startPos;

        void Awake() => _startPos = transform.localPosition;

        float GetOffsetY() => -(_startPos.y - transform.localPosition.y);
        float GetOffsetX() => -(_startPos.x - transform.localPosition.x);

        void Update() => UpdatePinPosToSetTargetObjectHeight();

        void UpdatePinPosToSetTargetObjectHeight()
        {
            if (_sliderIsHorizontal)
            {
                _targetTransform.position = new Vector3(_targetTransform.position.x, GetOffsetX() * _moveMultiplier, _targetTransform.position.z);
            }
            else
            {
                _targetTransform.position = new Vector3(_targetTransform.position.x, GetOffsetY() * _moveMultiplier, _targetTransform.position.z);
            }
        }
    }
}
