using InteriorCostEstimator.Application.Features.CategoryFeature.DTOs;
using InteriorCostEstimator.Application.Features.CategoryFeature.Services;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly AppDbContext _context;

        public CategoriesController(ICategoryService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var vendorId = _context.Vendors.Where(v => v.UserId == userId).Select(v => v.Id).FirstOrDefault();

            return Ok(await _service.GetAllAsync(vendorId));
        }


        [HttpPost]
        public async Task<IActionResult> Add(
            AddCategoryRequest request)
        {
            await _service.AddAsync(request);

            return Ok("Category Added");
        }

    }
}
