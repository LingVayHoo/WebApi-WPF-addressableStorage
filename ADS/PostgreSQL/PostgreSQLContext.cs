using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.PostgreSQL
{
    internal class PostgreSQLContext : DbContext
    {
        public DbSet<AddressDBModel> Addresses { get; set; } = null!;

        public PostgreSQLContext()
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseNpgsql("Host=192.168.1.103;Port=5432;Database=adsV2db;Username=postgres;Password=Inter101!");
        }
    }
}
