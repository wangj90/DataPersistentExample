using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataPersistentExample.Repositorys
{
    public class DapperRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        public readonly IConfiguration _configuration;

        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection { get => new SqlConnection(_configuration.GetConnectionString("Dapper")); }

        public int Create(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                return (int)conn.Insert(entity);
            }
        }

        public bool Delete(Guid id)
        {
            TEntity entity = this.GetById(id);
            if (entity == null)
            {
                throw new ArgumentException($"{id}对应的数据不存在");
            }
            using (IDbConnection conn = Connection)
            {
                return conn.Delete(entity);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                return conn.GetAll<TEntity>();
            }
        }

        public TEntity GetById(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.Get<TEntity>(id);
            }
        }

        public bool Modify(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.Update(entity);
            }
        }
    }
}
