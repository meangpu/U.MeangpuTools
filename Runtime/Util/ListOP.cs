using System.Collections.Generic;

namespace Meangpu.Util
{
    public static class ListOP
    {
        public static void Shuffle<T>(IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                (ts[r], ts[i]) = (ts[i], ts[r]);
            }
        }

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