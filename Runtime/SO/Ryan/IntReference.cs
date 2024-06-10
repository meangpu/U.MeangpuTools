// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;

namespace Meangpu
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = false;
        public int ConstantValue;
        public IntVariable Variable;
        public IntReference() { }
        public IntReference(int value)
        {
            UseConstant = false;
            ConstantValue = value;
        }

        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator int(IntReference reference) => reference.Value;
    }
}