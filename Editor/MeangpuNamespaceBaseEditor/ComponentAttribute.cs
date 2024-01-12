using System;

namespace Meangpu
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public string Name { get; } = null;
        public string Description { get; } = null;

        public ComponentAttribute(string name, string description) : this(name)
        {
            Description = description;
        }

        public ComponentAttribute(string name)
        {
            Name = name;
        }
    }
}