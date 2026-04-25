using InteriorCostEstimator.Application.Features.CategoryFeature.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.CategoryFeature.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task AddAsync(AddCategoryRequest request);
    }
}
