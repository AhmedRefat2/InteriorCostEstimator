using InteriorCostEstimator.Application.Features.ProductFeature.DTOs;
using InteriorCostEstimator.Application.Features.ProjectFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IAiService _aiService;
        private readonly IFileService _fileService;

        public ProjectService(
            AppDbContext context,
            IAiService aiService, IFileService fileService)
        {
            _context = context;
            _aiService = aiService;
            _fileService = fileService;
        }

        private async Task<List<AiDetectedItemDto>>
    GetDetectedObjectsAsync(Guid projectId)
        {
            return await _context.DetectedObjects
                .Where(d => d.ProjectId == projectId)
                .Include(d => d.MatchedProducts)
                    .ThenInclude(m => m.Product)
                        .ThenInclude(p => p.Category)
                .Select(d => new AiDetectedItemDto
                {
                    Type = d.Type,
                    Confidence = d.Confidence,
                    Crop_Url = d.Crop_Url,

                    Recommendations = d.MatchedProducts
                        .OrderBy(m => m.Rank)
                        .Select(m => new MatchedProductDto
                        {
                            Similarity_Score = m.SimilarityScore,
                            Rank = m.Rank,

                            Product = new ProductDto
                            {
                                Id = m.Product.Id,
                                AI_Id = m.Product.AI_Id,
                                Name = m.Product.Name,
                                Price = m.Product.Price,
                                Description = m.Product.Description,
                                ImageUrl = m.Product.ImageUrl,
                                Stock = m.Product.Stock,
                                InStock = m.Product.Stock > 0,
                                Material = m.Product.Material,
                                Length = m.Product.Length,
                                Width = m.Product.Width,
                                Height = m.Product.Height,
                                Size =
                                  $"{m.Product.Length}x{m.Product.Width}x{m.Product.Height}",
                                CategoryName = m.Product.Category.Name
                            }
                        }).ToList()
                }).ToListAsync();
        }


        public async Task<object> CreateProjectAsync(
            string userId,
            CreateProjectRequest request)
        {
            var aiResult =
                await _aiService.AnalyzeRoomAsync(
                    request.Image);

            string imageUrl =
                 await _fileService.UploadImageAsync(
                 request.Image,
                 "projects"
                  );
            Project project = new()
            {
                Name = request.Name,
                RoomType = request.RoomType,
                CustomerBudget = request.Budget,
                Status = ProjectStatus.Processing,
                ImageUrl = imageUrl,
                UserId = userId,
                Detection_Image_Url = aiResult.Detection_Image_Url
            };

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();


            foreach (var item in aiResult!.Data)
            {
                DetectedObject detected = new()
                {
                    Type = item.Type,
                    Confidence = item.Confidence,
                    ProjectId = project.Id,
                    Crop_Url = item.Crop_Url,
                };

                _context.DetectedObjects.Add(detected);

                await _context.SaveChangesAsync();

                int rank = 1;

                foreach (var rec in item.Recommendations)
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(
                            x => x.AI_Id == rec.Product_Id);

                    if (product is null)
                        continue;

                    MatchedProduct match = new()
                    {
                        DetectedObjectId = detected.Id,
                        ProductId = product.Id,
                        SimilarityScore =
                            rec.Similarity_Score,
                        Rank = rank++,
                        ProjectId = project.Id,
                    };

                    _context.MatchedProducts.Add(match);
                }
            }

            project.Status = ProjectStatus.Completed;

            await _context.SaveChangesAsync();

            var detectedObjects = await _context.DetectedObjects
             .Where(d => d.ProjectId == project.Id)
             .Include(d => d.MatchedProducts)
                 .ThenInclude(m => m.Product)
                     .ThenInclude(p => p.Category)
             .Select(d => new AiDetectedItemDto
             {
             Type = d.Type,
             Confidence = d.Confidence,
             Crop_Url = d.Crop_Url,

             Recommendations = d.MatchedProducts
            .OrderBy(m => m.Rank)
            .Select(m => new MatchedProductDto
            {
                Similarity_Score = m.SimilarityScore,
                Rank = m.Rank,

                Product = new ProductDto
                {
                    Id = m.Product.Id,  
                    AI_Id = m.Product.AI_Id,// AI_Id
                    Name = m.Product.Name,
                    Price = m.Product.Price,
                    Description = m.Product.Description,
                    ImageUrl = m.Product.ImageUrl,
                    Stock = m.Product.Stock,
                    InStock = m.Product.Stock > 0,
                    Material = m.Product.Material,
                    Length = m.Product.Length,
                    Width = m.Product.Width,
                    Height = m.Product.Height,
                    Size =
                        $"{m.Product.Length}x{m.Product.Width}x{m.Product.Height}",
                    CategoryName = m.Product.Category.Name
                             }
                         }).ToList()
                 }).ToListAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CreatedAt = project.CreatedAt,
                CustomerBudget = project.CustomerBudget,
                Detection_Image_Url = project.Detection_Image_Url,
                ImageUrl = project.ImageUrl,
                Processing_Time = project.Processing_Time,
                RoomType = project.RoomType,
                Status = project.Status,
                DetectedObjects = detectedObjects
            };
        }

        public async Task<List<ProjectDto>>
    GetAllProjectsAsync(string userId)
        {
            var projects = await _context.Projects
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            List<ProjectDto> result = new();

            foreach (var project in projects)
            {
                var detectedObjects =
                    await GetDetectedObjectsAsync(project.Id);

                result.Add(new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    CreatedAt = project.CreatedAt,
                    CustomerBudget = project.CustomerBudget,
                    Detection_Image_Url = project.Detection_Image_Url,
                    ImageUrl = project.ImageUrl,
                    Processing_Time = project.Processing_Time,
                    RoomType = project.RoomType,
                    Status = project.Status,
                    DetectedObjects = detectedObjects
                });
            }

            return result;
        }


        public async Task<ProjectDto>
    GetProjectByIdAsync(
    string userId,
    Guid projectId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p =>
                    p.Id == projectId &&
                    p.UserId == userId);

            if (project is null)
                throw new Exception("Project not found");

            var detectedObjects =
                await GetDetectedObjectsAsync(project.Id);

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CreatedAt = project.CreatedAt,
                CustomerBudget = project.CustomerBudget,
                Detection_Image_Url = project.Detection_Image_Url,
                ImageUrl = project.ImageUrl,
                Processing_Time = project.Processing_Time,
                RoomType = project.RoomType,
                Status = project.Status,
                DetectedObjects = detectedObjects
            };
        }
    }
}
