using InteriorCostEstimator.Application.Features.ProductFeature.Services;
using InteriorCostEstimator.Application.Features.ProjectFeature.DTOs;
using InteriorCostEstimator.Application.Features.ProjectFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(
            IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(
            [FromForm] CreateProjectRequest request)
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _projectService
                .CreateProjectAsync(userId, request);

            return Ok(result);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetById(Guid projectId)
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            var result = await _projectService
                .GetProjectByIdAsync(
                    userId!,
                    projectId);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            var result = await _projectService
                .GetAllProjectsAsync(userId!);

            return Ok(result);
        }
    }
}
