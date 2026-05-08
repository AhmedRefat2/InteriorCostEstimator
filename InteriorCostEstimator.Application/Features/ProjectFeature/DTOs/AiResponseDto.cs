using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class AiResponseDto
    {
        public string Status { get; set; } = string.Empty;

        public int Items_Found { get; set; }

        public string Detection_Image_Url { get; set; }

        public List<AiDetectedItemDto> Data
        { get; set; } = new();
    }
}
