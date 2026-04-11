using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{

    public class ProjectConfigs : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.RoomType)
                .HasMaxLength(100);

            // Enum → int
            builder.Property(p => p.Status)
                .HasConversion<int>();

            // Relationship: User → Projects
            builder.HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships
            builder.HasMany(p => p.DetectedObjects)
                .WithOne(d => d.Project)
                .HasForeignKey(d => d.ProjectId);

            builder.HasMany(p => p.MatchedProducts)
                .WithOne(m => m.Project)
                .HasForeignKey(m => m.ProjectId);
        }
    }
}
