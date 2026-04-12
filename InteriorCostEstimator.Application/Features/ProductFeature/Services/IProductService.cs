using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProductFeature.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateAsync(CreateProductRequest request, string userId);

        Task<List<ProductResponse>> GetAllAsync();

        Task<List<ProductResponse>> GetByCategoryAsync(string category);

        Task<List<ProductResponse>> GetMyProductsAsync(string userId);
    }
}
