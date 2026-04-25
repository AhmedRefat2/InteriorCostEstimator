using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Application.Features.ProductFeature.Services;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly AppDbContext _context;

        public ProductsController(IProductService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }


        [Authorize(Roles = "Vendor")]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var vendorId = _context.Vendors.Where(v => v.UserId == userId).Select(v => v.Id).FirstOrDefault();

            // temp hardcoded vendor until auth claims
            await _service.AddAsync(request, vendorId);

            return Ok("Product Created");
        }


        [Authorize(Roles = "Vendor")]

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProductRequest request)
        {
            await _service.UpdateAsync(id, request);

            return Ok("Product Updated Successfully");
        }


        [Authorize(Roles = "Vendor")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok("Product Deleted Successfully");
        }

    }
}
