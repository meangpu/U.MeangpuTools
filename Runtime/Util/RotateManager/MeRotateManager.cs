using System.Collections.Generic;
using UnityEngine;

namespace Meangpu.Util
{
    public class MeRotateManager : MonoBehaviour
    {
        // learn this pattern from: [PERFECT Way to Rotate Coins | Unity Beginner Tutorial - YouTube](https://www.youtube.com/watch?v=pztDm7X5E9g)
        [SerializeField] float _rotateSpeed = 10;
        [SerializeField] Vector3 _rotateAxis = Vector3.up;
        Quaternion _rotation;
        List<MeRotateObj> _rotateTargets;

        #region SINGLETON PATTERN
        public static MeRotateManager Instance;
        private void Awake()
        {
            Instance = this;
            _rotation = Quaternion.identity;
            _rotateTargets = new();
        }
        #endregion

        private void Update()
        {
            _rotation *= Quaternion.Euler(_rotateAxis * _rotateSpeed * Time.deltaTime);
            foreach (MeRotateObj target in _rotateTargets) target.transform.rotation = _rotation;

#if UNITY_EDITOR
            Debug.Log($"{_rotateTargets.Count}");
#endif
        }

        public void Register(MeRotateObj newTarget)
        {
            if (!_rotateTargets.Contains(newTarget))
            {
                _rotateTargets.Add(newTarget);
            }
        }

        public void Unregister(MeRotateObj newTarget)
        {
            if (_rotateTargets.Contains(newTarget))
            {
                _rotateTargets.Remove(newTarget);
            }
        }
    }
}