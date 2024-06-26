using System;
using UnityEngine;

namespace Meangpu
{
    [Serializable]
    public class Vector2Reference
    {
        public bool UseConstant = false;
        public Vector2 ConstantValue;
        public Vector2Variable Variable;
        public Vector2Reference() { }
        public Vector2Reference(Vector2 value)
        {
            UseConstant = false;
            ConstantValue = value;
        }
        public Vector2 Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
        public static implicit operator Vector2(Vector2Reference reference) => reference.Value;
    }
}