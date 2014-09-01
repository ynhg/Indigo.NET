using Indigo.Infrastructure.Data;

namespace Indigo.Modules.Data
{
    public interface IComponentDao : IGenericDao<Component, string>
    {
        Component GetByName(string name);
    }
}