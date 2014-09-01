using System;

namespace Indigo.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FunctionAttribute : Attribute
    {
        public FunctionAttribute(string title)
            : this(title, null, 0)
        {
        }

        public FunctionAttribute(string title, string description)
            : this(title, description, 0)
        {
        }

        public FunctionAttribute(string title, string description, bool protect)
            : this(title, description, protect, 0)
        {
        }

        public FunctionAttribute(string title, string description, bool protect, int ordinal)
            : this(title, description, ordinal)
        {
            Protect = protect;
        }

        public FunctionAttribute(string title, string description, int ordinal)
        {
            Title = title;
            Description = description;
            Ordinal = ordinal;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Protect { get; set; }
        public int Ordinal { get; set; }
    }
}