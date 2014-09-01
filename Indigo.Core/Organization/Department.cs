using System.Collections.Generic;
using Indigo.Security;

namespace Indigo.Organization
{
    public class Department : UserEntity<string>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Ordinal { get; set; }

        public virtual Department SuperDepartment { get; set; }
        public virtual ICollection<Department> SubDepartments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}