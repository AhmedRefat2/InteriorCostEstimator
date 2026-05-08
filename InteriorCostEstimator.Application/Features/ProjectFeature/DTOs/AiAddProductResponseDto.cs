using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class AiAddProductResponseDto
    {
        public string Status { get; set; } = string.Empty;

        public int Product_Id { get; set; }

        public int Faiss_Total { get; set; }
    }
}
