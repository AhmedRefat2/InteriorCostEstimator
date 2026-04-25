using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProductFeature.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Stock = p.Stock,
                    InStock = p.Stock > 0,
                    Material = p.Material,
                    Length = p.Length,
                    Width = p.Width,
                    Height = p.Height,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
        }



        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                 .Include(p => p.Category)
                 .Where(p => p.Id == id)
                 .Select(p => new ProductDto
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Price = p.Price,
                     Description = p.Description,
                     ImageUrl = p.ImageUrl,
                     Stock = p.Stock,
                     InStock = p.Stock > 0,
                     Material = p.Material,
                     Length = p.Length,
                     Width = p.Width,
                     Height = p.Height,
                     CategoryName = p.Category.Name
                 })
                 .FirstOrDefaultAsync();
        }



        public async Task AddAsync(AddProductRequest request, Guid vendorId)
        {
            Product product = new()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Stock = request.Stock,
                Material = request.Material,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                CategoryId = request.CategoryId,
                VendorId = vendorId
            };


            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category != null)
            {
                category.NumberOfProducts++;

                _context.Categories.Update(category);
                _context.Products.Add(product);

                await _context.SaveChangesAsync();
            
            }

            else
            {
                throw new Exception("Category Not Found");
            }
        }



        public async Task UpdateAsync(Guid id, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                throw new Exception("Product Not Found");


            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Stock = request.Stock;
            product.Material = request.Material;
            product.Length = request.Length;
            product.Width = request.Width;
            product.Height = request.Height;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                throw new Exception("Product Not Found");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}
