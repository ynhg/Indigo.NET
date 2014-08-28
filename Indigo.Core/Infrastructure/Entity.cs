using Indigo.Infrastructure.Util;

namespace Indigo.Infrastructure
{
    public abstract class Entity<ID>
    {
        public virtual ID Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (!typeof(Entity<ID>).IsInstanceOfType(obj)) return false;

            var rhs = (Entity<ID>)obj;
            return new EqualsBuilder()
                .Append(Id, rhs.Id)
                .IsEquals();
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(Id)
                .HashCode;
        }
    }
}
