namespace Meangpu.Datatype
{
    [System.Serializable]
    public class FloatWithBool
    {
        public float floatValue;
        public bool boolValue;

        public FloatWithBool(float _floatValue, bool _boolValue)
        {
            boolValue = _boolValue;
            floatValue = _floatValue;
        }
    }
}