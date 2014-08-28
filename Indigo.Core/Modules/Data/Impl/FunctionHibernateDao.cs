using Indigo.Infrastructure.Data.Impl;
using Spring.Stereotype;

namespace Indigo.Modules.Data.Impl
{
    [Repository]
    public class FunctionHibernateDao : GenericHibernateDao<Function, string>, IFunctionDao
    {
    }
}
