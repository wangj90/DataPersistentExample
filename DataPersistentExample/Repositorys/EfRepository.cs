using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPersistentExample.Repositorys
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        public readonly EfDbContext _dbContext;

        public EfRepository(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(TEntity entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            TEntity entity = this.GetById(id);
            if (entity == null)
            {
                throw new ArgumentException($"{id}对应的数据不存在");
            }
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        public TEntity GetById(Guid id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public bool Modify(TEntity entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
