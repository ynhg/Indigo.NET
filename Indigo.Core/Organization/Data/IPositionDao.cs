using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data;

namespace Indigo.Organization.Data
{
    public interface IPositionDao : IGenericSecurityDao<Position, string>
    {
        Position GetByName(string name);
        Page<Position> Search(PositionSearchForm searchForm);
    }
}