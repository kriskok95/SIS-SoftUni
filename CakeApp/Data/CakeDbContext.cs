using System;
using System.Collections.Generic;
using System.Text;
using CakeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CakeApp.Data
{
    public class CakeDbContext : DbContext
    {
        public CakeDbContext()
        {
            
        }

        public CakeDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-MKU8P03\SQLEXPRESS;Database=CakeDb;Integrated Security=true");
        }
    }
}
