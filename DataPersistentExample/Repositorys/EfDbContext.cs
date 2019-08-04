using DataPersistentExample.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPersistentExample.Repositorys
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BusinessObject> BusinessObjects { get; set; }
    }
}
