using System;
using Indigo.Infrastructure;
using Indigo.Infrastructure.Util;
using Indigo.Security.Exceptions;

namespace Indigo.Security
{
    public abstract class UserEntity<TId> : Entity<TId>
    {
        public virtual DateTime Created { get; protected internal set; }
        public virtual User CreatedBy { get; protected internal set; }
        public virtual DateTime? LastModified { get; protected internal set; }
        public virtual User LastModifiedBy { get; protected internal set; }

        public virtual void CheckGetPermission(User user)
        {
            if (!IsAllowedToGetBy(user))
                throw new UnauthorizedException("用户[{0}]无权获取实体[{1}]", user, this);
        }

        public virtual bool IsAllowedToGetBy(User user)
        {
            return true;
        }

        public virtual void CheckUpdatePermission(User user)
        {
            if (!IsAllowedToUpdateBy(user))
                throw new UnauthorizedException("用户[{0}]无权对实体[{1}]执行更新操作", user, this);
        }

        public virtual bool IsAllowedToUpdateBy(User user)
        {
            if (user == null) return false;
            if (user.IsAdmin()) return true;

            return ObjectUtils.Equals(CreatedBy, user);
        }

        public virtual void CheckDeletePermission(User user)
        {
            if (!IsAllowedToDeleteBy(user))
                throw new UnauthorizedException("用户[{0}]无权对实体[{1}]执行删除操作", user, this);
        }

        public virtual bool IsAllowedToDeleteBy(User user)
        {
            if (user == null) return false;
            if (user.IsAdmin()) return true;

            return ObjectUtils.Equals(CreatedBy, user);
        }
    }
}