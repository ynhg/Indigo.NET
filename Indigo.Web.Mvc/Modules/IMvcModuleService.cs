using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indigo.Modules
{
    public interface IMvcModuleService : IModuleService
    {
        Component GetComponent(string controllerName, string area);
    }
}
