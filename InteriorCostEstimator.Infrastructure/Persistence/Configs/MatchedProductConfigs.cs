using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class MatchedProductConfigs : IEntityTypeConfiguration<MatchedProduct>
    {
        public void Configure(EntityTypeBuilder<MatchedProduct> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.SimilarityScore)
                .IsRequired();

            builder.Property(m => m.Rank)
                .IsRequired();

            // Relationship: Project
            builder.HasOne(m => m.Project)
                .WithMany(p => p.MatchedProducts)
                .HasForeignKey(m => m.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relationship: DetectedObject
            builder.HasOne(m => m.DetectedObject)
                .WithMany(d => d.MatchedProducts)
                .HasForeignKey(m => m.DetectedObjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Product
            builder.HasOne(m => m.Product)
                .WithMany()
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
