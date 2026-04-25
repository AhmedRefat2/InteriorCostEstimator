using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // New Fields
        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public string Material { get; set; } = string.Empty;

        // FK
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        // Foreign Key
        public Guid VendorId { get; set; }

        // Navigation
        public Vendor Vendor { get; set; }
    }
}
