using System.Collections.Generic;

namespace Meangpu.Util
{
    public static class CompareList
    {
        public static bool IsTwoListIsTheSame<T>(List<T> _listA, List<T> _listB)
        {
            if (_listA.Count != _listB.Count) return false;

            for (int i = 0; i < _listA.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(_listA[i], _listB[i])) return false;
            }

            return true;
        }
    }
}