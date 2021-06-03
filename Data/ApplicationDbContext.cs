using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<KeKhai> KeKhai { get; set; }
        public DbSet<KeKhaiCt> KeKhaiCt { get; set; }
        public DbSet<Permission> Permission { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id=99,Username = "T", Roler = "user", Index = true, Create = true, Edit = true, Delete = true, Approval = true }
                );
        }
    }
}
