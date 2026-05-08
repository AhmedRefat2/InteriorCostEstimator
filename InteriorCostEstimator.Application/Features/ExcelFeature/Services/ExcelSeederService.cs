using InteriorCostEstimator.Application.Features.CategoryFeature.Services;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
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

            //if (string.IsNullOrWhiteSpace(measurements))
            //    return (0, 0, 0);

            //// remove cm and spaces
            //measurements = measurements
            //    .ToLower()
            //    .Replace("cm", "")
            //    .Trim();

            //// split by x
            //var parts = measurements.Split('x');

            //if (parts.Length != 3)
            //    return (0, 0, 0);

            //bool widthParsed =
            //    decimal.TryParse(parts[0].Trim(), out decimal width);

            //bool lengthParsed =
            //    decimal.TryParse(parts[1].Trim(), out decimal length);

            //bool heightParsed =
            //    decimal.TryParse(parts[2].Trim(), out decimal height);

            //return (
            //    widthParsed ? width : 0,
            //    lengthParsed ? length : 0,
            //    heightParsed ? height : 0
            //);
        }

        private readonly AppDbContext _context;

        public ExcelSeederService(AppDbContext context)
        {
            _context = context;
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

            var vendor = new Vendor
            {
                UserId = "59836cca-f4bf-486e-b661-89529316fc63",
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
