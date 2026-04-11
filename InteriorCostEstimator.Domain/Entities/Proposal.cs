using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class Proposal
    {
        public Guid Id { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VAT { get; set; }

        public decimal Delivery { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public Guid ProjectId { get; set; }

        // Navigation
        public Project Project { get; set; }

        public List<ProposalItem> Items { get; set; } = new();
    }

    public class ProposalItem
    {
        public Guid Id { get; set; }

        public Guid ProposalId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // Navigation
        public Proposal Proposal { get; set; }

        public Product Product { get; set; }
    }

}
