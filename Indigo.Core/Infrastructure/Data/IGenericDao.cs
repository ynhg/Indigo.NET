using System.Collections.Generic;

namespace Indigo.Infrastructure.Data
{
    public interface IGenericDao<T, ID> where T : Entity<ID>
    {
        ID Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(ID id);
        T GetReferenceById(ID id);
        IList<T> FindAll();
    }
}