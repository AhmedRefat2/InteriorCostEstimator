using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public class  FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileService(
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadImageAsync(
            IFormFile file,
            string folderName)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Invalid file");

            string extension = Path.GetExtension(file.FileName);

            string[] allowedExtensions =
            {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

            if (!allowedExtensions.Contains(extension.ToLower()))
                throw new Exception("Invalid image format");

            string uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                folderName
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName =
                $"{Guid.NewGuid()}{extension}";

            string filePath = Path.Combine(
                uploadsFolder,
                fileName
            );

            using var stream = new FileStream(
                filePath,
                FileMode.Create
            );

            await file.CopyToAsync(stream);

            var request = _httpContextAccessor.HttpContext!.Request;

            string baseUrl =
                $"{request.Scheme}://{request.Host}";

            return
                $"{baseUrl}/uploads/{folderName}/{fileName}";
        }
    }
}
