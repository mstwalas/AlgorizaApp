using CoreLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositryLayer
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AlgorziAPIDB;Integrated Security=True");
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeHistory> ExchangeHistory { get; set; }
        public DbSet<Identity> Identities { get; set; }
    }
}
