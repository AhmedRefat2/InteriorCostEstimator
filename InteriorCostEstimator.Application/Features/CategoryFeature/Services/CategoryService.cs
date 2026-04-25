using InteriorCostEstimator.Application.Features.CategoryFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace InteriorCostEstimator.Application.Features.CategoryFeature.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    NumberOfProducts = c.NumberOfProducts
                })
                .ToListAsync();
        }



        public async Task AddAsync(AddCategoryRequest request)
        {
            bool exists = await _context.Categories
                 .AnyAsync(x => x.Name == request.Name);

            if (exists)
                throw new Exception("Category exists");


            Category category = new()
            {
                Name = request.Name
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
        }
    }
}
