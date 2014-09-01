using System.Collections.Generic;
using System.Reflection;
using Indigo.Modules.Data;
using Spring.Objects.Factory.Attributes;
using Spring.Transaction.Interceptor;

namespace Indigo.Modules
{
    public abstract class BaseModuleService : IModuleService
    {
        [Autowired]
        public IModuleDao ModuleDao { get; set; }

        [Autowired]
        public IComponentDao ComponentDao { get; set; }

        public abstract void BuildModules(params Assembly[] assemblies);

        [Transaction(ReadOnly = true)]
        public IList<Module> GetModules()
        {
            return ModuleDao.FindAll();
        }

        [Transaction(ReadOnly = true)]
        public Module GetModule(string name)
        {
            return ModuleDao.GetByName(name);
        }

        [Transaction(ReadOnly = true)]
        public Component GetComponent(string name)
        {
            return ComponentDao.GetByName(name);
        }
    }
}