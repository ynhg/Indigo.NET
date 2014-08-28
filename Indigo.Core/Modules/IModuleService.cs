using System.Collections.Generic;
using System.Reflection;

namespace Indigo.Modules
{
    public interface IModuleService
    {
        void BuildModules(params Assembly[] assemblies);
        IList<Module> GetModules();
        Module GetModule(string name);
        Component GetComponent(string name);
    }
}
