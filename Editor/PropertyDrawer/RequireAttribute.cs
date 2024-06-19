using System;
using UnityEngine;

namespace Meangpu
{
    // from: [Required For Unity Development - YouTube](https://www.youtube.com/watch?v=BN6NXMHJ8v0)
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class RequireAttribute : PropertyAttribute { }
}