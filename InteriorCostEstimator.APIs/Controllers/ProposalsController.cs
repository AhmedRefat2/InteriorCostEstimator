using InteriorCostEstimator.Application.Features.ProposalFeature.DTOs;
using InteriorCostEstimator.Application.Features.ProposalFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProposalsController : ControllerBase
    {
        private readonly IProposalService _service;

        public ProposalsController(
            IProposalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProposalRequest request)
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            var result = await _service.CreateProposalAsync( userId!, request);

            return Ok(result);
        }


        [HttpGet("{proposalId}")]
        public async Task<IActionResult> GetById(Guid proposalId)
        {
            var userId = User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            var result = await _service.GetProposalByIdAsync(
                userId!,
                proposalId);

            return Ok(result);
        }
    }
}
