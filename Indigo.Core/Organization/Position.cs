using Indigo.Security;

namespace Indigo.Organization
{
    public class Position : UserEntity<string>
    {
        public virtual string Name { get { return name; } set { name = value; } }
        public virtual int Rank { get { return rank; } set { rank = value; } }

        public Position() { }

        public Position(string name, int rank)
        {
            this.name = name;
            this.rank = rank;
        }

        public override string ToString()
        {
            return name;
        }

        private string name;
        private int rank;
    }
}
