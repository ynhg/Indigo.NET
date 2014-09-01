namespace Indigo.Infrastructure.Support
{
    public sealed class HashCodeBuilder
    {
        private readonly int _constant;
        private int _total;

        public HashCodeBuilder()
        {
            _constant = 31;
            _total = 17;
        }

        public int HashCode
        {
            get { return _total; }
        }

        public HashCodeBuilder AppendBase(int baseHashCode)
        {
            _total = _total*_constant + baseHashCode;

            return this;
        }

        public HashCodeBuilder Append(object obj)
        {
            _total = _total*_constant + (obj == null ? 0 : obj.GetHashCode());

            return this;
        }
    }
}