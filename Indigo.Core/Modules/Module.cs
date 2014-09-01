using System.Collections.Generic;
using System.Linq;
using Indigo.Infrastructure;
using Indigo.Infrastructure.Support;

namespace Indigo.Modules
{
    public class Module : Entity<string>
    {
        private ICollection<Component> _components = new HashSet<Component>();

        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Ordinal { get; set; }

        protected virtual ICollection<Component> Components
        {
            get { return _components; }
            set { _components = value; }
        }

        public virtual void AddComponent(Component component)
        {
            if (component.Module != null)
                component.Module.RemoveComponent(component);

            if (!Components.Contains(component))
                Components.Add(component);

            component.Module = this;
        }

        public virtual void RemoveComponent(Component component)
        {
            if (Equals(component.Module))
                component.Module = null;

            Components.Remove(component);
        }

        public virtual IList<Component> GetComponents()
        {
            return Components.ToList().AsReadOnly();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Module)) return false;

            var rhs = (Module) obj;
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
            return Title ?? Name;
        }
    }
}