using Indigo.Infrastructure.Data;

namespace Indigo.Modules.Data
{
    public interface IModuleDao : IGenericDao<Module, string>
    {
        Module GetByName(string name);
    }
}