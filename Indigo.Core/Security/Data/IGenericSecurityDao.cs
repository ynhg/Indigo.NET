using System.Collections.Generic;
using Indigo.Infrastructure.Data;

namespace Indigo.Security.Data
{
    public interface IGenericSecurityDao<T, TId> : IGenericDao<T, TId> where T : UserEntity<TId>
    {
        TId Save(T entity, User user);
        void Update(T entity, User user);
        void Delete(T entity, User user);
        T GetById(TId id, User user);
        T GetReferenceById(TId id, User user);
        IList<T> FindAll(User user);
    }
}