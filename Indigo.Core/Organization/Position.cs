using Indigo.Security;

namespace Indigo.Organization
{
    public class Position : UserEntity<string>
    {
        public virtual string Name { get; set; }
        public virtual int Rank { get; set; }
    }
}
