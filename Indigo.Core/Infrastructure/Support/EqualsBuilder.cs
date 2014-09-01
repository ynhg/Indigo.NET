namespace Indigo.Infrastructure.Support
{
    public sealed class EqualsBuilder
    {
        private bool _equals;

        public EqualsBuilder()
        {
            _equals = true;
        }

        public EqualsBuilder AppendBase(bool baseEquals)
        {
            if (_equals == false) return this;

            _equals = baseEquals;
            return this;
        }

        public EqualsBuilder Append(object lhs, object rhs)
        {
            if (_equals == false) return this;

            if (ReferenceEquals(lhs, rhs)) return this;

            if (lhs == null || rhs == null)
            {
                _equals = false;
                return this;
            }

            _equals = lhs.Equals(rhs);

            return this;
        }

        public bool IsEquals()
        {
            return _equals;
        }
    }
}