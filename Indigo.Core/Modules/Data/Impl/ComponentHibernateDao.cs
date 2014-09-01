using Indigo.Infrastructure.Data.Impl;
using NHibernate;
using Spring.Stereotype;

namespace Indigo.Modules.Data.Impl
{
    [Repository]
    public class ComponentHibernateDao : GenericHibernateDao<Component, string>, IComponentDao
    {
        public Component GetByName(string name)
        {
            IQuery query = CreateQuery("from Component where Name = :name").SetString("name", name);
            return query.SetCacheable(true).UniqueResult<Component>();
        }
    }
}