using InteriorCostEstimator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Infrastructure.Persistence.Configs
{
    internal class ProposalConfigs : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.VAT)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Delivery)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TotalCost)
                .HasColumnType("decimal(18,2)");

            // One-to-One with Project
            builder.HasOne(p => p.Project)
                .WithOne(p => p.Proposal)
                .HasForeignKey<Proposal>(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
