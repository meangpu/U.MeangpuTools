using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    [ExecuteInEditMode]
    public class GridSetTransform : MonoBehaviour
    {
        [SerializeField] Transform[] transformToSet;
        [SerializeField] Transform _parentTrans;

        [SerializeField] Vector3 _nowOffset = new();
        int _gridXNow;
        int _gridYNow;
        [SerializeField] int _gridXCount = 3;
        [SerializeField] float _gridXOffset = .12f;
        [SerializeField] float _gridYOffset = 0.06f;
        [SerializeField] bool _doLoadAllTheTime = true;

        private void Start()
        {
            if (_doLoadAllTheTime)
            {
                SpawnAllUIBtn();
            }
        }

        [Button]
        public void LoadChildTransform()
        {
            if (_parentTrans == null) _parentTrans = transform;
            transformToSet = _parentTrans.GetComponentsInChildren<Transform>();
        }

        [Button]
        public void SpawnAllUIBtn()
        {
            _gridXNow = 0;
            _gridYNow = 0;

            int YCount = transformToSet.Length / _gridXCount;
            float removeX = _gridXOffset * (_gridXCount + 1) * 0.5f;
            float removeY = _gridYOffset * (YCount + 2) * 0.5f;

            _nowOffset = Vector3.zero;
            foreach (Transform item in transformToSet)
            {
                _nowOffset = new Vector3((_gridXNow * _gridXOffset) - removeX, (_gridYNow * _gridYOffset) - removeY, 0);
                item.localPosition = _nowOffset;

                if (_gridXNow % _gridXCount == 0)
                {
                    _gridXNow = 0;
                    _gridYNow++;
                }

                _gridXNow++;

            }
        }

    }
}