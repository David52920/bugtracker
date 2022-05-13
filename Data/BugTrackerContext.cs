using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bugtracker.Models;

    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext (DbContextOptions<BugTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<bugtracker.Models.Issue>? Issues { get; set; }
        public DbSet<bugtracker.Models.User>? Users { get; set; }
    }
