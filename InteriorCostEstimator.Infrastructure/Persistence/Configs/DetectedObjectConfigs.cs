using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class DetectedObjectConfigs : IEntityTypeConfiguration<DetectedObject>
    {
        public void Configure(EntityTypeBuilder<DetectedObject> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Relationship: Project → DetectedObjects
            builder.HasOne(d => d.Project)
                .WithMany(p => p.DetectedObjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
