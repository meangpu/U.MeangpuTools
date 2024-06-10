
using System;
using UnityEngine;

namespace Meangpu
{
    [Serializable]
    public class Vector3Reference
    {
        public bool UseConstant = false;
        public Vector3 ConstantValue;
        public Vector3Variable Variable;
        public Vector3Reference() { }
        public Vector3Reference(Vector3 value)
        {
            UseConstant = false;
            ConstantValue = value;
        }
        public Vector3 Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
        public static implicit operator Vector3(Vector3Reference reference) => reference.Value;
    }
}