using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class ProposalItemConfigs : IEntityTypeConfiguration<ProposalItem>
    {
        public void Configure(EntityTypeBuilder<ProposalItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Quantity)
                .IsRequired();

            // Relationship: Proposal → Items
            builder.HasOne(p => p.Proposal)
                .WithMany(p => p.Items)
                .HasForeignKey(p => p.ProposalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Product
            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
