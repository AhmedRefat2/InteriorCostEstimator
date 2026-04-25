using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProductFeature.DTOs
{
    public class AddProductRequest
    {

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Stock { get; set; }

        public bool InStock { get; set; }

        public string Material { get; set; } = string.Empty;

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public int CategoryId { get; set; } 
    }
}
