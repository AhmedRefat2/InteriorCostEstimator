using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class Vendor
    {
        public Guid Id { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CompanyName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string WhatsAppLink { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string LogoImageUrl { get; set; } = string.Empty;

        // Foreign Key
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
