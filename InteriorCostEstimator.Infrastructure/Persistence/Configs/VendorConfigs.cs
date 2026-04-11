using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class VendorConfigs : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.Phone)
                .HasMaxLength(20);

            builder.Property(v => v.WhatsAppLink)
                .HasMaxLength(200);

            // Relationship with User (One-to-One)
            builder.HasOne(v => v.User)
                .WithOne(u => u.Vendor)
                .HasForeignKey<Vendor>(v => v.UserId);
        }
    }
}
