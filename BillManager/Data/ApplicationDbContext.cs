using System;
using System.Collections.Generic;
using System.Text;
using BillManager.Models;
using BillManager.Models.ModelConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BillManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)        { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Bill> Bill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new InformationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
