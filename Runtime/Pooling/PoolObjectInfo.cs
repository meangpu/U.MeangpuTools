using UnityEngine;
using System.Collections.Generic;

namespace Meangpu.Pool
{
    public class PoolObjectInfo
    {
        public string Id;
        public List<GameObject> InactiveObj = new();
    }
}