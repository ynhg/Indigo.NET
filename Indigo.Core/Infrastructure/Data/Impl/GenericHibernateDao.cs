using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using NHibernate;
using Spring.Objects.Factory.Attributes;

namespace Indigo.Infrastructure.Data.Impl
{
    public abstract class GenericHibernateDao<T, TId> : IGenericDao<T, TId> where T : Entity<TId>
    {
        protected ISession CurrentSession
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        [Autowired]
        public ISessionFactory SessionFactory { get; set; }

        public virtual TId Save(T entity)
        {
            return (TId) CurrentSession.Save(entity);
        }

        public virtual void Update(T entity)
        {
            CurrentSession.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            CurrentSession.Delete(entity);
        }

        public virtual T GetById(TId id)
        {
            return CurrentSession.Get<T>(id);
        }

        public virtual T GetReferenceById(TId id)
        {
            return CurrentSession.Load<T>(id);
        }

        public virtual IList<T> FindAll()
        {
            return QueryOver().List();
        }

        protected Page<T> GetPage(IQueryOver<T, T> queryOver, SearchForm searchForm)
        {
            return GetPage(queryOver, searchForm.PageNumber, searchForm.PageSize);
        }

        protected Page<T> GetPage(IQueryOver<T, T> queryOver, int pageNumber, int pageSize)
        {
            int firstResult = (pageNumber - 1)*pageSize;
            int totalRecords = queryOver.RowCount();
            IList<T> records = queryOver.Skip(firstResult).Take(pageSize).List();

            return new Page<T>(records, totalRecords, pageNumber, pageSize);
        }

        protected IQuery CreateQuery(string queryString)
        {
            return CurrentSession.CreateQuery(queryString);
        }

        protected ICriteria CreateCriteria()
        {
            return CurrentSession.CreateCriteria<T>();
        }

        protected IQueryOver<T, T> QueryOver()
        {
            return CurrentSession.QueryOver<T>();
        }
    }
}