using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groceries.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Groceries.DataAccess
{
    public class GroceriesDbContext : DbContext
    {
        public GroceriesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = -1,
                Name = "admin",
                Email = "admin@groceries.com",
                Role = "admin",
                Password = "admin"
            });
            base.OnModelCreating(modelBuilder);

        }

    }
}
