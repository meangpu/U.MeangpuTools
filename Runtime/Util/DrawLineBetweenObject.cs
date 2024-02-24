using UnityEngine;

namespace Meangpu.Util
{
    public class DrawLineBetweenObject : MonoBehaviour
    {
        [SerializeField] LineRenderer _line;

        [SerializeField] protected Transform _firstTarget;
        [SerializeField] protected Transform _secondTarget;

        public void ChangeFirstTarget(Transform newTrans) => _firstTarget = newTrans;
        public void ChangeSecondTarget(Transform newTrans) => _secondTarget = newTrans;

        public Transform FirstTarget => _firstTarget;
        public Transform SecondTarget => _secondTarget;

        private void Start() => _line.positionCount = 2;

        protected virtual void UpdateLinePos()
        {
            _line.SetPosition(0, _firstTarget.position);
            _line.SetPosition(1, _secondTarget.position);
        }

        private void Update() => UpdateLinePos();
    }

}