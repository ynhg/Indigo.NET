using Indigo.Infrastructure.Data.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indigo.Security.Data.Impl
{
    public abstract class GenericSecurityHibernateDao<T, ID> : GenericHibernateDao<T, ID>, IGenericSecurityDao<T, ID> where T : UserEntity<ID>
    {
        public override ID Save(T entity)
        {
            return Save(entity, null);
        }

        public ID Save(T entity, User user)
        {
            entity.Created = DateTime.Now;
            entity.CreatedBy = user;

            return base.Save(entity);
        }

        public void Update(T entity, User user)
        {
            entity.CheckUpdatePermission(user);

            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = user;

            base.Update(entity);
        }

        public void Delete(T entity, User user)
        {
            entity.CheckDeletePermission(user);

            base.Delete(entity);
        }

        public T GetById(ID id, User user)
        {
            T entity = base.GetById(id);

            if (entity == null) return null;

            entity.CheckGetPermission(user);

            return entity;
        }

        public T GetReferenceById(ID id, User user)
        {
            return GetById(id, user);
        }

        public IList<T> FindAll(User user)
        {
            return base.FindAll().Where(e => e.IsAllowedToGetBy(user)).ToList();
        }
    }
}
