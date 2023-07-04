using UnityEngine;
using System;

namespace Meangpu.Util
{
    public class TransformConstrain : MonoBehaviour
    {
        [Serializable]
        public class TranslateConstraints
        {
            public FloatConstraint MinX;
            public FloatConstraint MaxX;
            public FloatConstraint MinY;
            public FloatConstraint MaxY;
            public FloatConstraint MinZ;
            public FloatConstraint MaxZ;
        }

        [Serializable]
        public class FloatConstraint
        {
            public bool Constrain;
            public float Value;
        }

        [SerializeField]
        private TranslateConstraints _constraints =
            new()
            {
                MinX = new FloatConstraint(),
                MaxX = new FloatConstraint(),
                MinY = new FloatConstraint(),
                MaxY = new FloatConstraint(),
                MinZ = new FloatConstraint(),
                MaxZ = new FloatConstraint()
            };

        private TranslateConstraints _parentConstraints;
        private Vector3 _initialPosition;
        private Vector3 constrainedPosition;

        public void Awake()
        {
            _initialPosition = transform.position;
            GenerateParentConstraints();
        }

        private void GenerateParentConstraints()
        {
            _parentConstraints = new TranslateConstraints
            {
                MinX = new FloatConstraint(),
                MinY = new FloatConstraint(),
                MinZ = new FloatConstraint(),
                MaxX = new FloatConstraint(),
                MaxY = new FloatConstraint(),
                MaxZ = new FloatConstraint()
            };

            if (_constraints.MinX.Constrain)
            {
                _parentConstraints.MinX.Constrain = true;
                _parentConstraints.MinX.Value = _constraints.MinX.Value + _initialPosition.x;
            }
            if (_constraints.MaxX.Constrain)
            {
                _parentConstraints.MaxX.Constrain = true;
                _parentConstraints.MaxX.Value = _constraints.MaxX.Value + _initialPosition.x;
            }
            if (_constraints.MinY.Constrain)
            {
                _parentConstraints.MinY.Constrain = true;
                _parentConstraints.MinY.Value = _constraints.MinY.Value + _initialPosition.y;
            }
            if (_constraints.MaxY.Constrain)
            {
                _parentConstraints.MaxY.Constrain = true;
                _parentConstraints.MaxY.Value = _constraints.MaxY.Value + _initialPosition.y;
            }
            if (_constraints.MinZ.Constrain)
            {
                _parentConstraints.MinZ.Constrain = true;
                _parentConstraints.MinZ.Value = _constraints.MinZ.Value + _initialPosition.z;
            }
            if (_constraints.MaxZ.Constrain)
            {
                _parentConstraints.MaxZ.Constrain = true;
                _parentConstraints.MaxZ.Value = _constraints.MaxZ.Value + _initialPosition.z;
            }
        }

        public void UpdateTransform()
        {
            constrainedPosition = transform.position;
            if (_parentConstraints.MinX.Constrain)
            {
                constrainedPosition.x = Mathf.Max(constrainedPosition.x, _parentConstraints.MinX.Value);
            }
            if (_parentConstraints.MaxX.Constrain)
            {
                constrainedPosition.x = Mathf.Min(constrainedPosition.x, _parentConstraints.MaxX.Value);
            }
            if (_parentConstraints.MinY.Constrain)
            {
                constrainedPosition.y = Mathf.Max(constrainedPosition.y, _parentConstraints.MinY.Value);
            }
            if (_parentConstraints.MaxY.Constrain)
            {
                constrainedPosition.y = Mathf.Min(constrainedPosition.y, _parentConstraints.MaxY.Value);
            }
            if (_parentConstraints.MinZ.Constrain)
            {
                constrainedPosition.z = Mathf.Max(constrainedPosition.z, _parentConstraints.MinZ.Value);
            }
            if (_parentConstraints.MaxZ.Constrain)
            {
                constrainedPosition.z = Mathf.Min(constrainedPosition.z, _parentConstraints.MaxZ.Value);
            }

            transform.position = constrainedPosition;
        }

        private void Update() => UpdateTransform();
    }
}