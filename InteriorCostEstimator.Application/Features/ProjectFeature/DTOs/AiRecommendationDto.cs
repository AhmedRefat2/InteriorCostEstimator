using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class AiRecommendationDto
    {
        public Guid Product_Id { get; set; }

        public double Similarity_Score { get; set; }
    }
}
