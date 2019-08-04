using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPersistentExample.Repositorys
{
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <returns></returns>
        TEntity GetById(Guid id);
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Modify(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);
    }
}
