using Indigo.Infrastructure.Data.Impl;
using NHibernate;
using Spring.Stereotype;

namespace Indigo.Modules.Data.Impl
{
    [Repository]
    public class ModuleHibernateDao : GenericHibernateDao<Module, string>, IModuleDao
    {
        public Module GetByName(string name)
        {
            IQuery query = CreateQuery("from Module where Name = :name").SetString("name", name);
            return query.SetCacheable(true).UniqueResult<Module>();
        }
    }
}