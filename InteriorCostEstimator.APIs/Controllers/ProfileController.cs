using InteriorCostEstimator.Application.Features.ProfileFeature.DTOs;
using InteriorCostEstimator.Application.Features.VendorFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IVendorService _service;

        public ProfileController(
           IVendorService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Vendor")]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId =
            User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

            
                var profile =
                   await _service.GetProfileAsync(userId!);

                return Ok(profile);
            
        }


        [Authorize(Roles = "Vendor")]
        [HttpPut]
        public async Task<IActionResult> Update(
           UpdateProfileRequest request)
        {
            var userId =
            User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

            await _service.UpdateProfileAsync(
               userId!,
               request);

            return Ok();
        }

    }
}
