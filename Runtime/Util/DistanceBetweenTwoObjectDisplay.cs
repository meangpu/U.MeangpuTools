using TMPro;
using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class DistanceBetweenTwoObjectDisplay : MonoBehaviour
    {
        [SerializeField] protected Transform _firstTarget;
        [SerializeField] protected Transform _secondTarget;
        [SerializeField] protected TMP_Text _distanceTxt;
        [SerializeField] protected float _scaleFactor = 1;
        protected float _nowDistance;
        [SerializeField] protected bool _haveParentScaleFactor;
        [ShowIf("_haveParentScaleFactor")]
        [SerializeField] Transform _parentScale;

        public void ChangeFirstTarget(Transform newTrans) => _firstTarget = newTrans;
        public void ChangeSecondTarget(Transform newTrans) => _secondTarget = newTrans;
        public void ChangeScale(float newScale) => _scaleFactor = newScale;

        public Transform FirstTarget => _firstTarget;
        public Transform SecondTarget => _secondTarget;

        private void Update()
        {
            if (_firstTarget != null && _secondTarget != null && _distanceTxt != null)
            {
                CalculateDistance();
                UpdateTextDistance();
            };
        }

        protected virtual void CalculateDistance()
        {
            _nowDistance = Vector3.Distance(_firstTarget.position, _secondTarget.position) * _scaleFactor;
            if (!_haveParentScaleFactor) return;
            _nowDistance /= _parentScale.localScale.x;
        }

        protected virtual void UpdateTextDistance()
        {
            _distanceTxt.SetText(_nowDistance.ToString("F2"));
        }
    }
}