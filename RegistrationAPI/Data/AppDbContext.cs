using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Models;
using System.Collections.Generic;

namespace RegistrationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        
        }
    }




 