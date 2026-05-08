using InteriorCostEstimator.Application.Features.ProjectFeature.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public interface IAiService
    {
        Task<AiResponseDto?> AnalyzeRoomAsync(
            IFormFile image);

        Task<AiAddProductResponseDto?> AddProductToAiAsync(
            IFormFile image,
            int AI_Id);
    }
}
