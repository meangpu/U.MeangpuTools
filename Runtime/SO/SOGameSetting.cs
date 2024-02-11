using UnityEngine;

namespace Meangpu
{
    public abstract class SOGameSetting : ScriptableObject
    {
        static SOGameSetting _instance;
        public static SOGameSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SOGameSetting>("GameSetting");
                }
                return _instance;
            }
        }
    }
}
