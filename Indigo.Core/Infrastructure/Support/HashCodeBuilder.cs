namespace Indigo.Infrastructure.Util
{
    public sealed class HashCodeBuilder
    {
        private readonly int constant;
        private int total;

        public HashCodeBuilder()
        {
            constant = 31;
            total = 17;
        }

        public HashCodeBuilder AppendBase(int baseHashCode)
        {
            total = total * constant + baseHashCode;

            return this;
        }

        public HashCodeBuilder Append(object obj)
        {
            total = total * constant + (obj == null ? 0 : obj.GetHashCode());

            return this;
        }

        public int HashCode
        {
            get { return total; }
        }
    }
}
