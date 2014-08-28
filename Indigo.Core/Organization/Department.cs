using Indigo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
