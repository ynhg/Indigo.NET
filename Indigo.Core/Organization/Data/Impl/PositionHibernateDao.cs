using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data.Impl;
using Spring.Stereotype;
using System.Collections.Generic;

namespace Indigo.Organization.Data.Impl
{
    [Repository]
    public class PositionHibernateDao : GenericSecurityHibernateDao<Position, string>, IPositionDao
    {
        public Position GetByName(string name)
        {
            var query = CreateQuery("from Position where Name = :name").SetString("name", name);
            return query.UniqueResult<Position>();
        }

        public override IList<Position> FindAll()
        {
            return QueryOver().OrderBy(e => e.Rank).Asc.List();
        }

        public Page<Position> Search(PositionSearchForm searchForm)
        {
            var query = QueryOver();

            query = query.OrderBy(e => e.Rank).Asc;

            return GetPage(query, searchForm);
        }
    }
}
