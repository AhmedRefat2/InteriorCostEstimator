using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class ProductConfigs : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.Property(x => x.Material)
                .HasMaxLength(100);

            builder.Property(x => x.Length);

            builder.Property(x => x.Width);

            builder.Property(x => x.Height);

            // Relationship
            builder.HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
             .WithMany()
             .HasForeignKey(x => x.CategoryId);
        }
    }   
}
