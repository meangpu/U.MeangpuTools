using System.Collections.Generic;
using UnityEngine;

namespace Meangpu.Util
{
    public static class SpawnUsingList
    {
        public static void SpawnChildUsingList<T>(List<T> childList, Transform _parent) where T : Object
        {
            KillAllChild.KillAllChildInTransform(_parent);
            foreach (T obj in childList)
            {
                GameObject nowObj = new(obj.name);
                nowObj.transform.SetParent(_parent);
            }
        }
    }
}