using Indigo.Infrastructure.Search;
using Indigo.Security.Search;
using NHibernate;
using NHibernate.Criterion;
using Spring.Stereotype;

namespace Indigo.Security.Data.Impl
{
    [Repository]
    public class UserHibernateDao : GenericSecurityHibernateDao<User, string>, IUserDao
    {
        public Page<User> Search(UserSearchForm searchForm)
        {
            IQueryOver<User, User> query = QueryOver();

            if (!string.IsNullOrWhiteSpace(searchForm.Name))
            {
                query.AndRestrictionOn(u => u.Name).IsLike(searchForm.Name, MatchMode.Anywhere);
            }

            query = query.OrderBy(u => u.Created).Desc;

            return GetPage(query, searchForm);
        }

        public User GetByName(string name)
        {
            IQuery query = CreateQuery("from User where Name = :name").SetString("name", name);
            return query.UniqueResult<User>();
        }
    }
}