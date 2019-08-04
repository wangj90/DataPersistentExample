using DataPersistentExample.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DataPersistentExample.Repositorys
{
    public class AdoNetRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        public readonly IConfiguration _configuration;

        public AdoNetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection { get => new SqlConnection(_configuration.GetConnectionString("AdoNet")); }

        public int Create(TEntity entity)
        {
            string sql = "insert into " + typeof(TEntity).Name + "s values (";
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                sql += "@" + property.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (var property in properties)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + property.Name, property.GetValue(entity, null)));
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Delete(Guid id)
        {
            string sql = "delete from " + typeof(TEntity).Name + "s where Id = @Id";
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            IList<TEntity> all = new List<TEntity>();
            string sql = "select * from " + typeof(TEntity).Name + "s";
            DataTable dt = new DataTable();
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    sda.Fill(dt);
                }
            }
            foreach (DataRow dataRow in dt.Rows)
            {
                TEntity entity = new TEntity();
                for (int i = 0; i < dataRow.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = entity.GetType().GetProperty(dataRow.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dataRow[i] != DBNull.Value)
                        propertyInfo.SetValue(entity, dataRow[i], null);
                }
                all.Add(entity);
            }
            return all.AsEnumerable();
        }

        public TEntity GetById(Guid id)
        {
            TEntity entity = new TEntity();
            string sql = "select * from " + typeof(TEntity).Name + "s where Id = @Id";
            DataTable dt = new DataTable();
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    sda.SelectCommand.Parameters.Add(new SqlParameter("@Id", id));
                    sda.Fill(dt);
                }
            }
            for (int i = 0; i < dt.Rows[0].Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = entity.GetType().GetProperty(dt.Rows[0].Table.Columns[i].ColumnName);
                if (propertyInfo != null && dt.Rows[0][i] != DBNull.Value)
                    propertyInfo.SetValue(entity, dt.Rows[0][i], null);
            }
            return entity;
        }

        public bool Modify(TEntity entity)
        {
            string sql = "update " + typeof(TEntity).Name + "s set ";
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "Id")
                {
                    sql += property.Name + "=" + "@" + property.Name + ",";
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " where Id = @Id";
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (var property in properties)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + property.Name, property.GetValue(entity, null)));
                    }
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
