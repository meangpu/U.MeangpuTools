using System.Collections.Generic;

namespace Meangpu.Util
{
    public static class ListShuffle
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
    }
}