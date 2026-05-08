using InteriorCostEstimator.Application.Features.ProjectFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public interface IProjectService
    {
        Task<object> CreateProjectAsync(
            string userId,
            CreateProjectRequest request);
    }
}
