using InteriorCostEstimator.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<DetectedObject> DetectedObjects { get; set; }

        public DbSet<MatchedProduct> MatchedProducts { get; set; }

        public DbSet<Proposal> Proposals { get; set; }

        public DbSet<ProposalItem> ProposalItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply All Configurations Automatically
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
