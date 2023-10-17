namespace Meangpu.Datatype
{
    [System.Serializable]
    public struct IntWithBool
    {
        public int intValue;
        public bool boolValue;

        public IntWithBool(int _intValue, bool _boolValue)
        {
            boolValue = _boolValue;
            intValue = _intValue;
        }
    }
}