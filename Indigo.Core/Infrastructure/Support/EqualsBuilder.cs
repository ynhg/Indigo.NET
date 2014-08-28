namespace Indigo.Infrastructure.Util
{
    public sealed class EqualsBuilder
    {
        private bool equals;

        public EqualsBuilder()
        {
            equals = true;
        }

        public EqualsBuilder AppendBase(bool baseEquals)
        {
            if (equals == false) return this;

            equals = baseEquals;
            return this;
        }

        public EqualsBuilder Append(object lhs, object rhs)
        {
            if (equals == false) return this;

            if (object.ReferenceEquals(lhs, rhs)) return this;

            if (lhs == null || rhs == null)
            {
                equals = false;
                return this;
            }

            equals = lhs.Equals(rhs);

            return this;
        }

        public bool IsEquals()
        {
            return equals;
        }
    }
}
