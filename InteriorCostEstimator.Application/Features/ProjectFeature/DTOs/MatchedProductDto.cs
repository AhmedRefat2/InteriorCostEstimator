using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class MatchedProductDto
    {


        public int Product_Id { get; set; }

        public double Similarity_Score { get; set; }

        public int Rank { get; set; } // 1 = best match

        // Navigation


        public ProductDto Product { get; set; }
    }
}
