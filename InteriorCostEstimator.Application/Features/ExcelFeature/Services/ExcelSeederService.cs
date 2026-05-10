using InteriorCostEstimator.Application.Features.CategoryFeature.Services;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;


namespace InteriorCostEstimator.Application.Features.ExcelFeature.Services
{

    public class ExcelSeederService
    {
        private (decimal width, decimal length, decimal height)
        ParseMeasurements(string measurements)
        {
            if (string.IsNullOrWhiteSpace(measurements))
                return (0, 0, 0);

            // Extract all numbers
            var matches = Regex.Matches(measurements, @"\d+(\.\d+)?");

            var numbers = matches
                .Select(m => decimal.Parse(m.Value))
                .ToList();

            if (numbers.Count < 3)
                return (0, 0, 0);

            return (
                width: numbers[0],
                length: numbers[1],
                height: numbers[2]
            );

          
        }

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ExcelSeederService(AppDbContext context, UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedCategoriesAndProducts(string filePath)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Basel");

            using var package =
                new ExcelPackage(new FileInfo(filePath));

            var worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            // =========================
            // 1. CREATE DEFAULT VENDOR
            // =========================


            ApplicationUser user = new ApplicationUser
            {
                UserName = "ikea@seed.com",
                Email = "ikea@seed.com",
                FullName = "IKEA Seed Vendor"
            };

            var result = await _userManager.CreateAsync(
                user,
                "Ikea@123456"
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(",",
                    result.Errors.Select(e => e.Description))
                );
            }

            await _userManager.AddToRoleAsync(
                user,
                "Vendor"
            );

            var vendor = new Vendor
            {
                UserId = user.Id,
                Id = Guid.NewGuid(),
                CompanyName = "IKEA",
            };

            _context.Vendors.Add(vendor);

            await _context.SaveChangesAsync();

            // =========================
            // 2. ADD CATEGORIES
            // =========================

            var categoryMap = new Dictionary<string, int>();

            for (int row = 2; row <= rowCount; row++)
            {
                string categoryName = worksheet.Cells[row, 7].Text.Trim();

                if (string.IsNullOrWhiteSpace(categoryName))
                    continue;

                if (!categoryMap.ContainsKey(categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };

                    _context.Categories.Add(category);

                    await _context.SaveChangesAsync();

                    categoryMap[categoryName] = category.Id;
                }

               
            }

            // =========================
            // 3. ADD PRODUCTS
            // =========================

            int newrow;
            for ( newrow = 2; newrow <= rowCount; newrow++)
            {
                string categoryName = worksheet.Cells[newrow, 7].Text.Trim();

                var measurements =  ParseMeasurements(worksheet.Cells[newrow, 5].Text);

                var product = new Product
                {
                    //Id = Guid.NewGuid(),
                    Name = worksheet.Cells[newrow, 3].Text == null ? "No Available" : worksheet.Cells[newrow, 3].Text,
                    Description = worksheet.Cells[newrow, 6].Text == null ? "No Available" : worksheet.Cells[newrow, 6].Text,

                    Material = worksheet.Cells[newrow, 4].Text  == null ? "No Available" : worksheet.Cells[newrow, 4].Text,

                    Price = decimal.TryParse(
                        worksheet.Cells[newrow, 14].Text,
                        out decimal price)
                        ? price
                        : 0,

                    ImageUrl = worksheet.Cells[newrow, 19].Text == null ? "No Available" : worksheet.Cells[newrow, 19].Text,

                    Stock = 10,

                    CreatedAt = DateTime.Now,

                    VendorId = vendor.Id /*Guid.Parse("6d799746-69ac-4a83-9225-50658f7e230c")*/,

                    CategoryId = categoryMap[categoryName],


                    Height = measurements.height,
                    Length = measurements.length,
                    Width = measurements.width,
                    AI_Id = int.TryParse(worksheet.Cells[newrow, 2].Text, out int Id) ? Id : 0,
                };

                _context.Products.Add(product);

              
            }

            await _context.SaveChangesAsync();
        }
    }
}
