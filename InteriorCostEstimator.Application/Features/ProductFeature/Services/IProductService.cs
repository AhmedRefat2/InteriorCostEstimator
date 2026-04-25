using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProductFeature.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllByVendorIdAsync(Guid vendorId);

        Task<ProductDto?> GetByIdAsync(Guid id);

        Task AddAsync(AddProductRequest request, Guid vendorId);

        Task UpdateAsync(Guid id, UpdateProductRequest request);

        Task DeleteAsync(Guid id);
    }
}
