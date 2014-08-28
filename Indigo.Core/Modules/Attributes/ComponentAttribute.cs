using System;

namespace Indigo.Modules.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Protect { get; set; }
        public int Ordinal { get; set; }

        public ComponentAttribute(string title)
            : this(title, null, true, 0)
        { }

        public ComponentAttribute(string title, string description)
            : this(title, description, true, 0)
        { }

        public ComponentAttribute(string title, bool protect)
            : this(title, null, true)
        { }

        public ComponentAttribute(string title, int ordinal)
            : this(title, null, ordinal)
        { }

        public ComponentAttribute(string title, string description, bool protect)
            : this(title, description, protect, 0)
        { }

        public ComponentAttribute(string title, string description, int ordinal)
            : this(title, description, true, ordinal)
        { }

        public ComponentAttribute(string title, bool protect, int ordinal)
            : this(title, null, protect, ordinal)
        { }

        public ComponentAttribute(string title, string description, bool protect, int ordinal)
        {
            Title = title;
            Description = description;
            Protect = protect;
            Ordinal = ordinal;
        }
    }
}
