using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProductFeature.Services
{
    public class ProductService: IProductService
    {
    
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> CreateAsync(CreateProductRequest request, string userId)
        {
            // Get Vendor
            var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.UserId == userId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category.ToLower(),
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Stock = request.Stock,
                VendorId = vendor.Id
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                VendorName = vendor.CompanyName
            };
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Vendor)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    VendorName = p.Vendor.CompanyName
                })
                .ToListAsync();
        }

        public async Task<List<ProductResponse>> GetByCategoryAsync(string category)
        {
            return await _context.Products
                .Include(p => p.Vendor)
                .Where(p => p.Category == category.ToLower())
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    VendorName = p.Vendor.CompanyName
                })
                .ToListAsync();
        }

        public async Task<List<ProductResponse>> GetMyProductsAsync(string userId)
        {
            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(v => v.UserId == userId);

            if (vendor == null)
                throw new Exception("Vendor not found");

            return await _context.Products
                .Where(p => p.VendorId == vendor.Id)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    VendorName = vendor.CompanyName
                })
                .ToListAsync();
        }
    }
}
