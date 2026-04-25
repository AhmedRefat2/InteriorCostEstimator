using InteriorCostEstimator.Application.Features.VendorFeature.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _service;

        public VendorsController(
           IVendorService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
             await _service.GetAllVendorsAsync()
            );
        }

    }
}
