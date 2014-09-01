using System.Collections.Generic;
using System.Linq;
using Indigo.Infrastructure;
using Indigo.Infrastructure.Support;
using Indigo.Infrastructure.Util;

namespace Indigo.Modules
{
    public class Component : Entity<string>
    {
        private ICollection<Function> _functions = new HashSet<Function>();

        public virtual Module Module { get; protected internal set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Ordinal { get; set; }

        protected virtual ICollection<Function> Functions
        {
            get { return _functions; }
            set { _functions = value; }
        }

        public virtual void AddFunction(Function function)
        {
            if (function.Component != null)
                function.Component.RemoveFunction(function);

            if (!Functions.Contains(function))
                Functions.Add(function);

            function.Component = this;
        }

        public virtual void RemoveFunction(Function function)
        {
            if (Equals(function.Component))
                function.Component = null;

            Functions.Remove(function);
        }

        public virtual IList<Function> GetFunctions()
        {
            return Functions.ToList().AsReadOnly();
        }

        public virtual Function GetFunction(string name)
        {
            return Functions.SingleOrDefault(f => StringUtils.Equals(f.Name, name, true));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Component)) return false;

            var rhs = (Component) obj;
            return new EqualsBuilder()
                .Append(Module, rhs.Module)
                .Append(Name, rhs.Name)
                .IsEquals();
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder()
                .Append(Module)
                .Append(Name)
                .HashCode;
        }

        public override string ToString()
        {
            return Title ?? Name;
        }
    }
}