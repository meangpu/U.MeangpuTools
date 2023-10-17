namespace Meangpu.Datatype
{
    [System.Serializable]
    public struct StringWithBool
    {
        public string stringValue;
        public bool boolValue;

        public StringWithBool(string _stringValue, bool _boolValue)
        {
            boolValue = _boolValue;
            stringValue = _stringValue;
        }
    }
}