using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Indigo.Infrastructure.Support;
using Indigo.Infrastructure.Util;
using Indigo.Modules;

namespace Indigo.Security
{
    public class User : UserEntity<string>, IPrincipal
    {
        private ISet<Function> _allFunctions;
        private ICollection<Function> _functions = new HashSet<Function>();
        private ICollection<Role> _roles = new HashSet<Role>();

        public virtual string Name { get; protected internal set; }
        public virtual string Password { get; protected internal set; }
        public virtual string Email { get; protected internal set; }

        public virtual bool IsOnline { get; protected internal set; }
        public virtual DateTime? LastSignInTime { get; protected internal set; }
        public virtual DateTime? LastSignOutTime { get; protected internal set; }
        public virtual TimeSpan TotalOnlineTime { get; protected internal set; }
        public virtual int TotalSignInCount { get; protected internal set; }

        protected virtual ICollection<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        protected virtual ICollection<Function> Functions
        {
            get { return _functions; }
            set { _functions = value; }
        }

        protected virtual ISet<Function> AllFunctions
        {
            get
            {
                if (_allFunctions == null)
                {
                    _allFunctions = new HashSet<Function>(Functions);

                    foreach (Role role in Roles)
                    {
                        _allFunctions.UnionWith(role.GetFunctions());
                    }
                }

                return _allFunctions;
            }
        }

        public virtual IIdentity Identity { get; set; }

        public virtual bool IsInRole(string role)
        {
            return Roles.Any(r => StringUtils.Equals(r.Name, role));
        }

        public virtual bool IsAdmin()
        {
            return Roles.Any(r => r.IsAdmin);
        }

        public virtual bool IsPermitted(Function function)
        {
            if (function == null || Functions.Contains(function)) return true;

            return Roles.Any(role => role.IsPermitted(function));
        }

        public virtual bool Contains(Role role)
        {
            return Roles.Any(r => r.Contains(role));
        }

        public virtual bool Contains(User user)
        {
            if (IsAdmin()) return !user.IsAdmin();

            if (AllFunctions.Count <= user.AllFunctions.Count) return false;

            return user.AllFunctions.All(function => AllFunctions.Contains(function));
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

            _allFunctions = null;
        }

        protected internal virtual void RemoveRole(Role role)
        {
            role.Users.Remove(this);
            Roles.Remove(role);

            _allFunctions = null;
        }

        protected internal virtual void ClearRoles()
        {
            foreach (Role role in Roles.ToArray())
            {
                RemoveRole(role);
            }

            _allFunctions = null;
        }

        protected internal virtual void AddFunction(Function function)
        {
            Functions.Add(function);

            _allFunctions = null;
        }

        protected internal virtual void RemoveFunction(Function function)
        {
            Functions.Remove(function);

            _allFunctions = null;
        }

        protected internal virtual void ClearFunctions()
        {
            Functions.Clear();

            _allFunctions = null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is User)) return false;

            var rhs = (User) obj;
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