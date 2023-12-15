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

        public static bool IsTwoListIsTheSame<T>(List<T> list1, List<T> list2, bool SortBeforeCompare = false)
        {

            // if sorting special class list item, the class need reference on how to compare list before sort,
            // so it would need ```IComparable<T>``` interface in that class,
            // SEE EXAMPLE IN ```ListOpTest```

            // ! have flaw on like list of my own create class, the value in class don't get all compared BEWARE!!!

            if (list1 == null && list2 == null) return true;
            if ((list1 == null && list2 != null) || (list1 != null && list2 == null)) return false;
            if (list1.Count != list2.Count) return false;

            if (SortBeforeCompare)
            {
                list1.Sort();
                list2.Sort();
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(list1[i], list2[i])) return false;
            }

            return true;
        }
    }
}