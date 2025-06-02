using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class VCSDbContext: DbContext
    {
        public VCSDbContext(DbContextOptions<VCSDbContext> options):base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
