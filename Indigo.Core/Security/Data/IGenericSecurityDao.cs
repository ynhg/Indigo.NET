using Indigo.Infrastructure.Data;
using System.Collections.Generic;

namespace Indigo.Security.Data
{
    public interface IGenericSecurityDao<T, ID> : IGenericDao<T, ID> where T : UserEntity<ID>
    {
        ID Save(T entity, User user);
        void Update(T entity, User user);
        void Delete(T entity, User user);
        T GetById(ID id, User user);
        T GetReferenceById(ID id, User user);
        IList<T> FindAll(User user);
    }
}
