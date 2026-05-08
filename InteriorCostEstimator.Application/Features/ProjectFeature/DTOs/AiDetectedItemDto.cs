using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class AiDetectedItemDto
    {
        public string Type { get; set; } = string.Empty;

        public decimal Confidence { get; set; }

        public string Crop_Url { get; set; }


        //public List<AiRecommendationDto> Recommendations
        //{ get; set; } = new();

        public List<MatchedProductDto> Recommendations
        { get; set; } = new();

    }
}
