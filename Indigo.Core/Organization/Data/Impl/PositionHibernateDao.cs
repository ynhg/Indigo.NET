using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data.Impl;
using NHibernate;
using Spring.Stereotype;

namespace Indigo.Organization.Data.Impl
{
    [Repository]
    public class PositionHibernateDao : GenericSecurityHibernateDao<Position, string>, IPositionDao
    {
        public Position GetByName(string name)
        {
            IQuery query = CreateQuery("from Position where Name = :name").SetString("name", name);
            return query.UniqueResult<Position>();
        }

        public override IList<Position> FindAll()
        {
            return QueryOver().OrderBy(e => e.Rank).Asc.List();
        }

        public Page<Position> Search(PositionSearchForm searchForm)
        {
            IQueryOver<Position, Position> query = QueryOver();

            query = query.OrderBy(e => e.Rank).Asc;

            return GetPage(query, searchForm);
        }
    }
}