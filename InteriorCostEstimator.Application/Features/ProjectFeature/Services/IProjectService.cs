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

        Task<List<ProjectDto>> GetAllProjectsAsync(string userId);

        Task<ProjectDto> GetProjectByIdAsync(
            string userId,
            Guid projectId);
    }
}
