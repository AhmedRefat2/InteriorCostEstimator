using InteriorCostEstimator.Application.Features.CategoryFeature.DTOs;
using InteriorCostEstimator.Application.Features.CategoryFeature.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteriorCostEstimator.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
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
