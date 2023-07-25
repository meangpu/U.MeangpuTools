using UnityEngine;
using System;

namespace Meangpu
{
    // when used, tag above variable with [Expandable] keyword
    [AttributeUsage(AttributeTargets.Field)]
    public class ExpandableAttribute : PropertyAttribute
    {
    }
}