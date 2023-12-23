using System.Collections.Generic;
using UnityEngine;

namespace Meangpu.Util
{
    public class MeRotateManager : MeSingleton<MeRotateManager>
    {
        // learn this pattern from: [PERFECT Way to Rotate Coins | Unity Beginner Tutorial - YouTube](https://www.youtube.com/watch?v=pztDm7X5E9g)
        // use this to prevent singleton error [Some objects were not cleaned up when closing the scene - Questions & Answers - Unity Discussions](https://discussions.unity.com/t/some-objects-were-not-cleaned-up-when-closing-the-scene/177426)

        [SerializeField] float _rotateSpeed = 60;
        [SerializeField] Vector3 _rotateAxis = Vector3.up;
        Quaternion _rotation;
        List<MeRotateObj> _rotateTargets;

        protected override void Awake()
        {
            base.Awake();
            _rotation = Quaternion.identity;
            _rotateTargets = new();
        }

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