using System.Collections.Generic;

namespace Indigo.Infrastructure.Data
{
    /// <summary>
    /// 支持透明持久化的数据访问对象。
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <typeparam name="TId">标识类型</typeparam>
    public interface IGenericDao<T, TId> where T : Entity<TId>
    {
        /// <summary>
        /// 将瞬时对象持久化。
        /// </summary>
        /// <param name="entity">瞬时对象</param>
        /// <returns>持久化对象标识</returns>
        TId Save(T entity);

        /// <summary>
        /// 将托管对象重新纳入持久化上下文中管理。
        /// </summary>
        /// <param name="entity">托管对象</param>
        void Update(T entity);

        /// <summary>
        /// 删除持久化对象。
        /// </summary>
        /// <param name="entity">持久化对象</param>
        void Delete(T entity);

        /// <summary>
        /// 通过标识获取持久化对象。
        /// </summary>
        /// <param name="id">对象标识</param>
        /// <returns>持久化对象</returns>
        T GetById(TId id);

        /// <summary>
        /// 通过标识获取持久化对象的引用，不要求在对象返回时完成所有状态的加载。
        /// </summary>
        /// <param name="id">对象标识</param>
        /// <returns>持久化对象引用</returns>
        T GetReferenceById(TId id);

        /// <summary>
        /// 返回持久化对象列表。
        /// </summary>
        /// <returns>持久化对象列表</returns>
        IList<T> FindAll();
    }
}