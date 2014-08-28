using Indigo.Infrastructure;
using Indigo.Infrastructure.Util;

namespace Indigo.Modules
{
    public class Function : Entity<string>
    {
        public virtual Component Component { get; protected internal set; }
        public virtual string Name { get; set; }
        public virtual bool Protect { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Ordinal { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (!typeof(Function).IsInstanceOfType(obj)) return false;

            var rhs = (Function)obj;
            return new EqualsBuilder()
                .Append(Component, rhs.Component)
                .Append(Name, rhs.Name)
                .Append(Protect, rhs.Protect)
                .IsEquals();
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(Component)
                .Append(Name)
                .Append(Protect)
                .HashCode;
        }
    }
}
