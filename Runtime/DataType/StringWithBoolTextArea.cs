using UnityEngine;
namespace Meangpu.Datatype
{
    [System.Serializable]
    public class StringWithBoolTextArea
    {
        [TextArea]
        public string stringValue;
        public bool boolValue;

        public StringWithBoolTextArea(string _stringValue, bool _boolValue)
        {
            boolValue = _boolValue;
            stringValue = _stringValue;
        }
    }
}