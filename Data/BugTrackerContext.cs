using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bugtracker.Models;
using bugtracker.Configuration;
using Microsoft.AspNetCore.Identity;

    public class BugTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public BugTrackerContext (DbContextOptions<BugTrackerContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
                        modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.FirstName)
            .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);
        }
        public DbSet<bugtracker.Models.Issue>? Issues { get; set; }

    }
