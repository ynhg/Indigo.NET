using Indigo.Infrastructure.Util;
using Indigo.Modules;
using System.Collections.Generic;
using System.Linq;

namespace Indigo.Security
{
    public class Role : UserEntity<string>
    {
        private ICollection<Function> functions = new HashSet<Function>();
        private ICollection<User> users = new HashSet<User>();

        public virtual bool IsAdmin { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        protected virtual ICollection<Function> Functions { get { return functions; } set { functions = value; } }
        protected internal virtual ICollection<User> Users { get { return users; } set { users = value; } }

        public virtual bool IsPermitted(Function function)
        {
            if (IsAdmin) return true;

            return Functions.Contains(function);
        }

        public virtual bool Contains(Role role)
        {
            if (IsAdmin) return !role.IsAdmin;

            if (Functions.Count <= role.Functions.Count) return false;

            foreach (Function function in role.Functions)
            {
                if (!Functions.Contains(function))
                    return false;
            }

            return true;
        }

        public virtual ICollection<User> GetUsers()
        {
            return Users.ToList().AsReadOnly();
        }

        public virtual ICollection<Function> GetFunctions()
        {
            return Functions.ToList().AsReadOnly();
        }

        protected internal virtual void AddFunction(Function function)
        {
            Functions.Add(function);
        }

        protected internal virtual void RemoveFunction(Function function)
        {
            Functions.Remove(function);
        }

        protected internal virtual void ClearFunctions()
        {
            Functions.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (!typeof(Role).IsInstanceOfType(obj)) return false;

            var rhs = (Role)obj;
            return new EqualsBuilder()
                .Append(Name, rhs.Name)
                .IsEquals();
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(Name)
                .HashCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
