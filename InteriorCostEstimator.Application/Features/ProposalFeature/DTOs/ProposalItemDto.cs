using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProposalFeature.DTOs
{
    public class ProposalItemDto
    {
        public string ProductName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string Material { get; set; }

        public string VendorName { get; set; }

        public string Category { get; set; }

        public string Dimensions { get; set; }


    }
}
