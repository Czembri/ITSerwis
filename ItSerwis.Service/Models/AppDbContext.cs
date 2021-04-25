using ItSerwis.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSerwis.Service.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserLogin> UserLogin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLogin>().HasData(new UserLogin
            {
                UserID = 1,
                FirstName = "Serwisowy",
                LastName = "user",
                Age = 9999,
                LoginHash = "de1c072cf178e96f3d7994747403ee22",
                PasswordHash = "de1c072cf178e96f3d7994747403ee22"
            });
        }
    }
}
