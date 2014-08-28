using Indigo.Infrastructure.Util;
using Indigo.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Indigo.Security
{
    public class User : UserEntity<string>, IPrincipal
    {
        private ICollection<Role> roles = new HashSet<Role>();
        private ICollection<Function> functions = new HashSet<Function>();
        private ISet<Function> allFunctions;

        public virtual string Name { get; protected internal set; }
        public virtual string Password { get; protected internal set; }
        public virtual string Email { get; protected internal set; }

        public virtual IIdentity Identity { get; set; }

        public virtual bool IsOnline { get; protected internal set; }
        public virtual DateTime? LastSignInTime { get; protected internal set; }
        public virtual DateTime? LastSignOutTime { get; protected internal set; }
        public virtual TimeSpan TotalOnlineTime { get; protected internal set; }
        public virtual int TotalSignInCount { get; protected internal set; }

        protected virtual ICollection<Role> Roles { get { return roles; } set { roles = value; } }
        protected virtual ICollection<Function> Functions { get { return functions; } set { functions = value; } }
        protected virtual ISet<Function> AllFunctions
        {
            get
            {
                if (allFunctions == null)
                {
                    allFunctions = new HashSet<Function>(Functions);

                    foreach (Role role in Roles)
                    {
                        allFunctions.UnionWith(role.GetFunctions());
                    }
                }

                return allFunctions;
            }
        }

        public virtual bool IsAdmin()
        {
            return Roles.Any(r => r.IsAdmin);
        }

        public virtual bool IsInRole(string role)
        {
            return Roles.Any(r => StringUtils.Equals(r.Name, role));
        }

        public virtual bool IsPermitted(Function function)
        {
            if (function == null || Functions.Contains(function)) return true;

            foreach (var role in Roles)
            {
                if (role.IsPermitted(function)) return true;
            }

            return false;
        }

        public virtual bool Contains(Role role)
        {
            foreach (Role r in Roles)
            {
                if (r.Contains(role)) return true;
            }

            return false;
        }

        public virtual bool Contains(User user)
        {
            if (IsAdmin()) return !user.IsAdmin();

            if (AllFunctions.Count <= user.AllFunctions.Count) return false;

            foreach (Function function in user.AllFunctions)
            {
                if (!AllFunctions.Contains(function))
                    return false;
            }

            return true;
        }

        public virtual ICollection<Role> GetRoles()
        {
            return Roles.ToList().AsReadOnly();
        }

        public virtual ICollection<Function> GetFunctions()
        {
            return AllFunctions.ToList().AsReadOnly();
        }

        protected internal virtual void AddRole(Role role)
        {
            role.Users.Add(this);
            Roles.Add(role);

            allFunctions = null;
        }

        protected internal virtual void RemoveRole(Role role)
        {
            role.Users.Remove(this);
            Roles.Remove(role);

            allFunctions = null;
        }

        protected internal virtual void ClearRoles()
        {
            foreach (Role role in Roles.ToArray())
            {
                RemoveRole(role);
            }

            allFunctions = null;
        }

        protected internal virtual void AddFunction(Function function)
        {
            Functions.Add(function);

            allFunctions = null;
        }

        protected internal virtual void RemoveFunction(Function function)
        {
            Functions.Remove(function);

            allFunctions = null;
        }

        protected internal virtual void ClearFunctions()
        {
            Functions.Clear();

            allFunctions = null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (!typeof(User).IsInstanceOfType(obj)) return false;

            var rhs = (User)obj;
            return new EqualsBuilder()
                .Append(Name, rhs.Name)
                .Append(Email, rhs.Email)
                .IsEquals();
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(Name)
                .Append(Email)
                .HashCode;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
