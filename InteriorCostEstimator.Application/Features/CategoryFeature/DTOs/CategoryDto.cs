using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.CategoryFeature.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int NumberOfProducts { get; set; }
    }
}
