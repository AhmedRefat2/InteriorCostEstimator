using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Application.Features.ProductFeature.Services;
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
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // 1. Create Product

        [HttpPost]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _productService.CreateAsync(request, userId);

            return Ok(result);
        }

        // 2. Get All Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        // 3. Get By Category
        [HttpGet("by-category")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var result = await _productService.GetByCategoryAsync(category);
            return Ok(result);
        }

        // 4. Get My Products (Vendor)
        [HttpGet("my")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> GetMyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _productService.GetMyProductsAsync(userId);

            return Ok(result);
        }
    }
}
