using Indigo.Infrastructure.Support;

namespace Indigo.Infrastructure
{
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Entity<TId>)) return false;

            var rhs = (Entity<TId>) obj;
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