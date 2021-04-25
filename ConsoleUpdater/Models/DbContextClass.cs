using ItSerwis.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUpdater.Models
{
    class DbContextClass : DbContext
    {
        private const string connectionString = "server=(localdb)\\MSSQLLocalDB;database=ItService_dev;Trusted_Connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<UserLogin> UserLogin { get; set; }

    }

}
