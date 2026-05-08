using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.DTOs
{
    public class CreateProjectRequest
    {
        public string Name { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        public decimal Budget { get; set; }

        public IFormFile Image { get; set; } = null!;
    }
}
