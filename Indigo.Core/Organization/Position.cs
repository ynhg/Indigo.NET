using Indigo.Security;

namespace Indigo.Organization
{
    public class Position : UserEntity<string>
    {
        private string _name;
        private int _rank;

        public Position()
        {
        }

        public Position(string name, int rank)
        {
            _name = name;
            _rank = rank;
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}