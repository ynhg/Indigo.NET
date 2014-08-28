using Indigo.Infrastructure;
using Indigo.Infrastructure.Util;
using System.Collections.Generic;
using System.Linq;

namespace Indigo.Modules
{
    public class Module : Entity<string>
    {
        private ICollection<Component> components = new HashSet<Component>();

        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int Ordinal { get; set; }
        protected virtual ICollection<Component> Components { get { return components; } set { components = value; } }

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
            if (object.ReferenceEquals(this, obj)) return true;
            if (!typeof(Module).IsInstanceOfType(obj)) return false;

            var rhs = (Module)obj;
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
    }
}
